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
        public DbSet<CakeVote> Votes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define properties for Users

            // Define key
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            // Define index (unique email)
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

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

            // Key
            modelBuilder.Entity<CakeRequest>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<CakeRequest>()
                .Property(r => r.Reason)
                .IsRequired();

            // CreatedBy relationship
            modelBuilder.Entity<CakeRequest>()
                .HasOne(r => r.CreatedBy)
                .WithMany()
                .HasForeignKey(r => r.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Assigned to relationship
            modelBuilder.Entity<CakeRequest>()
                .HasOne(r => r.AssignedTo)
                .WithMany()
                .HasForeignKey(r => r.AssignedToId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CakeRequest>()
                .HasMany(r => r.Votes)
                .WithOne()
                .HasForeignKey(v => v.CakeRequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define the value conversion for status
            modelBuilder.Entity<CakeRequest>()
                .Property(r => r.Status)
                .HasConversion<string>();               

            // Define properties for CakeVote
            // Define key
            modelBuilder.Entity<CakeVote>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<CakeVote>()
                .HasOne(v => v.CreatedBy)
                .WithMany()
                .HasForeignKey(v => v.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);



            // Seed data

        }
    }
}
