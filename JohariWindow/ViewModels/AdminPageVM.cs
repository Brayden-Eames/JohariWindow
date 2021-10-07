using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JohariWindow.ViewModels
{
    public class AdminPageVM
    {
        public Client Client { get; set; }
        public List<ClientsListData> ClientListData { get; set; }
        public string selectedClientId { get; set; }
        public List<AdjectiveDataStruct> OpenSelfSlot { get; set; }
        public List<AdjectiveDataStruct> BlindSelfSlot { get; set; }
        public List<AdjectiveDataStruct> HiddenSelfSlot { get; set; }
        public List<AdjectiveDataStruct> UnknownSelfSlot { get; set; }
    }

    public struct ClientsListData
    {
        public string ClientFullName { get; set; }
        public string ClientId { get; set; }
    }

    public struct AdjectiveDataStruct
    {
        public string Adjective { get; set; }
        public int chosenCount { get; set; }
    }
}
