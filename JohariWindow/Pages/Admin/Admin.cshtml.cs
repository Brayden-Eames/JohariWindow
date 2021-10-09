using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using JohariWindow.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JohariWindow.Pages.Admin
{
    public class AdminModel : PageModel
    {
        [BindProperty]
        public AdminPageVM AdminPageObject { get; set; }

        private readonly IUnitOfWork _unitofWork;
        public AdminModel(IUnitOfWork unitofWork) => _unitofWork = unitofWork;

        public void OnGet()
        {
            IEnumerable<ApplicationCore.Models.Client> clients = _unitofWork.Client.List();

            AdminPageObject = new AdminPageVM()
            {
                ClientListData = new List<ClientsListData>(),
                selectedClientId = ""
            };

            foreach (ApplicationCore.Models.Client client in clients)
            {
                AdminPageObject.ClientListData.Add(new ClientsListData()
                {
                    ClientFullName = client.FirstName + " " + client.LastName,
                    ClientId = client.Id,
                    
                });
            }
        }
        public IActionResult OnPostBuildJohari(string clientId)
        {
            IEnumerable<ApplicationCore.Models.Client> clients = _unitofWork.Client.List();

            AdminPageObject = new AdminPageVM()
            {
                Client = _unitofWork.Client.Get(c => c.Id == clientId),
                ClientListData = new List<ClientsListData>(),
                OpenSelfSlot = new List<AdjectiveDataStruct>(),
                HiddenSelfSlot = new List<AdjectiveDataStruct>(),
                BlindSelfSlot = new List<AdjectiveDataStruct>(),
                UnknownSelfSlot = new List<AdjectiveDataStruct>(),
            };

            foreach (ApplicationCore.Models.Client client in clients)
            {
                AdminPageObject.ClientListData.Add(new ClientsListData()
                {
                    ClientFullName = client.FirstName + " " + client.LastName,
                    ClientId = client.Id,
                });
            }

            List<ClientResponse> clientResponses = (_unitofWork.ClientResponse.List(cr => cr.Client.Id == AdminPageObject.Client.Id)).ToList();
            List<FriendResponse> friendResponses = (_unitofWork.FriendResponse.List(fr => fr.Client.Id == AdminPageObject.Client.Id)).ToList();
            List<Adjective> adjectives = (_unitofWork.Adjective.List()).ToList();

            var openSelfQuery = from adjective in adjectives
                                join cr in clientResponses on adjective.AdjectiveId equals cr.Adjective.AdjectiveId into crAdjTbl
                                from subCr in crAdjTbl

                                join fr in friendResponses on subCr.Adjective.AdjectiveId equals fr.Adjective.AdjectiveId into frSubCrTbl

                                where frSubCrTbl.Count() > 0
                                select new
                                {
                                    name = adjective.AdjName,
                                    definition = adjective.AdjDefinition,
                                    type = adjective.AdjType,
                                    count = frSubCrTbl.Count()
                                };

            foreach (var query in openSelfQuery)
            {
                AdminPageObject.OpenSelfSlot.Add(new AdjectiveDataStruct()
                {
                    Adjective = query.name,
                    chosenCount = query.count
                });
            }

            var hiddenSelfQuery = from cr in clientResponses

                                  join fr in friendResponses on cr.Adjective.AdjectiveId equals fr.Adjective.AdjectiveId into crFrTbl
                                  from crFr in crFrTbl.DefaultIfEmpty()

                                  join adj in adjectives on cr.Adjective.AdjectiveId equals adj.AdjectiveId into crFrAdjTbl
                                  from crFrAdj in crFrAdjTbl.DefaultIfEmpty()

                                  where crFr == null
                                  select new
                                  {
                                      AdjectiveName = crFrAdj.AdjName,
                                      AdjectiveType = crFrAdj.AdjType,
                                      AdjectiveDefinition = crFrAdj.AdjDefinition
                                  };



            foreach (var query in hiddenSelfQuery)
            {
                AdminPageObject.HiddenSelfSlot.Add(new AdjectiveDataStruct()
                {
                    Adjective = query.AdjectiveName,
                    chosenCount = 0
                });
            }


            var blindSelfQuery = from fr in friendResponses

                                 join cr in clientResponses on fr.Adjective.AdjectiveId equals cr.Adjective.AdjectiveId into crFrTbl
                                 from crFr in crFrTbl.DefaultIfEmpty()

                                 join adj in adjectives on fr.Adjective.AdjectiveId equals adj.AdjectiveId into crFrAdjTbl
                                 from crFrAdj in crFrAdjTbl.DefaultIfEmpty()

                                 where crFr == null

                                 group crFrAdj by crFrAdj.AdjectiveId into orderedTbl

                                 orderby orderedTbl.Key
                                 select orderedTbl;

            foreach (var group in blindSelfQuery)
            {
                AdjectiveDataStruct tempAdj = new AdjectiveDataStruct
                {
                    chosenCount = group.Count()
                };

                foreach (var query in group)
                {
                    tempAdj.Adjective = query.AdjName;
                    break;
                }
                AdminPageObject.BlindSelfSlot.Add(tempAdj);
            }

            var unknownSelfQuery = from adjective in adjectives
                                   join fr in friendResponses on adjective.AdjectiveId equals fr.Adjective.AdjectiveId into frAdjTbl
                                   from frAdj in frAdjTbl.DefaultIfEmpty()

                                   join cr in clientResponses on adjective.AdjectiveId equals cr.Adjective.AdjectiveId into crAdjTbl
                                   from crAdj in crAdjTbl.DefaultIfEmpty()

                                   where frAdj == null && crAdj == null

                                   select new
                                   {
                                       AdjectiveName = adjective.AdjName,
                                       AdjectiveType = adjective.AdjType,
                                       AdjectiveDefinition = adjective.AdjType
                                   };

            foreach (var query in unknownSelfQuery)
            {
                AdminPageObject.UnknownSelfSlot.Add(new AdjectiveDataStruct()
                {
                    Adjective = query.AdjectiveName,
                    chosenCount = 0
                });
            }
            return Page();
        }
    }
}
