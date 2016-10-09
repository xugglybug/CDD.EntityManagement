using MVC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.Models
{
    public interface IIndividualRepository
    {

        void CreateNewIndividual(cIndividual contactToCreate);
        void DeleteIndividual(int id);
        cIndividual GetIndividualByID(int id);
        IEnumerable<cIndividual> GetAllIndividuals();
        int SaveChanges();
    }

    public class EF_IndividualRepository : IIndividualRepository
    {

        private EntitiesDBContext _db = new EntitiesDBContext();

        public cIndividual GetIndividualByID(int id)
        {
            return _db.Individuals.FirstOrDefault(d => d.ID == id);
        }

        public IEnumerable<cIndividual> GetAllIndividuals()
        {
            return _db.Individuals.ToList();
        }

        public void CreateNewIndividual(cIndividual individualToCreate)
        {
            _db.Individuals.Add(individualToCreate);
            _db.SaveChanges();
            //   return contactToCreate;
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void DeleteIndividual(int id)
        {
            var conToDel = GetIndividualByID(id);
            _db.Individuals.Remove(conToDel);
            _db.SaveChanges();
        }

    }

}
