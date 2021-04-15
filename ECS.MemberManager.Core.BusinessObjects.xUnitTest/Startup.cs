using Csla.Configuration;
using ECS.MemberManager.Core.DataAccess.EF;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCsla(); // necessary for the CSLA [Inject] attribute to work.

            services.AddTransient(typeof(IDal<Address>), typeof(AddressDal));
            services.AddTransient(typeof(IDal<CategoryOfOrganization>), typeof(CategoryOfOrganizationDal));
            services.AddTransient(typeof(IDal<CategoryOfPerson>), typeof(CategoryOfPersonDal));
            services.AddTransient(typeof(IDal<ContactForSponsor>), typeof(ContactForSponsorDal));
            services.AddTransient(typeof(IDal<DocumentType>), typeof(DocumentTypeDal));
            services.AddTransient(typeof(IDal<EMail>), typeof(EMailDal));
            services.AddTransient(typeof(IDal<Person>),typeof(PersonDal));
            services.AddTransient(typeof(IDal<Sponsor>),typeof(SponsorDal));
            services.AddTransient(typeof(IDal<EMailType>),typeof(EMailTypeDal));
            services.AddTransient(typeof(IDal<Event>),typeof(EventDal));
            services.AddTransient(typeof(IDal<EventDocument>),typeof(EventDocumentDal));
            services.AddTransient(typeof(IDal<EventMember>),typeof(EventMemberDal));
            services.AddTransient(typeof(IDal<MemberInfo>),typeof(MemberInfoDal));
            services.AddTransient(typeof(IDal<MembershipType>),typeof(MembershipTypeDal));
            services.AddTransient(typeof(IDal<MemberStatus>),typeof(MemberStatusDal));
            services.AddTransient(typeof(IDal<Office>),typeof(OfficeDal));
            services.AddTransient(typeof(IDal<Organization>),typeof(OrganizationDal));
            services.AddTransient(typeof(IDal<OrganizationType>),typeof(OrganizationTypeDal));
            services.AddTransient(typeof(IDal<Payment>),typeof(PaymentDal));
            services.AddTransient(typeof(IDal<PaymentSource>),typeof(PaymentSourceDal));
            services.AddTransient(typeof(IDal<PaymentType>),typeof(PaymentTypeDal));
            services.AddTransient(typeof(IDal<PersonalNote>),typeof(PersonalNoteDal));
            /*
            services.AddTransient(typeof(IPhoneDal),typeof(PhoneDal));
            services.AddTransient(typeof(IPrivacyLevelDal),typeof(PrivacyLevelDal));
            services.AddTransient(typeof(ITaskForEventDal),typeof(TaskForEventDal));
            services.AddTransient(typeof(ITermInOfficeDal),typeof(TermInOfficeDal));
            services.AddTransient(typeof(ITitleDal),typeof(TitleDal));
 */
        }
    }
}