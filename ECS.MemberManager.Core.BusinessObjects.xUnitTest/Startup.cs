using System;
using System.IO;
using System.Security.Cryptography;
using System.Xml;
using Csla.Configuration;
using ECS.MemberManager.Core.DataAccess.ADO;
using ECS.MemberManager.Core.DataAccess.EF;
using ECS.MemberManager.Core.DataAccess.Mock;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ECS.MemberManager.Core.BusinessObjects.xUnitTest
{
    public class Startup
    {
        private string qualifiedName = default;
        
        public Startup()
        {
            // This is a bit of a hack.  It will allow the unit test project to assign the correct
            // data access layer to the test process merely by changing the appsettings.json to one of 
            // the following:
            //
            // "TestLibrary" : "EF" ...for the ECS.MemberManager.Core.DataAccess.EF
            // "TestLibrary" : "ADO" ...for the ECS.MemberManager.Core.DataAccess.ADO
            // "TestLibrary" : "Mock" ...for the ECS.MemberManager.Core.DataAccess.Mock
            //
            // The code will read the entry in appsettings, and create the qualified name for the DAL library
            // by loading the AddressDal member from the appropriate assembly. Any dal member would work just
            // as well.
            //
            // When to service is created, it substitutes the appropriate DAL name into the {0} string parameter
            // that was replaced in qualifiedName in each of the switch cases.
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            var _config = builder.Build();
            var testLibrary = _config.GetValue<string>("TestLibrary");
            
            switch (testLibrary) 
            {
               case "ADO":
                   var testObject1 = new DataAccess.ADO.AddressDal();
                   var typeName1 = testObject1.GetType().AssemblyQualifiedName;
                   qualifiedName = typeName1.Replace("AddressDal", "{0}");
                   break;
               case "EF":
                   var testObject2 = new DataAccess.EF.AddressDal();
                   var typeName2 = testObject2.GetType().AssemblyQualifiedName;
                   qualifiedName = typeName2.Replace("AddressDal", "{0}");
                   break;  
               case "Mock":
                   var testObject3 = new DataAccess.Mock.AddressDal();
                   var typeName3 = testObject3.GetType().AssemblyQualifiedName;
                   qualifiedName = typeName3.Replace("AddressDal", "{0}");
                   break;        
            }
          
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCsla(); // necessary for the CSLA [Inject] attribute to work.
             
            services.AddTransient(typeof(IDal<Address>), 
                Type.GetType(String.Format(qualifiedName,"AddressDal")));
            services.AddTransient(typeof(IDal<CategoryOfOrganization>), 
                Type.GetType(String.Format(qualifiedName,"CategoryOfOrganizationDal")));
            services.AddTransient(typeof(IDal<CategoryOfPerson>), 
                Type.GetType(String.Format(qualifiedName,"CategoryOfPersonDal")));
            services.AddTransient(typeof(IDal<ContactForSponsor>), 
                Type.GetType(String.Format(qualifiedName,"ContactForSponsorDal")));
            services.AddTransient(typeof(IDal<DocumentType>), 
                Type.GetType(String.Format(qualifiedName,"DocumentTypeDal")));
            services.AddTransient(typeof(IDal<EMail>), 
                Type.GetType(String.Format(qualifiedName,"EMailDal")));
            services.AddTransient(typeof(IDal<Person>),
                Type.GetType(String.Format(qualifiedName,"PersonDal")));
            services.AddTransient(typeof(IDal<Sponsor>),
                Type.GetType(String.Format(qualifiedName,"SponsorDal")));
            services.AddTransient(typeof(IDal<EMailType>),
                Type.GetType(String.Format(qualifiedName,"EMailTypeDal")));
            services.AddTransient(typeof(IDal<Event>),
                Type.GetType(String.Format(qualifiedName,"EventDal")));
            services.AddTransient(typeof(IDal<EventDocument>),
                Type.GetType(String.Format(qualifiedName,"EventDocumentDal")));
            services.AddTransient(typeof(IDal<EventMember>),
                Type.GetType(String.Format(qualifiedName,"EventMemberDal")));
            services.AddTransient(typeof(IDal<MemberInfo>),
                Type.GetType(String.Format(qualifiedName,"MemberInfoDal")));
            services.AddTransient(typeof(IDal<MembershipType>),
                Type.GetType(String.Format(qualifiedName,"MembershipTypeDal")));
            services.AddTransient(typeof(IDal<MemberStatus>),
                Type.GetType(String.Format(qualifiedName,"MemberStatusDal")));
            services.AddTransient(typeof(IDal<Office>),
                Type.GetType(String.Format(qualifiedName,"OfficeDal")));
            services.AddTransient(typeof(IDal<Organization>),
                Type.GetType(String.Format(qualifiedName,"OrganizationDal")));
            services.AddTransient(typeof(IDal<OrganizationType>),
                Type.GetType(String.Format(qualifiedName,"OrganizationTypeDal")));
            services.AddTransient(typeof(IDal<Payment>),
                Type.GetType(String.Format(qualifiedName,"PaymentDal")));
            services.AddTransient(typeof(IDal<PaymentSource>),
                Type.GetType(String.Format(qualifiedName,"PaymentSourceDal")));
            services.AddTransient(typeof(IDal<PaymentType>),
                Type.GetType(String.Format(qualifiedName,"PaymentTypeDal")));
            services.AddTransient(typeof(IDal<PersonalNote>),
                Type.GetType(String.Format(qualifiedName,"PersonalNoteDal")));
            services.AddTransient(typeof(IDal<Phone>),
                Type.GetType(String.Format(qualifiedName,"PhoneDal")));
            services.AddTransient(typeof(IDal<PrivacyLevel>),
                Type.GetType(String.Format(qualifiedName,"PrivacyLevelDal")));
            services.AddTransient(typeof(IDal<TaskForEvent>),
                Type.GetType(String.Format(qualifiedName,"TaskForEventDal")));
            services.AddTransient(typeof(IDal<TermInOffice>),
                Type.GetType(String.Format(qualifiedName,"TermInOfficeDal")));
            services.AddTransient(typeof(IDal<Title>),
                Type.GetType(String.Format(qualifiedName,"TitleDal")));
        }
    }
}