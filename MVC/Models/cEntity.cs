using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MVC.Models
{
    //JYL, GYL, BVL, CML, HKL, LXL
    public class cOffice {
        public int ID { get; set; }
        public string OfficeCode { get; set; }
        public string OfficeDescription { get; set; }
        
    };

    public enum eCDDStatus { Complete, PendingDocuments, Cancelled, Referred };

    public enum eRelType { Shareholder, Director, Partner, Spouse, Trustee };

    public enum eOrgType { Banking, Investment, insurance, trust, Funds, Accountants, Lawyers, EstateAgents, NPO };

    public class cMatter
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Display(Name = "Matter Number")]
        public string ID { get; set; }
        public string Description { get; set; }
        
        public int OfficeID { get; set; }
        public string Team { get; set; }
        public string ResponsiblePartner { get; set; }

        public string MatterManager { get; set; }
        public string Sec_PA { get; set; }

        public bool Advice_Provided_Without_CDD { get; set; }


        // Navigation Fields
        public virtual cOffice Office { get; set; }


        //-------------------------//
        // MTM between Matters and Entities
        //-------------------------//
        public virtual ICollection<cEntity> RelatedEntities { get; set; }
        
    }

    

    public abstract class cEntity
    {
        [Key]
        public int ID { get; set; }

        //-------------------------//
        // Navigation Fields
        //-------------------------//
        
        // 1 to 1 between Entity ans CDD_Details
        public virtual cCDD_Details CDD_Details { get; set; }

        //-------------------------//
        // MTM between Entities and Matters
        //-------------------------//
        public virtual ICollection<cMatter> Matters { get; set; }


        //-------------------------//
        // MTM between Entities and other Entities !!
        //-------------------------//
        public virtual ICollection<cEntityRelationship> IsA { get; set; }

        public virtual ICollection<cEntityRelationship> HasA { get; set; }
    }


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

    public class cCDD_Details
    {
        [Key]
        public int ID { get; set; }
        

        [Display(Name = "CDD Contact - Name")]
        public string CDDContact_Name { get; set; }

        [Display(Name = "CDD Contact - Telephone Number")]
        public string CDDContact_TelNo { get; set; }

        [Display(Name = "CDD Contact - Email")]
        [DataType(DataType.EmailAddress)]
        public string CDDContact_Email { get; set; }

        [Display(Name = "Compliance - Sign Off - Name")]
        public string ComplianceSignOff_Name { get; set; }

        [Display(Name = "Compliance - Sign Off - Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ComplianceSignOff_Date { get; set; }

        [Display(Name = "CDD Status")]
        public eCDDStatus CDDStatus { get; set; }


        //-------------------------//
        // Each CDD Details record is associated with 1 Entity
        //-------------------------//
        public virtual cEntity Entity { get; set; }
    }










    /*public class EntityInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<EntitiesDBContext>
    {
        protected override void Seed(EntitiesDBContext context)
        {





            
            var entitymatters = new List<EntityMatter>
            {
            new EntityMatter{ cEntityID=1, cMatterID ="123456.00001"},
            new EntityMatter{ cEntityID=1, cMatterID ="123456.00002"},
            new EntityMatter{ cEntityID=2, cMatterID ="123456.00001"},
            new EntityMatter{ cEntityID=3, cMatterID ="123456.00002"},
            new EntityMatter{ cEntityID=4, cMatterID ="123456.00002"}
            };
            entitymatters.ForEach(s => context.EntityMatters.Add(s));
            context.SaveChanges();
            
        }
    }
    */

}