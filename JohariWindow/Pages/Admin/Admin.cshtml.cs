using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using JohariWindow.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace JohariWindow.Pages.Admin
{
    public class AdminModel : PageModel
    {

        private readonly IUnitOfWork _unitofWork;
        public AdminModel(IUnitOfWork unitofWork) => _unitofWork = unitofWork;
        [BindProperty]
        public AdminPageVM AdminPageObject { get; set; }
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
                    ClientId = client.Id
                });
            }
        }
    }
}
