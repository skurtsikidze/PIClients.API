using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIClients.API.Models
{
  public partial class GenderDescriptions
  {
    public GenderDescriptions()
    {
      Clients = new HashSet<Clients>();
    }

    public int GenderDescriptionId { get; set; }
    public string GenderDescription { get; set; }
    [NotMapped]
    public virtual ICollection<Clients> Clients { get; set; }
  }
}
