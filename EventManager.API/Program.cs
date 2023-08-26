using EventManager.API.EfData;
using EventManager.API.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config["JwtSettings:Issuer"],
        ValidAudience = config["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(config["JwtSettings:Key"]!)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true
    };

    x.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                context.Response.Headers.Add("Token-Expired", "true");
            return Task.CompletedTask;
        }
    };
});

builder.Services.AddAuthentication();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(StaticData.ApplicationUserRolesEnum.SuperAdmin.ToString(), policy => policy.RequireClaim(ClaimTypes.Role, StaticData.ApplicationUserRolesEnum.SuperAdmin.ToString()));
    options.AddPolicy(StaticData.ApplicationUserRolesEnum.EventPlannerOrg.ToString(), policy => policy.RequireClaim(ClaimTypes.Role, StaticData.ApplicationUserRolesEnum.EventPlannerOrg.ToString()));
    options.AddPolicy(StaticData.ApplicationUserRolesEnum.EventManagerForOrg.ToString(), policy => policy.RequireClaim(ClaimTypes.Role, StaticData.ApplicationUserRolesEnum.EventManagerForOrg.ToString()));
    options.AddPolicy(StaticData.ApplicationUserRolesEnum.TicketScanner.ToString(), policy => policy.RequireClaim(ClaimTypes.Role, StaticData.ApplicationUserRolesEnum.TicketScanner.ToString()));
    options.AddPolicy(StaticData.ApplicationUserRolesEnum.Guest.ToString(), policy => policy.RequireClaim(ClaimTypes.Role, StaticData.ApplicationUserRolesEnum.Guest.ToString()));
});
builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
             options.UseSqlServer(builder.Configuration.GetConnectionString("EventPlannerApiDbLocal")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
