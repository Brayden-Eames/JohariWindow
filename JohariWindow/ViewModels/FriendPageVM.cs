using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JohariWindow.ViewModels
{
    public class FriendPageVM
    {
        public Friend Friend { get; set; }
        public IEnumerable<Adjective> ListOfAdjectives { get; set; }
        public string ClientId { get; set; }
        public string Relationship { get; set; }
        public int HowLong { get; set; }
        public SelectedAdjectiveList listOfSelectedAdjectives { get; internal set; }
    }
}
