using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace JohariWindow.Pages.Client
{
    public class AdjectiveModel : PageModel
    {
        private readonly IUnitOfWork _unitofWork;

        public AdjectiveModel(IUnitOfWork unitofWork) => _unitofWork = unitofWork;
        //{
        //    _unitofWork = unitofWork;
        //}

        [BindProperty]
       public IList<SelectListItem> Adjectives { get; set; }

        [TempData]
        public string SelectedAdjectives { get; set; }

        [TempData]
        public string SelectedAdjectiveIDs { get; set; }

        public void OnGet()
        {
            //populate the model from the database
            List<Adjective> AdjectiveList = new List<Adjective>();
            AdjectiveList = (List<Adjective>)_unitofWork.Adjective.List(null, c => c.AdjectiveId, null);
            Adjectives = AdjectiveList.ToList<Adjective>()
                .Select(c => new SelectListItem { Text = c.AdjName, Value = c.AdjType.ToString() }) //this was c.Adjectiveid.ToString()
                .ToList<SelectListItem>();
        }

        public IActionResult OnPost()
        {
            foreach (SelectListItem Adjective in Adjectives)
            {
                if (Adjective.Selected)
                {
                    //here is where you want to send the values to the database
                    //SelectedAdjectives = _unitofWork.ClientResponse.Add();
                    SelectedAdjectives = $"{Adjective.Text}, {SelectedAdjectives}";
                    SelectedAdjectiveIDs = $"{Adjective.Value}, {SelectedAdjectiveIDs}";
                }   
            }
            SelectedAdjectives = SelectedAdjectives.TrimEnd(',');
            SelectedAdjectiveIDs = SelectedAdjectiveIDs.TrimEnd(',');
            return Page();
        }
    }
}
