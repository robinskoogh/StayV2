#nullable disable

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Data.Data;
using Core.Models;

namespace Web.Controllers
{
    public class RealtorFirmsController : Controller
    {
        private readonly StayContext _context;
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;

        public RealtorFirmsController(StayContext context, UserManager<User> userMngr, RoleManager<IdentityRole> roleMngr)
        {
            _context = context;
            userManager = userMngr;
            roleManager = roleMngr;
        }

        // GET: RealtorFirms
        public async Task<IActionResult> Index()
        {
            return View(await _context.RealtorFirms.Include(rf => rf.Employees).ToListAsync());
        }

        // GET: RealtorFirms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realtorFirm = await _context.RealtorFirms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realtorFirm == null)
            {
                return NotFound();
            }

            return View(realtorFirm);
        }

        // GET: RealtorFirms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RealtorFirms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,PhoneNumber,WebsiteURL,RealtorLogoPath")] RealtorFirm realtorFirm)
        {
            if (ModelState.IsValid)
            {
                _context.Add(realtorFirm);
                await _context.SaveChangesAsync();
                return RedirectToAction("RealtorFirms", "Admin");
            }
            return View(realtorFirm);
        }

        // GET: RealtorFirms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realtorFirm = await _context.RealtorFirms.FindAsync(id);
            if (realtorFirm == null)
            {
                return NotFound();
            }
            return View(realtorFirm);
        }

        // POST: RealtorFirms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,PhoneNumber,WebsiteURL,RealtorLogoPath")] RealtorFirm realtorFirm)
        {
            if (id != realtorFirm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(realtorFirm);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RealtorFirmExists(realtorFirm.Id))
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
            return View(realtorFirm);
        }

        // GET: RealtorFirms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realtorFirm = await _context.RealtorFirms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realtorFirm == null)
            {
                return NotFound();
            }

            return View(realtorFirm);
        }

        // POST: RealtorFirms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var realtorFirm = await _context.RealtorFirms.FindAsync(id);
            _context.RealtorFirms.Remove(realtorFirm);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RealtorFirmExists(int id)
        {
            return _context.RealtorFirms.Any(e => e.Id == id);
        }

        [HttpGet]
        public async Task<IActionResult> EditRealtorsInFirm(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realtorFirm = await _context.RealtorFirms
                .Include(rf => rf.Employees)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realtorFirm == null)
            {
                return NotFound();
            }

            return View(realtorFirm);
        }

        [HttpPost]
        public async Task<IActionResult> EditRealtorsInFirm(int? id, string userId, bool isSelected)
        {
            if (id == null)
            {
                return NotFound();
            }

            var realtorFirm = await _context.RealtorFirms
                .FirstOrDefaultAsync(m => m.Id == id);
            if (realtorFirm == null)
            {
                return NotFound();
            }

            //var realtor = await _context.Users.FirstOrDefaultAsync(user => user.Id == userId);

            var realtors = await userManager.GetUsersInRoleAsync("Mäklare");

            foreach (var realtor in realtors)
            {
                if (isSelected && !realtorFirm.Employees.Contains(realtor))
                {
                    realtorFirm.Employees.Add(realtor);
                    await userManager.AddToRoleAsync(realtor, "Chef");
                }
                else if (!isSelected && realtorFirm.Employees.Contains(realtor))
                {
                    realtorFirm.Employees.Remove(realtor);
                }
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("RealtorFirms", "Admin");
        }
    }
}