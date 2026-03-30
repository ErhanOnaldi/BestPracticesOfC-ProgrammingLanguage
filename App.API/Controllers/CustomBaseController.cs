using System.Net;
using App.Application;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class CustomBaseController : ControllerBase
{
    [NonAction]
    protected IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        return result.Status switch
        {
            HttpStatusCode.NoContent => NoContent(),
            HttpStatusCode.Created => Created(result.UrlAsCreated, result),
            _ => new ObjectResult(result) { StatusCode = result.Status.GetHashCode() }
        };
    }
    [NonAction]
    protected IActionResult CreateActionResult(ServiceResult result)
    {
        if (result.Status == HttpStatusCode.NoContent)
        {
            return NoContent();
        }
        return new ObjectResult(result) { StatusCode = result.Status.GetHashCode() };
    }
}