using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webapp.Features.Agent.Home
{
    public class HomeViewModel
    {
        public UserViewModel User { get; }
        public IEnumerable<ClientViewModel> Clients { get; }
        public string GreenStatusCount { get; }
        public string UndoneStatusCount { get; }
        public string YellowStatusCount { get; }
        public string BrownStatusCount { get; }
        public string Sort { get; }
        public PageViewModel Page { get; }
    }
}