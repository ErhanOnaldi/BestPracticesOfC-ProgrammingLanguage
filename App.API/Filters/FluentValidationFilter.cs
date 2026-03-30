using App.Application;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

//Filterlar değişebilir, result filter result'u sarar, exception filter tüm bir processi sarar, biz request response döngüsünü saran bir filter kullanıyoruz
//.NET'in kendi validation filterini deaktif ettik, şimdi bunu yazıyoruz
//Request Gelmeden yakalamalıyız

namespace App.API.Filters;

public class FluentValidationFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState
                .Values.SelectMany(x => x.Errors).Select(error => error.ErrorMessage).ToList();
            var resultModel = ServiceResult.Fail(errors);
            context.Result = new BadRequestObjectResult(resultModel);
            return;
        }
        await next();
    }
}