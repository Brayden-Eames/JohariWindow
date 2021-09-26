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
        private readonly UserManager<Client> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UpdateModel(IUnitOfWork unitOfWork, UserManager<Client> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public Client AppUser { get; set; }

        public List<string> UsersRoles { get; set; }
        public List<string> AllRoles { get; set; }

        public List<string> OldRoles { get; set; }
        public async Task OnGetAsync(string id)
        {
            AppUser = _unitOfWork.Client.Get(u => u.Id == id); //gets the user from the ASPNetUser table (single row) and places in object AppUser
            var roles = await _userManager.GetRolesAsync(AppUser); //this retrieves the roles from the ASPNetUserRoles table (single row) and places them in roles object. 
            UsersRoles = roles.ToList(); //cast roles object to string list
            OldRoles = roles.ToList(); //cast roles object to string list
            AllRoles = _roleManager.Roles.Select(r => r.Name).ToList(); //gets all possible roles and stores them in string list
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var newRoles = Request.Form["roles"]; //grabs all roles visible on the html page
            UsersRoles = newRoles.ToList(); //cast the user roles to a string list
            var oldRoles = await _userManager.GetRolesAsync(AppUser); //gets the user's roles from the ASPNetUserRoles Table (old ones)
            OldRoles = oldRoles.ToList(); //cast old roles to string list
            var rolesToAdd = new List<string>(); //creates new string list for which roles need to be added to the ASPNetUserRoles table. 
            var user = _unitOfWork.Client.Get(u => u.Id == AppUser.Id); //gets the user from the ASPNetUser table through the Client model
            user.FirstName = AppUser.FirstName; //updates firstname 
            user.LastName = AppUser.LastName; //updates lastname 
            user.Email = AppUser.Email; //updates email address
            user.PhoneNumber = AppUser.PhoneNumber; //updates phone number
            _unitOfWork.Client.Update(user); //updates the entire user object
            _unitOfWork.Commit(); //commits the changed user data to the ASPNetUser table 

            foreach (var r in UsersRoles)  //loops through the string list UserRoles (the currently assigned roles)
            {
                if (!OldRoles.Contains(r)) //if the role is NOT contained in the oldRoles, add it to the rolesToAdd string list
                {
                    rolesToAdd.Add(r); //adds the new roles (stored in r) to the rolesToAdd list
                }
            }
            foreach (var r in OldRoles) //loops through string list of OldRoles. 
            {
                if (!UsersRoles.Contains(r)) //if the new list of roles UserRoles does NOT contain the current role (r), remove it from the user.
                {
                    var result = await _userManager.RemoveFromRoleAsync(user, r); //removes old roles from the user (based on values in oldRoles)
                }
            }
            var result1 = await _userManager.AddToRolesAsync(user, rolesToAdd.AsEnumerable()); //Adds the roles to the userManager Roles table
            return RedirectToPage("./Index", new { success = true, message = "Update Successful" }); //return to the page
        }
    }
}

