using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    /// <summary>
    /// Relationship Type - link entity (Individual or Organisation) to entity (Individual or Organisation)
    /// Table also has payload = Realtionship Type
    /// </summary>
    public class cEntityRelationship
    {
        public int ID { get; set; }
        public int entityA_ID { get; set; }
        public int entityB_ID { get; set; }
        public virtual cEntity entityA { get; set; }
        public virtual cEntity entityB { get; set; }

        public eRelType relType { get; set; }

    }
}