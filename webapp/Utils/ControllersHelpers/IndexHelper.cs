using webapp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using webapp.Models.Dto.Business;
using webapp.Security;

namespace webapp.Utils.ControllersHelpers
{
    public class IndexHelper
    {
        public static IEnumerable<BusinessDataDto> GetAllBusinesses(IPrincipal user, WebappContext context)
        {
            return context.Businesses.Where(b => b.CreatorId == ((CustomPrincipal)user).CustomIdentity.UserId)
                .Join(context.BusinessDatas, d => d.Id, d => d.BusinessId, (b, d) => new BusinessDataDto()
                {
                    Business = b,
                    Data = d
                }).ToList();
            //return context.Businesses.Where(b => b.CreatorId == userId).ToList();
        }
    }
}