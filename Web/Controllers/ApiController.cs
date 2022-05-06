#nullable disable

using Core.Models;
using Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;

namespace Web.Controllers
{
    [Route("stay/api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly StayContext _context;
        private readonly UserManager<User> _userManager;

        public ApiController(StayContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //--------------------------------------REAL ESTATE OBJECTS---------------------------//
        [Route("realestate")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RealEstateObject>>> GetRealEstateObjects()
        {
            return await _context.RealEstateObjects.ToListAsync();
        }

        [Route("realestate")]
        [HttpGet("realestate/{id}")]
        public async Task<ActionResult<RealEstateObject>> GetRealEstateObject(int id)
        {
            var realEstateObject = await _context.RealEstateObjects.FindAsync(id);

            if (realEstateObject == null)
            {
                return NotFound();
            }

            return realEstateObject;
        }

        //--------------------------------------USERS---------------------------//
        [Route("user")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        [Route("user")]
        [HttpGet("user/{id}")]
        public async Task<ActionResult<UserViewModel>> GetUser(int indexValue)
        {
            var userList = await _context.Users.ToListAsync();

            if (indexValue >= userList.Count)
            {
                return NotFound();
            }
            // Use id parameter as an index of the user table instead of long complicated strings..
            var user = userList[indexValue];

            if (user == null)
            {
                return NotFound();
            }

            var userDTO = CreateUserDTO(user, indexValue);

            return userDTO;
        }

        #region Realtors
        [Route("realtor")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetRealtors()
        {
            return await _userManager.GetUsersInRoleAsync("Mäklare") as List<User>;
        }

        [Route("realtor")]
        [HttpGet("realtor/{id}")]
        public async Task<ActionResult<User>> GetRealtor(int indexValue)
        {
            var realtorList = await _userManager.GetUsersInRoleAsync("Mäklare") as List<User>;

            if (indexValue >= realtorList.Count)
            {
                return NotFound();
            }
            // Use id parameter as an index of the user table instead of long complicated strings..
            var realtor = realtorList[indexValue];

            if (realtor == null)
            {
                return NotFound();
            }

            return realtor;
        }
        #endregion


        [Route("realtorObjects")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetRealtorsObjects()
        {
            var realtorList = await _userManager.GetUsersInRoleAsync("Mäklare") as List<User>;

            var reoList = await _context.RealEstateObjects.Include(reo => reo.InterestedUsers).Include(reo => reo.UsersFavorited).ToListAsync();

            reoList.ForEach(reo => reo.UsersFavorited = null);
            reoList.ForEach(reo => reo.Realtor = null);

            return realtorList;
        }

        public UserViewModel CreateUserDTO(User user, int id)
        {
            UserViewModel userDTO = new UserViewModel()
            {
                NormalizedId = id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Address = user.Address,
                ZipCode = user.ZipCode,
                City = user.City,
                DateOfBirth = user.DateOfBirth,
                Favorites = user.Favorites,
                InterestingListings = user.InterestingListings,
                UploadedRealEstateObjects = user.UploadedRealEstateObjects,
            };
            return userDTO;
        }
    }
}