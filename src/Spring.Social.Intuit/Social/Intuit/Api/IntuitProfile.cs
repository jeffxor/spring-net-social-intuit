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
        public string Ticket { get; set; }
        public string AgentId { get; set; }
        public QboUserCompanyMapping CurrentCompany { get; set; }

        public IntuitProfile(QboUser qboUser)
        {
            Name = qboUser.LoginName;
            Ticket = qboUser.Ticket;
            AgentId = qboUser.AgentId;
            CurrentCompany = qboUser.CurrentCompany;
        }
    }
}
