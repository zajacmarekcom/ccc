using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using webapp.Models;
using webapp.Models.ViewModels;
using webapp.Models.ViewModels.Json;
using webapp.Utils;
using webapp.ViewModels;
using webapp.ViewModels.Builders;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using VisitDetailsViewModel = webapp.Models.ViewModels.VisitDetailsViewModel;

namespace webapp.Controllers
{
    public class ClientController : Controller
    {
        readonly WebappContext context = new WebappContext();

        public ActionResult ClientDetails(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            var model = ClientDetailsViewModelBuilder.CreateClientDetailsViewModel(id, context);
            if (model == null) return RedirectToAction("Index");

            return View(model);
        }

        public ActionResult VisitDetails(int id)
        {
            @ViewBag.YesNo = new List<string>() {
                "Brak danych",
                "Tak",
                "Nie",
                "Nie dotyczy"
            };

            var visitDetails = new VisitDetailsViewModel();

            @ViewBag.Provinces = context.Provinces.ToList();
            @ViewBag.Districts = context.Districts.ToList();

            var visit = context.Visits.SingleOrDefault(v => v.Id == id);
            if (visit == null) return HttpNotFound();
            var data = context.BusinessDatas.Single(d => d.BusinessId == visit.BusinessId);
            visitDetails.Data = Utils.ClientData.GetClientDetails(data, context);

            var step1 = context.ClientSteps1.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == id);
            var step2 = context.ClientSteps2.Include(s => s.Assortment).Include(s => s.Assortment.Package).Include(s => s.Assortment.PartOfCement).OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == id);
            var step3 = context.ClientSteps3.Include(s => s.Manufacturers).Include(s => s.ManufacturersGroups).Include(s => s.Suppliers).OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == id);
            var step3_5 = context.ClientSteps3_5.Include(s => s.Distributors).Include(s => s.Manufacturers).Include(s => s.ManufacturersGroups).OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == id);
            var step4 = context.ClientSteps4.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == id);
            var step5 = context.ClientSteps5.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == id);
            var step7 = context.ClientSteps7.Include(s => s.LaxTypes).Include(s => s.PackedTypes).OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == id);
            visitDetails.VisitId = visit.Id;
            visitDetails.MainMarketSegment = context.MarketSegments.SingleOrDefault(m => m.Id == visit.MainMarketSegmentId).Name;
            visitDetails.VisitDate = visit.VisitDate.ToString("yyyy-MM-dd");
            visitDetails.VisitComments = visit.Comments;
            if (step1 != null)
            {
                foreach (var ms in step1.VisitMarketSegments)
                {
                    if (ms.MarketSegmentId != 0)
                        ms.MarketSegment = context.MarketSegments.Single(s => s.Id == ms.MarketSegmentId);
                }
                visitDetails.MarketSegments = step1.VisitMarketSegments;
                visitDetails.Branches = step1.Branches;
            }
            if (step2 != null)
            {
                visitDetails.Assortment = step2.Assortment;
                if (step2.LaxTypes != null)
                {
                    foreach (VisitLaxType t in step2.LaxTypes)
                    {
                        if (t.Id != 0)
                            t.LaxType = context.LaxCementTypes.Single(p => p.Id == t.LaxTypeId);
                    }
                    visitDetails.VisitLaxTypes = step2.LaxTypes;
                }
                if (step2.PackedTypes != null)
                {
                    foreach (VisitPackedType t in step2.PackedTypes)
                    {
                        if (t.PackedTypeId != 0)
                            t.PackedType = context.PackedCementTypes.Single(p => p.Id == t.PackedTypeId);
                    }
                    visitDetails.VisitPackedTypes = step2.PackedTypes;
                }
            }
            if (step4 != null)
            {
                visitDetails.Logistic = step4.Logistic;
                visitDetails.UnloadTypes = new List<string>();
                foreach (VisitUnloadType t in step4.UnloadTypes)
                {
                    if (t.UnloadTypeId != 0)
                    {
                        var type = context.UnloadTypes.Single(u => u.Id == t.UnloadTypeId).Name;
                        visitDetails.UnloadTypes.Add(type);
                    }
                }
            }
            if (step5 != null)
            {
                visitDetails.Clients = new List<KeyValuePair<string, int>>();
                foreach (VisitClients c in step5.Clients)
                {
                    if (c.ClientId != 0)
                    {
                        var client = context.Clients.Single(v => v.Id == c.ClientId);
                        var cl = new KeyValuePair<string, int>(client.Name, c.Percent);
                        visitDetails.Clients.Add(cl);
                    }
                }
            }
            if (step7 != null)
            {
                visitDetails.LaxPrices = new List<ViewPrice>();
                if (step7.LaxTypes != null)
                {
                    foreach (ClientLax c in step7.LaxTypes)
                    {
                        var price = new ViewPrice();
                        price.Buy = c.BuyPrice;
                        price.Sell = c.SellPrice;
                        if (c.PriceType == 1)
                        {
                            price.Type = "Brak danych";
                        }
                        if (c.PriceType == 2)
                        {
                            price.Type = "Loco";
                        }
                        if (c.PriceType == 3)
                        {
                            price.Type = "Franco";
                        }
                        if (c.CementId != 0)
                            price.Name = context.LaxCementTypes.Single(t => t.Id == c.CementId).Name;
                        visitDetails.LaxPrices.Add(price);
                    }
                }
                visitDetails.PackedPrices = new List<ViewPrice>();
                if (step7.PackedTypes != null)
                {
                    foreach (ClientPacked c in step7.PackedTypes)
                    {
                        var price = new ViewPrice();
                        price.Buy = c.BuyPrice;
                        price.Sell = c.SellPrice;
                        if (c.PriceType == 1)
                        {
                            price.Type = "Brak danych";
                        }
                        if (c.PriceType == 2)
                        {
                            price.Type = "Loco";
                        }
                        if (c.PriceType == 3)
                        {
                            price.Type = "Franco";
                        }
                        if (c.CementId != 0)
                            price.Name = context.PackedCementTypes.Single(t => t.Id == c.CementId).Name;
                        visitDetails.PackedPrices.Add(price);
                    }
                }
            }
            if (step3 != null)
            {
                visitDetails.Cooperation = step3.Cooperation;
                visitDetails.Suppliers = new List<ViewSupplier>();
                if (step3.Suppliers != null && step3.Cooperation)
                {
                    foreach (VisitSupplier s in step3.Suppliers)
                    {
                        var sup = new ViewSupplier();
                        if (s.SupplierId != 0)
                            sup.Name = context.Suppliers.Single(ss => ss.Id == s.SupplierId).Name;
                        sup.Text = s.Text;
                        visitDetails.Suppliers.Add(sup);
                    }
                }

                visitDetails.Step3Manufs = new List<ViewManufacturer>();
                if (step3.Manufacturers != null)
                {
                    foreach (VisitManufacturers s in step3.Manufacturers)
                    {
                        if (s.GroupId == 0)
                        {
                            var sup = new ViewManufacturer();
                            if (s.ManufacturerId != 0)
                                sup.Name = context.Manufacturers.Single(ss => ss.Id == s.ManufacturerId).Name;
                            sup.Percent = s.Percent;
                            visitDetails.Step3Manufs.Add(sup);
                        }
                    }
                }

                visitDetails.Step3ManufsGroups = new List<ViewManGroup>();
                if (step3.ManufacturersGroups != null)
                {
                    foreach (VisitManufacturersGroup g in step3.ManufacturersGroups)
                    {
                        var gr = new ViewManGroup();
                        if (g.ManufacturersGroupId != 0)
                            gr.Name = context.ManufacturersGroups.Single(a => a.Id == g.ManufacturersGroupId).Name;
                        gr.Percent = step3.Manufacturers.Any(m => m.GroupId == g.ManufacturersGroupId) ? 0 : g.Percent;
                        gr.Manufacturers = new List<ViewManufacturer>();
                        foreach (VisitManufacturers s in step3.Manufacturers)
                        {
                            if (s.GroupId == g.ManufacturersGroupId)
                            {
                                var sup = new ViewManufacturer();
                                if (s.ManufacturerId != 0)
                                    sup.Name = context.Manufacturers.Single(ss => ss.Id == s.ManufacturerId).Name;
                                sup.Percent = s.Percent;
                                gr.Percent += s.Percent;
                                gr.Manufacturers.Add(sup);
                            }
                        }
                        visitDetails.Step3ManufsGroups.Add(gr);
                    }
                }
            }
            if (step3_5 != null)
            {
                if (step3_5.ReceiptId != 0)
                    visitDetails.RecType = context.Receipts.Single(r => r.Id == step3_5.ReceiptId).Name;

                visitDetails.Manufs = new List<ViewManufacturer>();
                if (step3_5.Manufacturers != null)
                {
                    foreach (VisitManufacturers s in step3_5.Manufacturers)
                    {
                        if (s.GroupId == 0)
                        {
                            var sup = new ViewManufacturer();
                            if (s.ManufacturerId != 0)
                                sup.Name = context.Manufacturers.Single(ss => ss.Id == s.ManufacturerId).Name;
                            sup.Percent = s.Percent;
                            visitDetails.Manufs.Add(sup);
                        }
                    }
                }

                visitDetails.ManGroups = new List<ViewManGroup>();
                if (step3_5.ManufacturersGroups != null)
                {
                    foreach (VisitManufacturersGroup g in step3_5.ManufacturersGroups)
                    {
                        var gr = new ViewManGroup();
                        if (g.ManufacturersGroupId != 0)
                            gr.Name = context.ManufacturersGroups.Single(a => a.Id == g.ManufacturersGroupId).Name;
                        gr.Percent = g.Percent;
                        gr.Manufacturers = new List<ViewManufacturer>();
                        foreach (VisitManufacturers s in step3_5.Manufacturers)
                        {
                            if (s.GroupId == g.ManufacturersGroupId)
                            {
                                var sup = new ViewManufacturer();
                                if (s.ManufacturerId != 0)
                                    sup.Name = context.Manufacturers.Single(ss => ss.Id == s.ManufacturerId).Name;
                                sup.Percent = s.Percent;
                                gr.Manufacturers.Add(sup);
                            }
                        }
                        visitDetails.ManGroups.Add(gr);
                    }
                }
                if (step3_5.Distributors != null)
                {
                    visitDetails.Distributors = new List<DistributorViewModel>();
                    foreach (VisitDistributor d in step3_5.Distributors)
                    {
                        var dist = new DistributorViewModel();
                        dist.IsMarket = false;
                        if (d.DistributorId != 0)
                        {
                            var dd = context.Distributors.Single(dis => dis.Id == d.DistributorId);
                            dist.Name = dd.Name;
                            dist.IsMarket = dd.IsMarket;
                            if (dd.IsMarket && d.MarketAddressId != null && d.MarketAddressId > 0)
                            {
                                dist.Address = d.MarketAddress;
                            }
                        }
                        dist.Percent = d.Percent;
                        if (d.Manufacturers != null)
                        {
                            dist.Manufacturers = new List<ViewManufacturer>();
                            foreach (VisitDistManufacturer m in d.Manufacturers)
                            {
                                var man = new ViewManufacturer();
                                if (m.ManufacturerId != 0)
                                    man.Name = context.Manufacturers.Single(mm => mm.Id == m.ManufacturerId).Name;
                                man.Percent = m.Percent;
                                dist.Manufacturers.Add(man);
                            }
                        }
                        if (d.ManufacturersGroups != null)
                        {
                            dist.Groups = new List<ViewManGroup>();
                            foreach (VisitDistManufacturersGroup g in d.ManufacturersGroups)
                            {
                                var gr = new ViewManGroup();
                                if (g.ManufacturersGroupId != 0)
                                    gr.Name = context.ManufacturersGroups.Single(gg => gg.Id == g.ManufacturersGroupId).Name;
                                gr.Percent = g.Percent;
                                if (g.Mans != null && g.Mans.Count > 0)
                                {
                                    gr.Percent = 0;
                                    gr.Manufacturers = new List<ViewManufacturer>();
                                    foreach (var gm in g.Mans)
                                    {
                                        var vm = new ViewManufacturer();
                                        if (gm.ManufacturerId != 0)
                                            vm.Name = context.Manufacturers.Single(mm => mm.Id == gm.ManufacturerId).Name;
                                        vm.Percent = gm.Percent;
                                        gr.Percent += gm.Percent;
                                        gr.Manufacturers.Add(vm);
                                    }
                                }
                                dist.Groups.Add(gr);
                            }
                        }
                        visitDetails.Distributors.Add(dist);
                    }
                }
            }
            visitDetails.Step4 = step4;
            visitDetails.Step5 = step5;
            visitDetails.Step6 = context.ClientSteps6.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == id);
            visitDetails.Step7 = step7;
            visitDetails.Step8 = context.ClientSteps8.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == id);
            return View(visitDetails);
        }

        public ActionResult GetXLS(int id)
        {
            var visit = context.Visits.Single(v => v.Id == id);
            var data = context.BusinessDatas.Single(b => b.BusinessId == visit.BusinessId);
            var details = ClientData.GetClientDetails(data, context);
            var step1 = context.ClientSteps1.Include(s => s.VisitMarketSegments).Include(s => s.Branches).Single(s => s.VisitId == visit.Id);
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Dane klienta");

            ICellStyle style = workbook.CreateCellStyle();
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            style.SetFont(font);

            CreateRow(0, sheet, "Status", details.Status);
            CreateRow(1, sheet, "Rodzaj współpracy", details.CooperationType);
            CreateRow(2, sheet, "Data dodania", details.AddDate);
            CreateTitleRow(3, sheet, "Adres", style);
            CreateRow(4, sheet, "Nazwa odbiorcy", details.Name);
            CreateRow(5, sheet, "Ulica", details.Street);
            CreateRow(6, sheet, "Numer budynku", details.BuildingNumebr);
            CreateRow(7, sheet, "Kod pocztowy", details.PostCode);
            CreateRow(8, sheet, "Województwo", details.Province);
            CreateRow(9, sheet, "Powiat", details.District);
            CreateRow(10, sheet, "Miasto", details.City);
            CreateTitleRow(11, sheet, "Pozostałe dane", style);
            CreateRow(12, sheet, "NIP", details.NIP);
            CreateRow(13, sheet, "Forma prawna", details.LegalForm);
            CreateRow(14, sheet, "Członek grupy", details.GroupMember);
            CreateRow(15, sheet, "Grupa", details.Group);
            CreateRow(16, sheet, "Właściciel", details.Owner);
            CreateRow(17, sheet, "Telefon właściciela", details.OwnerPhone);
            CreateRow(18, sheet, "Telefon 1", details.Phone1);
            CreateRow(19, sheet, "Telefon 2", details.Phone2);
            CreateRow(20, sheet, "Telefon 3", details.Phone3);
            CreateRow(21, sheet, "Adres e-mail", details.Email);
            CreateRow(22, sheet, "Strona www", details.Website);
            CreateRow(23, sheet, "Przedstawiciel handlowy", details.Agent);
            CreateTitleRow(24, sheet, "Osoba do kontaktu", style);
            CreateRow(25, sheet, "Osoba do kontaktu", details.ContactPerson);
            CreateRow(26, sheet, "Stanowisko osoby do kontaktu", details.ContactPersonPosition);
            CreateRow(27, sheet, "Adres e-mail", details.ContactPersonEmail);
            CreateRow(28, sheet, "Telefon", details.ContactPersonPhone);

            CreateTitleRow(30, sheet, "Wizyta", style);
            CreateRow(31, sheet, "Data wizyty", visit.VisitDate.ToString("yyyy-MM-dd"));
            CreateRow(32, sheet, "Segment rynku wiodący", context.MarketSegments.Single(s => s.Id == visit.MainMarketSegmentId).Name);
            CreateTitleRow(33, sheet, "Segmenty rynku", style);
            int i = 34;
            for (; i < step1.VisitMarketSegments.Count + 34; i++)
            {
                CreateRow(i, sheet, step1.VisitMarketSegments.ElementAt(i - 34).MarketSegment.Name, step1.VisitMarketSegments.ElementAt(i - 34).Percent.ToString() + "%");
            }
            CreateTitleRow(i++, sheet, "Oddziały", style);
            var k = i;
            for (; i < step1.Branches.Count + k; i++)
            {
                var x = i;
                var branch = step1.Branches.ElementAt(x - k);
                CreateRow(i, sheet, "Nazwa", branch.Name);
                CreateRow(++i, sheet, "Ulica", branch.Street);
                CreateRow(++i, sheet, "Numer budynku", branch.BuildingNumber);
                CreateRow(++i, sheet, "Kod pocztowy", branch.PostCode);
                var province = context.Provinces.Single(p => p.Id == branch.ProvinceId);
                var district = context.Districts.Single(d => d.Id == branch.DistrictId);
                CreateRow(++i, sheet, "Województwo", province.Name);
                CreateRow(++i, sheet, "Powiat", district.Name);
                CreateRow(++i, sheet, "Miasto", branch.City);
                CreateRow(++i, sheet, "Telefon", branch.PhoneNumber);
                CreateRow(++i, sheet, "Handluje cementem", branch.Trades ? "Tak" : "Nie");
            }

            CreateRow(++i, sheet, "Uwagi", visit.Comments);
            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport.xls");
        }

        private void CreateRow(int i, ISheet sheet, string name, string value)
        {
            var row = sheet.CreateRow(i);
            row.CreateCell(0).SetCellValue(name);
            row.CreateCell(1).SetCellValue(value);
        }

        private void CreateTitleRow(int i, ISheet sheet, string text, ICellStyle style)
        {
            var cell = sheet.CreateRow(i).CreateCell(0);
            cell.CellStyle = style;
            cell.SetCellValue(text);
        }

        private IRow GetRow(ISheet sheet, int i, Business business, BusinessData data, ClientStep1 step1)
        {
            var row = sheet.CreateRow(i);
            var user = context.Users.Single(u => u.Id == business.CreatorId);
            row.CreateCell(0).SetCellValue(user.FirstName + " " + user.LastName);
            row.CreateCell(1).SetCellValue(data.AddDate.ToString("dd-MM-yyyy"));
            row.CreateCell(2).SetCellValue(data.RecipientName);
            row.CreateCell(3).SetCellValue(data.Street);
            row.CreateCell(4).SetCellValue(data.BuildingNumber);
            row.CreateCell(5).SetCellValue(data.PostCode);
            row.CreateCell(6).SetCellValue(data.City);
            row.CreateCell(7).SetCellValue(context.Provinces.Single(p => p.Id == data.ProvinceId).Name);
            row.CreateCell(8).SetCellValue(context.Districts.Single(d => d.Id == data.DistrictId).Name);
            row.CreateCell(9).SetCellValue(data.NIP);
            row.CreateCell(10).SetCellValue(data.StartYear);
            row.CreateCell(3).SetCellValue(context.CooperationTypes.Single(c => c.Id == data.CooperationTypeId).Name);
            row.CreateCell(11).SetCellValue(data.Owner);
            row.CreateCell(12).SetCellValue(data.OwnerPhoneNumber);
            row.CreateCell(13).SetCellValue(data.PhoneNumber);
            row.CreateCell(14).SetCellValue(data.PhoneNumber2);
            row.CreateCell(15).SetCellValue(data.PhoneNumber3);
            row.CreateCell(16).SetCellValue(data.Emial);
            row.CreateCell(17).SetCellValue(data.Website);
            row.CreateCell(18).SetCellValue(data.ContactPerson);
            row.CreateCell(19).SetCellValue(data.ContactPersonEmail);
            row.CreateCell(20).SetCellValue(data.ContactPersonPhoneNumber);
            row.CreateCell(21).SetCellValue(data.ContactPersonPosition);

            return row;
        }

        public ActionResult ProducersMap()
        {
            var producers = context.Producers.ToList().OrderBy(x => x.Name);
            return View(producers);
        }

        public JsonResult GetClientsWithProducer(int producerId)
        {
            var steps = context.ClientSteps2.Include(s => s.LaxTypes).Include(s => s.PackedTypes).ToList();

            var model = new List<ClientMapJson>();
            foreach (var item in steps)
            {
                if ((item.LaxTypes != null && item.LaxTypes.Any(t => context.LaxCementTypes.Single(s => s.Id == t.LaxTypeId).ProducerId == producerId)) || (item.PackedTypes != null && item.PackedTypes.Any(t => context.PackedCementTypes.Single(s => s.Id == t.PackedTypeId).ProducerId == producerId)))
                {
                    var visit = context.Visits.OrderByDescending(v => v.Id).First(v => v.Id == item.VisitId);
                    var data = context.BusinessDatas.OrderByDescending(v => v.Id).First(d => d.BusinessId == visit.BusinessId);
                    if (string.IsNullOrEmpty(data.Lat) || string.IsNullOrEmpty(data.Lng))
                    {
                        DataController.GetPosition(ref data);
                        context.Entry(data);
                    }
                    if (!string.IsNullOrEmpty(data.Lat) && !string.IsNullOrEmpty(data.Lng))
                    {
                        var newItem = new ClientMapJson();
                        newItem.Name = data.RecipientName;
                        newItem.Lat = data.Lat;
                        newItem.Lng = data.Lng;
                        newItem.Id = visit.BusinessId;
                        model.Add(newItem);
                    }
                }
            }
            context.SaveChanges();
            return Json(model);
        }

        public ActionResult DistributorsMap()
        {
            var distributors = context.Distributors.ToList().OrderBy(x => x.City).ThenBy(x => x.Name);
            return View(distributors);
        }

        public JsonResult GetClientsWithDistributor(int distributorId)
        {
            var steps = context.ClientSteps3_5.Include(s => s.Distributors).ToList();

            var model = new List<ClientMapJson>();
            foreach (var item in steps)
            {
                if (item.Distributors != null && item.Distributors.Any(d => d.DistributorId == distributorId))
                {
                    var visit = context.Visits.OrderByDescending(v => v.Id).First(v => v.Id == item.VisitId);
                    var data = context.BusinessDatas.OrderByDescending(v => v.Id).First(d => d.BusinessId == visit.BusinessId);
                    if (string.IsNullOrEmpty(data.Lat) || string.IsNullOrEmpty(data.Lng))
                    {
                        DataController.GetPosition(ref data);
                        context.Entry(data);
                    }
                    if (string.IsNullOrEmpty(data.Lat) || string.IsNullOrEmpty(data.Lng)) continue;
                    var newItem = new ClientMapJson();
                    newItem.Name = data.RecipientName;
                    newItem.Lat = data.Lat;
                    newItem.Lng = data.Lng;
                    newItem.Id = visit.BusinessId;
                    model.Add(newItem);
                }
            }
            context.SaveChanges();
            return Json(model);
        }
    }
}
