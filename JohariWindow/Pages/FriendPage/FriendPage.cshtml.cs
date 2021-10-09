using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationCore.Models;
using JohariWindow.ViewModels;
using Microsoft.AspNetCore.Identity;

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
                HowLong = 0,
                Relationship = "",
                listOfSelectedAdjectives = new SelectedAdjectiveList()
                {
                    PositiveAdjectives = new string[12],
                    NegativeAdjectives = new string[7]
                }
            };
            return Page();
        }

        public async Task<IActionResult> OnPost(string? clientID, string? relationship, int howLong) 
        {
            var clientId = FriendObject.ClientId;
            var friendRelationship = FriendObject.Relationship;
            var friendHowLong = FriendObject.HowLong;

            if (!ModelState.IsValid)
            {
                return Page();
            }
            var clientUser = _unitofWork.Client.Get(c=> c.Id == clientId);

            _unitofWork.Friend.Add(new ApplicationCore.Models.Friend() //creates the 'friend' and their relationship info in the Friend table. 
            {
                Relationship = friendRelationship,
                howLong = friendHowLong
            });
            await _unitofWork.CommitAsync(); //commits the stuff to the Friend Table before doing the FriendResponse table insert. 

            var storedFriendId = _unitofWork.Friend.Get(f => f.Relationship == friendRelationship && f.howLong == friendHowLong);

            foreach (string id in Adjectives)
            {
                _unitofWork.FriendResponse.Add(new ApplicationCore.Models.FriendResponse()
                {
                    Adjective = _unitofWork.Adjective.Get(a => a.AdjectiveId == int.Parse(id)),
                    Client = clientUser,
                    Friend = _unitofWork.Friend.Get(f => f.FriendId == storedFriendId.FriendId)
                });
            }
            _unitofWork.Commit();
            return RedirectToPage("./FriendConfirmationPage");
        }
    }
}

