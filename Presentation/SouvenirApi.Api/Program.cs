using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence;
using SouvenirApi.Application;
using SouvenirApi.Application.Exceptions;
using SouvenirApi.Infrastructure;
using SouvenirApi.Mapper;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



//development yada production ortamlar�n� kullanabilmek i�in
var env = builder.Environment;
builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json",optional:false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true);

//db configurationlar�n� yazd���m�z yer
builder.Services.AddPersistence(builder.Configuration);

//JWT i�in yazd���m�z servis
builder.Services.AddInfrastructure(builder.Configuration);

//application'� burada �a��r�yoruz.��nk� productController'da MediatR ile �al��abilmek i�in.
builder.Services.AddAplication();

//automap i�in service eklendi
builder.Services.AddCustomMapper();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Souvenir API", Version = "v1", Description = "Souvenir APIswagger client." });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "'Bearer' yaz�p bo�luk b�rakt�ktan sonra Token'a Girebilirsiniz \r\n\r\n �rne�in: \"Bearer aaaaaaaaaaaaaa\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExceptionHandlingMiddleware();
app.UseAuthorization();

app.MapControllers();

app.Run();
