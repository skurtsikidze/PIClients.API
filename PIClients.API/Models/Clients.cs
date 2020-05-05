using Microsoft.AspNetCore.Http;
using PIClients.API.Helpers.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIClients.API.Models
{
  public partial class Clients
  {
    public Clients()
    {
      PhoneNumbers = new HashSet<PhoneNumbers>();
      RelatedClientsClient = new HashSet<RelatedClients>();
    }

    public int ClientId { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    [NameValidation(ErrorMessage = "Name is not Valid!")]
    public string FirstName { get; set; }
    
    [Required]
    [StringLength(50, MinimumLength = 2)]
    [NameValidation(ErrorMessage = "Name is not Valid!")]
    public string LastName { get; set; }
    
    [Required]
    public int GenderDescriptionId { get; set; }

    [Required]
    [StringLength(11, MinimumLength = 11)]
    [RegularExpression(@"^\d+$")]
    public string PersonalNumber { get; set; }
    
    [Required]
    [DataType(DataType.Date)]
    [BirthDateValidation(acceptedAge:18,ErrorMessage ="Birth Date is not Valid!")]
    public DateTime BirthDate { get; set; }
    
    [Required]
    public int CityId { get; set; }
    
    public string Photo { get; set; }
    
    [NotMapped]
    public string Image { get; set; }

    public virtual Cities City { get; set; }
    public virtual GenderDescriptions GenderDescription { get; set; }
    public virtual RelatedClients RelatedClientsRelatedClientsNavigation { get; set; }
    public virtual ICollection<PhoneNumbers> PhoneNumbers { get; set; }
    public virtual ICollection<RelatedClients> RelatedClientsClient { get; set; }
  }
}
