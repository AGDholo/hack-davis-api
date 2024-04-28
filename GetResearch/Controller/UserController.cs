using GetResearch.Db.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GetResearch.Controller;

[Route("user")]
public class UserController(PostgresContext postgresContext) : BaseController
{
    // get user from db
    [HttpGet("db-user")]
    public async Task<IActionResult> GetUser([FromQuery] user model)
    {
        var user = await postgresContext.users.FirstOrDefaultAsync(u => u.user_id == model.user_id);
        return Ok(user);
    }

    // update user in db
    [HttpPost("update-user")]
    public async Task<IActionResult> UpdateUser([FromBody] user model)
    {
        var user = await postgresContext.users.FirstOrDefaultAsync(u => u.user_id == model.user_id);
        if (user == null) return NotFound();
        user.name = model.name;
        user.university = model.university;
        user.biography = model.biography;
        await postgresContext.SaveChangesAsync();
        return Ok(user);
    }
}