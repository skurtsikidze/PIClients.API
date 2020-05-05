using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PIClients.API.Helpers;
using PIClients.API.Helpers.enumerators;
using PIClients.API.Models;
using PIClients.API.Objects;
using System;

namespace PIClients.API.ActionFilters
{
  public class ValidationActionFilter<T> : IActionFilter where T : class
  {
    private readonly ClientsContext _context;

    public ValidationActionFilter(ClientsContext context)
    {
      _context = context;
    }

    private HttpMethods GetHttpMethod(ActionExecutingContext context)
    {
      HttpMethods retValue = HttpMethods.Get;

      string method = context.HttpContext.Request.Method;

      switch (method)
      {
        case "GET":
          retValue = HttpMethods.Get;
          break;
        case "POST":
          retValue = HttpMethods.Post;
          break;
        case "PUT":
          retValue = HttpMethods.Put;
          break;
        case "DELETE":
          retValue = HttpMethods.Delete;
          break;
        default:
          retValue = HttpMethods.Get;
          break;
      }

      return retValue;
    }

    private Valid ModelIsValid(int id, Clients clients, ActionExecutingContext AEcontext)
    {
      Valid retValue = new Valid() { IsValid = true, Message = string.Empty };
      HttpMethods httpMethod = GetHttpMethod(AEcontext);

      if (httpMethod == HttpMethods.Put)
      {
        if (id != 0 && clients != null)
        {
          if (id != clients.ClientId)
          {
            retValue = PrepareRetValue(false, "Client Id is not match!");
            return retValue;
          }
        }
      }
      using (var validationHelper = new ValidationHelper(clients))
      {
        if (httpMethod == HttpMethods.Post || httpMethod == HttpMethods.Put)
        {
          retValue = validationHelper.ObjectIsValid();
          if (!retValue.IsValid) return retValue;
        }
      }


      return retValue;
    }

    private Valid PrepareRetValue(bool IsValid, string Message)
    {
      return new Valid() { IsValid = IsValid, Message = Message };
    }

    public void OnActionExecuting(ActionExecutingContext AEcontext)
    {
      int id = 0;
      Clients clients = null;
      BadRequestResult badRequestResult = new BadRequestResult();
      Valid requestIsValid;
      try
      {
        if (AEcontext.ActionArguments.ContainsKey("id"))
        {
          id = (int)AEcontext.ActionArguments["id"];
        }

        if (AEcontext.ActionArguments.ContainsKey("clients"))
        {
          clients = (Clients)AEcontext.ActionArguments["clients"];
        }

        requestIsValid = ModelIsValid(id, clients, AEcontext);

        if (!requestIsValid.IsValid)
        {
          AEcontext.Result = new BadRequestObjectResult(requestIsValid.Message);
        }
      }
      catch (Exception Err)
      {
        AEcontext.Result = new BadRequestObjectResult(Err.Message);
      }

    }


    public void OnActionExecuted(ActionExecutedContext context)
    {

    }
  }
}
