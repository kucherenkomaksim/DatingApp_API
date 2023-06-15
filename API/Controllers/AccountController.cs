using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[AllowAnonymous]
public class AccountController : BaseApiController
{
    private readonly DataContext _context;
    private readonly ITokenService _tokenService;
    
    public AccountController(DataContext context, ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register([FromBody] RegisterDto dto, CancellationToken cancellationToken)
    {
        if (await IsUserExists(dto.UserName, cancellationToken).ConfigureAwait(false))
        {
            return BadRequest("UserName is exists");
        }
        
        using var hmac = new HMACSHA512();

        var user = new AppUser
        {
            UserName = dto.UserName.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password)),
            PasswordSalt = hmac.Key,
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        var userDto = new UserDto
        {
            UserName = dto.UserName,
            Token = _tokenService.GenerateToken(user),
        };
        
        return Ok(userDto);
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto dto, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.UserName == dto.UserName, cancellationToken)
            .ConfigureAwait(false);

        if (user is null) return Unauthorized();

        using var hmac = new HMACSHA512(user.PasswordSalt);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(dto.Password));

        if (computedHash.Where((t, i) => t != user.PasswordHash[i]).Any())
        {
            return Unauthorized("Invalid password");
        }
        
        var userDto = new UserDto
        {
            UserName = dto.UserName,
            Token = _tokenService.GenerateToken(user),
        };

        return userDto;
    }

    private async Task<bool> IsUserExists(string userName, CancellationToken cancellationToken)
    {
        return await _context.Users
            .AnyAsync(x => x.UserName == userName.ToLower(), cancellationToken)
            .ConfigureAwait(false);
    }
}