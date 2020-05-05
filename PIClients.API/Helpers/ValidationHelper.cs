using PIClients.API.Models;
using PIClients.API.Objects;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PIClients.API.Helpers
{
  public class ValidationHelper : IDisposable
  {
    bool _disposed;

    private Clients _clients;

    public ValidationHelper(Clients clients)
    {
      _clients = clients;
    }

    public Valid ObjectIsValid()
    {
      // ამ მეთოდში მოთავსდება ყველა ის ვალიდაცია რომელიც ატრიბუტით არ კეთდება
      Valid retValue = new Valid() { IsValid = true, Message = String.Empty };
      string errorMessage = "Object not Valid!";

      if (_clients == null)
      {
        retValue.IsValid = false;
        retValue.Message = errorMessage;
      }

      return retValue;
    }

    #region *** IDisposable members

    public void Dispose()
    {
      Dispose(true);
    }

    public void Dispose(bool disposing)
    {
      if (_disposed) return;

      if (disposing)
      {
        _clients = null;
      }

      _disposed = true;
    }

    #endregion

  }
}
