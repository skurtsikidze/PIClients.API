using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PIClients.API.Models
{
  public partial class PhoneNumbers
  {
    public int PhoneNumberId { get; set; }
    public int ClientId { get; set; }
    public int PhoneNumberTypeId { get; set; }
    
    [StringLength(50, MinimumLength = 4)]
    [RegularExpression(@"^\d+$")]
    public string PhoneNumber { get; set; }

    public virtual Clients Client { get; set; }
    public virtual PhoneNumberTypes PhoneNumberType { get; set; }
  }
}
