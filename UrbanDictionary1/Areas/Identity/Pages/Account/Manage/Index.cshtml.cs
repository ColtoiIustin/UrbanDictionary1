// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UrbanDictionary1.Areas.Identity.Data;
using UrbanDictionary1.Data.Services;

namespace UrbanDictionary1.Areas.Identity.Pages.Account.Manage
{
    [Authorize(Roles = "Admx")]
    public class IndexModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IExpressionsService _service;

        public IndexModel(
            UserManager<ApplicationUser> userManager,
            IExpressionsService service,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _service = service; 
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
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
            [Required(ErrorMessage = "Camp obligatoriu")]
            [Display(Name = "Noul Username")]
            public string NewUsername { get; set; }
        }

        [Authorize]
        private async Task LoadAsync(ApplicationUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            

            Username = userName;

            Input = new InputModel
            {
                
            };
        }

        
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        
        public async Task<IActionResult> OnPostAsync()
        {
            
            var user = await _userManager.GetUserAsync(User);
            
            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var newUsername = await _userManager.GetUserNameAsync(user);
            if (Input.NewUsername != newUsername)
            {
                user.UserName = Input.NewUsername; 
                var setUserNameResult = await _userManager.SetUserNameAsync(user, Input.NewUsername);
                if (!setUserNameResult.Succeeded)
                {
                    StatusMessage = "Eroare neasteptata la schimbarea UserName-ului";
                    return RedirectToPage("/Expressions/Index");
                }
                else
                {
                    _service.ChangeAuthor(newUsername, Input.NewUsername);
                }
            }
            
            
            
            StatusMessage = "Profilul tau a fost updatat";
     
            return RedirectToPage();
        }
    }
}
