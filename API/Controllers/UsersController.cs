using System.Net;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

[ApiController]
[Consumes("application/json")]
[Produces("application/json")]
[ProducesResponseType(typeof(ActionResult<AppUser>), (int)HttpStatusCode.OK)]
[ProducesResponseType(typeof(ActionResult<AppUser>), (int)HttpStatusCode.BadRequest)]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly DataContext context;
    
    public UsersController(DataContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(CancellationToken cancellationToken)
    {
        var users = await context.Users.ToListAsync(cancellationToken).ConfigureAwait(false);

        return users;
    }

    [HttpGet("{id:long}")]
    public async Task<ActionResult<AppUser>> GetUser(long id, CancellationToken cancellationToken)
    {
        var user = await context.Users.FindAsync(new object?[] { id }, cancellationToken).ConfigureAwait(false);

        return (user is not null) ? user : BadRequest();
    }
}