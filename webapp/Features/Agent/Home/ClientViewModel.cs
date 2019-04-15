using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webapp.Enums;

namespace webapp.Features.Agent.Home
{
    public class ClientViewModel
    {
        public int Id { get; }
        public string Name { get; }
        public BusinessStatus Status { get; }
    }
}