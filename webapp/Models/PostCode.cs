using System.ComponentModel.DataAnnotations;

namespace webapp.Models
{
    public class PostCode
    {
        [Key]
        public string PostCodeNumber { get; set; }
        public string City { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
    }
}