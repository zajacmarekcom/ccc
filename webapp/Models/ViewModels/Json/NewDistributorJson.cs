using System.Collections.Generic;

namespace webapp.Models.ViewModels.Json
{
    public class NewDistributorJson
    {
        public NewDistributorJson()
        {
            ErrorsList = new List<string>();
        }

        public bool Error { get; set; }
        public ICollection<string> ErrorsList { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}