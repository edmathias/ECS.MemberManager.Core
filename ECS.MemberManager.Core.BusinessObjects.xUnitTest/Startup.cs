using Csla.Configuration;
using ECS.MemberManager.Core.DataAccess.EF;
using ECS.MemberManager.Core.DataAccess.Dal;
using Microsoft.Extensions.DependencyInjection;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCsla(); // necessary for the CSLA [Inject] attribute to work.

            services.AddTransient(typeof(IAddressDal), typeof(AddressDal));
            services.AddTransient(typeof(ICategoryOfOrganizationDal), typeof(CategoryOfOrganizationDal));
            services.AddTransient(typeof(ICategoryOfPersonDal), typeof(CategoryOfPersonDal));
            services.AddTransient(typeof(IContactForSponsorDal), typeof(ContactForSponsorDal));
            services.AddTransient(typeof(IDocumentTypeDal), typeof(DocumentTypeDal));
            services.AddTransient(typeof(IEMailDal), typeof(EMailDal));
            /*
            services.AddTransient(typeof(IEMailTypeDal),typeof(EMailTypeDal));
            services.AddTransient(typeof(IEventDal),typeof(EventDal));
            services.AddTransient(typeof(IEventDocumentDal),typeof(EventDocumentDal));
            services.AddTransient(typeof(IEventMemberDal),typeof(EventMemberDal));
            services.AddTransient(typeof(IMemberInfoDal),typeof(MemberInfoDal));
            services.AddTransient(typeof(IMembershipTypeDal),typeof(MembershipTypeDal));
            services.AddTransient(typeof(IMemberStatusDal),typeof(MemberStatusDal));
            services.AddTransient(typeof(IOfficeDal),typeof(OfficeDal));
            services.AddTransient(typeof(IOrganizationDal),typeof(OrganizationDal));
            services.AddTransient(typeof(IOrganizationTypeDal),typeof(OrganizationTypeDal));
            services.AddTransient(typeof(IPaymentDal),typeof(PaymentDal));
            services.AddTransient(typeof(IPaymentSourceDal),typeof(PaymentSourceDal));
            services.AddTransient(typeof(IPaymentTypeDal),typeof(PaymentTypeDal));
            services.AddTransient(typeof(IPersonDal),typeof(PersonDal));
            services.AddTransient(typeof(IPhoneDal),typeof(PhoneDal));
            services.AddTransient(typeof(IPrivacyLevelDal),typeof(PrivacyLevelDal));
            services.AddTransient(typeof(ITaskForEventDal),typeof(TaskForEventDal));
            services.AddTransient(typeof(ITermInOfficeDal),typeof(TermInOfficeDal));
            services.AddTransient(typeof(ITitleDal),typeof(TitleDal));
 */
        }
    }
}