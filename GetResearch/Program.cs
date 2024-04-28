using DotNetEnv;
using GetResearch.Db.Entity;
using Microsoft.EntityFrameworkCore;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

RegisterServices(builder.Services);
RegisterWebApplication(builder.Build());

void RegisterServices(IServiceCollection builderServices)
{
    builderServices.AddCors(c =>
        c.AddDefaultPolicy(policy => { policy.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin(); }));


    builderServices.AddControllers();


    // db
    builderServices.AddDbContext<PostgresContext>(options =>
        options.UseNpgsql(Env.GetString("CONNECTION_STRING")));

    builderServices.AddHttpContextAccessor();
    builderServices.AddHttpClient();
    builderServices.AddMemoryCache();
}

void RegisterWebApplication(WebApplication webApplication)
{
    webApplication.UseCors(x =>
        x.AllowAnyHeader()
            .SetIsOriginAllowed(origin => true)
            .AllowCredentials());
    webApplication.UseRouting();

    webApplication.UseStaticFiles();

    webApplication.UseAuthentication();
    webApplication.UseAuthorization();
    webApplication.MapControllers();
    webApplication.Run();
}