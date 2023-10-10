using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Reflection;
using System.Text.Json.Serialization;
using WebTechnologies.Application.Authentication;
using WebTechnologies.Application.Extensions;
using WebTechnologies.Application.Interfaces;
using WebTechnologies.Domain.Constants;
using WebTechnologies.Domain.Models;
using WebTechnologies.Infrastructure.Data;
using WebTechnologies.Infrastructure.Extensions;
using WebTechnologies.Presentation.Controllers;
using WebTechnologies.WebAPI.Middleware;
using WebTechnologies.WebAPI.OptionsSetup;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Web Technologies API",
        Description = "An ASP.NET Core Web API task for WebTechnologies company",
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});


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

app.UseSerilogRequestLogging();

app.UseMiddleware<LoggingMiddleware>();
app.UseMiddleware<ExceptionHandlingMiddleware>();

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