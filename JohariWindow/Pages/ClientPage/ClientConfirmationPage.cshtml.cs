using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JohariWindow.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace JohariWindow.Pages.ClientPage
{
    public class ClientConfirmationPageModel : PageModel
    {
        [BindProperty]
        public ClientPageVM ClientObject { get; set; }
        public void OnGet()
        {
            ClientObject = new ClientPageVM()
            {
                Client = new ApplicationCore.Models.Client(),
                ClientId = User.FindFirstValue(ClaimTypes.NameIdentifier)
        };
            
        }
    }
}
