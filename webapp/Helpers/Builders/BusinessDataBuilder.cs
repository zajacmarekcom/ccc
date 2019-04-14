using webapp.Models;
using webapp.Models.ViewModels;

namespace webapp.Helpers.Builders
{
    public class BusinessDataBuilder
    {
        public static BusinessData CreateBusinessDataFromBranchDataViewModel(BranchDataViewModel data)
        {
            var model = new BusinessData()
            {
                RecipientName = data.Name,
                ProvinceId = data.ProvinceId,
                DistrictId = data.DistrictId,
                PostCode = data.PostCode,
                PhoneNumber = data.PhoneNumber,
                City = data.City,
                Street = data.Street,
                BuildingNumber = data.BuildingNumber,
                Emial = data.Email,
                IsBranch = true
            };

            return model;
        }
    }
}