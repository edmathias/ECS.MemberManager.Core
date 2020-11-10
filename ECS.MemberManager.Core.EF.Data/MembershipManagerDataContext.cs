using System.Collections.Generic;
using System.Linq;
using ECS.BizBricks.CRM.Core.EF.Domain;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ECS.MemberManager.Core.EF.Data
{
    public class MembershipManagerDataContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressOrganization> AddressOrganizations { get; set; }
        public DbSet<AddressPerson> AddressPersons { get; set; }
        public DbSet<CategoryOfOrganization> CategoryOfOrganizations { get; set; }
        public DbSet<CategoryOfPerson> CategoryOfPersons { get; set; }
        public DbSet<CategoryOrganization> CategoryOrganizations { get; set; }
        public DbSet<CategoryPerson> CategoryPersons { get; set; }
        public DbSet<ContactForSponsor> ContactForSponsors { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<OrganizationPerson> OrganizationPersons { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<TitleSuffix> TitleSuffixes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ECSMemberManager");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressOrganization>()
                .HasKey(ao => new {ao.AddressId, ao.OrganizationId});
            modelBuilder.Entity<AddressPerson>().HasKey(ap => new {ap.AddressId, ap.PersonId});
            modelBuilder.Entity<CategoryOrganization>()
                .HasKey(co => new {co.OrganizationId, co.CategoryOfOrganizationId});
            modelBuilder.Entity<CategoryPerson>()
                .HasKey(cp => new {cp.PersonId, cp.CategoryOfPersonId});
            modelBuilder.Entity<OrganizationPerson>()
                .HasKey(op => new {op.OrganizationId, op.PersonId});

  
        }
    }
}
