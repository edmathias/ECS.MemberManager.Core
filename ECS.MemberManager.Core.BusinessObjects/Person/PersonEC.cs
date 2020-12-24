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
    public class PersonEC : BusinessBase<PersonEC>
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

        public static readonly PropertyInfo<CategoryOfPersonECL> CategoryOfPersonListProperty = RegisterProperty<CategoryOfPersonECL>(p => p.CategoryOfPersonList);
        public CategoryOfPersonECL CategoryOfPersonList
        {
            get => GetProperty(CategoryOfPersonListProperty);
            set => SetProperty(CategoryOfPersonListProperty, value);
        }

        public static readonly PropertyInfo<EventECL> EventsProperty = RegisterProperty<EventECL>(p => p.Events);
        public EventECL Events
        {
            get => GetProperty(EventsProperty);
            set => SetProperty(EventsProperty, value);
        }

        public static readonly PropertyInfo<AddressECL> AddressesProperty = RegisterProperty<AddressECL>(p => p.Addresses);
        public AddressECL Addresses
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

        public static readonly PropertyInfo<AddressECL> AddressListProperty = RegisterProperty<AddressECL>(p => p.AddressList);
        public AddressECL AddressList
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

        public static async Task<PersonEC> NewPerson()
        {
            return await DataPortal.CreateChildAsync<PersonEC>();
        }

        public static async Task<PersonEC> GetPerson(Person childData)
        {
            return await DataPortal.FetchChildAsync<PersonEC>(childData);
        }

        public static async Task DeletePerson(int id)
        {
            await DataPortal.DeleteAsync<PersonEC>(id);
        }
        
        #endregion

        #region Data Access

        [Create]
        private async void Create()
        {
            CategoryOfPersonList = await CategoryOfPersonECL.NewCategoryOfPersonList();
            Events = await EventECL.NewEventList();
            
            
            BusinessRules.CheckRules();
        }
        
        [FetchChild]
        private async void Fetch(Person childData)
        {
            using (BypassPropertyChecks)
            {
                Id = childData.Id;
                Code = childData.Code;
                Notes = childData.Notes;
                BirthDate = childData.BirthDate;
                FirstName = childData.FirstName;
                MiddleName = childData.MiddleName;
                LastName = childData.LastName;
                DateOfFirstContact = childData.DateOfFirstContact;
                LastUpdatedDate = childData.LastUpdatedDate;
                LastUpdatedBy = childData.LastUpdatedBy;
                
                // TODO : Relationships
                CategoryOfPersonList = await CategoryOfPersonECL.GetCategoryOfPersonList(childData.CategoryOfPersons);
                Events = await EventECL.GetEventList(childData.Events);
                Addresses = await AddressECL.GetAddressList(childData.Addresses);
                PhoneList = await PhoneECL.GetPhoneList(childData.Phones);
                //   Title = new Title();

            }
        }

        [InsertChild]
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

        [UpdateChild]
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

        [DeleteSelfChild]
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