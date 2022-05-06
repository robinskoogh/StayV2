#nullable disable

using Core.Models;
using Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Web.ViewModels;

namespace Web.Controllers
{
    //[Authorize(Roles = "Mäklare")]
    public class RealtorController : Controller
    {
        public static HttpClient Client { get; set; } = new HttpClient();
        private readonly StayContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<User> _userManager;

        public RealtorController(StayContext context, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager, IEmailSender emailSender)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.RealEstateObjects.Include(reo => reo.Images).ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstateObject = await _context.RealEstateObjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realEstateObject == null)
            {
                return NotFound();
            }

            return View(realEstateObject);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Address,ZipCode,City,Price,Description,PropType,ContractType,NrOfRooms,LivingArea,GrossArea,PlotSize,ConstructionYear,Balcony,DateForViewing,DateUploaded,Longitude,Latitude")] RealEstateObject realEstateObject, ICollection<IFormFile> inputImages)
        {
            if (realEstateObject.DateUploaded == null)
            {
                realEstateObject.DateUploaded = DateTime.Now;
            }

            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={realEstateObject.Address},{realEstateObject.City}&key=AIzaSyABG0LJ7DX2Z6R252r4J8Yy0f1aRlyhNFk";
            var coordinatesAsJson = await Client.GetStringAsync(url).ConfigureAwait(false);
            dynamic result = JsonConvert.DeserializeObject<dynamic>(coordinatesAsJson);

            if (result["status"] == "OK")
            {
                IEnumerable<JToken> addressComponents = result["results"][0]["address_components"];
                foreach (JToken addressComponent in addressComponents)
                {
                    foreach (string type in addressComponent["types"])
                    {
                        if (type.Equals("postal_town"))
                        {
                            realEstateObject.PostalTown = addressComponent["long_name"].ToString();
                        }
                        else if (type.Equals("sublocality_level_1"))
                        {
                            realEstateObject.Sublocality = addressComponent["long_name"].ToString();
                        }
                    }
                }
                realEstateObject.Longitude = result["results"][0]["geometry"]["location"]["lng"].Value;
                realEstateObject.Latitude = result["results"][0]["geometry"]["location"]["lat"].Value;
            }

            if (ModelState.IsValid)
            {
                if (inputImages != null)
                {
                    foreach (var image in inputImages)
                    {
                        string fileName = Guid.NewGuid().ToString() + image.FileName;
                        string folder = "images/houses/" + fileName;
                        string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                        using (var stream = new FileStream(serverFolder, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }

                        Image imageModel = new Image();
                        imageModel.FileName = fileName;
                        imageModel.RealEstateObject = realEstateObject;

                        realEstateObject.Images.Add(imageModel);
                    }
                }
                var user = await _userManager.GetUserAsync(User);
                realEstateObject.Realtor = user;
                user.UploadedRealEstateObjects.Add(realEstateObject);
                var firm = await _context.RealtorFirms.Where(f => f.Employees.Contains(user)).FirstOrDefaultAsync();
                if (firm != null)
                {
                    firm.FirmListings.Add(realEstateObject);
                }

                _context.Add(realEstateObject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(realEstateObject);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstateObject = await _context.RealEstateObjects.FindAsync(id);
            if (realEstateObject == null)
            {
                return NotFound();
            }
            return View(realEstateObject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Address,ZipCode,City,Price,Description,PropType,ContractType,NrOfRooms,LivingArea,GrossArea,PlotSize,ConstructionYear,Balcony,DateForViewing,DateUploaded,Longitude,Latitude")] RealEstateObject realEstateObject)
        {
            if (id != realEstateObject.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realEstateObject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealEstateObjectExists(realEstateObject.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(realEstateObject);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realEstateObject = await _context.RealEstateObjects
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realEstateObject == null)
            {
                return NotFound();
            }

            return View(realEstateObject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var realEstateObject = await _context.RealEstateObjects.Include(reo => reo.Images).FirstOrDefaultAsync(reo => reo.Id == id);

            if (realEstateObject != null)
            {
                foreach (var image in realEstateObject.Images)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images\\houses", image.FileName);

                    if (System.IO.File.Exists(path))
                    {
                        System.IO.File.Delete(path);
                    }
                }
            }

            _context.RealEstateObjects.Remove(realEstateObject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealEstateObjectExists(int id)
        {
            return _context.RealEstateObjects.Any(e => e.Id == id);
        }

        public async Task<IActionResult> CreateVM(RealEstateObjectViewModel model)
        {
            var reo = new RealEstateObject()
            {
                Description = model.DescriptionString,
                PlotSize = Convert.ToDouble(model.PlotSize),
                Balcony = model.Balcony.Equals("1"),
            };

            _context.RealEstateObjects.Add(reo);
            await _context.SaveChangesAsync();

            return View(model);
        }

        public IActionResult InterestedUsers(int id)
        {
            var reo = _context.RealEstateObjects.Include(reo => reo.InterestedUsers).FirstOrDefault(reo => reo.Id == id);

            return View(reo);
        }

        public async Task<IActionResult> NotifyInterestedUsers(int id)
        {
            var reo = _context.RealEstateObjects.Include(reo => reo.InterestedUsers).FirstOrDefault(reo => reo.Id == id);

            var realtorId = _userManager.GetUserId(User);
            var realtor = _context.Users.SingleOrDefault(r => r.Id == realtorId);

            if (reo != null)
            {
                foreach (var user in reo.InterestedUsers)
                {
                    if (user.Email != null)
                    {
                        var defaultMessage = $"<p>Hej {user.FirstName}!</p><br><p>Du får detta meddelande eftersom du har anmält intresse på ett av våra objekt " +
                            $"och nu är det snart dags för visning!</p><p>Den {reo.DateForViewing.ToString("yyyy/MM/dd H:mm")} är du välkommen på visning på {reo.Address}</p><p>" +
                            $"Om du skulle ha några frågor så tveka inte på att höra av dig till {realtor.FirstName} {realtor.LastName}, {realtor.PhoneNumber}." +
                            $"</p><p>Varmt välkommen!</p><br><p>Med vänliga hälsingar från {"*mäklarfirman*"}</p>";
                        await _emailSender.SendEmailAsync(user.Email, $"Välkommen på visning!", defaultMessage);
                    }
                }
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}