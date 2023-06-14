using System.Net;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ProducesResponseType(typeof(ActionResult<AppUser>), (int)HttpStatusCode.OK)]
[ProducesResponseType(typeof(ActionResult<AppUser>), (int)HttpStatusCode.BadRequest)]
[Authorize]
public class UsersController : BaseApiController
{
    private readonly DataContext _context;
    
    public UsersController(DataContext context)
    {
        _context = context;
    }
    
    // [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await _context.Users.ToListAsync(cancellationToken).ConfigureAwait(false);

        return users;
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<AppUser>> GetUser(long id, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FindAsync(new object?[] { id }, cancellationToken).ConfigureAwait(false);

        return (user is not null) ? user : BadRequest();
    }
}