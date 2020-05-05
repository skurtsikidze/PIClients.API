using System;
using System.Collections.Generic;

namespace PIClients.API.Models
{
    public partial class RelationTypes
    {
        public RelationTypes()
        {
            RelatedClients = new HashSet<RelatedClients>();
        }

        public int RelationTypeId { get; set; }
        public string RelationTypeDescription { get; set; }

        public virtual ICollection<RelatedClients> RelatedClients { get; set; }
    }
}
