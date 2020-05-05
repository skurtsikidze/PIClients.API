using System;
using System.Collections.Generic;

namespace PIClients.API.Models
{
    public partial class PhoneNumberTypes
    {
        public PhoneNumberTypes()
        {
            PhoneNumbers = new HashSet<PhoneNumbers>();
        }

        public int PhoneNumberTypeId { get; set; }
        public string PhoneNumberTypeDescription { get; set; }

        public virtual ICollection<PhoneNumbers> PhoneNumbers { get; set; }
    }
}
