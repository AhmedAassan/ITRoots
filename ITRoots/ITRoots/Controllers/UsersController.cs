using ITRoots.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ITRoots.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            return View(users);
        }

        public async Task<IActionResult> Details(string id, string viewName = "Details")
        {
            if (id == null)
                return NotFound();
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            return View(viewName, user);

        }

        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] string id, ApplicationUser updatedUser)
        {
            if (id != updatedUser.Id)
                return BadRequest();
            if (ModelState.IsValid)  // Server Side Validation
            {
                try
                {
                    var user = await userManager.FindByIdAsync(id);
                    user.UserName = updatedUser.UserName;
                    user.NormalizedUserName = updatedUser.UserName.ToUpper();
                    user.PhoneNumber = updatedUser.PhoneNumber;
                    

                    var result = await userManager.UpdateAsync(user);
                    if(result.Succeeded)
                        return RedirectToAction("Index");
                    foreach (var error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return View(updatedUser);

        }

        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete([FromRoute] string id, ApplicationUser deletedUser)
        {
            if(id != deletedUser.Id)
                return BadRequest();
            try
            {
                var user = await userManager.FindByIdAsync(deletedUser.Id);
                var result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(deletedUser);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
