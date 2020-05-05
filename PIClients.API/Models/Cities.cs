using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIClients.API.Models
{
  public partial class Cities
  {
    public Cities()
    {
      Clients = new HashSet<Clients>();
    }

    public int CityId { get; set; }
    public string CityName { get; set; }
    [NotMapped]
    public virtual ICollection<Clients> Clients { get; set; }
  }
}
