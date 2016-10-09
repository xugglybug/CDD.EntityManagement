using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using MVC.Models;
using System.Web.Mvc;

namespace MVC.ViewModels
{
    public class EntityRelationshipViewModel
    {
        public eRelType eRelType { get; set; }
        public int nEntityA_ID { get; set; }
        public int nEntityB_ID { get; set; }
        public SelectList entities { get; set; }
    }
}