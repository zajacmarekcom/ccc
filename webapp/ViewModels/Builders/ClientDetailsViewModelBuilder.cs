using System.Linq;
using webapp.Enums;
using webapp.Models;

namespace webapp.ViewModels.Builders
{
    class ClientDetailsViewModelBuilder
    {
        public static ClientDetailsViewModel CreateClientDetailsViewModel(int? businessId, WebappContext context)
        {
            if (businessId == null) return null;
            var business = context.Businesses.SingleOrDefault(x => x.Id == businessId);
            if (business == null) return null;
            var model = new ClientDetailsViewModel()
            {
                Business = business
            };

            AddBusinessDataToVm(model, context.BusinessDatas.SingleOrDefault(x => x.BusinessId == business.Id), context);
            AddVisitsToVm(model, context);

            return model;
        }

        private static void AddBusinessDataToVm(ClientDetailsViewModel model, BusinessData data, WebappContext context)
        {
            if (data == null) return;

            model.Status = (ClientStatus)data.Status;
            model.CooperationType = context.CooperationTypes.Single(x => x.Id == data.CooperationTypeId).Name;
            model.AddDate = data.AddDate;
            model.Name = data.RecipientName;
            model.Street = data.Street;
            model.BuildingNumber = data.BuildingNumber;
            model.PostCode = data.PostCode;
            model.Province = context.Provinces.Single(x => x.Id == data.ProvinceId).Name;
            model.District = context.Districts.Single(x => x.Id == data.DistrictId).Name;
            model.City = data.City;
            model.Nip = data.NIP;
            model.LegalForm = context.LegalForms.Single(x => x.Id == data.LegalFormId).Name;
            model.StartYear = data.StartYear.ToString();
            model.GroupMember = data.GroupMember;
            var group = context.Groups.SingleOrDefault(x => x.Id == data.GroupId);
            model.Group = group != null ? group.Name : string.Empty;
            model.Owner = data.Owner;
            model.OwnerPhone = data.OwnerPhoneNumber;
            model.Phone1 = data.PhoneNumber;
            model.Phone2 = data.PhoneNumber2;
            model.Phone3 = data.PhoneNumber3;
            model.Email = data.Emial;
            model.Website = data.Website;
            model.Agent = context.Agents.Single(x => x.Id == data.AgentId).FullName;
            model.ContactPerson = data.ContactPerson;
            model.ContactPersonPosition = data.ContactPersonPosition;
            model.ContactPersonEmail = data.ContactPersonEmail;
            model.ContactPersonPhone = data.ContactPersonPhoneNumber;
        }

        private static void AddVisitsToVm(ClientDetailsViewModel model, WebappContext context)
        {
            var visits = context.Visits.Where(v => v.BusinessId == model.Business.Id).OrderBy(d => d.VisitDate).ToList();
            foreach (var visit in visits)
            {
                var step8 = context.ClientSteps8.Where(v => v.VisitId == visit.Id).AsEnumerable().OrderBy(v => v.Id).LastOrDefault();
                if (step8 != null && step8.NewVisitDate != null && step8.NewVisitDate.Value.Year > 1980)
                {
                    visit.VisitDate = step8.NewVisitDate.Value;
                }
            }

            model.Visits = visits;
        }
    }
}
