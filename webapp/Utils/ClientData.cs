using webapp.Models;
using webapp.Models.ViewModels;
using System.Linq;
using webapp.Enums;
using webapp.Helpers.Atttributes;

namespace webapp.Utils
{
    public class ClientData
    {
        public static ClientDetails GetClientDetails(BusinessData data, WebappContext context)
        {
            var details = new ClientDetails
            {
                BusinessId = data.BusinessId,
                Status = EnumHelper<ClientStatus>.GetDisplayValue((ClientStatus) data.Status),
                AddDate = data.AddDate.ToString("yyyy-MM-dd"),
                CooperationType = context.CooperationTypes.Single(c => c.Id == data.CooperationTypeId).Name,
                Name = data.RecipientName,
                Street = data.Street,
                BuildingNumebr = data.BuildingNumber,
                PostCode = data.PostCode,
                Province = context.Provinces.Single(p => p.Id == data.ProvinceId).Name,
                District = context.Districts.Single(d => d.Id == data.DistrictId).Name,
                City = data.City,
                NIP = data.NIP,
                Owner = data.Owner,
                StartYear = data.StartYear.ToString(),
                Agent = context.Agents.Single(a => a.Id == data.AgentId).FullName,
                ContactPerson = data.ContactPerson,
                ContactPersonEmail = data.ContactPersonEmail,
                ContactPersonPhone = data.ContactPersonPhoneNumber,
                ContactPersonPosition = data.ContactPersonPosition,
                Email = data.Emial
            };
            var group = context.Groups.SingleOrDefault(g => g.Id == data.GroupId);
            details.Group = group != null ? group.Name : string.Empty;
            details.GroupMember = data.GroupMember ? "Tak" : "Nie";
            details.LegalForm = context.LegalForms.Single(f => f.Id == data.LegalFormId).Name;
            details.OwnerPhone = data.OwnerPhoneNumber;
            details.Phone1 = data.PhoneNumber;
            details.Phone2 = data.PhoneNumber2;
            details.Phone3 = data.PhoneNumber3;
            details.Website = data.Website;

            return details;
        }
        public static bool CanBeGreen(int visitId, WebappContext context)
        {
            var visit = context.Visits.FirstOrDefault(v => v.Id == visitId);
            if (visit == null) return false;

            var step0 = context.BusinessDatas.FirstOrDefault(b => b.BusinessId == visit.BusinessId);
            if (step0 == null) return false;

            var step1 = context.ClientSteps1.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == visit.Id);
            if (step1 == null) return false;

            if (step1.VisitMarketSegments == null || step1.VisitMarketSegments.Count < 1) return false;

            var step2 = context.ClientSteps2.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == visit.Id);
            if (step2 == null) return false;

            if (step2.Assortment.AnnualNeed < 10) return false;
            if ((step2.Assortment.PackageId == 1 || step2.Assortment.PackageId == 3) && (step2.LaxTypes == null || step2.LaxTypes.Count < 1)) return false;
            if ((step2.Assortment.PackageId == 2 || step2.Assortment.PackageId == 3) && (step2.PackedTypes == null || step2.PackedTypes.Count < 1)) return false;

            var step3 = context.ClientSteps3.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == visit.Id);
            if (step3 == null) return false;
            var sum = step3.Manufacturers.Sum(m => m.Percent) + step3.ManufacturersGroups.Where(g => !g.SelectedManufacturers).Sum(g => g.Percent);

            if (sum != 100) return false;

            if ((step3.Manufacturers == null && step3.ManufacturersGroups == null) || (step3.Manufacturers.Count < 1 && step3.ManufacturersGroups.Count < 1)) return false;

            var step5 = context.ClientSteps5.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == visit.Id);
            if (step5 == null) return false;

            return step5.Clients != null && step5.Clients.Count >= 1;
        }
    }
}