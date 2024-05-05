using Persistence;
using SouvenirApi.Application;
using SouvenirApi.Application.Exceptions;
using SouvenirApi.Infrastructure;
using SouvenirApi.Mapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
