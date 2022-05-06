#nullable disable

using Core.Models;
using Data.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public class StayContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RealEstateObject> RealEstateObjects { get; set; }
        public DbSet<RealtorRequest> RealtorRequests { get; set; }
        public DbSet<RealEstateObjectViewModel> RealEstateObjectViewmodel { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<RealtorFirm> RealtorFirms { get; set; }

        public StayContext(DbContextOptions<StayContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(user => user.RealtorRequests)
                .WithMany(requests => requests.UserRequests);
            modelBuilder.Entity<User>()
                .HasMany(user => user.Favorites)
                .WithMany(reo => reo.UsersFavorited);
            modelBuilder.Entity<User>()
                .HasMany(user => user.UploadedRealEstateObjects)
                .WithOne(reo => reo.Realtor);
            modelBuilder.Entity<User>()
                .HasOne(user => user.RealtorFirm)
                .WithMany(rf => rf.Employees);

            modelBuilder.Entity<RealEstateObject>()
                .HasMany(reo => reo.InterestedUsers)
                .WithMany(user => user.InterestingListings);

            modelBuilder.Entity<RealtorRequest>()
                .HasMany(requests => requests.UserRequests)
                .WithMany(user => user.RealtorRequests);

            modelBuilder.Entity<RealtorFirm>()
                .HasMany(rf => rf.Employees)
                .WithOne(user => user.RealtorFirm);

            modelBuilder.Seed();
        }
    }
}