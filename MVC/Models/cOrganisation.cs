using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class cOrganisation : cEntity
    {
        public string registeredName { get; set; }

        public string tradingName { get; set; }

        public bool keyClient { get; set; }

        public eOrgType organisationType { get; set; }


        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
    }
}