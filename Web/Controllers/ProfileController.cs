using Core.Models;
using Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;

namespace Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly StayContext _dbContext;

        public ProfileController(UserManager<User> userManager, StayContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public IActionResult Index(RealtorRequest model) //konto
        {
            var realtorRequests = _dbContext.RealtorRequests.Include(br => br.UserRequests);
            foreach (var br in realtorRequests)
            {
                foreach (var ur in br.UserRequests)
                {
                    model.UserRequests.Add(ur);
                }
            }
            return View(model);
        }

        public IActionResult MyResidence()
        {
            var reo = _dbContext.RealEstateObjects.SingleOrDefault(reo => reo.Id == 8);

            return View(reo);
        }

        public IActionResult MyFavourites()
        {
            var id = _userManager.GetUserId(User);
            User user = _dbContext.Users.Where(user => user.Id == id).Include(user => user.Favorites).FirstOrDefault();

            return View(user.Favorites);
        }

        public IActionResult MySearchHistory()
        {
            var id = _userManager.GetUserId(User);

            User user = _dbContext.Users.First(user => user.Id == id);

            IEnumerable<RealEstateObjectViewModel> orderedList = _dbContext.RealEstateObjectViewmodel.Where(reoVM => reoVM.User == user).OrderByDescending(reoVM => reoVM.SearchTime);

            return View(orderedList);
        }

        public IActionResult MyPreferences()
        {
            return View();
        }
    }
}