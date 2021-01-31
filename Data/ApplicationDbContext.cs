using System;
using System.Collections.Generic;
using System.Text;
using FacilityManager.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace FacilityManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Building> Building { get; set; }
        public DbSet<Facility> Facility { get; set; }
        public DbSet<Equipment> Equipment { get; set; }
       
        public DbSet<Task> Task { get; set; }
        public DbSet<WorkOrder> WorkOrder { get; set; }

        public DbSet<Contractor> Contractor { get; set; }

        public DbSet<Contract> Contract { get; set; }

        public DbSet<Department> Department { get; set; }

        public DbSet<Employee> Employee { get; set; }

        public DbSet<Report> Report { get; set; }

        public DbSet<Company> Company { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Required for setting Identity
                                                // Below are required, to setup the ProductCategory weak entity
                                                // which connects one or more products to one or more categories
            modelBuilder.Entity<Facility>()
                        .HasMany(b => b.Buildings)
                        .WithOne(f => f.Facility)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Employees)
                .WithOne(d => d.Department)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Company>()
                           .HasOne(d => d.Contractor)
                           .WithOne()
                           .HasForeignKey<Contractor>(c => c.ContractorId);


            modelBuilder.Entity<WorkOrderTask>()
                .HasKey(k => new { k.WorkOrderId, k.TaskId });
            modelBuilder.Entity<WorkOrderTask>()
                .HasOne(w => w.WorkOrder)
                .WithMany(t => t.Tasks)
                .HasForeignKey(w => w.WorkOrderId);
            modelBuilder.Entity<WorkOrderTask>()
                .HasOne(w => w.Task)
                .WithMany(t => t.WorkOrders)
                .HasForeignKey(w => w.TaskId);


            modelBuilder.Entity<WorkOrderEmployee>()
               .HasKey(k => new { k.WorkOrderId, k.EmployeeId });
            modelBuilder.Entity<WorkOrderEmployee>()
                .HasOne(w => w.WorkOrder)
                .WithMany(e => e.Employees)
                .HasForeignKey(w => w.WorkOrderId);
            modelBuilder.Entity<WorkOrderEmployee>()
                .HasOne(w => w.Employee)
                .WithMany(t => t.WorkOrders)
                .HasForeignKey(w => w.EmployeeId);




            modelBuilder.Entity<Building>()
                .HasMany(e => e.Equipment)
                .WithOne(b =>b.Building)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Company>()
                .HasMany(e => e.Facilities)
                .WithOne(b => b.Company)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Facility>()
               .HasMany(b => b.Buildings)
               .WithOne(b => b.Facility)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ContractorContract>()
               .HasKey(k => new { k.ContractId, k.ContractorId });
            
            modelBuilder.Entity<ContractorContract>()
                .HasOne(w => w.Contract)
                .WithMany(t => t.Contractors)
                .HasForeignKey(w => w.ContractorId);
            
            modelBuilder.Entity<ContractorContract>()
                .HasOne(w => w.Contractor)
                .WithMany(t => t.Contracts)
                .HasForeignKey(w => w.ContractId);


            modelBuilder.Entity<Company>()
                       .HasMany(b => b.Departments)
                       .WithOne(f => f.Company)
                       .OnDelete(DeleteBehavior.Cascade);


           

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Employees)
                .WithOne(d => d.Department)
                .OnDelete(DeleteBehavior.Cascade);


           
            
            modelBuilder.Entity<ContractorWorkOrder>()
                .HasKey(k => new { k.ContractorId, k.WorkOrderId });
            modelBuilder.Entity<ContractorWorkOrder>()
                .HasOne(c => c.Contractor)
                .WithMany(w => w.WorkOrders)
                .HasForeignKey(c => c.ContractorId);

            modelBuilder.Entity<ContractorWorkOrder>()
                .HasOne(w => w.WorkOrder)
                .WithMany(c => c.Contractors)
                .HasForeignKey(w => w.WorkOrderId);


            modelBuilder.Entity<IdentityRole>().HasData(
                    new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN" },
                    new IdentityRole() { Name = "Technitian", NormalizedName = "TECHNITIAN" },
                    new IdentityRole() { Name = "Manager", NormalizedName = "MANAGER" }
                );


         



        }

        



    }
 }

