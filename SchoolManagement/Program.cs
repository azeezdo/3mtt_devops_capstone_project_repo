using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using SchoolManagement.Domain.Interface;
using SchoolManagement.Infrastructure.context;
using SchoolManagement.Infrastructure.Repositories;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddHttpContextAccessor();
// Add services to the container.
builder.Services.AddDbContext<SchoolDbContext>();
//builder.Services.AddIdentityService(configuration);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
//builder.Services.AddTransient<AgencyBankingDbContext>();
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
    });
});
builder.Services.AddControllers().AddJsonOptions(_ =>
{
    _.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    _.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
}); ;

builder.Services.AddRouting(context => context.LowercaseUrls = true);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "School Management API",
        Version = "v1"
    });
    c.CustomSchemaIds(i => i.FullName);
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Scheme = "Bearer",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        }, new List<string>()
                }
             });
});
//builder.Services.Configure<ApiBehaviorOptions>(options =>
//              options.InvalidModelStateResponseFactory = ActionContext =>
//              {
//                  var error = ActionContext.ModelState
//                              .Where(e => e.Value.Errors.Count > 0)
//                              .SelectMany(e => e.Value.Errors)
//                              .Select(e => e.ErrorMessage).ToArray();
//                  var errorresponce = new APIValidationErrorResponse
//                  {
//                      Errors = error
//                  };
//                  return new BadRequestObjectResult(error);
//              }
//            );


builder.Services.TryAddSingleton<ISystemClock, SystemClock>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

