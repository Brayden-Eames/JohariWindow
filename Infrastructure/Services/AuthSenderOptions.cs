using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class AuthSenderOptions
    {
        private readonly string user = "Johari Window"; // The name you want to show up on your email
                                                        // Make sure the string passed in below matches your API Key
        private readonly string key = "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";
        public string SendGridUser { get { return user; } }
        public string SendGridKey { get { return key; } }

    }

}
