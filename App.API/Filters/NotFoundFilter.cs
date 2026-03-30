using App.Application;
using App.Application.Interfaces.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace App.API.Filters;

public class NotFoundFilter<T>(IGenericRepository<T> genericRepository) : Attribute, IAsyncActionFilter where T : class
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        //action metod çalışmadan önce
        var idValue = context.ActionArguments["id"];
        if (idValue is null)
        {
            await next();
            return;
        }

        if (!int.TryParse(idValue.ToString(), out var id))
        {
            await next();
            return;
        }

        var hasEntity = await genericRepository.GetByIdAsync(id);

        if (hasEntity is null)
        {
            var name = typeof(T).Name;
            var actionName = context.ActionDescriptor.DisplayName;
            var result = ServiceResult.Fail($"{name} not found at action {actionName}");
            context.Result = new NotFoundObjectResult(new {message = "Not found"});
        }
        
        await next();
        //action metod çalışıtktan sonra
    }
}