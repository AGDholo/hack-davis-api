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
        // 首先，获取学生的所有 applications 包括其 research_id 和 status
        var applications = await postgresContext.applications
            .Where(a => a.student_id == new Guid(model.user_id))
            .OrderByDescending(a => a.time)
            .Select(a => new { a.research_id, a.status })
            .ToListAsync();

        // 将 application 的 research_id 提取出来进行 research 查询
        var researchIds = applications.Select(a => a.research_id).Distinct().ToList();

        // 查询所有匹配的 research 对象
        var researches = await postgresContext.researches
            .Where(r => researchIds.Contains(r.id))
            .OrderByDescending(r => r.time)
            .ToListAsync();

        // 将 researches 和 applications 的 status 信息合并
        var researchesWithStatus = researches.Select(r => new
        {
            Research = r,
            Status = applications.FirstOrDefault(a => a.research_id == r.id)?.status
        }).ToList();

        return Ok(researchesWithStatus);
    }
}