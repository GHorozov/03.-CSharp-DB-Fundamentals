﻿namespace Employees.Data
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using Employees.Models;

    public class EmployeesContext : DbContext
    {
        public EmployeesContext()
        {

        }

        public EmployeesContext(DbContextOptions options)
            :base(options)
        {

        }


        public DbSet<Employee> Employees { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Configuration.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .IsUnicode(true)
                    .HasMaxLength(60);

                entity.Property(e => e.LastName)
                   .IsRequired()
                   .IsUnicode(true)
                   .HasMaxLength(60);

                entity.Property(e => e.Salary)
                    .IsRequired();

                entity.Property(e => e.Birthdate)
                    .IsRequired(false);

                entity.Property(e => e.Address)
                    .IsRequired(false)
                    .HasMaxLength(250);

                entity.HasOne(e => e.Manager)
                    .WithMany(me => me.ManagedEmployees)
                    .HasForeignKey(e => e.ManagerId);
                    
            });
                
        }
    }
}
