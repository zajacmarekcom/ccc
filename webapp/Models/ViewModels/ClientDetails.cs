using System.Collections.Generic;

namespace webapp.Models.ViewModels
{
    public class ClientDetails
    {
        public int BusinessId { get; set; }
        public string Status { get; set; }
        public string CooperationType { get; set; }
        public string AddDate { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string BuildingNumebr { get; set; }
        public string PostCode { get; set; }
        public string Province { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string NIP { get; set; }
        public string LegalForm { get; set; }
        public string StartYear { get; set; }
        public string GroupMember { get; set; }
        public string Group { get; set; }
        public string Owner { get; set; }
        public string OwnerPhone { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Phone3 { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Agent { get; set; }
        public string ContactPerson { get; set; }
        public string ContactPersonPosition { get; set; }
        public string ContactPersonEmail { get; set; }
        public string ContactPersonPhone { get; set; }
        public IEnumerable<Visit> Visits { get; set; } 
    }
}