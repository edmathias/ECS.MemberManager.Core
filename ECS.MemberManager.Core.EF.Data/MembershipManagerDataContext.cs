using ECS.BizBricks.CRM.Core.EF.Domain;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;

namespace ECS.MemberManager.Core.EF.Data
{
    public class MembershipManagerDataContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<AddressOrganization> AddressOrganizations { get; set; }
        public DbSet<AddressPerson> AddressPersons { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<CategoryOfOrganization> CategoryOfOrganizations { get; set; } 
        public DbSet<Person> Persons { get; set; }
        public DbSet<CategoryOfPerson> CategoryOfPersons { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<TitleSuffix> TitleSuffixes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = ECSMemberManager");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressOrganization>().HasKey(ao => new { ao.AddressId, ao.OrganizationId });
            modelBuilder.Entity<AddressPerson>().HasKey(ap => new { ap.AddressId, ap.PersonId });
        }
    }
}
