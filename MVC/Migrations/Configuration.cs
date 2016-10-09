namespace MVC.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using System.Collections.Generic;

    using MVC.DAL;
    using MVC.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<EntitiesDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntitiesDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var offices = new List<cOffice>
            {
                new Models.cOffice { OfficeCode="JYL", OfficeDescription="Jersey" },
                new Models.cOffice { OfficeCode="GYL", OfficeDescription="Guernsey"},
                new Models.cOffice { OfficeCode="BVL", OfficeDescription="BVI"},
                new Models.cOffice { OfficeCode="CML", OfficeDescription="Cayman"},
                new Models.cOffice { OfficeCode="HKL", OfficeDescription="Hong Kong"},
                new Models.cOffice { OfficeCode="LXL", OfficeDescription="Luxembourg"}
            };
            offices.ForEach(m => context.Offices.AddOrUpdate(m));
            context.SaveChanges();


            var matters = new List<cMatter>
            {
            new cMatter{ ID = "123456.00001", Description="Test1", MatterManager="AAA", OfficeID = offices.Single( s => s.OfficeCode == "JYL").ID, ResponsiblePartner = "AAA", Sec_PA = "ZZZ", Team="BTLG1", Advice_Provided_Without_CDD = true},
            new cMatter{ ID = "123456.00002", Description="Test2", MatterManager="BBB", OfficeID = offices.Single( s => s.OfficeCode == "GYL").ID, ResponsiblePartner = "AAA", Sec_PA = "YYY", Team="BTLG2", Advice_Provided_Without_CDD = false},
            new cMatter{ ID = "111111.00001", Description="Test3", MatterManager="CCC", OfficeID = offices.Single( s => s.OfficeCode == "BVL").ID, ResponsiblePartner = "BBB", Sec_PA = "XXX", Team="BTLG3", Advice_Provided_Without_CDD = true},
            new cMatter{ ID = "111111.00002", Description="Test4", MatterManager="AAA", OfficeID = offices.Single( s => s.OfficeCode == "CML").ID, ResponsiblePartner = "BBB", Sec_PA = "XXX", Team="BTLG4", Advice_Provided_Without_CDD = false},
            new cMatter{ ID = "222222.00001", Description="Test5", MatterManager="BBB", OfficeID = offices.Single( s => s.OfficeCode == "HKL").ID, ResponsiblePartner = "CCC", Sec_PA = "YYY", Team="BTLG1", Advice_Provided_Without_CDD = false},
            new cMatter{ ID = "222222.00002", Description="Test6", MatterManager="CCC", OfficeID = offices.Single( s => s.OfficeCode == "LXL").ID, ResponsiblePartner = "BBB", Sec_PA = "ZZZ", Team="BTLG2", Advice_Provided_Without_CDD = true}
            };
            matters.ForEach(s => context.Matters.AddOrUpdate(s));
            context.SaveChanges();


            var cddRecords = new List<cCDD_Details>
            {
            new cCDD_Details{CDDContact_Name="a", CDDContact_TelNo="1", CDDContact_Email="a@a.com", CDDStatus = eCDDStatus.Cancelled, ComplianceSignOff_Date = DateTime.Parse("2016-01-01"), ComplianceSignOff_Name = "TOTA" },
            new cCDD_Details{CDDContact_Name="b", CDDContact_TelNo="2", CDDContact_Email="b@b.com", CDDStatus = eCDDStatus.Complete, ComplianceSignOff_Date = DateTime.Parse("2016-02-02"), ComplianceSignOff_Name = "TOTB" },
            new cCDD_Details{CDDContact_Name="c", CDDContact_TelNo="3", CDDContact_Email="c@c.com", CDDStatus = eCDDStatus.PendingDocuments, ComplianceSignOff_Date = DateTime.Parse("2016-03-03"), ComplianceSignOff_Name = "TOTC" },
            new cCDD_Details{CDDContact_Name="d", CDDContact_TelNo="4", CDDContact_Email="d@d.com", CDDStatus = eCDDStatus.Referred, ComplianceSignOff_Date = DateTime.Parse("2016-03-03"), ComplianceSignOff_Name = "TOTD" }
            };


            
            var individuals = new List<cIndividual>
            {
                 new cIndividual { ID = 1, Firstname = "Abraham", Middlename = "Gerald", Surname = "Lincoln", Previousname = "", DateOfBirth = DateTime.Parse("1809-02-12"), Matters = new List<cMatter>() }
                ,new cIndividual { ID = 2, Firstname = "Barack", Middlename = "Hussein", Surname = "Obama", Previousname = "", DateOfBirth = DateTime.Parse("1961-08-04"), Matters = new List<cMatter>() }
                ,new cIndividual { ID = 3, Firstname = "Bill", Middlename = "", Surname = "Gates", Previousname = "c", DateOfBirth = DateTime.Parse("2016-03-03"), Matters = new List<cMatter>() }
                ,new cIndividual { ID = 4, Firstname = "Amancio", Middlename = "", Surname = "Ortega", Previousname = "d", DateOfBirth = DateTime.Parse("2016-04-04"), Matters = new List<cMatter>() }
                ,new cIndividual { ID = 5, Firstname = "Jeff", Middlename = "", Surname = "Bezos", Previousname = "d", DateOfBirth = DateTime.Parse("2016-04-04"), Matters = new List<cMatter>() }
                ,new cIndividual { ID = 6, Firstname = "Warren", Middlename = "", Surname = "Buffett", Previousname = "d", DateOfBirth = DateTime.Parse("2016-04-04"), Matters = new List<cMatter>() }
                ,new cIndividual { ID = 7, Firstname = "Mark", Middlename = "", Surname = "Zuckerberg", Previousname = "d", DateOfBirth = DateTime.Parse("2016-04-04"), Matters = new List<cMatter>() }
                ,new cIndividual { ID = 8, Firstname = "Larry", Middlename = "", Surname = "Elison", Previousname = "d", DateOfBirth = DateTime.Parse("2016-04-04"), Matters = new List<cMatter>() }
                ,new cIndividual { ID = 9, Firstname = "Carlos", Middlename = "", Surname = "Slim", Previousname = "d", DateOfBirth = DateTime.Parse("2016-04-04"), Matters = new List<cMatter>() }
                ,new cIndividual { ID = 10, Firstname = "Micheal", Middlename = "", Surname = "Bloomberg", Previousname = "d", DateOfBirth = DateTime.Parse("2016-04-04"), Matters = new List<cMatter>() }
            };

            for(int i = 11; i <=100; i++)
            {

                individuals.Add(new cIndividual { ID = i, Firstname = GetRandomString(), Middlename = GetRandomString(), Surname = GetRandomString(), Previousname = GetRandomString(), DateOfBirth = GetRandomDate(i), Matters = new List<cMatter>() });
            }

            individuals.ForEach(s => context.Entities.AddOrUpdate(s));
            context.SaveChanges();

            AddOrUpdateMatter(context, 1, "123456.00001");
            AddOrUpdateMatter(context, 1, "123456.00002");
            AddOrUpdateMatter(context, 2, "123456.00001");
            AddOrUpdateMatter(context, 2, "123456.00002");
            //AddOrUpdateMatter(context, 4, "123456.00002");
            
            individuals.ForEach(s => context.Entities.AddOrUpdate(s));
            context.SaveChanges();


            var orgs = new List<cOrganisation>
            {
                new cOrganisation { ID = 200, tradingName = "Google", registeredName = "Google", keyClient= true, Matters = new List<cMatter>() }
                ,new cOrganisation { ID = 201, tradingName = "John Lewis", registeredName = "bbb", keyClient= false, Matters = new List<cMatter>() }
                ,new cOrganisation { ID = 202, tradingName = "Microsoft", registeredName = "ccc", keyClient= true, Matters = new List<cMatter>() }
                ,new cOrganisation { ID = 203, tradingName = "Accenture", registeredName = "ddd", keyClient= false, Matters = new List<cMatter>() }
                ,new cOrganisation { ID = 204, tradingName = "Jaguar Land Rover", registeredName = "ddd", keyClient= false, Matters = new List<cMatter>() }
                ,new cOrganisation { ID = 205, tradingName = "JP Morgan", registeredName = "ddd", keyClient= false, Matters = new List<cMatter>() }
                ,new cOrganisation { ID = 206, tradingName = "Three", registeredName = "ddd", keyClient= false, Matters = new List<cMatter>() }
                ,new cOrganisation { ID = 207, tradingName = "Cisco Systems", registeredName = "ddd", keyClient= false, Matters = new List<cMatter>() }
                ,new cOrganisation { ID = 208, tradingName = "Deloitte", registeredName = "ddd", keyClient= false, Matters = new List<cMatter>() }
                ,new cOrganisation { ID = 209, tradingName = "PWC", registeredName = "ddd", keyClient= false, Matters = new List<cMatter>() }
            };

            for (int i = 210; i <= 300; i++)
            {

                orgs.Add(new cOrganisation { ID = i, tradingName = GetRandomString(), registeredName = GetRandomString(), keyClient = false, Matters = new List<cMatter>() });
            }

            //AddOrUpdateMatter(context, 15, "123456.00001");
            //AddOrUpdateMatter(context, 16, "123456.00002");
            //AddOrUpdateMatter(context, 7, "123456.00001");
            //AddOrUpdateMatter(context, 8, "123456.00002");
            //AddOrUpdateMatter(context, 8, "123456.00002");

            orgs.ForEach(s => context.Entities.AddOrUpdate(s));
            context.SaveChanges();


            //var er = new List<cEntityRelationship>
            //{
            //    new cEntityRelationship { ID=1, relType=eRelType.Director },
            //    new cEntityRelationship { ID=2, relType=eRelType.Partner}
            //};
        }


  

        void AddOrUpdateMatter(EntitiesDBContext context, int entityID, string matterNumber)
        {
            var crs = context.Entities.SingleOrDefault(c => c.ID == entityID);
            var inst = crs.Matters.SingleOrDefault(i => i.ID == matterNumber);
            if (inst == null)
                crs.Matters.Add(context.Matters.Single(i => i.ID == matterNumber));
        }

        private string GetRandomString()
        {
            string s = System.IO.Path.GetRandomFileName();
            return s.Replace(".", ""); // Remove period.
        }

        
        private DateTime GetRandomDate(int? i)
        {
            DateTime start = new DateTime(1950, 1, 1);
            int range = (DateTime.Today - start).Days;

            if (i == null) i = (int)DateTime.Now.Ticks;
            Random gen = new Random((int)i);
            return start.AddDays(gen.Next(range));
        }
    }
}
