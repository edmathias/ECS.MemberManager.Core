﻿// <auto-generated />
using System;
using ECS.MemberManager.Core.EF.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ECS.MemberManager.Core.EF.Data.Migrations
{
    [DbContext(typeof(MembershipManagerDataContext))]
    partial class MembershipManagerDataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ECS.BizBricks.CRM.Core.EF.Domain.Person", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Birthdate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfFirstContact")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("LastUpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MaritalStatusId")
                        .HasColumnType("int");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PersonCategoryId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<int>("TitleId")
                        .HasColumnType("int");

                    b.Property<int?>("TitleSuffixId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MaritalStatusId");

                    b.HasIndex("PersonCategoryId");

                    b.HasIndex("TitleId");

                    b.HasIndex("TitleSuffixId");

                    b.ToTable("Persons");
                });

            modelBuilder.Entity("ECS.BizBricks.CRM.Core.EF.Domain.PersonCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PersonCategories");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address1")
                        .IsRequired()
                        .HasColumnType("nvarchar(35)")
                        .HasMaxLength(35);

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(35)")
                        .HasMaxLength(35);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PersonId")
                        .HasColumnType("int");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(9)")
                        .HasMaxLength(9);

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.AddressOrganization", b =>
                {
                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("OrganizationId")
                        .HasColumnType("int");

                    b.HasKey("AddressId", "OrganizationId");

                    b.HasIndex("OrganizationId");

                    b.ToTable("AddressOrganizations");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.AddressPerson", b =>
                {
                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("PersonId")
                        .HasColumnType("int");

                    b.HasKey("AddressId", "PersonId");

                    b.HasIndex("PersonId");

                    b.ToTable("AddressPersons");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.MaritalStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("MaritalStatuses");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.Organization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfFirstContact")
                        .HasColumnType("datetime2");

                    b.Property<int>("LastUpdatedBy")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizationTypeId")
                        .HasColumnType("int");

                    b.Property<byte[]>("RowVersion")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("OrganizationTypeId");

                    b.ToTable("Organizations");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.OrganizationCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(35)")
                        .HasMaxLength(35);

                    b.HasKey("Id");

                    b.ToTable("OrganizationCategories");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.OrganizationType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<int>("OrganizationCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("TypeName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("OrganizationCategoryId");

                    b.ToTable("OrganizationTypes");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.Title", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Titles");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.TitleSuffix", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Abbreviation")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TitleSuffixes");
                });

            modelBuilder.Entity("ECS.BizBricks.CRM.Core.EF.Domain.Person", b =>
                {
                    b.HasOne("ECS.MemberManager.Core.EF.Domain.MaritalStatus", "MaritalStatus")
                        .WithMany()
                        .HasForeignKey("MaritalStatusId");

                    b.HasOne("ECS.BizBricks.CRM.Core.EF.Domain.PersonCategory", "PersonCategory")
                        .WithMany()
                        .HasForeignKey("PersonCategoryId");

                    b.HasOne("ECS.MemberManager.Core.EF.Domain.Title", "Title")
                        .WithMany()
                        .HasForeignKey("TitleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECS.MemberManager.Core.EF.Domain.TitleSuffix", "TitleSuffix")
                        .WithMany()
                        .HasForeignKey("TitleSuffixId");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.Address", b =>
                {
                    b.HasOne("ECS.BizBricks.CRM.Core.EF.Domain.Person", null)
                        .WithMany("Addresses")
                        .HasForeignKey("PersonId");
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.AddressOrganization", b =>
                {
                    b.HasOne("ECS.MemberManager.Core.EF.Domain.Address", "Address")
                        .WithMany("AddressOrganizations")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECS.MemberManager.Core.EF.Domain.Organization", "Organization")
                        .WithMany("AddressOrganizations")
                        .HasForeignKey("OrganizationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.AddressPerson", b =>
                {
                    b.HasOne("ECS.MemberManager.Core.EF.Domain.Address", "Address")
                        .WithMany("AddressPersons")
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ECS.BizBricks.CRM.Core.EF.Domain.Person", "Person")
                        .WithMany()
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.Organization", b =>
                {
                    b.HasOne("ECS.MemberManager.Core.EF.Domain.OrganizationType", "OrganizationType")
                        .WithMany()
                        .HasForeignKey("OrganizationTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ECS.MemberManager.Core.EF.Domain.OrganizationType", b =>
                {
                    b.HasOne("ECS.MemberManager.Core.EF.Domain.OrganizationCategory", "OrganizationCategory")
                        .WithMany()
                        .HasForeignKey("OrganizationCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
