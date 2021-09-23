using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;

namespace JohariWindow.Pages.Admin.User
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Client> _userManager;
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(UserManager<Client> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Client> ApplicationUsers { get; set; }
        public Dictionary<string, List<string>> UserRoles { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }

        public async Task OnGetAsync(bool success = false, string message = null)
        {
            Success = success;
            Message = message;
            UserRoles = new Dictionary<string, List<string>>();
            ApplicationUsers = _unitOfWork.Client.List();
            foreach (var user in ApplicationUsers)
            {
                var userRole = await _userManager.GetRolesAsync(user);
                UserRoles.Add(user.Id, userRole.ToList());
            }
        }
        public async Task<IActionResult> OnPostLockUnlock(string id)
        {
            var user = _unitOfWork.Client.Get(u => u.Id == id);
            if (user.LockoutEnd == null)
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
            }
            else if (user.LockoutEnd > DateTime.Now)
            {
                user.LockoutEnd = DateTime.Now;
            }
            else
            {
                user.LockoutEnd = DateTime.Now.AddYears(100);
            }
            _unitOfWork.Client.Update(user);
            await _unitOfWork.CommitAsync();
            return RedirectToPage();
        }
    }
}


