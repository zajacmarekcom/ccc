using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class AgentBusinessData
    {
        public string AgentFullName { get; set; }
        public List<BusinessItem> Businesses { get; set; }
    }
}