using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationCore.Interfaces;

namespace JohariWindow.Pages.Admin.User
{
    public class UpdateModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UpdateModel(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public ApplicationUser AppUser { get; set; }

        public List<string> UsersRoles { get; set; }
        public List<string> AllRoles { get; set; }

        public List<string> OldRoles { get; set; }
        public async Task OnGetAsync(string id)
        {
            AppUser = _unitOfWork.ApplicationUser.Get(u => u.Id == id);
            var roles = await _userManager.GetRolesAsync(AppUser);
            UsersRoles = roles.ToList();
            OldRoles = roles.ToList();
            AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var newRoles = Request.Form["roles"];
            UsersRoles = newRoles.ToList();
            var oldRoles = await _userManager.GetRolesAsync(AppUser);
            OldRoles = oldRoles.ToList();
            var rolesToAdd = new List<string>();
            var user = _unitOfWork.ApplicationUser.Get(u => u.Id == AppUser.Id);
            user.FirstName = AppUser.FirstName;
            user.LastName = AppUser.LastName;
            user.Email = AppUser.Email;
            user.PhoneNumber = AppUser.PhoneNumber;
            _unitOfWork.ApplicationUser.Update(user);
            _unitOfWork.Commit();

            foreach (var r in UsersRoles)
            {
                if (!OldRoles.Contains(r))
                {
                    rolesToAdd.Add(r);
                }
            }
            foreach (var r in OldRoles)
            {
                if (!UsersRoles.Contains(r))
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, r);
                }
            }
            var result1 = await _userManager.AddToRolesAsync(user, rolesToAdd.AsEnumerable());
            return RedirectToPage("./Index", new { success = true, message = "Update Successful" });
        }
    }
}

