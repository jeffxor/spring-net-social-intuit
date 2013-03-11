using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Intuit.Sb.Cdm.V2;

namespace Spring.Social.Intuit.Api
{
    public class IntuitProfile
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }

        public IntuitProfile(User user)
        {
            Name = user.ScreenName;
            FirstName = user.FirstName;
            LastName = user.LastName;
            EmailAddress = user.EmailAddress;
        }
    }
}
