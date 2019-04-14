using System;

namespace webapp.Models.ViewModels
{
    [Serializable]
    public class BranchDataViewModel
    {
        public string Name { get; set; }
        public string Street { get; set; }
        public string BuildingNumber { get; set; }
        public string PostCode { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int BusinessId { get; set; }
        public int BranchId { get; set; }
    }
}