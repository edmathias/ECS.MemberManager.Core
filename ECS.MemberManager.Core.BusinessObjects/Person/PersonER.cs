using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Csla;
using ECS.MemberManager.Core.DataAccess;
using ECS.MemberManager.Core.DataAccess.Dal;
using ECS.MemberManager.Core.EF.Domain;

namespace ECS.MemberManager.Core.BusinessObjects
{
    [Serializable]
    public class PersonER : BusinessBase<PersonER>
    {
        #region Business Methods

        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(p => p.Id);
        public int Id
        {
            get => GetProperty(IdProperty);
            set => SetProperty(IdProperty, value);
        }

        public static readonly PropertyInfo<string> LastNameProperty = RegisterProperty<string>(p => p.LastName);
        [Required,MaxLength(50)]
        public string LastName
        {
            get => GetProperty(LastNameProperty);
            set => SetProperty(LastNameProperty, value);
        }

        public static readonly PropertyInfo<string> MiddleNameProperty = RegisterProperty<string>(p => p.MiddleName);
        [MaxLength(50)]
        public string MiddleName
        {
            get => GetProperty(MiddleNameProperty);
            set => SetProperty(MiddleNameProperty, value);
        }
        
        public static readonly PropertyInfo<string> FirstNameProperty = RegisterProperty<string>(p => p.FirstName);
        [MaxLength(50)]
        public string FirstName
        {
            get => GetProperty(FirstNameProperty);
            set => SetProperty(FirstNameProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> DateOfFirstContactProperty = RegisterProperty<SmartDate>(p => p.DateOfFirstContact);
        public SmartDate DateOfFirstContact
        {
            get => GetProperty(DateOfFirstContactProperty);
            set => SetProperty(DateOfFirstContactProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> BirthDateProperty = RegisterProperty<SmartDate>(p => p.BirthDate);
        public SmartDate BirthDate
        {
            get => GetProperty(BirthDateProperty);
            set => SetProperty(BirthDateProperty, value);
        }

        public static readonly PropertyInfo<string> LastUpdatedByProperty = RegisterProperty<string>(p => p.LastUpdatedBy);
        [Required,MaxLength(255)]
        public string LastUpdatedBy
        {
            get => GetProperty(LastUpdatedByProperty);
            set => SetProperty(LastUpdatedByProperty, value);
        }

        public static readonly PropertyInfo<SmartDate> LastUpdatedDateProperty = RegisterProperty<SmartDate>(p => p.LastUpdatedDate);
        [Required]
        public SmartDate LastUpdatedDate
        {
            get => GetProperty(LastUpdatedDateProperty);
            set => SetProperty(LastUpdatedDateProperty, value);
        }

        public static readonly PropertyInfo<string> CodeProperty = RegisterProperty<string>(p => p.Code);
        [MaxLength(5)]
        public string Code
        {
            get => GetProperty(CodeProperty);
            set => SetProperty(CodeProperty, value);
        }

        public static readonly PropertyInfo<string> NotesProperty = RegisterProperty<string>(p => p.Notes);
        public string Notes
        {
            get => GetProperty(NotesProperty);
            set => SetProperty(NotesProperty, value);
        }
        
        
        // TODO : Relationships
        public static readonly PropertyInfo<TitleER> TitleProperty = RegisterProperty<TitleER>(p => p.Title);
        public TitleER Title
        {
            get => GetProperty(TitleProperty);
            set => SetProperty(TitleProperty, value);
        }

        public static readonly PropertyInfo<CategoryOfPersonERL> CategoryOfPersonListProperty = RegisterProperty<CategoryOfPersonERL>(p => p.CategoryOfPersonList);
        public CategoryOfPersonERL CategoryOfPersonList
        {
            get => GetProperty(CategoryOfPersonListProperty);
            set => SetProperty(CategoryOfPersonListProperty, value);
        }

        public static readonly PropertyInfo<EventERL> EventsProperty = RegisterProperty<EventERL>(p => p.Events);
        public EventERL Events
        {
            get => GetProperty(EventsProperty);
            set => SetProperty(EventsProperty, value);
        }

        public static readonly PropertyInfo<AddressERL> AddressesProperty = RegisterProperty<AddressERL>(p => p.Addresses);
        public AddressERL Addresses
        {
            get => GetProperty(AddressesProperty);
            set => SetProperty(AddressesProperty, value);
        }

        public static readonly PropertyInfo<PhoneECL> PhoneListProperty = RegisterProperty<PhoneECL>(p => p.PhoneList);
        public PhoneECL PhoneList
        {
            get => GetProperty(PhoneListProperty);
            set => SetProperty(PhoneListProperty, value);
        }

        public static readonly PropertyInfo<AddressERL> AddressListProperty = RegisterProperty<AddressERL>(p => p.AddressList);
        public AddressERL AddressList
        {
            get => GetProperty(AddressListProperty);
            set => SetProperty(AddressListProperty, value);
        }

//        Addresses = new List<Address>(),
//        Phones = new List<Phone>(),

        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();

            // TODO: add business rules
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static void AddObjectAuthorizationRules()
        {
            // TODO: add object-level authorization rules
        }

        #endregion

        #region Factory Methods

        public static async Task<PersonER> NewPerson()
        {
            return await DataPortal.CreateAsync<PersonER>();
        }

        public static async Task<PersonER> GetPerson(int id)
        {
            return await DataPortal.FetchAsync<PersonER>(id);
        }

        public static async Task DeletePerson(int id)
        {
            await DataPortal.DeleteAsync<PersonER>(id);
        }
        
        #endregion

        #region Data Access

        [Create]
        private async void Create()
        {
            CategoryOfPersonList = await CategoryOfPersonERL.NewCategoryOfPersonList();
            Events = await EventERL.NewEventList();
            
            
            BusinessRules.CheckRules();
        }
        
        [Fetch]
        private async void Fetch(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            var data = dal.Fetch(id);
            using (BypassPropertyChecks)
            {
                Id = data.Id;
                Code = data.Code;
                Notes = data.Notes;
                BirthDate = data.BirthDate;
                FirstName = data.FirstName;
                MiddleName = data.MiddleName;
                LastName = data.LastName;
                DateOfFirstContact = data.DateOfFirstContact;
                LastUpdatedDate = data.LastUpdatedDate;
                LastUpdatedBy = data.LastUpdatedBy;
                
                // TODO : Relationships
                CategoryOfPersonList = await CategoryOfPersonERL.GetCategoryOfPersonList(data.CategoryOfPersons);
                Events = await EventERL.GetEventList(data.Events);
                Addresses = await AddressERL.GetAddressList(data.Addresses);
                PhoneList = await PhoneECL.GetPhoneList(data.Phones);
                //   Title = new Title();

            }
        }

        [Insert]
        private void Insert()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            using (BypassPropertyChecks)
            {
                var personToInsert = new Person()
                {
                    BirthDate = BirthDate,
                    Code = Code,
                    DateOfFirstContact = DateOfFirstContact,
                    FirstName = FirstName,
                    LastName = LastName,
                    MiddleName = MiddleName,
                    Notes = Notes,
                    LastUpdatedBy = LastUpdatedBy,
                    LastUpdatedDate = LastUpdatedDate,
                };
                
                    // TODO : Relationships
                CreatePersonRelationships(personToInsert);
                //     Phones = new List<Phone>(),
               //     Title = new Title()

                Id = dal.Insert(personToInsert);
            }
        }

        private void CreatePersonRelationships(Person personToInsert)
        {
            CreateCategoryOfPersonRelationship(personToInsert);

            CreateEventRelationship(personToInsert);

            CreateAddressRelationship(personToInsert);
            
            CreatePhoneRelationship(personToInsert);
        }

        private void CreatePhoneRelationship(Person personToInsert)
        {
        }
        private void CreateAddressRelationship(Person personToInsert)
        {
            foreach (var addressToProcess in Addresses)
            {
                var addressToInsert = new Address()
                {
                    Id = addressToProcess.Id,
                    Address1 = addressToProcess.Address1,
                    Address2 = addressToProcess.Address2,
                    City = addressToProcess.City,
                    State = addressToProcess.State,
                    PostCode = addressToProcess.PostCode,
                    Notes = addressToProcess.Notes,
                    LastUpdatedBy = addressToProcess.LastUpdatedBy,
                    LastUpdatedDate = addressToProcess.LastUpdatedDate
                };
                personToInsert.Addresses.Add(addressToInsert);
            }
        }

        private void CreateEventRelationship(Person personToInsert)
        {
            foreach (var eventToProcess in Events)
            {
                var eventToInsert = new Event()
                {
                    Id = eventToProcess.Id,
                    Description = eventToProcess.Description,
                    Notes = eventToProcess.Notes,
                    EventName = eventToProcess.EventName,
                    NextDate = eventToProcess.NextDate,
                    IsOneTime = eventToProcess.IsOneTime,
                    LastUpdatedBy = eventToProcess.LastUpdatedBy,
                    LastUpdatedDate = eventToProcess.LastUpdatedDate
                };
                personToInsert.Events.Add(eventToInsert);
            }
        }

        private void CreateCategoryOfPersonRelationship(Person personToInsert)
        {
            foreach (var categoryOfPerson in CategoryOfPersonList)
            {
                var categoryToInsert = new CategoryOfPerson()
                {
                    Id = categoryOfPerson.Id,
                    Category = categoryOfPerson.Category,
                    DisplayOrder = categoryOfPerson.DisplayOrder
                };
                personToInsert.CategoryOfPersons.Add(categoryToInsert);
            }
        }

        [Update]
        private void Update()
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();
            using (BypassPropertyChecks)
            {
                // format dto and update dal 
                var personToUpdate = dal.Fetch(Id);
                
                personToUpdate.BirthDate = BirthDate;
                personToUpdate.Code = Code;
                personToUpdate.DateOfFirstContact = DateOfFirstContact;
                personToUpdate.FirstName = FirstName;
                personToUpdate.LastName = LastName;
                personToUpdate.MiddleName = MiddleName;
                personToUpdate.Notes = Notes;
                personToUpdate.LastUpdatedBy = LastUpdatedBy;
                personToUpdate.LastUpdatedDate = LastUpdatedDate;

                // TODO : Relationships
                personToUpdate.CategoryOfPersons = new List<CategoryOfPerson>();
                personToUpdate.Events = new List<Event>();
                personToUpdate.Addresses = new List<Address>();
                personToUpdate.Phones = new List<Phone>();
                personToUpdate.Title = new Title();
                
                dal.Update(personToUpdate);
            }
        }

        [DeleteSelf]
        private void DeleteSelf()
        {
            Delete(this.Id);
        }

        [Delete]
        private void Delete(int id)
        {
            using IDalManager dalManager = DalFactory.GetManager();
            var dal = dalManager.GetProvider<IPersonDal>();

            dal.Delete(id);
        }


        #endregion

 
    }
}