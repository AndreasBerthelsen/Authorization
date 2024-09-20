using AuthorizationPOC.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NewsDbContext>(options =>
    options.UseSqlite(connectionString: "DefaultConnection")
    );

builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "news.com",
        ValidAudience = "news.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("KEY"))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EditArticle", policy => policy.RequireRole("Editor", "Journalist"));
    options.AddPolicy("DeleteArticle", policy => policy.RequireRole("Editor"));
    options.AddPolicy("EditComment", policy => policy.RequireRole("Editor"));
    options.AddPolicy("DeleteComment", policy => policy.RequireRole("Editor"));
    options.AddPolicy("CommentOnArticle", policy => policy.RequireRole("Subscriber"));
});

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
