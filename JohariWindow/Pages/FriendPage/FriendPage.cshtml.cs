using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationCore.Models;
using JohariWindow.ViewModels;

namespace JohariWindow.Pages.FriendPage
{
    public class AdjectiveModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;

        public AdjectiveModel(IUnitOfWork unitofWork) => _unitofWork = unitofWork;


        [BindProperty]
        public FriendPageVM FriendObject { get; set; }

        [BindProperty]
        public string[] Adjectives { get; set; }


        public IActionResult OnGet()
        {
            var listOfAdjectives = _unitofWork.Adjective.List();


            FriendObject = new FriendPageVM()
            {
                Friend = new ApplicationCore.Models.Friend(),
                ListOfAdjectives = listOfAdjectives,
                listOfSelectedAdjectives = new SelectedAdjectiveList()
                {
                    PositiveAdjectives = new string[12],
                    NegativeAdjectives = new string[5]
                }
            };
            return Page();
        }

        public IActionResult OnPost(string? clientID, string? relationship, int howLong) //need to pass in clientId from the text input field on the Friend page to OnPost()
        {
            //first thing we need to do is add the responses (relationship and tenure #s) to the 'Friend' table. 
            //then submit the adjective choices (also using the ClientId provided in text field) to the FriendResponse Table. 

            var clientId = clientID;
            var friendRelationship = relationship;
            var friendHowLong = howLong;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (String.IsNullOrEmpty(clientId))
            {
                return NotFound();
            }

            var clientUser = _unitofWork.Client.Get(c=> c.Id == clientId);

            _unitofWork.Friend.Add(new ApplicationCore.Models.Friend() //creates the 'friend' and their relationship info in the Friend table. 
            {
                Relationship = friendRelationship,
                howLong = friendHowLong
            });
            _unitofWork.Commit(); //commits the stuff to the Friend Table before doing the FriendResponse table insert. 

            foreach (string id in Adjectives)
            {
                _unitofWork.FriendResponse.Add(new ApplicationCore.Models.FriendResponse()
                {
                    Adjective = _unitofWork.Adjective.Get(a => a.AdjectiveId == int.Parse(id)),
                    Client = clientUser, 
                    Friend = _unitofWork.Friend.Get(f => f.FriendId == int.Parse(id))
                });
            }

            _unitofWork.Commit();

            return RedirectToPage("./ClientConfirmationPage");
        }
    }
}

