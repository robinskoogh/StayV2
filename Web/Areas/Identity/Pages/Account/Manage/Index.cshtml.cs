// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System.ComponentModel.DataAnnotations;
using Core.Models;
using Data.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly StayContext context;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            StayContext cntxt)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            context = cntxt;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [Display(Name = "Användarnamn")]
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Telefonnummer")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Förnamn")]
            public string FirstName { get; set; } = string.Empty;

            [Display(Name = "Efternamn")]
            public string LastName { get; set; } = string.Empty;

            [Display(Name = "Adress")]
            public string Address { get; set; } = string.Empty;

            [Display(Name = "Postnummer")]
            public int ZipCode { get; set; }

            [Display(Name = "Stad")]
            public string City { get; set; } = string.Empty;

            [PersonalData]
            [DataType(DataType.Date)]
            [Display(Name = "Födelsedatum")]
            [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/mm/åååå}")]
            public DateTime DateOfBirth { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Address = user.Address,
                ZipCode = user.ZipCode,
                City = user.City,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Fel: Kunde ej hitta användare med ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Fel: Kunde ej hitta användare med ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            user.FirstName = Input.FirstName;
            user.LastName = Input.LastName;
            user.Address = Input.Address;
            user.ZipCode = Input.ZipCode;
            user.City = Input.City;
            user.DateOfBirth = Input.DateOfBirth;

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Fel: Oväntat fel vid ändring av telefonnummer.";
                    return RedirectToPage();
                }
            }
            await context.SaveChangesAsync();
            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Din profil har blivit uppdaterad!";
            return RedirectToPage();
        }
    }
}