using Jwt_service.Application;
using Jwt_service.Domain;
using Jwt_service.Infrastructure;
using Jwt_service.Presentation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
