using System;
using System.Collections.Generic;

namespace PIClients.API.Models
{
    public partial class RelatedClients
    {
        public int RelatedClientsId { get; set; }
        public int ClientId { get; set; }
        public int RelationTypeId { get; set; }
        public int RelatedClientId { get; set; }

        public virtual Clients Client { get; set; }
        public virtual Clients RelatedClientsNavigation { get; set; }
        public virtual RelationTypes RelationType { get; set; }
    }
}
