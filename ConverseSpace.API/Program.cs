using ConverseSpace.API.Extensions;
using ConverseSpace.Application;
using ConverseSpace.Application.Authentication.Services;
using ConverseSpace.Data;
using ConverseSpace.Data.Repositories;
using ConverseSpace.Domain.Abstractions.Auth;
using ConverseSpace.Domain.Abstractions.Repositories;
using ConverseSpace.Infrastructure.Authentication;
using ConverseSpace.Infrastructure.Configuration;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddApiAuthentication(configuration);



// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<CSDBContext>(options =>
    options.UseNpgsql(configuration.GetConnectionString("conn")));

#region Services

#region Repositories

builder.Services.AddScoped<IUsersRepository, UsersRepository>();

#endregion

#region Services

builder.Services.AddScoped<IAuthService,AuthService>();

#endregion

#region Others
builder.Services.AddScoped<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddHttpContextAccessor();

#endregion

#endregion

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.Strict,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();