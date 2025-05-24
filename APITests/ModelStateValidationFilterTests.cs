using B3.API.Filters;
using B3.API.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;

namespace APITests
{
    public class ModelStateValidationFilterTests
    {
        [Fact]
        public void Deve_Retornar_BadRequest_Quando_ModelState_Invalido()
        {

            var modelState = new ModelStateDictionary();
            modelState.AddModelError("ValorInicial", "Valor inválido");

            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor(),
                modelState);

            var context = new ActionExecutingContext(
                actionContext,
                [],
                new Dictionary<string, object?>() { },
                new Mock<Controller>().Object);

            var filter = new ModelStateValidationFilter();


            filter.OnActionExecuting(context);


            context.Result.Should().BeOfType<BadRequestObjectResult>();
            var result = context.Result as BadRequestObjectResult;
            result!.StatusCode.Should().Be(400);

            var erro = result.Value as ErroResponse;
            erro.Should().NotBeNull();
            erro!.Mensagem.Should().Be("Erro de validação.");
            erro.Detalhe.Should().Contain("Valor inválido");
        }

        [Fact]
        public void Nao_Deve_Fazer_Nada_Se_ModelState_Valido()
        {

            var modelState = new ModelStateDictionary(); // Sem erros

            var actionContext = new ActionContext(
                new DefaultHttpContext(),
                new RouteData(),
                new Microsoft.AspNetCore.Mvc.Abstractions.ActionDescriptor(),
                modelState);

            var context = new ActionExecutingContext(
                actionContext,
                [],
                new Dictionary<string, object?>() { },
                new Mock<Controller>().Object);

            var filter = new ModelStateValidationFilter();


            filter.OnActionExecuting(context);


            context.Result.Should().BeNull();
        }
        [Fact]
        public void ModelStateValidationFilter_Nao_Deve_Alterar_Resultado_Quando_ModelState_Valido()
        {
            var modelState = new ModelStateDictionary(); // sem erros

            var context = new ActionExecutingContext(
                new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor(), modelState),
                [],
                new Dictionary<string, object?>() { },
                new Mock<Controller>().Object
            );

            var filter = new ModelStateValidationFilter();

            filter.OnActionExecuting(context);

            context.Result.Should().BeNull();
        }

        [Fact]
        public void Deve_Retornar_Erro_Com_Multiplos_Problemas_De_Validacao()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("ValorInicial", "Valor inválido");
            modelState.AddModelError("PrazoEmMeses", "Prazo inválido");

            var context = new ActionExecutingContext(
                new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor(), modelState),
                [],
                new Dictionary<string, object?>(),
                new Mock<Controller>().Object
            );

            var filter = new ModelStateValidationFilter();

            filter.OnActionExecuting(context);

            var result = context.Result as BadRequestObjectResult;
            var erro = result?.Value as ErroResponse;

            erro.Should().NotBeNull();
            erro!.Detalhe.Should().Contain("Valor inválido");
            erro!.Detalhe.Should().Contain("Prazo inválido");
        }

        [Fact]
        public void Deve_Retornar_BadRequest_Com_Apenas_Um_Erro()
        {
            var modelState = new ModelStateDictionary();
            modelState.AddModelError("Prazo", "Prazo inválido");

            var context = new ActionExecutingContext(
                new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor(), modelState),
                [],
                new Dictionary<string, object?>(),
                new Mock<Controller>().Object
            );

            var filter = new ModelStateValidationFilter();
            filter.OnActionExecuting(context);

            var result = context.Result as BadRequestObjectResult;
            var erro = result?.Value as ErroResponse;

            erro.Should().NotBeNull();
            erro!.Detalhe.Should().Be("Prazo inválido");
        }


    }
}
