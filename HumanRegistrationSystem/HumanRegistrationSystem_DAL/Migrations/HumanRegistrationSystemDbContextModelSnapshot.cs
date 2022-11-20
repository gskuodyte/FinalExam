﻿// <auto-generated />
using System;
using HumanRegistrationSystem_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HumanRegistrationSystem_DAL.Migrations
{
    [DbContext(typeof(HumanRegistrationSystemDbContext))]
    partial class HumanRegistrationSystemDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("HumanRegistrationSystem_Domain.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("ApartmentNumber")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseNumber")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("HumanRegistrationSystem_Domain.HumanInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PersonalId")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Picture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Humans");
                });

            modelBuilder.Entity("HumanRegistrationSystem_Domain.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("HumanRegistrationSystem_Domain.Address", b =>
                {
                    b.HasOne("HumanRegistrationSystem_Domain.HumanInfo", "HumanInfo")
                        .WithOne("Address")
                        .HasForeignKey("HumanRegistrationSystem_Domain.Address", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HumanInfo");
                });

            modelBuilder.Entity("HumanRegistrationSystem_Domain.HumanInfo", b =>
                {
                    b.HasOne("HumanRegistrationSystem_Domain.UserAccount", "UserAccount")
                        .WithOne("HumanInfo")
                        .HasForeignKey("HumanRegistrationSystem_Domain.HumanInfo", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("HumanRegistrationSystem_Domain.HumanInfo", b =>
                {
                    b.Navigation("Address")
                        .IsRequired();
                });

            modelBuilder.Entity("HumanRegistrationSystem_Domain.UserAccount", b =>
                {
                    b.Navigation("HumanInfo")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
