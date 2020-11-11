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
        public MembershipManagerDataContext()
        {
            
        }

        public MembershipManagerDataContext(DbContextOptions<MembershipManagerDataContext> options) : base(options)
        {
            
        }

        public DbSet<Address> Addresses { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<CategoryOfPerson> CategoryOfPersons { get; set; }
        public DbSet<CategoryOfOrganization> CategoryOfOrganizations { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<TitleSuffix> TitleSuffixes { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<MemberInfo> MemberInfo { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<MemberStatus> MemberStatuses { get; set; }
 /*
        public DbSet<ContactForSponsor> ContactForSponsors { get; set; }
        public DbSet<Payment> Payments { get; set; }
*/
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ECSMemberManager");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
        }
    }
}
