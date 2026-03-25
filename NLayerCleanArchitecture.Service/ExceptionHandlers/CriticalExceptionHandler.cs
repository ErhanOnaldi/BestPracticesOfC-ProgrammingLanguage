using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace NLayerCleanArchitecture.Service.ExceptionHandlers;

public class CriticalExceptionHandler : IExceptionHandler
{
    public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        //business injection
        //true döndürürsen, hatayı burda ele alrısın, false dönersen, hatayı başka handler'lar ele alır.

        if (exception is CriticalException)
        {
            Console.WriteLine($"Critical error: {exception.Message}");
            
        }
        
        return ValueTask.FromResult(false);
    }
}