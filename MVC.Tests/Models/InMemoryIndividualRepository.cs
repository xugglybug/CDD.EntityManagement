using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MVC.Models;

namespace MVC.Tests.Models
{
    public class InMemoryIndividualRepository : IIndividualRepository
    {
        private List<cIndividual> _db = new List<cIndividual>();

        public Exception ExceptionToThrow { get; set; }
        //public List<Contact> Items { get; set; }

        public void SaveChanges(cIndividual individualToUpdate)
        {

            foreach (cIndividual indiv in _db)
            {
                if (indiv.ID == individualToUpdate.ID)
                {
                    _db.Remove(indiv);
                    _db.Add(individualToUpdate);
                    break;
                }
            }
        }

        public void Add(cIndividual individualToAdd)
        {
            _db.Add(individualToAdd);
        }

        public cIndividual GetIndividualByID(int id)
        {
            return _db.FirstOrDefault(d => d.ID == id);
        }

        public void CreateNewIndividual(cIndividual individualToCreate)
        {
            if (ExceptionToThrow != null)
                throw ExceptionToThrow;

            _db.Add(individualToCreate);
            // return contactToCreate;
        }

        public int SaveChanges()
        {
            return 1;
        }

        public IEnumerable<cIndividual> GetAllIndividuals()
        {
            return _db.ToList();
        }


        public void DeleteIndividual(int id)
        {
            _db.Remove(GetIndividualByID(id));
        }
    }
}
