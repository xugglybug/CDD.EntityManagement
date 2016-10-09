using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class cIndividual : cEntity
    {
        public string Title { get; set; }

        public string Surname { get; set; }

        public string Firstname { get; set; }

        public string Middlename { get; set; }

        public string Previousname { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string PlaceOfBirth { get; set; }

        public string Nationality { get; set; }

        public string CountryOfResidence { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
    }
}