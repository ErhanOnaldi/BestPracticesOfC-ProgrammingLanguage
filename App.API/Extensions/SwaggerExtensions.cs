namespace App.API.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerExtension(this IServiceCollection services)
    {
        services.AddSwaggerGen(x =>
        {
            x.SwaggerDoc("v1", new() { Title = "CleanAppAPI", Version = "v1" });
        });
        return services;
    }

    //middle ware itself!
    public static IApplicationBuilder UseSwaggerExtension(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(x => x.SwaggerEndpoint("/swagger/v1/swagger.json","CleanAppAPI v1"));
        return app;
    }
}