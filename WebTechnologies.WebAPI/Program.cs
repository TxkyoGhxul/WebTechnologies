using System.Text.Json.Serialization;
using WebTechnologies.Application.Authentication;
using WebTechnologies.Application.Extensions;
using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Constants;
using WebTechnologies.Domain.Models;
using WebTechnologies.Infrastructure.Data;
using WebTechnologies.Infrastructure.Extensions;
using WebTechnologies.Presentation.Controllers;
using WebTechnologies.WebAPI.OptionsSetup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .InjectDbContext(builder.Configuration)
    .InjectRepositories()
    .InjectAutoMapper()
    .InjectJwtBearer(builder.Configuration)
    .InjectSorters()
    .InjectValidators()
    .InjectMediatR();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

builder.Services.AddControllers()
    .AddApplicationPart(typeof(UserController).Assembly)
    .AddControllersAsServices()
    .AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSingleton<JwtOptions>();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

var app = builder.Build();

await SeedDbContextAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task SeedDbContextAsync(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var scopedProvider = scope.ServiceProvider;
    var dbContext = scopedProvider.GetRequiredService<UserDbContext>();
    var unitOfWork = scopedProvider.GetRequiredService<IUnitOfWork>();
    if (!dbContext.Roles.Any())
    {
        var roles = new List<Role>
        {
            Roles.User,
            Roles.Support,
            Roles.Admin,
            Roles.SuperAdmin
        };

        dbContext.Roles.AddRange(roles);
        await unitOfWork.SaveChangesAsync();
    }
}