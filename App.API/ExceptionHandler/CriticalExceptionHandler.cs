using App.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace App.API.ExceptionHandler;

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