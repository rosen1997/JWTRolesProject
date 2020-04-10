﻿// <auto-generated />
using System;
using JWTRolesTestApp.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace JWTRolesTestApp.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    [Migration("20200410091730_AddAtWork")]
    partial class AddAtWork
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("JWTRolesTestApp.Repository.Entities.AtWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LoginTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.ToTable("AtWork");
                });

            modelBuilder.Entity("JWTRolesTestApp.Repository.Entities.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Employees");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Rosen",
                            LastName = "Lechev",
                            MiddleName = "Yavorov",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Neli",
                            LastName = "Zarkova",
                            MiddleName = "Lychezarova",
                            RoleId = 2
                        });
                });

            modelBuilder.Entity("JWTRolesTestApp.Repository.Entities.LoginInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("LoginInfos");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            EmployeeId = 1,
                            PasswordHash = new byte[] {  },
                            PasswordSalt = new byte[] {  },
                            Username = "rosen"
                        },
                        new
                        {
                            Id = 2,
                            EmployeeId = 2,
                            PasswordHash = new byte[] {  },
                            PasswordSalt = new byte[] {  },
                            Username = "neli"
                        });
                });

            modelBuilder.Entity("JWTRolesTestApp.Repository.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(64)")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            RoleDescription = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            RoleDescription = "User"
                        });
                });

            modelBuilder.Entity("JWTRolesTestApp.Repository.Entities.AtWork", b =>
                {
                    b.HasOne("JWTRolesTestApp.Repository.Entities.Employee", "Employee")
                        .WithOne("AtWork")
                        .HasForeignKey("JWTRolesTestApp.Repository.Entities.AtWork", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JWTRolesTestApp.Repository.Entities.Employee", b =>
                {
                    b.HasOne("JWTRolesTestApp.Repository.Entities.Role", "Role")
                        .WithMany("Employees")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("JWTRolesTestApp.Repository.Entities.LoginInfo", b =>
                {
                    b.HasOne("JWTRolesTestApp.Repository.Entities.Employee", "Employee")
                        .WithOne("LoginInfo")
                        .HasForeignKey("JWTRolesTestApp.Repository.Entities.LoginInfo", "EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
