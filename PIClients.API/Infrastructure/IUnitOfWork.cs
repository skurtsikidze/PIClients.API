using PIClients.API.Repositories;

namespace PIClients.API.Infrastructure
{
  public interface IUnitOfWork
  {
    IClientRepository ClientRepository { get; }
    void Save();
  }
}
