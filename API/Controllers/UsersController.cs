using System.Net;
using API.DTO;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ProducesResponseType(typeof(ActionResult<AppUser>), (int)HttpStatusCode.OK)]
[ProducesResponseType(typeof(ActionResult<AppUser>), (int)HttpStatusCode.BadRequest)]
[Authorize]
public class UsersController : BaseApiController
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
    {
        var users = await _userRepository.GetMembersAsync().ConfigureAwait(false);

        return Ok(users);
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<MemberDto>> GetUser(string username)
    {
        var user = await _userRepository.GetMemberAsync(username).ConfigureAwait(false);

        return (user is not null) 
            ? Ok(user)
            : BadRequest();
    }
}