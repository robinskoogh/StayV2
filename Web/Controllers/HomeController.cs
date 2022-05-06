using Core.Models;
using Core.Models.Enums;
using Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;
using System.Diagnostics;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StayContext _dbContext;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger, StayContext dbContext, UserManager<User> userManager)
        {
            _logger = logger;
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            GetNrOfViews();

            return View();
        }

        public async Task<IActionResult> SearchResult(RealEstateObjectViewModel reoVM)
        {
            if (User.Identity.IsAuthenticated)
            {
                // Add to search history
                AddToSearchHistory(reoVM);
            }

            var realEstateObjects = _dbContext.RealEstateObjects.Include(reo => reo.Images).Select(reo => reo);

            realEstateObjects = await FilterSearchResult(reoVM, realEstateObjects);

            //foreach (var reo in reoVM.RealEstateObjectsList)
            //{
            //    ICollection<Image> images = _dbContext.Images.Where(img => img.RealEstateObject == reo).ToList();
            //    reo.Images.AddRange(images);
            //}

            var loggedInUser = await _userManager.GetUserAsync(User);
            //ViewData["currentUser"] = _dbContext.Users.Include(u => u.Favorites).FirstOrDefault(u => u.Id == loggedInUser.Id);

            if (loggedInUser != null)
            {
                ViewData["currentUser"] = _dbContext.Users.Include(u => u.Favorites).FirstOrDefault(u => u.Id == loggedInUser.Id);
            }

            return View(reoVM);
        }

        private async Task<IQueryable<RealEstateObject>> FilterSearchResult(RealEstateObjectViewModel reoVM, IQueryable<RealEstateObject> realEstateObjects)
        {
            if (!string.IsNullOrEmpty(reoVM.SearchString))
            {
                realEstateObjects = realEstateObjects.Where(reo => reo.Address.Contains(reoVM.SearchString)
                                                                        || reo.City.Contains(reoVM.SearchString)
                                                                        || reo.PostalTown.Contains(reoVM.SearchString)
                                                                        || reo.Sublocality.Contains(reoVM.SearchString)
                                                                        || reo.Realtor.FirstName.Contains(reoVM.SearchString)
                                                                        || reo.Realtor.LastName.Contains(reoVM.SearchString));
            }

            if (!string.IsNullOrEmpty(reoVM.MinRooms))
            {
                int minRoomsAsInt = int.Parse(reoVM.MinRooms);
                realEstateObjects = realEstateObjects.Where(reo => reo.NrOfRooms >= minRoomsAsInt);
            }

            if (!string.IsNullOrEmpty(reoVM.MaxRooms))
            {
                int maxRoomsAsInt = int.Parse(reoVM.MaxRooms);
                realEstateObjects = realEstateObjects.Where(reo => reo.NrOfRooms <= maxRoomsAsInt);
            }

            if (!string.IsNullOrEmpty(reoVM.MinLivingArea))
            {
                double minLivingAreaAsDouble = double.Parse(reoVM.MinLivingArea);
                realEstateObjects = realEstateObjects.Where(reo => reo.LivingArea >= minLivingAreaAsDouble);
            }

            if (!string.IsNullOrEmpty(reoVM.MaxLivingArea))
            {
                double maxLivingAreaAsDouble = double.Parse(reoVM.MaxLivingArea);
                realEstateObjects = realEstateObjects.Where(reo => reo.LivingArea <= maxLivingAreaAsDouble);
            }

            if (!string.IsNullOrEmpty(reoVM.PlotSize))
            {
                double plotSizeAsDouble = double.Parse(reoVM.PlotSize);
                realEstateObjects = realEstateObjects.Where(reo => reo.PlotSize >= plotSizeAsDouble);
            }

            if (!string.IsNullOrEmpty(reoVM.MinPrice))
            {
                int minPriceAsInt = int.Parse(reoVM.MinPrice);
                realEstateObjects = realEstateObjects.Where(reo => reo.Price >= minPriceAsInt);
            }

            if (!string.IsNullOrEmpty(reoVM.MaxPrice))
            {
                int maxPriceAsInt = int.Parse(reoVM.MaxPrice);
                realEstateObjects = realEstateObjects.Where(reo => reo.Price <= maxPriceAsInt);
            }

            if (!string.IsNullOrEmpty(reoVM.DescriptionString))
            {
                realEstateObjects = realEstateObjects.Where(reo => reo.Description.Contains(reoVM.DescriptionString));
            }

            if (reoVM.NewConstruction == "1")
            {
                realEstateObjects = realEstateObjects.Where(reo => reo.ConstructionYear == null);
            }

            if (reoVM.NewConstruction == "0")
            {
                realEstateObjects = realEstateObjects.Where(reo => reo.ConstructionYear != null);
            }

            if (reoVM.Balcony == "1")
            {
                realEstateObjects = realEstateObjects.Where(reo => reo.Balcony);
            }

            if (reoVM.Balcony == "0")
            {
                realEstateObjects = realEstateObjects.Where(reo => !reo.Balcony);
            }

            if (reoVM.HouseCheckBox || reoVM.RowHouseCheckBox || reoVM.ApartmentCheckbox || reoVM.HolidayHomeCheckBox
                || reoVM.LotCheckBox || reoVM.FarmCheckBox || reoVM.OtherCheckBox)
            {
                if (!reoVM.HouseCheckBox)
                {
                    realEstateObjects = realEstateObjects.Where(reo => reo.PropType != PropertyType.House);
                }

                if (!reoVM.RowHouseCheckBox)
                {
                    realEstateObjects = realEstateObjects.Where(reo => reo.PropType != PropertyType.RowHouse);
                }

                if (!reoVM.ApartmentCheckbox)
                {
                    realEstateObjects = realEstateObjects.Where(reo => reo.PropType != PropertyType.Apartment);
                }

                if (!reoVM.HolidayHomeCheckBox)
                {
                    realEstateObjects = realEstateObjects.Where(reo => reo.PropType != PropertyType.HolidayHome);
                }

                if (!reoVM.LotCheckBox)
                {
                    realEstateObjects = realEstateObjects.Where(reo => reo.PropType != PropertyType.Lot);
                }

                if (!reoVM.FarmCheckBox)
                {
                    realEstateObjects = realEstateObjects.Where(reo => reo.PropType != PropertyType.Farm);
                }

                if (!reoVM.OtherCheckBox)
                {
                    realEstateObjects = realEstateObjects.Where(reo => reo.PropType != PropertyType.Other);
                }
            }

            reoVM.RealEstateObjectsList = await realEstateObjects.Include(reo => reo.Realtor).OrderByDescending(reo => reo.DateUploaded).ToListAsync();

            reoVM.RealEstateObjectsList = SortSelection(reoVM);
            return realEstateObjects;
        }

        private List<RealEstateObject> SortSelection(RealEstateObjectViewModel reoVM)
        {
            switch (reoVM.SortOrder)
            {
                case "newFirst":
                    return reoVM.RealEstateObjectsList.OrderByDescending(reo => reo.DateUploaded).ToList();

                case "newLast":
                    return reoVM.RealEstateObjectsList.OrderBy(reo => reo.DateUploaded).ToList();

                case "cheapestFirst":
                    return reoVM.RealEstateObjectsList.OrderBy(reo => reo.Price).ToList();

                case "cheapestLast":
                    return reoVM.RealEstateObjectsList.OrderByDescending(reo => reo.Price).ToList();

                case "leastRooms":
                    return reoVM.RealEstateObjectsList.OrderBy(reo => reo.NrOfRooms).ToList();

                case "mostRooms":
                    return reoVM.RealEstateObjectsList.OrderByDescending(reo => reo.NrOfRooms).ToList();

                case "leastArea":
                    return reoVM.RealEstateObjectsList.OrderBy(reo => reo.LivingArea).ToList();

                case "largestArea":
                    return reoVM.RealEstateObjectsList.OrderByDescending(reo => reo.LivingArea).ToList();

                default:
                    break;
            }

            return reoVM.RealEstateObjectsList;
        }

        private void AddToSearchHistory(RealEstateObjectViewModel reoVM)
        {
            var id = _userManager.GetUserId(User);

            User user = _dbContext.Users.FirstOrDefault(user => user.Id == id);

            if (user == null)
            {
                return;
            }

            reoVM.User = user;

            _dbContext.RealEstateObjectViewmodel.Add(reoVM);
            _dbContext.SaveChanges();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> RealEstateObject(int id)
        {
            var reo = _dbContext.RealEstateObjects
                .Include(reo => reo.Images)
                .Include(reo => reo.UsersFavorited)
                .Include(reo => reo.Realtor).ThenInclude(r => r.RealtorFirm)
                .SingleOrDefault(o => o.Id == id);

            if (reo != null)
            {
                reo.NrOfViews++;
                _dbContext.SaveChanges();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser != null)
            {
                ViewData["currentUser"] = _dbContext.Users.Include(u => u.InterestingListings).FirstOrDefault(u => u.Id == currentUser.Id);
            }

            return View(reo);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult AddToFavourites(int id)
        {
            var reo = GetRealEstateObjectById(id);

            var userId = _userManager.GetUserId(User);
            User user = _dbContext.Users.Where(user => user.Id == userId).Include(user => user.Favorites).FirstOrDefault();

            if (user.Favorites.FirstOrDefault(reo => reo.Id == id) == null)
            {
                user.Favorites.Add(reo);
                _dbContext.SaveChanges();
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult RemoveFromFavourites(int id)
        {
            var reo = GetRealEstateObjectById(id);

            var userId = _userManager.GetUserId(User);
            User user = _dbContext.Users.Where(user => user.Id == userId).Include(user => user.Favorites).FirstOrDefault();

            user.Favorites.Remove(reo);
            _dbContext.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public void GetNrOfViews()
        {
            var Views = _dbContext.RealEstateObjects.OrderByDescending(reo => reo.NrOfViews).ToList().GetRange(0, 3);

            ViewBag.MostViewdObjects = Views;
        }

        public IActionResult SubmitInterest(int id)
        {
            var reo = GetRealEstateObjectById(id);
            var userId = _userManager.GetUserId(User);

            var user = _dbContext.Users.Include(u => u.InterestingListings).SingleOrDefault(u => u.Id == userId);

            user.InterestingListings.Add(reo);
            _dbContext.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult WithdrawInterest(int id)
        {
            var reo = GetRealEstateObjectById(id);
            var userId = _userManager.GetUserId(User);

            var user = _dbContext.Users.Include(u => u.InterestingListings).SingleOrDefault(u => u.Id == userId);

            user.InterestingListings.Remove(reo);
            _dbContext.SaveChanges();

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public RealEstateObject GetRealEstateObjectById(int id)
        {
            return _dbContext.RealEstateObjects.SingleOrDefault(reo => reo.Id == id);
        }
    }
}