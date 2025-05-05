using B3.API.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace B3.API.Filters
{
    public class ModelStateValidationFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            ArgumentNullException.ThrowIfNull(context);

            if (!context.ModelState.IsValid)
            {
                var erros = context.ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToArray();

                var erroResponse = new ErroResponse
                {
                    Status = 400,
                    Mensagem = "Erro de validação.",
                    Detalhe = string.Join(" | ", erros)
                };

                context.Result = new BadRequestObjectResult(erroResponse);
            }
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

    }
}
