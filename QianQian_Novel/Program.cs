using QianQian_Novel.Domain.RedisDemo.Service;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;
using QianQian_Novel.Middleware;
using QianQian_Novel.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
var _configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddResponseCaching();
builder.Services.AddSingleton(provider => ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis")).GetDatabase(db: 1));
builder.Services.AddSingleton<JWTHelper>();
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddRepository(_configuration.GetConnectionString("PG_Master"));
builder.Services.AddDomain();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true
    };
});
builder.Services.AddSwaggerGen(option =>
{
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "QianQian_Novel.xml"), true);
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "QianQian_Entity.xml"), true);
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "QianQian_Model.xml"), true);
});
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
}
app.UseAuthentication();
app.UseAuthorization();
app.ConstantInitialization();
app.MapControllers();
app.UseResponseCaching();
app.Run();
