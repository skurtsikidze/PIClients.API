using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using PIClients.API.Helpers;
using PIClients.API.Models;
using PIClients.API.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIClients.API.Services
{
  public class ClientRepository : IClientRepository
  {
    private readonly ClientsContext _context;
    private readonly IWebHostEnvironment _env;
    public ClientRepository(ClientsContext context, IWebHostEnvironment env)
    {
      _context = context;
      _env = env;
    }

    public IEnumerable<Clients> GetAllClients()
    {
      return _context.Clients.Include(d => d.PhoneNumbers).ThenInclude(pn => pn.PhoneNumberType).Include(t => t.City).Include(tt => tt.GenderDescription).ToList();
    }
    public Clients GetClient(int id)
    {
      Clients client = _context.Clients.Include(d => d.PhoneNumbers).ThenInclude(pn => pn.PhoneNumberType).Include(t => t.City).Include(tt => tt.GenderDescription).Where(s => s.ClientId == id).FirstOrDefault();
      return client;
    }
    public Clients Add(Clients client)
    {
      string photo = "";

      using (var fu = new ImageUploader(client.Image))
      {
        photo = fu.SaveFile(_env.ContentRootPath);
      }

      client.Photo = photo;
      _context.Clients.Add(client);
      //_context.SaveChangesAsync();
      return client;
    }
    public Clients Update(int id, Clients client)
    {
      string photo = "";

      using (var fu = new ImageUploader(client.Image))
      {
        photo = fu.SaveFile(_env.ContentRootPath);
      }

      client.Photo = photo;


      var existingClient = _context.Clients.Where(p => p.ClientId == id).Include(p => p.PhoneNumbers).SingleOrDefault();

      _context.Entry(existingClient).CurrentValues.SetValues(client);

      // update phone numbers
      foreach (var item in existingClient.PhoneNumbers.ToList())
      {
        if (!client.PhoneNumbers.Any(c => c.PhoneNumberId == item.PhoneNumberId))
          _context.PhoneNumbers.Remove(item);
      }

      foreach (var phoneNumber in client.PhoneNumbers)
      {
        var existingNumber = existingClient.PhoneNumbers
            .Where(c => c.PhoneNumberId == phoneNumber.PhoneNumberId)
            .SingleOrDefault();

        if (phoneNumber.PhoneNumberId == 0)
          existingNumber = null;

        if (existingNumber != null)
          _context.Entry(existingNumber).CurrentValues.SetValues(phoneNumber);
        else
        {
          existingClient.PhoneNumbers.Add(phoneNumber);
        }
      }

      // update relations
      foreach (var item in existingClient.RelatedClientsClient.ToList())
      {
        if (!client.RelatedClientsClient.Any(c => c.RelatedClientsId == item.RelatedClientsId))
          _context.RelatedClients.Remove(item);
      }

      foreach (var relatedClient in client.RelatedClientsClient)
      {
        var existingRelation = existingClient.RelatedClientsClient
            .Where(c => c.RelatedClientsId == relatedClient.RelatedClientsId)
            .SingleOrDefault();

        if (relatedClient.RelatedClientsId == 0)
          existingRelation = null;

        if (existingRelation != null)
          _context.Entry(existingRelation).CurrentValues.SetValues(relatedClient);
        else
        {
          existingClient.RelatedClientsClient.Add(relatedClient);
        }
      }

      //_context.SaveChangesAsync();

      return client;
    }
    public Clients Delete(int id)
    {
      var client = _context.Clients.Include(d => d.PhoneNumbers).Include(t => t.RelatedClientsClient).Where(s => s.ClientId == id).FirstOrDefault();

      if (client != null)
      {
        foreach (var item in client.PhoneNumbers.ToList())
          _context.PhoneNumbers.Remove(item);

        foreach (var item in client.RelatedClientsClient.ToList())
            _context.RelatedClients.Remove(item);

        _context.Clients.Remove(client);
        //_context.SaveChangesAsync();
      }

      return client;
    }
    public IEnumerable<Clients> Search(string searchText, int? skip, int? take)
    {
      var clients = _context.Clients.Include(d => d.PhoneNumbers).ThenInclude(pn => pn.PhoneNumberType).Include(t => t.City).Include(tt => tt.GenderDescription).AsQueryable();

      if (!String.IsNullOrEmpty(searchText))
      {
        clients = clients.Where(i => i.FirstName.Contains(searchText) || i.LastName.Contains(searchText) || i.PersonalNumber.Contains(searchText));
      }

      if (skip != null)
      {
        clients = clients.Skip((int)skip);
      }

      if (take != null)
      {
        clients = clients.Take((int)take);
      }

      return clients.ToList();
    }
    public bool ClientsExists(int id)
    {
      return _context.Clients.Any(e => e.ClientId == id);
    }
  }
}
