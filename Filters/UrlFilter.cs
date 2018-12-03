using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Reflection;

public class UrlFilter : ActionFilterAttribute
{
  public override void OnActionExecuting(ActionExecutingContext context)
  {
    System.Console.WriteLine("Executing: " + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
    ILogger<UrlFilter> log = (ILogger<UrlFilter>)context.HttpContext.RequestServices.GetService(typeof(ILogger<UrlFilter>));
    log.LogInformation("Executing: " + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
  }
  public override void OnActionExecuted(ActionExecutedContext context)
  {
    System.Console.WriteLine("Executed: " + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
    ILogger<UrlFilter> log = (ILogger<UrlFilter>)context.HttpContext.RequestServices.GetService(typeof(ILogger<UrlFilter>));
    log.LogInformation("Executed: " + context.HttpContext.Request.Path + context.HttpContext.Request.QueryString);
  }
}