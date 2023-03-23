using ITRoots.BL.Models;
using ITRoots.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
namespace ITRoots.Controllers
{

    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        #region Properties
        public RoleManager<IdentityRole> RoleManager { get; }
        public UserManager<ApplicationUser> UserManager { get; }

        #endregion

        #region Constructors
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            RoleManager = roleManager;
            UserManager = userManager;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            var Roles = RoleManager.Roles;
            return View(Roles);

        }
        public async Task<IActionResult> Details(string id, string ViewName = "Details")
        {
            if (id == null)
                return NotFound();
            var role = await RoleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            return View(ViewName, role);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole model)
        {
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    var Result = await RoleManager.CreateAsync(model);
                    if (Result.Succeeded)
                        return RedirectToAction("Index");
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, IdentityRole model)
        {
            if (id != model.Id)
                return NotFound();
            if (ModelState.IsValid) // Server Side Validation
            {
                try
                {
                    var role = await RoleManager.FindByIdAsync(id);
                    role.Name = model.Name;
                    role.NormalizedName = model.Name.ToUpper();

                    var Result = await RoleManager.UpdateAsync(role);
                    if (Result.Succeeded)
                        return RedirectToAction("Index");
                    foreach (var error in Result.Errors)
                        ModelState.AddModelError("", error.Description);
                    return View(model);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            return await Details(id, "Delete");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id, IdentityRole role)
        {
            if (id != role.Id)
                return BadRequest();
            try
            {
                var Result = await RoleManager.DeleteAsync(role);
                if (Result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in Result.Errors)
                    ModelState.AddModelError("", error.Description);
                return View(role);
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<IActionResult> AddOrRemoveUsers(string RoleId)
        {
            var role = await RoleManager.FindByIdAsync(RoleId);
            if (role == null)
                return NotFound();
            ViewBag.RoleId = RoleId;
            var users = new List<UserInRoleVM>();
            foreach (var user in UserManager.Users)
            {
                var UserInRole = new UserInRoleVM
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await UserManager.IsInRoleAsync(user, role.Name))
                    UserInRole.IsSelected = true;
                else
                    UserInRole.IsSelected = false;
                users.Add(UserInRole);
            }
            return View(users);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrRemoveUsers(List<UserInRoleVM> model, string RoleId)
        {
            var role = await RoleManager.FindByIdAsync(RoleId);
            if (role == null)
                return BadRequest();
            if (ModelState.IsValid)
            {
                foreach (var item in model)
                {
                    var user = await UserManager.FindByIdAsync(item.UserId);
                    if (user != null)
                    {
                        if (item.IsSelected && !(await UserManager.IsInRoleAsync(user, role.Name)))
                            await UserManager.AddToRoleAsync(user, role.Name);
                        else if (!item.IsSelected && (await UserManager.IsInRoleAsync(user, role.Name)))
                            await UserManager.RemoveFromRoleAsync(user, role.Name);
                    }
                }
                return RedirectToAction(nameof(Edit), new { id = RoleId });
            }
            return View(model);
        }

        #endregion
    }
}
