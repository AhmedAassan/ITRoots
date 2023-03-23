using ITRoots.BL.Helper;
using ITRoots.BL.Models;
using ITRoots.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITRoots.Controllers
{
    
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        public RoleManager<IdentityRole> RoleManager { get; }

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            RoleManager = roleManager;
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser()
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    IsAgree = model.IsAgree
                };

                var result =await userManager.CreateAsync(user,model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var password = await userManager.CheckPasswordAsync(user, model.Password);
                    if (password)
                    {
                        var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                        if (result.Succeeded)
                        {
                            
                            var isFirstUser = await userManager.Users.CountAsync() == 1;
                            
                            if (isFirstUser)
                            {
                                var adminRoleExists = await RoleManager.RoleExistsAsync("Admin");
                                if (!adminRoleExists)
                                {
                                    
                                    await RoleManager.CreateAsync(new IdentityRole("Admin"));
                                }

                                
                                await userManager.AddToRoleAsync(user, "Admin");
                            }

                            return RedirectToAction("Index", "Home");
                        }
                    }
                    ModelState.AddModelError("", "Invalid Password");
                }
                ModelState.AddModelError("", "Invalid Email");
            }
            return View(model);
        }




        public new async Task<IActionResult> SignOut()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }






        #region Forget Password

        public IActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var token = await userManager.GeneratePasswordResetTokenAsync(user);
                    var resetPasswordLink = Url.Action("ResetPassword", "Account", new { Email = model.Email, Token = token }, Request.Scheme);

                    var email = new Email()
                    {
                        Title = "Reset Password",
                        Body = resetPasswordLink,
                        To = model.Email
                    };
                    EmailSettings.SendEmail(email);
                    return RedirectToAction("CompleteForgetPassword");
                }
                ModelState.AddModelError(string.Empty, "Email is not Existed");
            }
            return View(model);
        }
        public IActionResult CompleteForgetPassword()
        {
            return View();
        }
        #endregion

        #region Reset Password

        public IActionResult ResetPassword(string email, string token)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordVM model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                        return RedirectToAction(nameof(ResetPasswordDone));
                    foreach (var Error in result.Errors)
                        ModelState.AddModelError(string.Empty, Error.Description);

                }
            }
            return View(model);
        }

        public IActionResult ResetPasswordDone()
        {
            return View();
        }
        #endregion

    }
}
