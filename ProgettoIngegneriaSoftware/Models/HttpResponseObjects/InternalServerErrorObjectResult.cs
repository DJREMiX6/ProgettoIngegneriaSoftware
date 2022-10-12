using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Net;

namespace ProgettoIngegneriaSoftware.Models.HttpResponseObjects
{
    public class InternalServerErrorObjectResult : ObjectResult
    {
        public InternalServerErrorObjectResult([ActionResultObjectValue] object? value) : base(value)
        {
            base.StatusCode = (int)HttpStatusCode.InternalServerError;
        }

        public InternalServerErrorObjectResult([ActionResultObjectValue] ModelStateDictionary modelState) : base(modelState)
        {
            base.StatusCode = (int)HttpStatusCode.InternalServerError;
        }
    }
}
