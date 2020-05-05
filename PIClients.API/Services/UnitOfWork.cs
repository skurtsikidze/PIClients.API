using Microsoft.AspNetCore.Hosting;
using PIClients.API.Infrastructure;
using PIClients.API.Models;
using PIClients.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIClients.API.Services
{
  public class UnitOfWork : IUnitOfWork
  {
    private ClientsContext _context;
    public IClientRepository __clientRepository;
    public IWebHostEnvironment _env;

    public UnitOfWork(ClientsContext context, IWebHostEnvironment env)
    {
      _context = context;
      _env = env;
    }
    public IClientRepository ClientRepository
    {
      get
      {
        return __clientRepository = __clientRepository ?? new ClientRepository(_context,_env);
      }
    }

    public void Save()
    {
      _context.SaveChanges();
    }
  }
}
