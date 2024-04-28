using GetResearch.Db.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GetResearch.Controller;

[Route("research")]
public class ResearchController(PostgresContext postgresContext) : BaseController
{
    [HttpPost("apply")]
    public async Task<IActionResult> ApplyResearch([FromBody] ApplyDto applyDto)
    {
        var research = await postgresContext.researches.FindAsync(applyDto.Research.id);

        var app = await postgresContext.applications.AddAsync(new application
        {
            research_id = research.id,
            student_id = applyDto.user_id,
            letter = applyDto.Application.letter
        });
        await postgresContext.SaveChangesAsync();
        return Ok(research);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateResearch([FromBody] ResearchCreationDto creationDto)
    {
        creationDto.Research.professor_id = creationDto.user_id; // 设置professor_id为传入的UserId
        await postgresContext.researches.AddAsync(creationDto.Research);
        await postgresContext.SaveChangesAsync();
        return Ok(creationDto.Research);
    }

    [HttpGet("list-by-professor")]
    public async Task<IActionResult> GetResearchListByProfessor([FromQuery] user model)
    {
        var researchList = await postgresContext.researches
            .Where(w => w.professor_id == new Guid(model.user_id))
            .OrderByDescending(o => o.time)
            .ToListAsync();

        var viewModelList = new List<ResearchViewModel>();

        foreach (var research in researchList)
        {
            var applications = await postgresContext.applications
                .Where(w => w.research_id == research.id)
                .OrderByDescending(o => o.time)
                .ToListAsync();


            viewModelList.Add(new ResearchViewModel
            {
                ResearchData = research,
                Applications = applications
            });
        }

        return Ok(viewModelList);
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetResearchList([FromQuery] user model)
    {
        var researchList = await postgresContext.researches
            .OrderByDescending(o => o.time)
            .ToListAsync();
        return Ok(researchList);
    }

    [HttpGet("list-by-apply")]
    public async Task<IActionResult> GetResearchListByApply([FromQuery] user model)
    {
        var researchList = await postgresContext.applications
            .Where(w => w.student_id == new Guid(model.user_id))
            .OrderByDescending(o => o.time)
            .ToListAsync();
        return Ok(researchList);
    }

    [HttpPost("approve")]
    public async Task<IActionResult> ApproveResearch([FromBody] application model)
    {
        var application = await postgresContext.applications.FindAsync(model.id);
        application.status = model.status;
        await postgresContext.SaveChangesAsync();
        return Ok(application);
    }

    public class ResearchViewModel
    {
        public research ResearchData { get; set; }
        public List<application> Applications { get; set; }
    }
}

public class ResearchCreationDto
{
    public research Research { get; set; }
    public Guid user_id { get; set; }
}

public class ApplyDto
{
    public research? Research { get; set; }
    public application? Application { get; set; }
    public Guid user_id { get; set; }
}