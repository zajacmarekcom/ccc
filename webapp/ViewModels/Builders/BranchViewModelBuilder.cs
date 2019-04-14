using System.Collections.Generic;
using System.Linq;
using webapp.Models;

namespace webapp.ViewModels.Builders
{
    public class BranchViewModelBuilder
    {
        public static IEnumerable<BranchViewModel> CreateBranchViewModels(IEnumerable<Branch> branches, WebappContext context)
        {
            var model = branches.Select(x => CreateVm(x, context));

            return model;
        }

        public static BranchViewModel CreateVm(Branch branch, WebappContext context)
        {
            var vm = new BranchViewModel()
            {
                Name = branch.Name,
                BuildingNumber = branch.BuildingNumber,
                City = branch.City,
                Street = branch.Street,
                Province = context.Provinces.Single(x => x.Id == branch.ProvinceId).Name,
                District = context.Districts.Single(x => x.Id == branch.DistrictId).Name,
                Comments = branch.Comments,
                Phone = branch.PhoneNumber
            };

            return vm;
        }
    }
}
