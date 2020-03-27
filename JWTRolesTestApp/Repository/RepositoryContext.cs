﻿using JWTRolesTestApp.Repository.Entities;
using JWTRolesTestApp.Repository.Seeds;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWTRolesTestApp.Repository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Employee
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.LoginInfo)
                .WithOne(b => b.Employee)
                .HasForeignKey<LoginInfo>(b => b.EmployeeId);

            modelBuilder.Entity<Employee>()
                .HasData(EmployeeSeed.Seed());
            #endregion

            #region LoginInfo
            modelBuilder.Entity<LoginInfo>()
                .HasIndex(prop => prop.Username)
                .IsUnique();

            modelBuilder.Entity<LoginInfo>()
                .HasData(LoginInfoSeed.Seed());
            #endregion

            #region Role
            modelBuilder.Entity<Role>()
                .HasMany(a => a.Employees)
                .WithOne(b => b.Role);

            modelBuilder.Entity<Role>()
                .HasData(RoleSeed.Seed());
            #endregion
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<LoginInfo> LoginInfos { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}