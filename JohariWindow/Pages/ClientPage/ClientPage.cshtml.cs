using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using JohariWindow.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Security.Claims;

namespace JohariWindow.Pages.ClientPage
{
    public class AdjectiveModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;
        
        
        public AdjectiveModel(IUnitOfWork unitofWork) => _unitofWork = unitofWork;

        [BindProperty]
        public ClientPageVM ClientObject{ get; set; }

        [BindProperty]
        public string[] Adjectives { get; set; }

        
        public IActionResult OnGet(string? clientId) 
        {
            var listOfAdjectives = _unitofWork.Adjective.List();
            var clientID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            clientId = clientID;
            var client = _unitofWork.Client.Get(c => c.Id == clientId);
            var clientResponses = _unitofWork.ClientResponse.Get(c => c.Client.Id == clientId);

            if (String.IsNullOrEmpty(clientId))
            {
                return NotFound();
            }

            ClientObject = new ClientPageVM()
            {
                Client = new ApplicationCore.Models.Client(),
                ListOfAdjectives = listOfAdjectives,
                ClientId = clientId,
                listOfSelectedAdjectives = new SelectedAdjectiveList()
                {
                    PositiveAdjectives = new string[12],
                    NegativeAdjectives = new string[7]
                },
                hasSubmited = false               
            };

            if (clientResponses != null) //checks to see if the client has submitted responses previously by retrieving data from the Db. 
            {
                ClientObject.hasSubmited = true;
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            var clientUser = _unitofWork.Client.Get(c => c.Id == ClientObject.ClientId);
            var clientChoices = _unitofWork.ClientResponse.List(c=>c.Client.Id == clientUser.Id);

            if(clientChoices != null)
            {
                _unitofWork.ClientResponse.Delete(clientChoices);
            }

            if(!ModelState.IsValid)
            {
                return Page();
            }

            

            foreach (string id in Adjectives)
            {
                _unitofWork.ClientResponse.Add(new ApplicationCore.Models.ClientResponse()
                {
                    Client = clientUser,
                    Adjective = _unitofWork.Adjective.Get(a => a.AdjectiveId == int.Parse(id))
                });
            }

            _unitofWork.Commit();

            return RedirectToPage("./ClientConfirmationPage");
            
        }
    }
}
