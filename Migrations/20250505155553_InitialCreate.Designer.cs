using System;
using FinancialApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250505155553_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64); // MySQL default

            modelBuilder.Entity("FinancialApp.Models.Account", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>("AccountName")
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                b.Property<string>("AccountType")
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                b.Property<decimal>("Balance")
                    .HasColumnType("decimal(18,2)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime");

                b.Property<int>("UserId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("Accounts");
            });

            modelBuilder.Entity("FinancialApp.Models.Transaction", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<int>("AccountId")
                    .HasColumnType("int");

                b.Property<decimal>("Amount")
                    .HasColumnType("decimal(18,2)");

                b.Property<int?>("CategoryId")
                    .HasColumnType("int");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime");

                b.Property<string>("Description")
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                b.Property<string>("Type")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.HasKey("Id");

                b.HasIndex("AccountId");

                b.HasIndex("CategoryId");

                b.ToTable("Transactions");
            });

            modelBuilder.Entity("FinancialApp.Models.TransactionCategory", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                b.Property<int>("UserId")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.HasIndex("UserId");

                b.ToTable("TransactionCategories");
            });

            modelBuilder.Entity("FinancialApp.Models.User", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                b.Property<string>("PasswordHash")
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                b.Property<string>("Role")
                    .IsRequired()
                    .HasColumnType("varchar(50)");

                b.Property<string>("Username")
                    .IsRequired()
                    .HasColumnType("varchar(255)");

                b.HasKey("Id");

                b.ToTable("Users");
            });

            modelBuilder.Entity("FinancialApp.Models.Account", b =>
            {
                b.HasOne("FinancialApp.Models.User", "User")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("User");
            });

            modelBuilder.Entity("FinancialApp.Models.Transaction", b =>
            {
                b.HasOne("FinancialApp.Models.Account", "Account")
                    .WithMany()
                    .HasForeignKey("AccountId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.HasOne("FinancialApp.Models.TransactionCategory", "Category")
                    .WithMany()
                    .HasForeignKey("CategoryId");

                b.Navigation("Account");

                b.Navigation("Category");
            });

            modelBuilder.Entity("FinancialApp.Models.TransactionCategory", b =>
            {
                b.HasOne("FinancialApp.Models.User", "User")
                    .WithMany()
                    .HasForeignKey("UserId")
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();

                b.Navigation("User");
            });
#pragma warning restore 612, 618
        }
    }
}






















//// <auto-generated />
//using System;
//using FinancialApp.Data;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Infrastructure;
//using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Migrations;
//using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

//#nullable disable

//namespace FinancialApp.Migrations
//{
//    [DbContext(typeof(ApplicationDbContext))]
//    [Migration("20250505155553_InitialCreate")]
//    partial class InitialCreate
//    {
//        /// <inheritdoc />
//        protected override void BuildTargetModel(ModelBuilder modelBuilder)
//        {
//#pragma warning disable 612, 618
//            modelBuilder
//                .HasAnnotation("ProductVersion", "9.0.4")
//                .HasAnnotation("Relational:MaxIdentifierLength", 128);

//            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

//            modelBuilder.Entity("FinancialApp.Models.Account", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<string>("AccountName")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("AccountType")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<decimal>("Balance")
//                        .HasColumnType("decimal(18,2)");

//                    b.Property<DateTime>("CreatedAt")
//                        .HasColumnType("datetime2");

//                    b.Property<int>("UserId")
//                        .HasColumnType("int");

//                    b.HasKey("Id");

//                    b.HasIndex("UserId");

//                    b.ToTable("Accounts");
//                });

//            modelBuilder.Entity("FinancialApp.Models.Transaction", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<int>("AccountId")
//                        .HasColumnType("int");

//                    b.Property<decimal>("Amount")
//                        .HasColumnType("decimal(18,2)");

//                    b.Property<int?>("CategoryId")
//                        .HasColumnType("int");

//                    b.Property<DateTime>("CreatedAt")
//                        .HasColumnType("datetime2");

//                    b.Property<string>("Description")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Type")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.HasIndex("AccountId");

//                    b.HasIndex("CategoryId");

//                    b.ToTable("Transactions");
//                });

//            modelBuilder.Entity("FinancialApp.Models.TransactionCategory", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<DateTime>("CreatedAt")
//                        .HasColumnType("datetime2");

//                    b.Property<string>("Name")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<int>("UserId")
//                        .HasColumnType("int");

//                    b.HasKey("Id");

//                    b.HasIndex("UserId");

//                    b.ToTable("TransactionCategories");
//                });

//            modelBuilder.Entity("FinancialApp.Models.User", b =>
//                {
//                    b.Property<int>("Id")
//                        .ValueGeneratedOnAdd()
//                        .HasColumnType("int");

//                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

//                    b.Property<DateTime>("CreatedAt")
//                        .HasColumnType("datetime2");

//                    b.Property<string>("Email")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("PasswordHash")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Role")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.Property<string>("Username")
//                        .IsRequired()
//                        .HasColumnType("nvarchar(max)");

//                    b.HasKey("Id");

//                    b.ToTable("Users");
//                });

//            modelBuilder.Entity("FinancialApp.Models.Account", b =>
//                {
//                    b.HasOne("FinancialApp.Models.User", "User")
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("User");
//                });

//            modelBuilder.Entity("FinancialApp.Models.Transaction", b =>
//                {
//                    b.HasOne("FinancialApp.Models.Account", "Account")
//                        .WithMany()
//                        .HasForeignKey("AccountId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.HasOne("FinancialApp.Models.TransactionCategory", "Category")
//                        .WithMany()
//                        .HasForeignKey("CategoryId");

//                    b.Navigation("Account");

//                    b.Navigation("Category");
//                });

//            modelBuilder.Entity("FinancialApp.Models.TransactionCategory", b =>
//                {
//                    b.HasOne("FinancialApp.Models.User", "User")
//                        .WithMany()
//                        .HasForeignKey("UserId")
//                        .OnDelete(DeleteBehavior.Cascade)
//                        .IsRequired();

//                    b.Navigation("User");
//                });
//#pragma warning restore 612, 618
//        }
//    }
//}
