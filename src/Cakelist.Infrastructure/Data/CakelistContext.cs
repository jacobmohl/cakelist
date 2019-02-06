using System;
using System.Collections.Generic;
using System.Text;
using Cakelist.Business.Entities;
using Cakelist.Business.Entities.CakelistRequestAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Cakelist.Infrastructure.Data
{
    public class CakelistContext : DbContext
    {
        public CakelistContext(DbContextOptions<CakelistContext> options)
            : base(options)
        { }

        public DbSet<CakeRequest> Requests { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define properties for Users

            // Define key
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // FirstName is required
            modelBuilder.Entity<User>()
                .Property(u => u.FirstName)
                .IsRequired();
            
            // LastName is required
            modelBuilder.Entity<User>()
                .Property(u => u.LastName)
                .IsRequired();

            // Email is required
            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();


            // Define properties for Cakerequest

            /// Key
            modelBuilder.Entity<CakeRequest>()
                .HasKey(r => r.Id);

            // Required fields
            //modelBuilder.Entity<CakeRequest>()
            //    .Property(r => r.CreatedBy)
            //    .IsRequired();

            //modelBuilder.Entity<CakeRequest>()
            //    .Property(r => r.AssignedTo)
            //    .IsRequired();

            modelBuilder.Entity<CakeRequest>()
                .Property(r => r.Reason)
                .IsRequired();


            // CreatedBy relationship
            modelBuilder.Entity<CakeRequest>()
                .HasOne(r => r.CreatedBy)
                .WithMany(u => u.CreatedCakeRequests);

            // Assigned to relationship
            modelBuilder.Entity<CakeRequest>()
                .HasOne(r => r.AssignedTo)
                .WithMany(u => u.AssignedCakeRequests);

            // Define the value conversion for status
            modelBuilder.Entity<CakeRequest>()
                .Property(r => r.Status)
                .HasConversion<string>();



            // Define properties for CakeVote

            // Define key
            modelBuilder.Entity<CakeVote>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<CakeVote>()
                .HasOne(v => v.CakeRequest)
                .WithMany(r => r.Votes);

            modelBuilder.Entity<CakeVote>()
                .HasOne(v => v.CreatedBy)
                .WithMany(u => u.Votes);


        }
    }
}
