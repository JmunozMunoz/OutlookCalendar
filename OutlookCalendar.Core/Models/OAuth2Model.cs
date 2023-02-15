using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutlookCalendar.Domain.Core.Models
{
    public class OAuth2Model
    {
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public int ListenPort { get; set; }        
        public string AuthorizationEndpoint { get; set; }
        public string TokenEndpoint { get; set; }
        public string Scope { get; set; }
    }
}
