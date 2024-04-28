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
        user.professor = model.professor;
        user.name = model.name;
        user.university = model.university;
        user.biography = model.biography;
        user.eduemail = model.eduemail;
        await postgresContext.SaveChangesAsync();
        return Ok(user);
    }

    [HttpGet("my-applications")]
    public async Task<IActionResult> GetMyApplications([FromQuery] user model)
    {
        var applicationsID = await postgresContext.applications
            .Where(w => w.student_id == new Guid(model.user_id))
            .OrderByDescending(o => o.time)
            // find all research_id in applications
            .Select(s => s.research_id)
            .ToListAsync();

        // find all researches by research_id
        var applications = await postgresContext.researches
            .Where(w => applicationsID.Contains(w.id))
            .OrderByDescending(o => o.time)
            .ToListAsync();
        return Ok(applications);
    }
}