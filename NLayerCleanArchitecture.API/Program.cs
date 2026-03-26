using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayerCleanArchitecture.Repository;
using NLayerCleanArchitecture.Repository.Extensions;
using NLayerCleanArchitecture.Service;
using NLayerCleanArchitecture.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//kendimiz filter ekledik
builder.Services.AddControllers(options =>
{
    options.Filters.Add<FluentValidationFilter>();
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; // kendi kendine nullable olanlara hata mesajı eklemesin
});
//.NET'in default hata mesajlarını bastır
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddProblemDetails();

builder.Services.AddRepositories(builder.Configuration).AddServices(builder.Configuration);

var app = builder.Build();
app.UseExceptionHandler();

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