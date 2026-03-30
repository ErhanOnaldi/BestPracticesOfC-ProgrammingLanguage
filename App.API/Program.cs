using App.API.Extensions;
using App.Application.Extensions;
using App.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithFiltersExtension().AddSwaggerExtension().AddExceptionHandlerExtensions().AddCachingExtensions();
// Add services to the container.
//kendimiz filter ekledik
builder.Services.AddControllersWithFiltersExtension();
//.NET'in default hata mesajlarını bastır
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddProblemDetails();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerExtension();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();