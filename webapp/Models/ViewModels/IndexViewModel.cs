using System.Collections.Generic;
using webapp.Security;

namespace webapp.Models.ViewModels
{
    public class IndexViewModel
    {
        public int[] Status {get;set;}
        public ICollection<BusinessItem> Items { get; set; }
        public string Sort { get; set; }
        public int Page { get; set; }
        public int PageCount { get; set; }
        public CustomIdentity CurrentUser { get; set; }
    }
}