using System;
using webapp.Enums;

namespace webapp.ViewModels.Client
{
    public class NewClientViewModel
    {
        public int Id { get; set; }

        public ClientStatus Status { get; set; }

        public CooperationType CooperationType { get; set; }

        public DateTime AddDate { get; set; }

        public string Name { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public string PostCode { get; set; }
    }
}