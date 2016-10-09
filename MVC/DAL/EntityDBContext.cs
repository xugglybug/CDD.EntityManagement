//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

using MVC.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace MVC.DAL
{

    public class EntitiesDBContext : DbContext
    {
        public EntitiesDBContext() : base("EntitiesDBContext")
        {
        }

        public DbSet<cOffice> Offices { get; set; }
        public DbSet<cMatter> Matters { get; set; }
        public DbSet<cEntity> Entities { get; set; }
        public DbSet<cIndividual> Individuals { get; set; }
        public DbSet<cOrganisation> Organisations { get; set; }
        public DbSet<cCDD_Details> CDD_Details { get; set; }
        public DbSet<cEntityRelationship> Relationships { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<cEntity>().ToTable("Entity");
            modelBuilder.Entity<cIndividual>().ToTable("Individual");
            modelBuilder.Entity<cOrganisation>().ToTable("Organisation");

            //-------------------------//
            // Explicitly defining the relationships between the tables
            //-------------------------//

            // Entities has an MTM relationship with Matters
            // 1 Entity can be related to many matters
            // 1 Matter can be related to many entities
            modelBuilder.Entity<cEntity>()
             .HasMany(c => c.Matters).WithMany(i => i.RelatedEntities)
             .Map(t => t.MapLeftKey("cEntityID")
                 .MapRightKey("cMatterID")
                 .ToTable("EntityMatter"));

            // Entities have a MTM relationship with each other
            // An Entity can have many realtionships with another entity
            // The existence of a payload (RelationshipType) means this is expressed as
            // 2 x 1 : Many relationships with an EntityRelationship table
            modelBuilder.Entity<cEntityRelationship>()
                .HasRequired(p => p.entityA)
                .WithMany(e => e.IsA)
                .HasForeignKey(p => p.entityA_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<cEntityRelationship>()
                .HasRequired(p => p.entityB)
                .WithMany(e => e.HasA)
                .HasForeignKey(p => p.entityB_ID)
                .WillCascadeOnDelete(false);

            // An Entity has a 1:0or1 relationship with CDD
            modelBuilder.Entity<cEntity>()
                .HasOptional(p => p.CDD_Details).WithRequired(p => p.Entity);


        }


    }
}