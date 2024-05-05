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

//development yada production ortamlarýný kullanabilmek için
var env = builder.Environment;
builder.Configuration
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile("appsettings.json",optional:false)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json",optional:true);

//db configurationlarýný yazdýðýmýz yer
builder.Services.AddPersistence(builder.Configuration);

//JWT için yazdýðýmýz servis
builder.Services.AddInfrastructure(builder.Configuration);

//application'ý burada çaðýrýyoruz.Çünkü productController'da MediatR ile çalýþabilmek için.
builder.Services.AddAplication();

//automap için service eklendi
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
