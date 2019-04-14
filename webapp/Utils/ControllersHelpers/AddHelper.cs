using webapp.Models;
using webapp.Models.ViewModels;
using System.Linq;

namespace webapp.Utils.ControllersHelpers
{
    public class AddHelper
    {
        public static AddActionAdditionalData FillAdditionalData(WebappContext context, int? provinceId)
        {
            var data = new AddActionAdditionalData()
            {
                Districts = context.Districts.Where(d => d.ProvinceId == provinceId).ToList(),
                Provinces = context.Provinces.ToList(),
                Agents = context.Agents.ToList(),
                LegalForms = context.LegalForms.ToList(),
                Groups = context.Groups.ToList()
            };

            return data;
        }
    }
}