using MembershipProjectApp.Areas.Identity.Pages.Account;
using MembershipProjectApp.Data;
using MembershipProjectApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MembershipProjectApp.Controllers
{
    [Authorize]
    public class ManagementController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<RegisterModel> _logger;
        string ReturnUrl;
        private readonly ApplicationDbContext _db;

        [TempData]
        public string ErrorMessage { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
        public ManagementController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _db = context;
        }

        public IActionResult Index()
        {
            ViewBag.TotalMembers = _db.Memberships.Count();
            ViewBag.FoundingMembers = _db.Memberships.Count(x => x.Membership == "Founding Member");
            ViewBag.NewMembers = _db.Memberships.Count(x => x.Membership == "New Member");
            return View();
        }

        List<EditUserViewModel> _applicationUserViewModels = new List<EditUserViewModel>();
        public IActionResult UserList()
        {
            var applicationUserViewModels = new EditUserViewModel();
            var result = _db.AspNetUsers.ToList();
            ViewBag.TotalMembers = _db.Memberships.Count();
            ViewBag.TotalUsers = _db.AspNetUsers.Count();
            ViewBag.FoundingMembers = _db.Memberships.Count(x => x.Membership == "Founding Member");
            foreach (var item in result)
            {
                applicationUserViewModels = new EditUserViewModel()
                {
                    Id = item.Id,
                    FullName = item.FullName,
                    Gender = item.Gender,
                    Contact = item.Contact,
                    UserName = item.UserName,
                    Email = item.Email
                };
                _applicationUserViewModels.Add(applicationUserViewModels);
            }
            return View(_applicationUserViewModels);
        }

        //public async Task<IActionResult> UserList()
        //{
        //    var sql = await _db.AspNetUsers.ToListAsync();
        //    ViewBag.TotalMembers = _db.Memberships.Count();
        //    ViewBag.TotalUsers = _db.AspNetUsers.Count();
        //    return View(sql);
        //}

        // DETAILS METHOD
        public async Task<IActionResult> UserDetails(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var membershipModel = await _db.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (membershipModel == null)
            {
                return NotFound();
            }

            return View(membershipModel);
        }


        // CREATE METHOD
        public async Task<IActionResult> CreateUser(string id, string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            RegisterViewModel registerUserViewModel = new RegisterViewModel();
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return PartialView(registerUserViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/Management/UserList");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser 
                { 
                    FullName = registerViewModel.FullName,
                    Gender = registerViewModel.Gender,
                    Contact = registerViewModel.Contact,
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                };

                var result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = registerViewModel.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return View();
        }


        public async Task<IActionResult> EditUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _db.AspNetUsers.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(ApplicationUser model)
        {
            var user = await _userManager.FindByIdAsync(model.Id);

            if(user == null)
            {
                return NotFound();
            }
            else
            {
                user.FullName = model.FullName;
                user.Gender = model.Gender;
                user.Contact = model.Contact;
                user.UserName = model.Email;
                user.Email = model.Email;

                var res = await _userManager.UpdateAsync(user);

                if (res.Succeeded)
                {
                    return RedirectToAction("UserList");
                }
                return View(model);
            }
        }




        // DELETE METHODS
        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _db.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string Id)
        {
            var userModel = await _db.AspNetUsers.FindAsync(Id);
            _db.AspNetUsers.Remove(userModel);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(UserList));
        }

        private bool ManagementModelExists(string id)
        {
            return _db.AspNetUsers.Any(e => e.Id == id);
        }
    }
}
