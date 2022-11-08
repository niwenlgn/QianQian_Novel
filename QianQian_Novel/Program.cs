using QianQian_Novel.Domain.RedisDemo.Service;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var _configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddResponseCaching();
builder.Services.AddSingleton(provider => ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis")).GetDatabase(db: 1));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddRepository(_configuration.GetConnectionString("PG_Master"));
builder.Services.AddDomain();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.IncludeXmlComments(Path.Combine(builder.Environment.ContentRootPath, "QianQian_Novel.xml"), true);
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
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseResponseCaching();
app.Run();
