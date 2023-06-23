using ApiApplication.ViewModels.Shared;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace ApiApplication.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger<BaseController> _logger;

        public BaseController( ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();        

            var context = await next();

            stopwatch.Stop();
            var executionTime = stopwatch.ElapsedMilliseconds;
            string controllerName = ControllerContext.RouteData.Values["controller"].ToString(); ;

            _logger.LogInformation($"Execution time of controller: {controllerName}Controller {executionTime}ms");

            if (filterContext.ModelState.IsValid == false)
            {
                List<ValidationModel> validations = new List<ValidationModel>();
                foreach (var item in filterContext.ModelState.ToList())
                {
                    validations.Add(new ValidationModel
                    {
                        Attribute = item.Key,
                        Message = String.Join("\n ", item.Value.Errors.Select(x => x.ErrorMessage).ToArray())
                    });
                }

                var errorModel = new ResponseModel() { Message = "Invalid data", Success = false, Validations = validations };
                context.Result = BadRequest(errorModel);
            }

            if (context.Exception is Exception exception)
            {
                context.ExceptionHandled = true;
                var errorModel = new ResponseModel() { Message = exception.Message, Success = false };

                if (this.Request.Headers["x-requested-with"] == "XMLHttpRequest")
                    context.Result = new BadRequestResult();
                else
                    context.Result = BadRequest(errorModel);
            }
        }              
    }
}
