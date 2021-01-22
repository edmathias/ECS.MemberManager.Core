create table Addresses
(
    Id              int identity
        constraint PK_Addresses
        primary key,
    Address1        nvarchar(35) not null,
    Address2        nvarchar(35),
    City            nvarchar(50) not null,
    State           nvarchar(2)  not null,
    PostCode        nvarchar(9)  not null,
    Notes           nvarchar(max),
    LastUpdatedBy   nvarchar(255),
    LastUpdatedDate datetime2    not null,
    RowVersion      timestamp    null
)
    go

create table CategoryOfOrganizations
(
    Id           int identity
        constraint PK_CategoryOfOrganizations
        primary key,
    Category     nvarchar(35) not null,
    DisplayOrder int          not null,
    RowVersion   timestamp    null
)
    go

create table CategoryOfPersons
(
    Id           int identity
        constraint PK_CategoryOfPersons
        primary key,
    Category     nvarchar(50) not null,
    DisplayOrder int          not null,
    RowVersion   timestamp    null
)
    go

create table DocumentTypes
(
    Id              int identity
        constraint PK_DocumentTypes
        primary key,
    Description     nvarchar(50) not null,
    LastUpdatedBy   nvarchar(255),
    LastUpdatedDate datetime2    not null,
    Notes           nvarchar(max),
    RowVersion      timestamp    null
)
    go

create table EMailTypes
(
    Id          int identity
        constraint PK_EMailTypes
        primary key,
    Description nvarchar(50) not null,
    Notes       nvarchar(max),
    RowVersion  timestamp    null
)
    go

create table EMails
(
    Id              int identity
        constraint EMails_pk
        primary key nonclustered,
    EMailTypeId     int           not null,
    EMailAddress    nvarchar(max) not null,
    LastUpdatedBy   nvarchar(255) not null,
    LastUpdatedDate datetime2     not null,
    Notes           nvarchar(max) not null,
    RowVersion      timestamp     not null
)
    go

create table Events
(
    Id              int identity
        constraint PK_Events
        primary key,
    EventName       nvarchar(255) not null,
    Description     nvarchar(max),
    IsOneTime       bit           not null,
    NextDate        datetime2     not null,
    LastUpdatedBy   nvarchar(255),
    LastUpdatedDate datetime2     not null,
    Notes           nvarchar(max),
    RowVersion      timestamp     null
)
    go

create table EventDocuments
(
    Id              int identity
        constraint PK_EventDocuments
        primary key,
    EventId         int
        constraint FK_EventDocuments_Events_EventId
            references Events,
    DocumentName    nvarchar(50),
    DocumentTypeId  int
        constraint FK_EventDocuments_DocumentTypes_DocumentTypeId
            references DocumentTypes,
    PathAndFileName nvarchar(255),
    LastUpdatedBy   nvarchar(255),
    LastUpdatedDate datetime2 not null,
    Notes           nvarchar(max),
    RowVersion      timestamp null
)
    go

create index IX_EventDocuments_DocumentTypeId
    on EventDocuments (DocumentTypeId)
go

create index IX_EventDocuments_EventId
    on EventDocuments (EventId)
go

create table MemberStatuses
(
    Id          int identity
        constraint PK_MemberStatuses
        primary key,
    Description nvarchar(50) not null,
    Notes       nvarchar(max),
    RowVersion  timestamp    null
)
    go

create table MembershipTypes
(
    Id              int identity
        constraint PK_MembershipTypes
        primary key,
    Description     nvarchar(50) not null,
    Level           int          not null,
    LastUpdatedBy   nvarchar(255),
    LastUpdatedDate datetime2    not null,
    Notes           nvarchar(max),
    RowVersion      timestamp    null
)
    go

create table Offices
(
    Id              int identity
        constraint PK_Offices
        primary key,
    Name            nvarchar(50) not null,
    Term            int          not null,
    CalendarPeriod  nvarchar(25),
    ChosenHow       int          not null,
    Appointer       nvarchar(50),
    LastUpdatedBy   nvarchar(255),
    LastUpdatedDate datetime2    not null,
    Notes           nvarchar(max),
    RowVersion      timestamp    null
)
    go

create table OrganizationTypes
(
    Id                       int identity
        constraint PK_OrganizationTypes
        primary key,
    CategoryOfOrganizationId int
        constraint FK_OrganizationTypes_CategoryOfOrganizations_CategoryOfOrganizationId
            references CategoryOfOrganizations,
    Name                     nvarchar(50) not null,
    Notes                    nvarchar(255),
    RowVersion               timestamp    null
)
    go

create index IX_OrganizationTypes_CategoryOfOrganizationId
    on OrganizationTypes (CategoryOfOrganizationId)
go

create table Organizations
(
    Id                 int identity
        constraint PK_Organizations
        primary key,
    Name               nvarchar(50) not null,
    OrganizationTypeId int
        constraint FK_Organizations_OrganizationTypes_OrganizationTypeId
            references OrganizationTypes,
    DateOfFirstContact datetime2    not null,
    LastUpdatedBy      nvarchar(255),
    LastUpdatedDate    datetime2    not null,
    Notes              nvarchar(max),
    RowVersion         timestamp    null
)
    go

create table AddressOrganization
(
    AddressesId     int not null
        constraint FK_AddressOrganization_Addresses_AddressesId
            references Addresses
            on delete cascade,
    OrganizationsId int not null
        constraint FK_AddressOrganization_Organizations_OrganizationsId
            references Organizations
            on delete cascade,
    constraint PK_AddressOrganization
        primary key (AddressesId, OrganizationsId)
)
    go

create index IX_AddressOrganization_OrganizationsId
    on AddressOrganization (OrganizationsId)
go

create table CategoryOfOrganizationOrganization
(
    CategoryOfOrganizationsId int not null
        constraint FK_CategoryOfOrganizationOrganization_CategoryOfOrganizations_CategoryOfOrganizationsId
            references CategoryOfOrganizations
            on delete cascade,
    OrganizationsId           int not null
        constraint FK_CategoryOfOrganizationOrganization_Organizations_OrganizationsId
            references Organizations
            on delete cascade,
    constraint PK_CategoryOfOrganizationOrganization
        primary key (CategoryOfOrganizationsId, OrganizationsId)
)
    go

create index IX_CategoryOfOrganizationOrganization_OrganizationsId
    on CategoryOfOrganizationOrganization (OrganizationsId)
go

create table EMailOrganization
(
    EMailsId        int not null,
    OrganizationsId int not null
        constraint FK_EMailOrganization_Organizations_OrganizationsId
            references Organizations
            on delete cascade,
    constraint PK_EMailOrganization
        primary key (EMailsId, OrganizationsId)
)
    go

create index IX_EMailOrganization_OrganizationsId
    on EMailOrganization (OrganizationsId)
go

create index IX_Organizations_OrganizationTypeId
    on Organizations (OrganizationTypeId)
go

create table PaymentSources
(
    Id          int identity
        constraint PK_PaymentSources
        primary key,
    Description nvarchar(50) not null,
    Notes       nvarchar(max),
    RowVersion  timestamp    null
)
    go

create table PaymentTypes
(
    Id          int identity
        constraint PK_PaymentTypes
        primary key,
    Description nvarchar(50) not null,
    Notes       nvarchar(max),
    RowVersion  timestamp    null
)
    go

create table Phones
(
    Id              int identity
        constraint PK_Phones
        primary key,
    PhoneType       nvarchar(10),
    AreaCode        nvarchar(3)  not null,
    Number          nvarchar(25) not null,
    Extension       nvarchar(25),
    DisplayOrder    int          not null,
    LastUpdatedBy   nvarchar(255),
    LastUpdatedDate datetime2    not null,
    Notes           nvarchar(max),
    RowVersion      timestamp    null
)
    go

create table OrganizationPhone
(
    OrganizationsId int not null
        constraint FK_OrganizationPhone_Organizations_OrganizationsId
            references Organizations
            on delete cascade,
    PhonesId        int not null
        constraint FK_OrganizationPhone_Phones_PhonesId
            references Phones
            on delete cascade,
    constraint PK_OrganizationPhone
        primary key (OrganizationsId, PhonesId)
)
    go

create index IX_OrganizationPhone_PhonesId
    on OrganizationPhone (PhonesId)
go

create table PrivacyLevels
(
    Id          int identity
        constraint PK_PrivacyLevels
        primary key,
    Description nvarchar(50) not null,
    Notes       nvarchar(max),
    RowVersion  timestamp    null
)
    go

create table TaskForEvents
(
    Id              int identity
        constraint PK_TaskForEvents
        primary key,
    EventId         int
        constraint FK_TaskForEvents_Events_EventId
            references Events,
    TaskName        nvarchar(50) not null,
    PlannedDate     datetime2    not null,
    ActualDate      datetime2    not null,
    Information     nvarchar(max),
    LastUpdatedBy   nvarchar(255),
    LastUpdatedDate datetime2    not null,
    Notes           nvarchar(max),
    RowVersion      timestamp    null
)
    go

create index IX_TaskForEvents_EventId
    on TaskForEvents (EventId)
go

create table Titles
(
    Id           int identity
        constraint PK_Titles
        primary key,
    Abbreviation nvarchar(10) not null,
    Description  nvarchar(50),
    DisplayOrder int          not null,
    RowVersion   timestamp    null
)
    go

create table Persons
(
    Id                 int identity
        constraint PK_Persons
        primary key,
    TitleId            int
        constraint FK_Persons_Titles_TitleId
            references Titles,
    LastName           nvarchar(50) not null,
    MiddleName         nvarchar(50),
    FirstName          nvarchar(50) not null,
    DateOfFirstContact datetime2    not null,
    BirthDate          datetime2    not null,
    LastUpdatedBy      nvarchar(max),
    LastUpdatedDate    datetime2    not null,
    Code               nvarchar(5),
    Notes              nvarchar(max),
    EMailId            int,
    RowVersion         timestamp    null
)
    go

create table AddressPerson
(
    AddressesId int not null
        constraint FK_AddressPerson_Addresses_AddressesId
            references Addresses
            on delete cascade,
    PersonsId   int not null
        constraint FK_AddressPerson_Persons_PersonsId
            references Persons
            on delete cascade,
    constraint PK_AddressPerson
        primary key (AddressesId, PersonsId)
)
    go

create index IX_AddressPerson_PersonsId
    on AddressPerson (PersonsId)
go

create table CategoryOfPersonPerson
(
    CategoryOfPersonsId int not null
        constraint FK_CategoryOfPersonPerson_CategoryOfPersons_CategoryOfPersonsId
            references CategoryOfPersons
            on delete cascade,
    PersonsId           int not null
        constraint FK_CategoryOfPersonPerson_Persons_PersonsId
            references Persons
            on delete cascade,
    constraint PK_CategoryOfPersonPerson
        primary key (CategoryOfPersonsId, PersonsId)
)
    go

create index IX_CategoryOfPersonPerson_PersonsId
    on CategoryOfPersonPerson (PersonsId)
go

create table EventPerson
(
    EventsId  int not null
        constraint FK_EventPerson_Events_EventsId
            references Events
            on delete cascade,
    PersonsId int not null
        constraint FK_EventPerson_Persons_PersonsId
            references Persons
            on delete cascade,
    constraint PK_EventPerson
        primary key (EventsId, PersonsId)
)
    go

create index IX_EventPerson_PersonsId
    on EventPerson (PersonsId)
go

create table MemberInfo
(
    Id               int identity
        constraint PK_MemberInfo
        primary key,
    PersonId         int
        constraint FK_MemberInfo_Persons_PersonId
            references Persons,
    MemberNumber     nvarchar(35),
    DateFirstJoined  datetime2 not null,
    PrivacyId        int
        constraint FK_MemberInfo_PrivacyLevels_PrivacyId
            references PrivacyLevels,
    MemberStatusId   int
        constraint FK_MemberInfo_MemberStatuses_MemberStatusId
            references MemberStatuses,
    MembershipTypeId int
        constraint FK_MemberInfo_MembershipTypes_MembershipTypeId
            references MembershipTypes,
    LastUpdatedBy    nvarchar(255),
    LastUpdatedDate  datetime2 not null,
    Notes            nvarchar(max),
    RowVersion       timestamp null
)
    go

create table EventMembers
(
    Id            int identity
        constraint PK_EventMembers
        primary key,
    MemberInfoId  int
        constraint FK_EventMembers_MemberInfo_MemberInfoId
            references MemberInfo,
    EventId       int
        constraint FK_EventMembers_Events_EventId
            references Events,
    Role          nvarchar(50),
    LastUpdatedBy nvarchar(255),
    LastUpdated   datetime2 not null,
    Notes         nvarchar(max),
    RowVersion    timestamp null
)
    go

create index IX_EventMembers_EventId
    on EventMembers (EventId)
go

create index IX_EventMembers_MemberInfoId
    on EventMembers (MemberInfoId)
go

create index IX_MemberInfo_MembershipTypeId
    on MemberInfo (MembershipTypeId)
go

create index IX_MemberInfo_MemberStatusId
    on MemberInfo (MemberStatusId)
go

create index IX_MemberInfo_PersonId
    on MemberInfo (PersonId)
go

create index IX_MemberInfo_PrivacyId
    on MemberInfo (PrivacyId)
go

create table Payments
(
    Id                    int identity
        constraint PK_Payments
        primary key,
    PersonId              int
        constraint FK_Payments_Persons_PersonId
            references Persons,
    Amount                float     not null,
    PaymentDate           datetime2 not null,
    PaymentExpirationDate datetime2 not null,
    PaymentSourceId       int
        constraint FK_Payments_PaymentSources_PaymentSourceId
            references PaymentSources,
    PaymentTypeId         int
        constraint FK_Payments_PaymentTypes_PaymentTypeId
            references PaymentTypes,
    LastUpdatedBy         nvarchar(255),
    LastUpdatedDate       datetime2 not null,
    Notes                 nvarchar(max),
    RowVersion            timestamp null
)
    go

create index IX_Payments_PaymentSourceId
    on Payments (PaymentSourceId)
go

create index IX_Payments_PaymentTypeId
    on Payments (PaymentTypeId)
go

create index IX_Payments_PersonId
    on Payments (PersonId)
go

create table PersonPhone
(
    PersonsId int not null
        constraint FK_PersonPhone_Persons_PersonsId
            references Persons
            on delete cascade,
    PhonesId  int not null
        constraint FK_PersonPhone_Phones_PhonesId
            references Phones
            on delete cascade,
    constraint PK_PersonPhone
        primary key (PersonsId, PhonesId)
)
    go

create index IX_PersonPhone_PhonesId
    on PersonPhone (PhonesId)
go

create table PersonalNotes
(
    Id              int identity
        constraint PK_PersonalNotes
        primary key,
    PersonId        int
        constraint FK_PersonalNotes_Persons_PersonId
            references Persons,
    Description     nvarchar(50),
    StartDate       datetime2 not null,
    DateEnd         datetime2 not null,
    LastUpdatedBy   nvarchar(255),
    LastUpdatedDate datetime2 not null,
    Note            nvarchar(max),
    RowVersion      timestamp null
)
    go

create index IX_PersonalNotes_PersonId
    on PersonalNotes (PersonId)
go

create index IX_Persons_EMailId
    on Persons (EMailId)
go

create index IX_Persons_TitleId
    on Persons (TitleId)
go

create table Sponsors
(
    Id                  int identity
        constraint PK_Sponsors
        primary key,
    PersonId            int
        constraint FK_Sponsors_Persons_PersonId
            references Persons,
    OrganizationId      int
        constraint FK_Sponsors_Organizations_OrganizationId
            references Organizations,
    Status              nvarchar(50),
    DateOfFirstContact  datetime2 not null,
    ReferredBy          nvarchar(255),
    DateSponsorAccepted datetime2 not null,
    TypeName            nvarchar(max),
    Details             nvarchar(max),
    SponsorUntilDate    datetime2 not null,
    Notes               nvarchar(max),
    LastUpdatedBy       nvarchar(255),
    LastUpdatedDate     datetime2 not null,
    RowVersion          timestamp null
)
    go

create table ContactForSponsors
(
    Id                 int identity
        constraint PK_ContactForSponsors
        primary key,
    SponsorId          int
        constraint FK_ContactForSponsors_Sponsors_SponsorId
            references Sponsors,
    DateWhenContacted  datetime2 not null,
    Purpose            nvarchar(255),
    RecordOfDiscussion nvarchar(max),
    PersonId           int
        constraint FK_ContactForSponsors_Persons_PersonId
            references Persons,
    Notes              nvarchar(max),
    LastUpdatedBy      nvarchar(255),
    LastUpdatedDate    datetime2 not null,
    RowVersion         timestamp null
)
    go

create index IX_ContactForSponsors_PersonId
    on ContactForSponsors (PersonId)
go

create index IX_ContactForSponsors_SponsorId
    on ContactForSponsors (SponsorId)
go

create index IX_Sponsors_OrganizationId
    on Sponsors (OrganizationId)
go

create index IX_Sponsors_PersonId
    on Sponsors (PersonId)
go

create table TermInOffices
(
    Id              int identity
        constraint PK_TermInOffices
        primary key,
    PersonId        int
        constraint FK_TermInOffices_Persons_PersonId
            references Persons,
    OfficeId        int
        constraint FK_TermInOffices_Offices_OfficeId
            references Offices,
    StartDate       datetime2 not null,
    LastUpdatedBy   nvarchar(255),
    LastUpdatedDate datetime2 not null,
    Notes           nvarchar(max),
    RowVersion      timestamp null
)
    go

create index IX_TermInOffices_OfficeId
    on TermInOffices (OfficeId)
go

create index IX_TermInOffices_PersonId
    on TermInOffices (PersonId)
go

create table __EFMigrationsHistory
(
    MigrationId    nvarchar(150) not null
    constraint PK___EFMigrationsHistory
    primary key,
    ProductVersion nvarchar(32)  not null
    )
    go

create table sysdiagrams
(
    name         sysname not null,
    principal_id int     not null,
    diagram_id   int identity
        primary key,
    version      int,
    definition   varbinary(max),
    constraint UK_principal_name
        unique (principal_id, name)
)
    go

exec sp_addextendedproperty 'microsoft_database_tools_support', 1, 'SCHEMA', 'dbo', 'TABLE', 'sysdiagrams'
go

