using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC.Controllers;

using MVC.Models;
using MVC.Tests.Models;
using System.Web;
using System.Web.Routing;
using System.Security.Principal;
using System;
using MVC.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MvcContacts.Tests.Controllers.Individuals
{

    public class Harness
    {
        public static IIndividualRepository GetRepo()
        {
            return new InMemoryIndividualRepository();
        }

        public static cIndividualController GetController(IIndividualRepository repository)
        {
            cIndividualController controller = new cIndividualController(repository);

            controller.ControllerContext = new ControllerContext()
            {
                Controller = controller,
                RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
            };
            return controller;
        }

        private class MockHttpContext : HttpContextBase
        {
            private readonly IPrincipal _user = new GenericPrincipal(
                     new GenericIdentity("someUser"), null /* roles */);

            public override IPrincipal User
            {
                get
                {
                    return _user;
                }
                set
                {
                    base.User = value;
                }
            }
        }


        public static IList<cIndividual> GetTestData(int recordCount)
        {

            var list = new List<cIndividual>();
            if (recordCount <= 0) return list;


            int nAlphaLoop = ((int)recordCount / 27) + 1;
            

            string sAlpha = "abcdefghijklmnopqrstuvwxyz";
            

            for (int i = 0; i < recordCount; i++)
            {
                string sTemp = "";
                DateTime dtTemp = DateTime.Now.AddMonths(-i);

                int nAlphaIndex = i;
                while (nAlphaIndex > 26) nAlphaIndex = nAlphaIndex - 26;

                for (int j = 0; j < nAlphaLoop; j++)
                {
                    sTemp += sAlpha[nAlphaIndex];
                }

                list.Add(new cIndividual {
                    ID =i,
                    Address1 = sTemp,
                    Address2 = sTemp,
                    Address3 = sTemp,
                    CDD_Details = null,
                    CountryOfResidence = sTemp,
                    DateOfBirth = dtTemp,
                    Firstname = sTemp,
                    HasA = null,
                    IsA = null,
                    Matters = null,
                    Middlename = sTemp,
                    Nationality = sTemp,
                    PlaceOfBirth = sTemp,
                    Previousname = sTemp,
                    Surname = sTemp,
                    Title = sTemp

                });
            }

            return list;
        }


    }


    [TestClass]
    public class IndividualControllerTest
    {


        [TestMethod]
        public void Index_Get_AsksForIndexView()
        {
            // Arrange
            var controller = Harness.GetController(new InMemoryIndividualRepository());

            // Act
            ViewResult result = (ViewResult)controller.Index(null);

            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }


        [TestMethod]
        public void Index_Get_RetrievesAllContactsFromRepository()
        {
            // Arrange
            cIndividual indiv1 = GetIndividual(1, "address1", "jersey", new DateTime(2016, 9, 28), "aaron", "aardvark");
            cIndividual indiv2 = GetIndividual(2, "address2", "guernsey", new DateTime(2016, 9, 27), "brian", "baron");
            InMemoryIndividualRepository repository = new InMemoryIndividualRepository();
            repository.Add(indiv1);
            repository.Add(indiv2);
            var controller = Harness.GetController(repository);

            // Act
            var result = controller.Index(null);



            // Assert
            var model = (IList<cIndividual>)((ViewResult)result).ViewData.Model;

            CollectionAssert.Contains(model.ToList(), indiv1);
            CollectionAssert.Contains(model.ToList(), indiv2);
        }


        [TestMethod]
        public void Create_Post_ReturnsViewIfModelStateIsNotValid()
        {
            // Arrange
            cIndividualController controller = Harness.GetController(new InMemoryIndividualRepository());
            // Simply executing a method during a unit test does just that - executes a method, and no more. 
            // The MVC pipeline doesn't run, so binding and validation don't run.
            controller.ModelState.AddModelError("", "mock error message");
            cIndividual model = GetIndividual(1, "", "", DateTime.Now, "", "");

            // Act
            var result = (ViewResult)controller.Create(model);

            // Assert
            Assert.AreEqual("Create", result.ViewName);
        }


        [TestMethod]
        public void Create_Post_PutsValidContactIntoRepository()
        {
            // Arrange
            InMemoryIndividualRepository repository = new InMemoryIndividualRepository();
            cIndividualController controller = Harness.GetController(repository);
            cIndividual indiv = GetIndividual();

            // Act
            controller.Create(indiv);

            // Assert
            IEnumerable<cIndividual> individuals = repository.GetAllIndividuals();
            Assert.IsTrue(individuals.Contains(indiv));
        }

        cIndividual GetIndividual()
        {
            return GetIndividual(1, "testaddress1", "Jersey", DateTime.Now, "John", "Doe");
        }

        cIndividual GetIndividual(int id, string address1, string countryOfResidence, DateTime dateOfBirth,
            string firstName, string surName)
        {
            return new cIndividual
            {

                ID = id,
                Address1 = address1,
                CDD_Details = new cCDD_Details(),
                CountryOfResidence = countryOfResidence,
                DateOfBirth = dateOfBirth,
                Firstname = firstName,
                Surname = surName
            };
        }

        //private static cIndividualController GetIndividualController(IIndividualRepository repository)
        //{
        //    cIndividualController controller = new cIndividualController(repository);

        //    controller.ControllerContext = new ControllerContext()
        //    {
        //        Controller = controller,
        //        RequestContext = new RequestContext(new MockHttpContext(), new RouteData())
        //    };
        //    return controller;
        //}



    }

    [TestClass]
    public class Given_5_Records_And_A_Key
    {
        InMemoryIndividualRepository _repo;
        cIndividualController _controller;

        [TestInitialize()]
        public void Initialize()
        {
            _repo = (InMemoryIndividualRepository)Harness.GetRepo();

            _repo.Add(new cIndividual { ID = 1, Firstname = "Phillipe", Surname = "Coutinho" });
            _repo.Add(new cIndividual { ID = 2, Firstname = "Bob", Surname = "Lalana" });
            _repo.Add(new cIndividual { ID = 4, Firstname = "Simon", Surname = "Mignolet" });
            _repo.Add(new cIndividual { ID = 5, Firstname = "Robert", Surname = "Firmino" });

            _controller = Harness.GetController(_repo);

            IList<cMatter> oMatters = new List<cMatter>();
            oMatters.Add(new cMatter { ID = "1", Description = "Test1" });
            oMatters.Add(new cMatter { ID = "2", Description = "Test2" });

            cCDD_Details oCDD = new cCDD_Details { ID = 1, CDDStatus = eCDDStatus.Complete };

            IList<cEntityRelationship> oER = new List<cEntityRelationship>();
            oER.Add(new cEntityRelationship {ID = 1, entityA_ID = 1, entityB_ID=2, relType = eRelType.Director });

            cIndividual indiv = new cIndividual();
            indiv.ID = 3;
            indiv.Firstname = "Adam";
            indiv.Surname = "Lalana";
            indiv.Matters = oMatters;
            indiv.CDD_Details = oCDD;
            indiv.IsA = oER;

            _repo.Add(indiv);
        }


        [TestMethod]
        public void indexShouldReturn5Records()
        {
            // Arrange (Given - see TestInitialise)

            // Act
            ViewResult result = (ViewResult)_controller.Index(3);
            int nIndividuals = ((IList<cIndividual>)result.Model).Count();

            // Assert
            Assert.AreEqual(5, nIndividuals);
        }

        [TestMethod]
        public void index_Should_Return_RelatedMatter_Details_For_Record()
        {
            // Arrange (Given - see TestInitialise)

            // Act
            ViewResult result = (ViewResult)_controller.Index(3);
            cIndividual ind = ((IList<cIndividual>)result.Model).Where(o => o.ID == 3).Single<cIndividual>();

            Assert.AreEqual(2, ind.Matters.Count<cMatter>());
        }

    }


    [TestClass]
    public class Given_5_Records_And_No_Key
    {
        InMemoryIndividualRepository _repo;
        cIndividualController _controller;

        [TestInitialize()]
        public void Initialize()
        {
            _repo = (InMemoryIndividualRepository)Harness.GetRepo();

            _repo.Add(new cIndividual { ID = 1, Firstname = "Phillipe", Surname = "Coutinho" });
            _repo.Add(new cIndividual { ID = 1, Firstname = "Bob", Surname = "Lalana" });
            _repo.Add(new cIndividual { ID = 1, Firstname = "Adam", Surname = "Lalana" });
            _repo.Add(new cIndividual { ID = 1, Firstname = "Simon", Surname = "Mignolet" });
            _repo.Add(new cIndividual { ID = 1, Firstname = "Robert", Surname = "Firmino" });

            _controller = Harness.GetController(_repo);
        }


        [TestMethod]
        public void indexShouldReturn5Records()
        {
            // Arrange (Given - see TestInitialise)

            // Act
            ViewResult result = (ViewResult)_controller.Index(null);
            int nIndividuals = ((IList<cIndividual>)result.Model).Count();

            // Assert
            Assert.AreEqual(5, nIndividuals);
        }

        [TestMethod]
        public void indexShouldReturnRecordsOrderedByLastNameAscAndFirstNameAsc()
        {
            // Arrange (Given - see TestInitialise)

            // Act
            ViewResult result = (ViewResult)_controller.Index(null);
            int nIndividuals = ((IList<cIndividual>)result.Model).Count();
            
            // Assert
            for (int i = 0; i < nIndividuals - 2; i++)
            {
                cIndividual indiv1 = ((IList<cIndividual>)result.Model).ElementAt<cIndividual>(i);
                cIndividual indiv2 = ((IList<cIndividual>)result.Model).ElementAt<cIndividual>(i + 1);
                Assert.IsTrue(String.Compare(indiv1.Surname, indiv2.Surname, false) <= 0);
                if (String.Compare(indiv1.Surname, indiv2.Surname, false) == 0)
                {
                    Assert.IsTrue(String.Compare(indiv1.Firstname, indiv2.Firstname, false) < 1);

                }

            }
        }
    }
}

/*
Controller Behaviour Analysis - Given (and), When, Then

Given a default result set of 10
and 10 or more records in the database
and no search criteria
When the user selects individual search
Then 10 results are retrieved
and the sort order is last name asc, first name asc, dob desc


 testShouldReturn10Records
 testShouldReturnRecordsSortedByLastNameFirstNameThenDOB
*/
