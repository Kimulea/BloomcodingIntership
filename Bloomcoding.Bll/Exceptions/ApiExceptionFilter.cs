using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bloomcoding.Bll.Exceptions
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if(context.Exception is ApiException apiException)
            {
                context.Result = new ObjectResult(apiException.Message) { StatusCode = (int) apiException.Code };
            }
        }
    }
}
