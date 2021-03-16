using ECS.MemberManager.Core.EF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ECS.MemberManager.Core.EF.Data
{
    public class MembershipManagerDataContext : DbContext
    {
    
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactForSponsor> ContactForSponsors { get; set; }
        public DbSet<CategoryOfPerson> CategoryOfPersons { get; set; }
        public DbSet<CategoryOfOrganization> CategoryOfOrganizations { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<EMail> EMails { get; set; }
        public DbSet<EMailType> EMailTypes { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<EventDocument> EventDocuments { get; set; }
        public DbSet<EventMember> EventMembers { get; set; }
        public DbSet<MemberInfo> MemberInfo { get; set; }
        public DbSet<MembershipType> MembershipTypes { get; set; }
        public DbSet<MemberStatus> MemberStatuses { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<OrganizationType> OrganizationTypes { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<PaymentSource> PaymentSources { get; set; }
        public DbSet<PaymentType> PaymentTypes { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonalNote> PersonalNotes { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<PrivacyLevel> PrivacyLevels { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<TaskForEvent> TaskForEvents { get; set; }
        public DbSet<TermInOffice> TermInOffices { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<Image> Images { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true);
            var _configurationRoot    =  configurationBuilder.Build();
            var cnxnString = _configurationRoot["ConnectionString"];
            
            optionsBuilder.UseSqlServer(cnxnString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
 
        }
    }
}
