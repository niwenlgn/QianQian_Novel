using QianQian_Novel.Domain.RedisDemo.Service;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using System.Reflection;
using QianQian_Novel.Middleware;
using QianQian_Novel.Helper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using QianQian_Novel.Filter;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSome", policy =>
    {
        policy
        .AllowAnyHeader().SetPreflightMaxAge(TimeSpan.FromSeconds(60))
        .AllowAnyMethod()
        .WithOrigins(_configuration.GetSection("AppSettings:CorsAllowUrl").Get<string[]>())
        .AllowCredentials()
        .WithExposedHeaders("Content-Disposition");//允许浏览器访问额外的自定义头
    });
});
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
        ValidateIssuerSigningKey = true,//是否验证签名
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:Secret").Value)),
        ValidateIssuer = true,//是否验证发行人
        ValidIssuer = _configuration.GetSection("JWT:Issuer").Value,
        ValidateAudience = true,//是否验证订阅人
        ValidAudience = _configuration.GetSection("JWT:Audience").Value,
    };
});
builder.Services.AddSwaggerGen(option =>
{
    var scheme = new OpenApiSecurityScheme()
    {
        Description = "Authorization header. \r\nExample: 'Bearer abcdefxxx'",
        Reference = new OpenApiReference
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Authorization"
        },
        Scheme = "oauth2",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
    };
    option.AddSecurityDefinition("Authorization", scheme);
    var requirement = new OpenApiSecurityRequirement();
    requirement[scheme] = new List<string>();
    option.AddSecurityRequirement(requirement);
    option.DocumentFilter<HiddenApiFilter>();
    option.SchemaFilter<HiddenFieldFilter>();
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
app.UseCors("AllowSome");
app.UseAuthentication();
app.UseAuthorization();
app.ConstantInitialization();
app.MapControllers();
app.UseResponseCaching();
app.Run();
