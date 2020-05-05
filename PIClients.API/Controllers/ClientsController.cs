using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using PIClients.API.ActionFilters;
using PIClients.API.Infrastructure;
using PIClients.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PIClients.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ClientsController : ControllerBase
  {
    private readonly ClientsContext _context;
    private readonly IWebHostEnvironment _env;
    private readonly IUnitOfWork _unitOfWork;

    public ClientsController(ClientsContext context, IWebHostEnvironment env, IUnitOfWork unitOfWork)
    {
      _context = context;
      _env = env;
      _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [ServiceFilter(typeof(ValidationActionFilter<ClientsContext>))]
    public ActionResult<IEnumerable<Clients>> GetClients()
    {
      try
      {
        return _unitOfWork.ClientRepository.GetAllClients().ToList();
      }
      catch (Exception Err)
      {
        return BadRequest(Err.Message);
      }
    }

    [HttpGet("{id}")]
    [ServiceFilter(typeof(ValidationActionFilter<ClientsContext>))]
    public ActionResult<Clients> GetClients(int id)
    {
      try
      {
        return _unitOfWork.ClientRepository.GetClient(id);
      }
      catch (Exception Err)
      {
        return BadRequest(Err.Message);
      }
      
    }

    [HttpPut("{id}")]
    [ServiceFilter(typeof(ValidationActionFilter<ClientsContext>))]
    public IActionResult PutClients(int id, Clients clients)
    {
      try
      {
        _unitOfWork.ClientRepository.Update(id, clients);
        return NoContent();
      }
      catch (Exception Err)
      {
        return BadRequest(Err.Message);
      }
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidationActionFilter<ClientsContext>))]
    public ActionResult<Clients> PostClients(Clients clients)
    {
      try
      {
        _unitOfWork.ClientRepository.Add(clients);
        return NoContent();
      }
      catch (Exception Err)
      {
        return BadRequest(Err.Message);
      }
    }

    [HttpDelete("{id}")]
    [ServiceFilter(typeof(ValidationActionFilter<ClientsContext>))]
    public ActionResult<Clients> DeleteClients(int id)
    {
      try
      {
        _unitOfWork.ClientRepository.Delete(id);
        return NoContent();
      }
      catch (Exception Err)
      {
        return BadRequest(Err.Message);
      }
    }

    [HttpGet("{searchText}/{skip}/{take}")]
    [ServiceFilter(typeof(ValidationActionFilter<ClientsContext>))]
    public ActionResult<IEnumerable<Clients>> SearchClients(string searchText, int? skip, int? take)
    {
      try
      {
        return _unitOfWork.ClientRepository.Search(searchText,skip,take).ToList();
      }
      catch (Exception Err)
      {
        return BadRequest(Err.Message);
      }
    }


  }
}
