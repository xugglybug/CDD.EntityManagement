using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.ViewModels
{
    public class AssignedMatterViewModel
    {
        public string MatterNumber { get; set; }
        public string MatterDescription { get; set; }
        public bool Assigned { get; set; }
    }

    
}