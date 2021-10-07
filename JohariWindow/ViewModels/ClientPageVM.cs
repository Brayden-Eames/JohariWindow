using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JohariWindow.ViewModels
{
    public class ClientPageVM
    {
        public Client Client { get; set; }
        public IEnumerable<Adjective> ListOfAdjectives { get; set; }
        public string ClientId { get; set; }
        public SelectedAdjectiveList listOfSelectedAdjectives { get; internal set; }
        public bool hasSubmited { get; set; }


    }
}
