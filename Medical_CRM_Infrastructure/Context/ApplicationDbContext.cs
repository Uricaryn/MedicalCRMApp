using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Medical_CRM_Domain.Entities;

namespace Medical_CRM_Infrastructure.Context
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Define other DbSets for your domain entities
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Procedure> Procedures { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<StockTransaction> StockTransactions { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Operator> Operators { get; set; }
        public DbSet<ProcedureProduct> ProcedureProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call base method to ensure Identity mappings are correctly applied
            base.OnModelCreating(modelBuilder);

            // Apply configurations for domain entities
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Seed SuperAdmin Role and User
            SeedSuperAdmin(modelBuilder);
        }

        private void SeedSuperAdmin(ModelBuilder builder)
        {
            // Create and seed the SuperAdmin role
            var superAdminRole = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(), // Generate a new GUID for the role ID
                Name = "SuperAdmin",
                NormalizedName = "SUPERADMIN" // Ensure the role name is normalized
            };

            // Seed the role using HasData method
            builder.Entity<IdentityRole>().HasData(superAdminRole);

            // Create and seed the SuperAdmin user
            var hasher = new PasswordHasher<User>();
            var superAdminUser = new User
            {
                Id = Guid.NewGuid().ToString(), // Generate a new GUID for the user ID
                UserName = "superadmin",
                NormalizedUserName = "SUPERADMIN", // Ensure the username is normalized
                Email = "superadmin@example.com",
                NormalizedEmail = "SUPERADMIN@EXAMPLE.COM", // Ensure the email is normalized
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "SuperAdmin123!"), // Replace with a strong, secure password
                SecurityStamp = Guid.NewGuid().ToString("D"),
                FullName = "Super Administrator",
                LastLoginDate = DateTime.Now,
                IsActive = true
            };

            // Seed the user using HasData method
            builder.Entity<User>().HasData(superAdminUser);

            // Create the relationship between the SuperAdmin role and the SuperAdmin user
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = superAdminRole.Id, // Ensure this matches the role's ID
                UserId = superAdminUser.Id // Ensure this matches the user's ID
            });
        }
    }
}
