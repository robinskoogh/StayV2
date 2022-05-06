using Core.Models;
using Core.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Data.Data
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            //User user1 = new User { Id = Guid.NewGuid().ToString(), FirstName = "Förnamn1", LastName = "Efternamn1", Address = "Testgatan1", ZipCode = 11111, City = "Teststad", DateOfBirth = new DateTime(1995, 01, 01), RealtorFirm = rf};
            //User user2 = new User { Id = Guid.NewGuid().ToString(), FirstName = "Förnamn2", LastName = "Efternamn2", Address = "Testgränd1", ZipCode = 11112, City = "Teststad", DateOfBirth = new DateTime(1995, 01, 01), RealtorFirm = rf };
            //User user3 = new User { Id = Guid.NewGuid().ToString(), FirstName = "Förnamn3", LastName = "Efternamn3", Address = "Teststigen2", ZipCode = 11113, City = "Teststad", DateOfBirth = new DateTime(1995, 01, 01), RealtorFirm = rf};
            //User user4 = new User { Id = Guid.NewGuid().ToString(), FirstName = "Förnamn4", LastName = "Efternamn4", Address = "Testgatan4", ZipCode = 11114, City = "Teststad", DateOfBirth = new DateTime(1995, 01, 01), RealtorFirm = rf};
            //User user5 = new User { Id = Guid.NewGuid().ToString(), FirstName = "Förnamn5", LastName = "Efternamn5", Address = "Testvägen2", ZipCode = 11115, City = "Teststad", DateOfBirth = new DateTime(1995, 01, 01), RealtorFirm = rf};
            //User user6 = new User { Id = Guid.NewGuid().ToString(), FirstName = "Förnamn6", LastName = "Efternamn6", Address = "Testgatan6", ZipCode = 11116, City = "Teststad", DateOfBirth = new DateTime(1995, 01, 01), RealtorFirm = rf};
            //User user7 = new User { Id = Guid.NewGuid().ToString(), FirstName = "Förnamn7", LastName = "Efternamn7", Address = "Testgatan7", ZipCode = 11117, City = "Teststad", DateOfBirth = new DateTime(1995, 01, 01), RealtorFirm = rf};
            //User user8 = new User { Id = Guid.NewGuid().ToString(), FirstName = "Förnamn8", LastName = "Efternamn8", Address = "Testgatan8", ZipCode = 11118, City = "Teststad", DateOfBirth = new DateTime(1995, 01, 01), RealtorFirm = rf };

            RealEstateObject realEstateObject1 = new RealEstateObject { Id = 1, NrOfViews = 20, Address = "Östmans väg 2", ZipCode = 95334, Sublocality = "Norrmalm", PostalTown = "Stockholm", City = "Haparanda", Price = 1000000, Description = "Bästa lägenhet", PropType = PropertyType.Apartment, ContractType = ContractType.Purchase, NrOfRooms = 2, LivingArea = 60, GrossArea = 120, PlotSize = 60, ConstructionYear = 1990, Balcony = true, DateForViewing = DateTime.Now, DateUploaded = new DateTime(2022, 2, 21), Longitude = 24.1122, Latitude = 65.8383 };
            RealEstateObject realEstateObject2 = new RealEstateObject { Id = 2, NrOfViews = 100, Address = "Pipons väg 2", ZipCode = 95334, Sublocality = "Norrmalm", PostalTown = "Stockholm", City = "Haparanda", Price = 400000, Description = "Dåligt lägenhet", PropType = PropertyType.Apartment, ContractType = ContractType.Rental, NrOfRooms = 2, LivingArea = 60, GrossArea = 130, PlotSize = 70, ConstructionYear = 1940, Balcony = false, DateForViewing = DateTime.Now, DateUploaded = new DateTime(2022, 2, 24), Longitude = 24.1081, Latitude = 65.8361 };
            RealEstateObject realEstateObject3 = new RealEstateObject { Id = 3, NrOfViews = 99, Address = "Sågaregatan 65", ZipCode = 95333, Sublocality = "Norrmalm", PostalTown = "Stockholm", City = "Haparanda", Price = 4000000, Description = "Bästa lägenhet", PropType = PropertyType.Apartment, ContractType = ContractType.Purchase, NrOfRooms = 4, LivingArea = 120, GrossArea = 220, PlotSize = 100, Balcony = false, DateForViewing = DateTime.Now, DateUploaded = new DateTime(2022, 2, 24), Longitude = 24.1216, Latitude = 65.8272 };
            RealEstateObject realEstateObject4 = new RealEstateObject { Id = 4, NrOfViews = 1000, Address = "Vindstigen 7", ZipCode = 16351, Sublocality = "Norrmalm", PostalTown = "Stockholm", City = "Spånga", Price = 100000, Description = "Dåligt lägenhet", PropType = PropertyType.Apartment, ContractType = ContractType.Rental, NrOfRooms = 1, LivingArea = 60, GrossArea = 90, PlotSize = 30, ConstructionYear = 1964, Balcony = true, DateForViewing = DateTime.Now, DateUploaded = new DateTime(2022, 2, 24), Longitude = 17.8854, Latitude = 59.3825 };
            RealEstateObject realEstateObject5 = new RealEstateObject { Id = 5, NrOfViews = 10, Address = "Solhems hagväg 168", ZipCode = 16356, Sublocality = "Märsta Norra", PostalTown = "Märsta", City = "Spånga", Price = 40000000, Description = "Bästa lägenhet", PropType = PropertyType.Lot, ContractType = ContractType.Purchase, NrOfRooms = 5, LivingArea = 60, GrossArea = 220, PlotSize = 160, Balcony = true, DateForViewing = DateTime.Now, DateUploaded = new DateTime(2022, 2, 24), Longitude = 17.8976, Latitude = 59.3886 };
            RealEstateObject realEstateObject6 = new RealEstateObject { Id = 6, NrOfViews = 22, Address = "Silverdalsvägen 39", ZipCode = 17834, Sublocality = "Märsta Norra", PostalTown = "Märsta", City = "Ekerö", Price = 2100000, Description = "Dåligt lägenhet", PropType = PropertyType.Apartment, ContractType = ContractType.Purchase, NrOfRooms = 3, LivingArea = 60, GrossArea = 70, PlotSize = 10, ConstructionYear = 2014, Balcony = false, DateForViewing = DateTime.Now, DateUploaded = new DateTime(2022, 2, 24), Longitude = 17.7982, Latitude = 59.2812 };
            RealEstateObject realEstateObject7 = new RealEstateObject { Id = 7, NrOfViews = 12, Address = "Växthusvägen 1", ZipCode = 17834, Sublocality = "Märsta Norra", PostalTown = "Märsta", City = "Ekerö", Price = 2900000, Description = "Bästa lägenhet", PropType = PropertyType.House, ContractType = ContractType.Purchase, NrOfRooms = 6, LivingArea = 200, GrossArea = 1200, PlotSize = 1000, ConstructionYear = 1960, Balcony = false, DateForViewing = DateTime.Now, DateUploaded = new DateTime(2022, 2, 24), Longitude = 17.7887, Latitude = 59.2812 };
            RealEstateObject realEstateObject8 = new RealEstateObject { Id = 8, NrOfViews = 1, Address = "Bostadsvägen 4", ZipCode = 12345, Sublocality = "Märsta Norra", PostalTown = "Märsta", City = "Hemma", Price = 12345678, Description = "Temporär default av min bostad, så att det går att se hur det ser ut när man laddar in sidan", PropType = PropertyType.House, ContractType = ContractType.Purchase, NrOfRooms = 6, LivingArea = 200, GrossArea = 1200, PlotSize = 1000, ConstructionYear = 1960, Balcony = false, DateForViewing = DateTime.Now, DateUploaded = new DateTime(2022, 2, 24), Longitude = 18.4286, Latitude = 59.5394 };
            RealEstateObject realEstateObject9 = new RealEstateObject { Id = 9, NrOfViews = 5000, Address = "Alsättersgatan 13B", ZipCode = 58435, Sublocality = "Ryd", PostalTown = "Linköping", City = "Linköping", Price = 5000, Description = "Temporär default av en bostad på en annan ort, så att det går att se hur auto zoom och auto pan funkar.", PropType = PropertyType.Apartment, ContractType = ContractType.Rental, NrOfRooms = 1, LivingArea = 25, GrossArea = 25, PlotSize = 25, ConstructionYear = 1960, Balcony = false, DateForViewing = DateTime.Now, DateUploaded = new DateTime(2022, 2, 24), Longitude = 15.5621776, Latitude = 58.4104317 };

            //modelBuilder.Entity<User>().HasData(
            //    user1, user2, user3, user4, user5, user6, user7, user8
            //    );

            modelBuilder.Entity<RealEstateObject>().HasData(
                realEstateObject1, realEstateObject2, realEstateObject3, realEstateObject4, realEstateObject5, realEstateObject6, realEstateObject7, realEstateObject8, realEstateObject9
                );

            //modelBuilder.Entity<User>()
            //    .HasMany(user => user.InterestingListings)
            //    .WithMany(reo => reo.InterestedUsers)
            //    .UsingEntity(j => j.HasData(
            //        new { InterestedUsersId = user1.Id, InterestingListingsId = realEstateObject1.Id },
            //        new { InterestedUsersId = user2.Id, InterestingListingsId = realEstateObject1.Id },
            //        new { InterestedUsersId = user3.Id, InterestingListingsId = realEstateObject1.Id },
            //        new { InterestedUsersId = user4.Id, InterestingListingsId = realEstateObject1.Id },
            //        new { InterestedUsersId = user5.Id, InterestingListingsId = realEstateObject1.Id },
            //        new { InterestedUsersId = user6.Id, InterestingListingsId = realEstateObject1.Id },
            //        new { InterestedUsersId = user7.Id, InterestingListingsId = realEstateObject1.Id },
            //        new { InterestedUsersId = user8.Id, InterestingListingsId = realEstateObject1.Id },
            //        new { InterestedUsersId = user4.Id, InterestingListingsId = realEstateObject2.Id },
            //        new { InterestedUsersId = user5.Id, InterestingListingsId = realEstateObject2.Id },
            //        new { InterestedUsersId = user6.Id, InterestingListingsId = realEstateObject2.Id },
            //        new { InterestedUsersId = user7.Id, InterestingListingsId = realEstateObject2.Id },
            //        new { InterestedUsersId = user8.Id, InterestingListingsId = realEstateObject2.Id },
            //        new { InterestedUsersId = user5.Id, InterestingListingsId = realEstateObject3.Id },
            //        new { InterestedUsersId = user6.Id, InterestingListingsId = realEstateObject3.Id },
            //        new { InterestedUsersId = user7.Id, InterestingListingsId = realEstateObject3.Id },
            //        new { InterestedUsersId = user8.Id, InterestingListingsId = realEstateObject3.Id },
            //        new { InterestedUsersId = user4.Id, InterestingListingsId = realEstateObject3.Id },
            //        new { InterestedUsersId = user5.Id, InterestingListingsId = realEstateObject4.Id },
            //        new { InterestedUsersId = user6.Id, InterestingListingsId = realEstateObject4.Id },
            //        new { InterestedUsersId = user7.Id, InterestingListingsId = realEstateObject4.Id },
            //        new { InterestedUsersId = user8.Id, InterestingListingsId = realEstateObject4.Id },
            //        new { InterestedUsersId = user8.Id, InterestingListingsId = realEstateObject5.Id },
            //        new { InterestedUsersId = user4.Id, InterestingListingsId = realEstateObject5.Id },
            //        new { InterestedUsersId = user5.Id, InterestingListingsId = realEstateObject5.Id },
            //        new { InterestedUsersId = user1.Id, InterestingListingsId = realEstateObject7.Id },
            //        new { InterestedUsersId = user2.Id, InterestingListingsId = realEstateObject7.Id }
            //        ));

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Administratör", ConcurrencyStamp = "1", NormalizedName = "Administratör" },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Mäklare", ConcurrencyStamp = "1", NormalizedName = "Mäklare" },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Användare", ConcurrencyStamp = "1", NormalizedName = "Användare" },
                new IdentityRole() { Id = Guid.NewGuid().ToString(), Name = "Chef", ConcurrencyStamp = "1", NormalizedName = "Chef" });
        }
    }
}