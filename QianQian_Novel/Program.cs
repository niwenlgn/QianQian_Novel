using QianQian_Novel.Domain.RedisDemo.Service;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var _configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option => {
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile), true);
});
builder.Services.AddSingleton(provider => ConnectionMultiplexer.Connect(_configuration["AppSettings:Redis"]).GetDatabase(db: 1));
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));
builder.Services.AddDomain();
builder.Services.AddRepository();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
