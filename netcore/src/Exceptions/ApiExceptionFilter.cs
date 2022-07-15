
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Contacts.Exceptions
{
    /// <summary>
    /// Implements a custom error handler for Scheduling controllers.
    /// </summary>
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// An custom filter which is being called by <see cref="ExceptionFilterAttribute.OnExceptionAsync"/> method.
        /// NOTE: No need to override OnExceptionAsync method, as mentioned on official documentation.
        /// </summary>
        /// <param name="context">The <see cref="ExceptionContext" />.</param>
        public override void OnException(ExceptionContext context)
        {
            switch(context.Exception) 
            {
                case ContactNotFoundException exception: {
                    context.Result = new NotFoundObjectResult(exception.Message);
                } break;
                case DuplicatedContactException exception: {
                    context.Result = new BadRequestObjectResult(exception.Message);
                } break;
                case InvalidCredentialsException exception: {
                    context.Result = new BadRequestObjectResult(exception.Message);
                } break;
                case RequiredFieldException exception: {
                    context.Result = new BadRequestObjectResult(exception.Message);
                } break;
            }

            base.OnException(context);
        }
    }
}

