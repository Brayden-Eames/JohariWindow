using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JohariWindow.ViewModels
{
    public class SelectedAdjectiveList
    {
        public string ClientId { get; set; }

        public string[] PositiveAdjectives { get; set; }

        public string[] NegativeAdjectives { get; set; }
    }
}
