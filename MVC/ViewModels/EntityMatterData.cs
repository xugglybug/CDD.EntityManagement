using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MVC.Models;

namespace MVC.ViewModels
{
    public class EntityMatterData
    {
        public int? EntityID { get; set; }
        public IEnumerable<cEntity> Entities { get; set; }
        public IEnumerable<cMatter> Matters { get; set; }
    }

    public class IndividualMatterData
    {
        public int? EntityID { get; set; }
        public IEnumerable<cIndividual> Individuals { get; set; }
        public IEnumerable<cMatter> Matters { get; set; }
        public IEnumerable<cEntityRelationship> EntityRelationships { get; set; }
}

    public class OrganisationMatterData
    {
        public int? EntityID { get; set; }
        public IEnumerable<cOrganisation> Organisations { get; set; }
        public IEnumerable<cMatter> Matters { get; set; }
    }
}