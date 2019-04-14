using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using webapp.Models;
using webapp.Models.ViewModels;
using webapp.Utils.ControllersHelpers;

namespace webapp.Helpers.Builders
{
    public class IndexViewModelBuilder
    {
        public static IndexViewModel CreateIndexiViewModel(IPrincipal user, WebappContext context, string sort, int? page)
        {
            var model = new IndexViewModel()
            {
                Items = new List<BusinessItem>()
            };

            foreach (var b in IndexHelper.GetAllBusinesses(user, context))
            {
                var status = 0;
                var data = b.Data;
                if (data == null) continue;
                var visitDate = new DateTime(1, 1, 1);
                var lastAddedVisit =
                    context.Visits.Where(v => v.BusinessId == b.Business.Id)
                        .OrderByDescending(v => v.VisitDate)
                        .FirstOrDefault();
                if (lastAddedVisit != null)
                {
                    DateTime? newVisitDate = null;
                    var step8 = context.ClientSteps8.FirstOrDefault(c => c.VisitId == lastAddedVisit.Id);
                    if (step8 != null)
                    {
                        status = step8.Status;
                        newVisitDate = step8.NewVisitDate.HasValue && step8.NewVisitDate.Value.Year > 1980 ? step8.NewVisitDate : null;
                    }
                    visitDate = newVisitDate ?? lastAddedVisit.VisitDate;
                }
                var item = BusinessItemBuilder.CreateBusinessItem(b.Business.Id, data.RecipientName, visitDate, status,
                    data.AddDate);

                model.Items.Add(item);
            }

            model.Status = new int[4];
            foreach (var item in model.Items)
            {
                model.Status[(int)item.Status]++;
            }

            switch (sort)
            {
                case null:
                case "ascAdd":
                    model.Sort = "ascAdd";
                    model.Items = model.Items.OrderBy(o => o.AddDate).ToList();
                    break;
                case "descAdd":
                    model.Sort = "descAdd";
                    model.Items = model.Items.OrderByDescending(o => o.AddDate).ToList();
                    break;
                case "ascVisit":
                    model.Sort = "ascVisit";
                    model.Items = model.Items.OrderBy(o => o.VisitDate).ToList();
                    break;
                case "descVisit":
                    model.Sort = "descVisit";
                    model.Items = model.Items.OrderByDescending(o => o.VisitDate).ToList();
                    break;
            }

            var pageCount = (model.Items.Count / 20) + 1;
            var pageSize = 20;
            if (model.Items.Count < 20)
            {
                pageSize = model.Items.Count;
            }
            else if (page != null && page > 1 && model.Items.Count % ((page.Value - 1) * pageSize) < 20)
            {
                pageSize = model.Items.Count % ((page.Value - 1) * pageSize);
            }

            if (page == null)
            {
                model.Page = 1;
                model.Items = model.Items.Take(pageSize).ToList();
            }
            else
            {
                model.Page = page.Value;
                model.Items = model.Items.Skip((page.Value - 1) * 20).Take(20).ToList();
            }

            model.PageCount = pageCount;

            model.CurrentUser = ((Security.CustomPrincipal) user).CustomIdentity;

            return model;
        }
    }
}