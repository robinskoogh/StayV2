using Core.Models;
using Data.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;

namespace Web.Controllers
{
    //[Authorize(Roles = "Administratör")]
    public class AdminController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<User> userManager;
        private readonly StayContext context;
        private readonly IEmailSender emailSender;
        public string realtor = "Mäklare";
        public string newRoleSubject = "Du har fått ny status!";
        public string newRoleMessage = "Grattis!<br /><br />Du har nu fått statusen ";
        public string removedRoleSubject = "Ändring av status hos Stay.";
        public string removedRoleMessage = "Hej!<br /><br />Du har förlorat din status som ";

        public AdminController(RoleManager<IdentityRole> roleMngr, UserManager<User> userMngr, StayContext cntx, IEmailSender sender)
        {
            roleManager = roleMngr;
            userManager = userMngr;
            context = cntx;
            emailSender = sender;
        }

        public IActionResult Dashboard()
        {
            return View();
        }

        public async Task<IActionResult> RealtorFirms()
        {
            return View(await context.RealtorFirms.ToListAsync());
        }

        //-----------------------ROLE MANAGER-------------------------------
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };
                IdentityResult result = await roleManager.CreateAsync(identityRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles", "Admin");
                }

                foreach (IdentityError error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Fel: Roll med ID {id} kunde ej hittas.";
                return BadRequest($"Fel: Roll med ID {id} kunde ej hittas.");
            }

            await roleManager.DeleteAsync(role);
            return RedirectToAction("ListRoles");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Fel: Roll med ID {model.Id} kunde ej hittas.";
                return BadRequest($"Fel: Roll med ID {model.Id} kunde ej hittas.");
            }
            else
            {
                var result = await roleManager.DeleteAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return RedirectToAction("ListRoles");
            }
        }

        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await roleManager.FindByIdAsync(id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Fel: Roll med ID {id} kunde ej hittas.";
                return BadRequest($"Fel: Roll med ID {id} kunde ej hittas.");
            }

            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in userManager.Users)
            {
                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Fel: Roll med ID {model.Id} kunde ej hittas.";
                return BadRequest($"Fel: Roll med ID {model.Id} kunde ej hittas.");
            }
            else
            {
                role.Name = model.RoleName;

                var result = await roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> EditUsersInRole(string roleId)
        {
            ViewBag.RoleId = roleId;
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Fel: Roll med ID {roleId} kunde ej hittas.";
                return BadRequest($"Fel: Roll med ID {roleId} kunde ej hittas.");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUsersInRole(List<UserRoleViewModel> model, string roleId)
        {
            var role = await roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Fel: Roll med ID {roleId} kunde ej hittas.";
                return BadRequest($"Fel: Roll med ID {roleId} kunde ej hittas.");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.AddToRoleAsync(user, role.Name);
                    await SendEmailConfirmation(user, newRoleSubject, newRoleMessage, role.Name);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await userManager.RemoveFromRoleAsync(user, role.Name);
                    await SendEmailConfirmation(user, removedRoleSubject, removedRoleMessage, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }
            return RedirectToAction("EditRole", new { Id = roleId });
        }

        //-----------------------------------REALTOR REQUESTS-------------------------------------------
        [HttpGet]
        public async Task<IActionResult> ListRealtorRequests(List<RealtorRequestViewModel> model)
        {
            var query = context.RealtorRequests.Include(br => br.UserRequests);

            foreach (var result in query)
            {
                foreach (var user in result.UserRequests)
                {
                    model.Add(new RealtorRequestViewModel()
                    {
                        UserId = user.Id,
                        UserName = user.UserName,
                        IsSelected = await userManager.IsInRoleAsync(user, realtor)
                    });
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditRealtorRequests()
        {
            var query = context.RealtorRequests.Include(br => br.UserRequests);
            var modelList = new List<RealtorRequestViewModel>();

            foreach (var result in query)
            {
                foreach (var user in result.UserRequests)
                {
                    if (await userManager.IsInRoleAsync(user, realtor))
                    {
                        modelList.Add(new RealtorRequestViewModel()
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            IsSelected = true
                        });
                    }
                    else
                    {
                        modelList.Add(new RealtorRequestViewModel()
                        {
                            UserId = user.Id,
                            UserName = user.UserName,
                            IsSelected = false
                        });
                    }
                }
            }
            return View("ListRealtorRequests");
        }

        [HttpPost]
        public async Task<IActionResult> EditRealtorRequests(List<RealtorRequestViewModel> model)
        {
            var role = await roleManager.FindByNameAsync(realtor);

            if (role == null)
            {
                ViewBag.ErrorMessage = "Fel: Kunde ej hitta roll med namn 'Mäklare'.";
                return BadRequest("Fel: Kunde ej hitta roll med namn 'Mäklare'.");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !await userManager.IsInRoleAsync(user, realtor))
                {
                    result = await userManager.AddToRoleAsync(user, realtor);
                    var request = context.RealtorRequests.FirstOrDefault(br => br.Id == user.Id);
                    context.RealtorRequests.Remove(request);
                    await SendEmailConfirmation(user, newRoleSubject, newRoleMessage, realtor);
                }
                else if (!model[i].IsSelected && await userManager.IsInRoleAsync(user, realtor))
                {
                    result = await userManager.RemoveFromRoleAsync(user, realtor);
                    var request = context.RealtorRequests.FirstOrDefault(br => br.Id == user.Id);
                    context.RealtorRequests.Remove(request);
                    await SendEmailConfirmation(user, removedRoleSubject, removedRoleMessage, realtor);
                }
                else
                {
                    var request = context.RealtorRequests.FirstOrDefault(br => br.Id == user.Id);
                    context.RealtorRequests.Remove(request);
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < model.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        context.SaveChanges();
                        return RedirectToAction("ListRealtorRequests");
                    }
                }
            }
            context.SaveChanges();
            return RedirectToAction("ListRealtorRequests");
        }

        [Authorize]
        public async Task<IActionResult> CreateRealtorRequest(RealtorRequestViewModel model)
        {
            var user = await userManager.GetUserAsync(User);

            model = new RealtorRequestViewModel()
            {
                UserId = user.Id,
                UserName = user.UserName
            };

            var query = context.RealtorRequests.Include(br => br.UserRequests);

            if (!query.Any(br => br.Id == model.UserId))
            {
                var request = new RealtorRequest()
                {
                    Id = model.UserId
                };
                request.UserRequests.Add(user);
                context.RealtorRequests.Add(request);
                await context.SaveChangesAsync();

                return RedirectToAction("Index", "Profile");
            }
            else
            {
                ViewBag.ErrorMessage = "Fel: Ansökan existerar redan.";
                return RedirectToAction("Index", "Profile");
            }
        }

        public async Task SendEmailConfirmation(User user, string subject, string message, string roleName)
        {
            await emailSender.SendEmailAsync(user.Email, subject, message + $"{roleName}.");
        }
    }
}