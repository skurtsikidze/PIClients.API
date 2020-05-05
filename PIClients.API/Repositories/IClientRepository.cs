using PIClients.API.Models;
using System.Collections.Generic;

namespace PIClients.API.Repositories
{
  public interface IClientRepository
  {
    IEnumerable<Clients> GetAllClients();
    Clients GetClient(int id);
    Clients Add(Clients client);
    Clients Update(int id,Clients client);
    Clients Delete(int id);
    IEnumerable<Clients> Search(string searchText, int? skip, int? take);
  }
}
