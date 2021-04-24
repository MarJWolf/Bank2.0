﻿// <auto-generated />
using System;
using Bank.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bank.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.4")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bank.Models.AccountType", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("LIHVA")
                        .HasColumnType("float");

                    b.Property<double>("MESECHNA_TAKSA")
                        .HasColumnType("float");

                    b.Property<string>("NAME")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("AccountType");
                });

            modelBuilder.Entity("Bank.Models.BankAccount", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("BALANCE")
                        .HasColumnType("real");

                    b.Property<int?>("EGN_client")
                        .HasColumnType("int");

                    b.Property<int?>("ID_currency")
                        .HasColumnType("int");

                    b.Property<float>("INTEREST")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("EGN_client");

                    b.HasIndex("ID_currency");

                    b.ToTable("BankAccount");
                });

            modelBuilder.Entity("Bank.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EGN")
                        .HasMaxLength(10)
                        .HasColumnType("VARCHAR(10)");

                    b.Property<string>("ID_user")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PN")
                        .HasMaxLength(13)
                        .HasColumnType("VARCHAR(13)");

                    b.Property<string>("address")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("ID_user");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("Bank.Models.Currency", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FULL_NAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NAME")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Currency");
                });

            modelBuilder.Entity("Bank.Models.Employee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ID_position")
                        .HasColumnType("int");

                    b.Property<string>("ID_user")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PN")
                        .HasMaxLength(13)
                        .HasColumnType("VARCHAR(13)");

                    b.HasKey("ID");

                    b.HasIndex("ID_position");

                    b.HasIndex("ID_user");

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("Bank.Models.Position", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NAME")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Position");
                });

            modelBuilder.Entity("Bank.Models.Transaction", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DATE")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ID_bankAccount")
                        .HasColumnType("int");

                    b.Property<int?>("ID_employee")
                        .HasColumnType("int");

                    b.Property<int?>("NAME_Category")
                        .HasColumnType("int");

                    b.Property<float>("SUM")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("ID_bankAccount");

                    b.HasIndex("ID_employee");

                    b.HasIndex("NAME_Category");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Bank.Models.TransactionCategory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("COEF")
                        .HasColumnType("int");

                    b.Property<string>("NAME")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("TransactionCategory");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("Bank.Models.BankAccount", b =>
                {
                    b.HasOne("Bank.Models.Client", "client")
                        .WithMany()
                        .HasForeignKey("EGN_client");

                    b.HasOne("Bank.Models.Currency", "currency")
                        .WithMany()
                        .HasForeignKey("ID_currency");

                    b.Navigation("client");

                    b.Navigation("currency");
                });

            modelBuilder.Entity("Bank.Models.Client", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "user")
                        .WithMany()
                        .HasForeignKey("ID_user");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Bank.Models.Employee", b =>
                {
                    b.HasOne("Bank.Models.Position", "position")
                        .WithMany()
                        .HasForeignKey("ID_position");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", "user")
                        .WithMany()
                        .HasForeignKey("ID_user");

                    b.Navigation("position");

                    b.Navigation("user");
                });

            modelBuilder.Entity("Bank.Models.Transaction", b =>
                {
                    b.HasOne("Bank.Models.BankAccount", "bankAccount")
                        .WithMany()
                        .HasForeignKey("ID_bankAccount");

                    b.HasOne("Bank.Models.Employee", "employee")
                        .WithMany()
                        .HasForeignKey("ID_employee");

                    b.HasOne("Bank.Models.TransactionCategory", "transactionCategory")
                        .WithMany()
                        .HasForeignKey("NAME_Category");

                    b.Navigation("bankAccount");

                    b.Navigation("employee");

                    b.Navigation("transactionCategory");
                });
#pragma warning restore 612, 618
        }
    }
}
