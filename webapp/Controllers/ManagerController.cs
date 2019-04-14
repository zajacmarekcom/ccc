using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Hosting;
using System.Web.Mvc;
using webapp.Enums;
using webapp.Models;
using webapp.Models.ViewModels;
using webapp.Repository;
using webapp.Utils;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using WebGrease.Css.Extensions;

namespace webapp.Controllers
{
    public class ManagerController : Controller
    {
        private readonly WebappContext _context;
        private readonly BusinessRepository _businessRepository;
        private readonly StepRepository _stepRepository;
        private readonly VisitRepository _visitRepository;
        private readonly SettingsRepository _settingsRepository;

        public ManagerController()
        {
            _context = new WebappContext();
            _businessRepository = new BusinessRepository(_context);
            _stepRepository = new StepRepository(_context);
            _visitRepository = new VisitRepository(_context);
            _settingsRepository = new SettingsRepository(_context);
        }

        [Authorize(Roles = "manager")]
        public ActionResult Index(string sort)
        {
            using (var context = new WebappContext())
            {
                var businesses = context.Businesses.ToList();

                var businessItems = new List<BusinessItem>();
                foreach (var b in businesses)
                {
                    var status = 0;
                    var data = _businessRepository.GetData(b.Id);
                    if (data == null) continue;
                    var visits = context.Visits.Where(v => v.BusinessId == b.Id);
                    var lastAddedVisit = visits.FirstOrDefault(v => v.VisitDate == visits.Max(a => a.VisitDate));
                    if (lastAddedVisit != null)
                    {
                        var step8 = _stepRepository.GetStep8(lastAddedVisit.Id);
                        if (step8 != null)
                        {
                            status = step8.Status;
                        }
                    }
                    businessItems.Add(new BusinessItem
                    {
                        Status = (BusinessStatus) status
                    });
                }

                int fromFile;
                int.TryParse(_settingsRepository.Get("GreenFromFile"), out fromFile);

                ViewBag.Status0 = businessItems.Count(x => x.Status == BusinessStatus.Undone);
                ViewBag.Status1 = businessItems.Count(x => x.Status == BusinessStatus.Green) + fromFile;
                ViewBag.Status2 = businessItems.Count(x => x.Status == BusinessStatus.Yellow);
                ViewBag.Status3 = businessItems.Count(x => x.Status == BusinessStatus.Brown);


                var datas = new List<AgentBusinessData>();
                var agents = context.Users.Where(u => u.RoleId == 3).ToList();
                foreach (var agent in agents)
                {
                    var data = new AgentBusinessData
                    {
                        AgentFullName = agent.FirstName + " " + agent.LastName,
                        Businesses = new List<BusinessItem>()
                    };

                    var visitDate = new DateTime(1, 1, 1);
                    var b = context.Businesses.Where(bb => bb.CreatorId == agent.Id).ToList();
                    foreach (var bis in b)
                    {
                        var bd = context.BusinessDatas.SingleOrDefault(bds => bds.BusinessId == bis.Id);
                        if (bd == null) continue;
                        var item = new BusinessItem
                        {
                            Name = bd.RecipientName,
                            Id = bis.Id,
                            Status = 0,
                            AddDate = bd.AddDate
                        };
                        var visits = context.Visits.Where(v => v.BusinessId == bis.Id);
                        var lastAddedVisit = visits.FirstOrDefault(v => v.VisitDate == visits.Max(a => a.VisitDate));
                        if (lastAddedVisit != null)
                        {
                            var step8 = _stepRepository.GetStep8(lastAddedVisit.Id);
                            if (step8 != null)
                            {
                                item.Status = (BusinessStatus)step8.Status;
                            }
                            visitDate = lastAddedVisit.VisitDate;
                        }
                        item.VisitDate = visitDate;
                        data.Businesses.Add(item);
                    }
                    datas.Add(data);
                }

                switch (sort)
                {
                    case null:
                    case "ascAdd":
                        ViewBag.Sort = "ascAdd";
                        foreach (var d in datas)
                        {
                            d.Businesses = d.Businesses.OrderBy(o => o.AddDate).ToList();
                        }
                        break;
                    case "descAdd":
                        ViewBag.Sort = "descAdd";
                        foreach (var d in datas)
                        {
                            d.Businesses = d.Businesses.OrderByDescending(o => o.AddDate).ToList();
                        }
                        break;
                    case "ascVisit":
                        ViewBag.Sort = "ascVisit";
                        foreach (var d in datas)
                        {
                            d.Businesses = d.Businesses.OrderBy(o => o.VisitDate).ToList();
                        }
                        break;
                    case "descVisit":
                        ViewBag.Sort = "descVisit";
                        foreach (var d in datas)
                        {
                            d.Businesses = d.Businesses.OrderByDescending(o => o.VisitDate).ToList();
                        }
                        break;
                }

                ViewBag.Data = datas;

                return View();
            }
        }

        public ActionResult ReportAnnual()
        {
            var model = GetAnnual();

            return View(model);
        }

        public FileResult GetReportAnnualXLS()
        {
            var data = GetAnnual();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Raport");
            var i = 0;
            foreach (var item in data)
            {
                var row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.Name);
                row.CreateCell(1).SetCellValue(item.City);
                row.CreateCell(2).SetCellValue(item.Annual);
                i++;
            }

            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport_roczne_zapotrzebowanie" + DateTime.Now.ToShortDateString() + ".xls");
        }

        public ActionResult ReportCements()
        {
            var model = GetCements();

            return View(model);
        }

        public FileResult GetReportCementsXLS()
        {
            var data = GetCements();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Raport");
            ICellStyle style = workbook.CreateCellStyle();
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            style.SetFont(font);
            var i = 1;
            var r = sheet.CreateRow(0);
            var c = r.CreateCell(0);
            c.CellStyle = style;
            c.SetCellValue("Cementy luz");
            foreach (var item in data.Lax)
            {
                var row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.GroupName);
                row.CreateCell(1).SetCellValue(item.Name);
                row.CreateCell(2).SetCellValue(item.Annual + " ton");
                i++;
            }

            r = sheet.CreateRow(i);
            c = r.CreateCell(0);
            c.CellStyle = style;
            c.SetCellValue("Cementy pakowane");
            i++;
            foreach (var item in data.Packed)
            {
                var row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.GroupName);
                row.CreateCell(1).SetCellValue(item.Name);
                row.CreateCell(2).SetCellValue(item.Annual + " ton");
                i++;
            }

            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport_cementy" + DateTime.Now.ToShortDateString() + ".xls");
        }

        public ActionResult ReportGroups()
        {
            var model = GetGroups();

            return View(model);
        }

        public FileResult GetReportGroupsXLS()
        {
            var data = GetGroups();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Raport");
            var i = 0;
            foreach (var item in data)
            {
                var row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.Name);
                row.CreateCell(1).SetCellValue(item.Annual + " ton");
                i++;
            }

            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport_zapotrzebowanie_grupy" + DateTime.Now.ToShortDateString() + ".xls");
        }

        public ActionResult ReportPrices()
        {
            var model = GetPrices();

            return View(model);
        }

        public FileResult GetReportPricesXLS()
        {
            var data = GetPrices();
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Raport");
            ICellStyle style = workbook.CreateCellStyle();
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            style.SetFont(font);
            var i = 1;
            var r = sheet.CreateRow(0);
            var c = r.CreateCell(0);
            c.CellStyle = style;
            c.SetCellValue("Cementy luz");
            foreach (var item in data.Lax)
            {
                var row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.GroupName);
                row.CreateCell(1).SetCellValue(item.Name);
                row.CreateCell(2).SetCellValue(item.Buy.ToString("#.00") + " zł");
                row.CreateCell(3).SetCellValue(item.Sell.ToString("#.00") + " zł");
                i++;
            }

            r = sheet.CreateRow(i);
            c = r.CreateCell(0);
            c.CellStyle = style;
            c.SetCellValue("Cementy pakowane");
            i++;
            foreach (var item in data.Packed)
            {
                var row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.GroupName);
                row.CreateCell(1).SetCellValue(item.Name);
                row.CreateCell(2).SetCellValue(item.Buy.ToString("#.00") + " zł");
                row.CreateCell(3).SetCellValue(item.Sell.ToString("#.00") + " zł");
                i++;
            }

            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            sheet.AutoSizeColumn(3);

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport_cementy_ceny" + DateTime.Now.ToShortDateString() + ".xls");
        }

        private IEnumerable<ReportAnnualViewModel> GetAnnual()
        {
            var model = new List<ReportAnnualViewModel>();

            var step = _context.ClientSteps2.Include(c => c.Assortment).OrderByDescending(c => c.Assortment.AnnualNeed).ToList();
            foreach (var item in step)
            {
                var visit = _context.Visits.First(v => v.Id == item.VisitId);
                var data = _context.BusinessDatas.First(b => b.BusinessId == visit.BusinessId);
                var modelItem = new ReportAnnualViewModel
                {
                    Name = data.RecipientName,
                    City = data.City,
                    Annual = item.Assortment.AnnualNeed
                };
                model.Add(modelItem);
            }

            return model;
        }

        private ReportCementsAnnualViewModel GetCements()
        {
            var laxList = new List<ReportCementViewModel>();

            foreach (var item in _context.LaxCementTypes.ToList())
            {
                var lax = _context.ClientSteps2.Include(c => c.LaxTypes).ToList();
                var sum = 0;
                foreach (var c in lax.Where(c => c.LaxTypes != null))
                {
                    if (c.Assortment.PackageId == 1)
                    {
                        c.Assortment.LaxCementPercent = 100;
                    }
                    var annual = c.Assortment.AnnualNeed * (c.Assortment.LaxCementPercent / 100);
                    sum += c.LaxTypes.Where(t => t.LaxTypeId == item.Id).Sum(t => (annual*(t.Percent/100)));
                }
                var newItem = new ReportCementViewModel
                {
                    GroupName = _context.Producers.First(p => p.Id == item.ProducerId).Name,
                    Annual = sum,
                    Name = item.Name
                };

                laxList.Add(newItem);
            }

            var packedList = new List<ReportCementViewModel>();

            foreach (var item in _context.PackedCementTypes.ToList())
            {
                var packed = _context.ClientSteps2.Include(c => c.PackedTypes).ToList();
                var sum = 0;
                foreach (var c in packed)
                {
                    if (c.PackedTypes == null) continue;
                    var annual = c.Assortment.AnnualNeed * ((100 - c.Assortment.LaxCementPercent) / 100);
                    sum += c.PackedTypes.Where(t => t.PackedTypeId == item.Id).Sum(t => (annual*(t.Percent/100)));
                }

                var newItem = new ReportCementViewModel
                {
                    GroupName = _context.Producers.First(p => p.Id == item.ProducerId).Name,
                    Annual = sum,
                    Name = item.Name
                };

                packedList.Add(newItem);
            }

            var model = new ReportCementsAnnualViewModel
            {
                Lax = laxList.OrderByDescending(c => c.Annual),
                Packed = packedList.OrderByDescending(c => c.Annual)
            };

            return model;
        }

        private IEnumerable<ReportGroupViewModel> GetGroups()
        {
            var model = new List<ReportGroupViewModel>();

            foreach (var item in _context.ManufacturersGroups.ToList())
            {
                var visitGroups =
                    _context.ClientSteps3.Include(c => c.ManufacturersGroups).Include(c => c.Manufacturers).ToList();
                var sum = 0;
                foreach (var c in visitGroups)
                {
                    if (c.ManufacturersGroups == null) continue;
                    var annual =
                        _context.ClientSteps2.Include(s => s.Assortment)
                            .First(s => s.VisitId == c.VisitId)
                            .Assortment.AnnualNeed;
                    foreach (var g in c.ManufacturersGroups.Where(g => g.ManufacturersGroupId == item.Id))
                    {
                        if (g.Percent == 0 || g.SelectedManufacturers)
                        {
                            g.Percent =
                                c.Manufacturers.Where(m => m.GroupId == g.ManufacturersGroupId)
                                    .Sum(m => m.Percent);
                        }
                        sum += (g.Percent / 100) * annual;
                    }
                }

                var newItem = new ReportGroupViewModel
                {
                    Annual = sum,
                    Name = item.Name
                };
                model.Add(newItem);
            }

            foreach (var item in _context.Manufacturers.Where(m => m.GroupId == 0).ToList())
            {
                var visited = _context.ClientSteps3.Include(c => c.Manufacturers).ToList();
                var sum = 0;
                foreach (var c in visited.Where(c => c.Manufacturers != null))
                {
                    var annual = _context.ClientSteps2.Include(s => s.Assortment).First(s => s.VisitId == c.VisitId).Assortment.AnnualNeed;
                    sum += c.Manufacturers.Where(m => m.ManufacturerId == item.Id).Sum(m => (m.Percent/100)*annual);
                }
                var newItem = new ReportGroupViewModel();
                newItem.Annual = sum;
                newItem.Name = item.Name;
                model.Add(newItem);
            }
            model = model.OrderByDescending(c => c.Annual).ToList();
            return model;
        }

        private ReportPricesListViewModel GetPrices()
        {
            var model = new ReportPricesListViewModel();
            var lax = new List<ReportCementPriceViewModel>();
            var step = _context.ClientSteps7.Include(c => c.LaxTypes).ToList();
            foreach (var item in _context.LaxCementTypes.ToList())
            {
                var buySum = 0.0M;
                var sellSum = 0.0M;
                var count = 0;
                foreach (var c in step)
                {
                    if (c.LaxTypes == null || c.LaxTypes.Count == 0) continue;
                    var step2 = _context.ClientSteps2.Include(s => s.Assortment).OrderByDescending(s => s.Id).First(s => s.VisitId == c.VisitId);
                    foreach (var t in c.LaxTypes)
                    {
                        if (t.CementId == item.Id && t.BuyPrice > 0.00001M && t.SellPrice > 0.00001M)
                        {
                            if (step2.Assortment.PackageId == 1)
                            {
                                step2.Assortment.LaxCementPercent = 100;
                            }
                            var annual = step2.Assortment.AnnualNeed * (step2.Assortment.LaxCementPercent / 100);
                            buySum += (t.BuyPrice * annual);
                            sellSum += (t.SellPrice * annual);
                            count += annual;
                        }
                    }
                }

                var newItem = new ReportCementPriceViewModel
                {
                    GroupName = _context.Producers.First(p => p.Id == item.ProducerId).Name,
                    Buy = (count > 0 ? buySum/count : 0.0M),
                    Sell = (count > 0 ? sellSum/count : 0.0M),
                    Name = item.Name
                };
                if (count > 0)
                    lax.Add(newItem);
            }

            var packed = new List<ReportCementPriceViewModel>();
            var stepPacked = _context.ClientSteps7.Include(c => c.PackedTypes).ToList();
            foreach (var item in _context.PackedCementTypes.ToList())
            {
                var buySum = 0.0M;
                var sellSum = 0.0M;
                var count = 0;
                foreach (var c in stepPacked)
                {
                    if (c.PackedTypes == null || c.PackedTypes.Count == 0) continue;
                    var step2 = _context.ClientSteps2.Include(s => s.Assortment).OrderByDescending(s => s.Id).First(s => s.VisitId == c.VisitId);
                    if (step2.Assortment.PackageId == 2)
                    {
                        step2.Assortment.LaxCementPercent = 0;
                    }
                    foreach (var t in c.PackedTypes)
                    {
                        if (t.CementId == item.Id && t.BuyPrice > 0.00001M && t.SellPrice > 0.00001M)
                        {
                            var annual = step2.Assortment.AnnualNeed * ((100 - step2.Assortment.LaxCementPercent) / 100);
                            buySum += (t.BuyPrice * annual);
                            sellSum += (t.SellPrice * annual);
                            count += annual;
                        }
                    }
                }

                var newItem = new ReportCementPriceViewModel
                {
                    GroupName = _context.Producers.First(p => p.Id == item.ProducerId).Name,
                    Buy = (count > 0 ? buySum/count : 0.0M),
                    Sell = (count > 0 ? sellSum/count : 0.0M),
                    Name = item.Name
                };
                if (count > 0)
                    packed.Add(newItem);
            }
            model.Lax = lax;
            model.Packed = packed;

            return model;
        }

        public ActionResult Search(string query)
        {
            var businessItems = _businessRepository.Search(query).Select(b => new BusinessItem
            {
                Id = b.BusinessId,
                Name = b.RecipientName,
                Status = (BusinessStatus)_stepRepository.GetStatus(b.BusinessId)
            }).ToList();

            return View(businessItems);
        }

        public FileResult GetReport()
        {
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Raport");

            var businesses = _context.Businesses.OrderBy(b => b.CreatorId).ToList();
            int i = 0;
            var row = sheet.CreateRow(0);
            ICellStyle style = workbook.CreateCellStyle();
            IFont font = workbook.CreateFont();
            font.Boldweight = (short)FontBoldWeight.Bold;
            style.SetFont(font);
            row.CreateCell(i++).SetCellValue("Imię i nazwisko użytkownika");
            row.CreateCell(i++).SetCellValue("Data wprowadzenia do bazy");
            row.CreateCell(i++).SetCellValue("Status wpisu");
            row.CreateCell(i++).SetCellValue("Status");
            row.CreateCell(i++).SetCellValue("Rodzaj współpracy");
            row.CreateCell(i++).SetCellValue("Nazwa");
            row.CreateCell(i++).SetCellValue("Ulica");
            row.CreateCell(i++).SetCellValue("Numer budynku");
            row.CreateCell(i++).SetCellValue("Kod pocztowy");
            row.CreateCell(i++).SetCellValue("Województwo");
            row.CreateCell(i++).SetCellValue("Powiat");
            row.CreateCell(i++).SetCellValue("Miasto");
            row.CreateCell(i++).SetCellValue("NIP");
            row.CreateCell(i++).SetCellValue("Forma prawna");
            row.CreateCell(i++).SetCellValue("Członek grupy");
            row.CreateCell(i++).SetCellValue("Grupa");
            row.CreateCell(i++).SetCellValue("Właściciel");
            row.CreateCell(i++).SetCellValue("Nr telefonu właściciela");
            row.CreateCell(i++).SetCellValue("Telefon1");
            row.CreateCell(i++).SetCellValue("Telefon2");
            row.CreateCell(i++).SetCellValue("Telefon3");
            row.CreateCell(i++).SetCellValue("Email");
            row.CreateCell(i++).SetCellValue("Strona www");
            row.CreateCell(i++).SetCellValue("Przedstawiciel");
            row.CreateCell(i++).SetCellValue("Osoba do kontaktu");
            row.CreateCell(i++).SetCellValue("Stanowisko osoby do kontaktu");
            row.CreateCell(i++).SetCellValue("Email osoby do kontaktu");
            row.CreateCell(i++).SetCellValue("Telefon osoby do kontaktu");
            row.CreateCell(i++).SetCellValue("Data wizyty");
            row.CreateCell(i++).SetCellValue("Wiodący segment rynku");
            row.CreateCell(i++).SetCellValue("Roczne zapotrzebowanie");
            row.CreateCell(i++).SetCellValue("Udział cementu w obrocie rocznym [%]");
            row.CreateCell(i++).SetCellValue("Rodzaj opakowania");
            row.CreateCell(i++).SetCellValue("Własna marka na worku");
            row.CreateCell(i++).SetCellValue("Wspólne działania marketingowe");
            row.CreateCell(i++).SetCellValue("Serwis doradczy");
            row.CreateCell(i++).SetCellValue("Świeży cement");
            row.CreateCell(i++).SetCellValue("Bezkosztowa reklamacja");
            row.CreateCell(i++).SetCellValue("Inne");
            row.CreateCell(i++).SetCellValue("Co ma wpływ na wybór dostawcy cementu");
            row.CreateCell(i++).SetCellValue("Uwagi dotyczące jakości");
            row.CreateCell(i++).SetCellValue("Luz");
            row.CreateCell(i++).SetCellValue("CEM I 32,5");
            row.CreateCell(i++).SetCellValue("CEM I 42,5");
            row.CreateCell(i++).SetCellValue("CEM I 52,5");
            row.CreateCell(i++).SetCellValue("CEM II 32,5");
            row.CreateCell(i++).SetCellValue("CEM II 42,5");
            row.CreateCell(i++).SetCellValue("CEM II 52,5");
            row.CreateCell(i++).SetCellValue("CEM III 32,5");
            row.CreateCell(i++).SetCellValue("CEM III 42,5");
            row.CreateCell(i++).SetCellValue("CEM III 52,5");
            row.CreateCell(i++).SetCellValue("CEM IV 32,5");
            row.CreateCell(i++).SetCellValue("CEM IV 42,5");
            row.CreateCell(i++).SetCellValue("CEM IV 52,5");
            row.CreateCell(i++).SetCellValue("CEM V 22,5");
            row.CreateCell(i++).SetCellValue("CEM V 32,5");
            row.CreateCell(i++).SetCellValue("Worek");
            row.CreateCell(i++).SetCellValue("CEM I 32,5");
            row.CreateCell(i++).SetCellValue("CEM I 42,5");
            row.CreateCell(i++).SetCellValue("CEM I 52,5");
            row.CreateCell(i++).SetCellValue("CEM II 32,5");
            row.CreateCell(i++).SetCellValue("CEM II 42,5");
            row.CreateCell(i++).SetCellValue("CEM II 52,5");
            row.CreateCell(i++).SetCellValue("CEM III 32,5");
            row.CreateCell(i++).SetCellValue("CEM III 42,5");
            row.CreateCell(i++).SetCellValue("CEM III 52,5");
            row.CreateCell(i++).SetCellValue("CEM IV 32,5");
            row.CreateCell(i++).SetCellValue("CEM IV 42,5");
            row.CreateCell(i++).SetCellValue("CEM IV 52,5");
            row.CreateCell(i++).SetCellValue("CEM V 22,5");
            row.CreateCell(i++).SetCellValue("CEM V 32,5");
            row.CreateCell(i++).SetCellValue("% stanowi luz");
            row.CreateCell(i++).SetCellValue("% stanowi worek");
            row.CreateCell(i++).SetCellValue("Współpraca z innymi dostawcami wyrobów budowlanych");
            row.CreateCell(i++).SetCellValue("Bezpośrednie");
            i += 6;
            row.CreateCell(i++).SetCellValue("Pośrednie");
            i += 6;
            row.CreateCell(i++).SetCellValue("Liczba silosów");
            row.CreateCell(i++).SetCellValue("Pojemność silosów");
            row.CreateCell(i++).SetCellValue("Pojemność magazynu cementu (zadaszony)");
            row.CreateCell(i++).SetCellValue("Pojemność magazynu cementu (plac)");
            row.CreateCell(i++).SetCellValue("Dostęp do bocznicy kolejowej czynnej");
            row.CreateCell(i++).SetCellValue("Czy odbierał cement transportem kolejowym");
            row.CreateCell(i++).SetCellValue("Czy posiada własne cementowozy");
            row.CreateCell(i++).SetCellValue("Liczba cementowozów");
            row.CreateCell(i++).SetCellValue("Czy posiada własne samochody typu platforma");
            row.CreateCell(i++).SetCellValue("Liczba samochodów typu platforma");
            row.CreateCell(i++).SetCellValue("Czy posiada własne samochody typu HDS");
            row.CreateCell(i++).SetCellValue("Liczba samochodów typu HDS");
            row.CreateCell(i++).SetCellValue("Czy posiada licencję na przewóz cementu luzem w kraju");
            row.CreateCell(i++).SetCellValue("Czy posiada licencję na przewóz cementu luzem za granicą");
            row.CreateCell(i++).SetCellValue("Luz (27 ton)");
            row.CreateCell(i++).SetCellValue("Max ilość dostawy luz");
            row.CreateCell(i++).SetCellValue("Worek (24 ton)");
            row.CreateCell(i++).SetCellValue("Max ilość dostawy worek");
            row.CreateCell(i++).SetCellValue("Obrót roczny");
            var maxClients = _context.ClientSteps5.Any() ? _context.ClientSteps5.Include(c => c.Clients).Select(c => c.Clients.Count).Max() : 0;
            for (var clients = 0; clients < maxClients; clients++)
            {
                row.CreateCell(i++).SetCellValue("Obsługiwany klient");
                row.CreateCell(i++).SetCellValue("Procent");
            }
            row.CreateCell(i++).SetCellValue("Laboratorium własne");
            row.CreateCell(i++).SetCellValue("Laboratorium obce");
            row.CreateCell(i++).SetCellValue("Notatki");

            i = 1;
            foreach (var business in businesses)
            {
                AddRow(ref sheet,ref i, business);
                ++i;
            }

            

            IWorkbook book;
            using (FileStream file = new FileStream(HostingEnvironment.MapPath(@"~/Content/data.xls"), FileMode.Open, FileAccess.Read))
            {
                book = WorkbookFactory.Create(file);
            }
            var st = book.GetSheetAt(0);
            for (var b = 0; b < st.LastRowNum; b++)
            {
                var r = sheet.CreateRow(sheet.LastRowNum + 1);
                var newRow = st.GetRow(b);
                if (newRow != null)
                {
                    for (var cell = 0; cell < st.GetRow(b).LastCellNum + 1; cell++)
                    { //TODO: Tutaj null czasami leci
                        if (cell == 1)
                        {
                            r.CreateCell(cell).SetCellValue(DateTime.Now.ToString("dd-MM-yyyy"));
                        }
                        else
                        {
                            var cl = newRow.GetCell(cell > 1 ? cell - 1 : cell);
                            if (cl != null)
                            {
                                cl.SetCellType(CellType.String);
                                var cellValue = cl.StringCellValue;
                                r.CreateCell(cell).SetCellValue(string.IsNullOrEmpty(cellValue) ? string.Empty : cellValue);
                            }
                        }
                    }
                }
            }

            for (var num = 0; num <= sheet.LastRowNum; num++)
            {
                if (sheet.GetRow(num) == null || sheet.GetRow(num).GetCell(0) == null || string.IsNullOrEmpty(sheet.GetRow(num).GetCell(0).StringCellValue))
                {
                    sheet.ShiftRows(num + 1, sheet.LastRowNum, -1);
                    --num;
                }
            }


            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport_ogolny" + DateTime.Now.ToShortDateString() + ".xls");
        }

        public FileResult GetCementPercentReport()
        {
            var steps = _context.ClientSteps2.Include(c => c.Assortment).Include(c => c.LaxTypes).Include(c => c.PackedTypes).ToList();
            var list = new List<CementItem>();
            foreach (var step in steps)
            {
                bool isOk = true;
                bool isOk2 = true;
                if ((step.Assortment.PackageId == 1 || step.Assortment.PackageId == 3) &&
                    step.LaxTypes.Sum(a => a.Percent) != 100)
                {
                    isOk = false;
                }
                if ((step.Assortment.PackageId == 2 || step.Assortment.PackageId == 3) &&
                    step.PackedTypes.Sum(a => a.Percent) != 100)
                {
                    isOk2 = false;
                }

                if (!isOk || !isOk2)
                {
                    var visit = _context.Visits.Where(v => v.Id == step.VisitId).AsEnumerable().LastOrDefault();
                    if (visit != null)
                    {
                        var business =
                            _context.Businesses.Where(b => b.Id == visit.BusinessId).AsEnumerable().LastOrDefault();
                        if (business != null)
                        {
                            var user = _context.Users.FirstOrDefault(u => u.Id == business.CreatorId);
                            var data =
                                _context.BusinessDatas.Where(b => b.BusinessId == business.Id)
                                    .AsEnumerable()
                                    .LastOrDefault();
                            var status = 0;
                            if (data != null)
                            {
                                var visits = _context.Visits.Where(v => v.BusinessId == business.Id);
                                var lastAddedVisit = visits.FirstOrDefault(v => v.VisitDate == visits.Max(a => a.VisitDate));
                                if (lastAddedVisit != null)
                                {
                                    var step8 = _context.ClientSteps8.FirstOrDefault(c => c.VisitId == lastAddedVisit.Id);
                                    if (step8 != null)
                                    {
                                        status = step8.Status;
                                    }
                                }
                            }
                            var item = new CementItem()
                            {
                                User = user != null ? user.FirstName + " " + user.LastName : string.Empty,
                                City = data != null ? data.City : string.Empty,
                                Name = data != null ? data.RecipientName : string.Empty
                            };

                            switch (status)
                            {
                                case 0:
                                    item.Status = "Niedokończony";
                                    break;
                                case 1:
                                    item.Status = "Zielony";
                                    break;
                                case 2:
                                    item.Status = "Żółty";
                                    break;
                                case 3:
                                    item.Status = "Brązowy";
                                    break;
                            }

                            list.Add(item);
                        }
                    }
                }
            }

            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Raport");

            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("Nazwa");
            row.CreateCell(1).SetCellValue("Status");
            row.CreateCell(2).SetCellValue("Miasto");
            row.CreateCell(3).SetCellValue("Użytkownik");
            var i = 1;
            foreach (var item in list)
            {
                row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.Name);
                row.CreateCell(1).SetCellValue(item.Status);
                row.CreateCell(2).SetCellValue(item.City);
                row.CreateCell(3).SetCellValue(item.User);
                i++;
            }

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport__procent_cementy" + DateTime.Now.ToShortDateString() + ".xls");
        }

        public FileResult GetCementPriceItemsReport()
        {
            var businessses = _context.Businesses.ToList();
            var list = new List<CementItem>();
            foreach (var business in businessses)
            {
                var visits = _context.Visits.Where(v => v.BusinessId == business.Id).ToList();
                foreach (var visit in visits)
                {
                    var step2 =
                        _context.ClientSteps2.Include(s => s.LaxTypes)
                            .Include(s => s.PackedTypes)
                            .Where(s => s.VisitId == visit.Id)
                            .AsEnumerable()
                            .LastOrDefault();

                    var step7 =
                        _context.ClientSteps7.Include(s => s.LaxTypes)
                            .Include(s => s.PackedTypes)
                            .Where(s => s.VisitId == visit.Id)
                            .AsEnumerable()
                            .LastOrDefault();

                    if (step2 != null && step7 != null)
                    {
                        if (step2.LaxTypes.Count != step7.LaxTypes.Count ||
                            !step2.LaxTypes.All(t => step7.LaxTypes.Any(c => c.CementId == t.LaxTypeId)) ||
                            step2.PackedTypes.Count != step7.PackedTypes.Count ||
                            !step2.PackedTypes.All(t => step7.PackedTypes.Any(c => c.CementId == t.PackedTypeId)))
                        {
                            var user = _context.Users.FirstOrDefault(u => u.Id == business.CreatorId);
                            var data =
                                _context.BusinessDatas.Where(b => b.BusinessId == business.Id)
                                    .AsEnumerable()
                                    .LastOrDefault();
                            var status = 0;
                            if (data != null)
                            {
                                var lastAddedVisit = visits.FirstOrDefault(v => v.VisitDate == visits.Max(a => a.VisitDate));
                                if (lastAddedVisit != null)
                                {
                                    var step8 = _context.ClientSteps8.FirstOrDefault(c => c.VisitId == lastAddedVisit.Id);
                                    if (step8 != null)
                                    {
                                        status = step8.Status;
                                    }
                                }
                            }
                            var item = new CementItem()
                            {
                                User = user != null ? user.FirstName + " " + user.LastName : string.Empty,
                                City = data != null ? data.City : string.Empty,
                                Name = data != null ? data.RecipientName : string.Empty
                            };

                            switch (status)
                            {
                                case 0:
                                    item.Status = "Niedokończony";
                                    break;
                                case 1:
                                    item.Status = "Zielony";
                                    break;
                                case 2:
                                    item.Status = "Żółty";
                                    break;
                                case 3:
                                    item.Status = "Brązowy";
                                    break;
                            }

                            list.Add(item);
                        }
                    }
                }
            }

            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Raport");

            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("Nazwa");
            row.CreateCell(1).SetCellValue("Status");
            row.CreateCell(2).SetCellValue("Miasto");
            row.CreateCell(3).SetCellValue("Użytkownik");
            var i = 1;
            foreach (var item in list)
            {
                row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.Name);
                row.CreateCell(1).SetCellValue(item.Status);
                row.CreateCell(2).SetCellValue(item.City);
                row.CreateCell(3).SetCellValue(item.User);
                i++;
            }

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport_cementy_typy_ceny" + DateTime.Now.ToShortDateString() + ".xls");
        }

        public FileResult GetCementGroupsReport()
        {
            var businessses = _context.Businesses.ToList();
            var list = new List<CementItem>();
            foreach (var business in businessses)
            {
                var visits = _context.Visits.Where(v => v.BusinessId == business.Id).ToList();
                foreach (var visit in visits)
                {
                    var step2 =
                        _context.ClientSteps2.Include(s => s.LaxTypes)
                            .Include(s => s.PackedTypes)
                            .Where(s => s.VisitId == visit.Id)
                            .AsEnumerable()
                            .LastOrDefault();

                    var step3 =
                        _context.ClientSteps3.Include(s => s.ManufacturersGroups)
                            .Include(s => s.Manufacturers)
                            .Where(s => s.VisitId == visit.Id)
                            .AsEnumerable()
                            .LastOrDefault();

                    if (step2 != null && step3 != null)
                    {
                        var laxPercent = step2.Assortment.PackageId == 1
                            ? 100
                            : (step2.Assortment.PackageId == 3 ? step2.Assortment.LaxCementPercent : 0);

                        var packedPercent = 100 - laxPercent;

                        var lax = new List<SelectedManufacturersGroups>();
                        if(step2.LaxTypes != null)
                        {
                            step2.LaxTypes.ForEach(s => s.LaxType = _context.LaxCementTypes.FirstOrDefault(t => t.Id == s.LaxTypeId));
                            foreach (var group in step2.LaxTypes.Where(s => s.LaxType != null).GroupBy(s => _context.Producers.First(p => p.Id == s.LaxType.ProducerId).Group))
                            {
                                var item = new SelectedManufacturersGroups
                                {
                                    Id = @group.Key,
                                    Percent = (int) Math.Round(@group.Sum(s => s.Percent*(laxPercent/100.0)))
                                };
                                lax.Add(item);
                            }
                        }

                        if(step2.PackedTypes != null)
                        {
                            step2.PackedTypes.ForEach(s => s.PackedType = _context.PackedCementTypes.FirstOrDefault(t => t.Id == s.PackedTypeId));
                            foreach (var group in step2.PackedTypes.Where(s => s.PackedType != null).GroupBy(s => _context.Producers.First(p => p.Id == s.PackedType.ProducerId).Group))
                            {
                                var item = new SelectedManufacturersGroups()
                                {
                                    Id = @group.Key,
                                    Percent = (int) Math.Round(@group.Sum(s => s.Percent*(packedPercent/100.0)))
                                };
                                if (lax.Any(c => c.Id == item.Id))
                                {
                                    lax.First(s => s.Id == item.Id).Percent += item.Percent;
                                }
                                else
                                {
                                    lax.Add(item);
                                }
                            }
                        }

                        var control = new List<SelectedManufacturersGroups>();

                        foreach (var group in step3.ManufacturersGroups)
                        {
                            if (step3.Manufacturers.Any(m => m.GroupId == group.ManufacturersGroupId))
                            {
                                var item = new SelectedManufacturersGroups()
                                {
                                    Id = group.ManufacturersGroupId,
                                    Percent = step3.Manufacturers.Sum(m => m.Percent)
                                };
                                control.Add(item);
                            }
                            else
                            {
                                var item = new SelectedManufacturersGroups()
                                {
                                    Id = group.ManufacturersGroupId,
                                    Percent = group.Percent
                                };
                                control.Add(item);
                            }
                        }

                        if (!control.All(g => lax.Any(s => s.Id == g.Id) && Math.Abs(g.Percent - (lax.First(m => m.Id == g.Id)).Percent) < 1))
                        {
                            var user = _context.Users.FirstOrDefault(u => u.Id == business.CreatorId);
                            var data =
                                _context.BusinessDatas.Where(b => b.BusinessId == business.Id)
                                    .AsEnumerable()
                                    .LastOrDefault();
                            var status = 0;
                            if (data != null)
                            {
                                var lastAddedVisit = visits.FirstOrDefault(v => v.VisitDate == visits.Max(a => a.VisitDate));
                                if (lastAddedVisit != null)
                                {
                                    var step8 = _context.ClientSteps8.FirstOrDefault(c => c.VisitId == lastAddedVisit.Id);
                                    if (step8 != null)
                                    {
                                        status = step8.Status;
                                    }
                                }
                            }
                            var item = new CementItem()
                            {
                                User = user != null ? user.FirstName + " " + user.LastName : string.Empty,
                                City = data != null ? data.City : string.Empty,
                                Name = data != null ? data.RecipientName : string.Empty
                            };

                            switch (status)
                            {
                                case 0:
                                    item.Status = "Niedokończony";
                                    break;
                                case 1:
                                    item.Status = "Zielony";
                                    break;
                                case 2:
                                    item.Status = "Żółty";
                                    break;
                                case 3:
                                    item.Status = "Brązowy";
                                    break;
                            }

                            list.Add(item);
                        }
                    }
                }
            }

            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("Raport");

            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("Nazwa");
            row.CreateCell(1).SetCellValue("Status");
            row.CreateCell(2).SetCellValue("Miasto");
            row.CreateCell(3).SetCellValue("Użytkownik");
            var i = 1;
            foreach (var item in list)
            {
                row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.Name);
                row.CreateCell(1).SetCellValue(item.Status);
                row.CreateCell(2).SetCellValue(item.City);
                row.CreateCell(3).SetCellValue(item.User);
                i++;
            }

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport_cementy_grupy" + DateTime.Now.ToShortDateString() + ".xls");
        }

        private void AddRow(ref ISheet sheet, ref int rowNumber, Business business)
        {
            var user = _context.Users.Single(u => u.Id == business.CreatorId);
            var data = _context.BusinessDatas.SingleOrDefault(d => d.BusinessId == business.Id);
            if (data == null) return;
            var row = sheet.CreateRow(rowNumber);
            var details = ClientData.GetClientDetails(data, _context);
            var visits = _context.Visits.Where(v => v.BusinessId == business.Id).ToList();
            foreach (var visit in visits)
            {
                var i = 0;
                var b = _context.Businesses.Single(bis => bis.Id == visit.BusinessId);
                var status = 0;
                if (data != null)
                {
                    var vis = _context.Visits.Where(v => v.BusinessId == b.Id);
                    var lastAddedVisit = visits.FirstOrDefault(v => v.VisitDate == vis.Max(a => a.VisitDate));
                    if (lastAddedVisit != null)
                    {
                        var s8 = _context.ClientSteps8.FirstOrDefault(c => c.VisitId == lastAddedVisit.Id);
                        if (s8 != null)
                        {
                            status = s8.Status;
                        }
                    }
                }
                var states = new List<string>
                {
                    "Niedokończona",
                    "Zielony",
                    "Żółty",
                    "Brązowy"
                };
                row.CreateCell(i++).SetCellValue(user.FirstName + " " + user.LastName);
                row.CreateCell(i++).SetCellValue(data.AddDate.ToString("dd-MM-yyyy"));
                row.CreateCell(i++).SetCellValue(states[status]);
                row.CreateCell(i++).SetCellValue(details.Status);
                row.CreateCell(i++).SetCellValue(details.CooperationType);
                row.CreateCell(i++).SetCellValue(details.Name);
                row.CreateCell(i++).SetCellValue(details.Street);
                row.CreateCell(i++).SetCellValue(details.BuildingNumebr);
                row.CreateCell(i++).SetCellValue(details.PostCode);
                row.CreateCell(i++).SetCellValue(details.Province);
                row.CreateCell(i++).SetCellValue(details.District);
                row.CreateCell(i++).SetCellValue(details.City);
                row.CreateCell(i++).SetCellValue(details.NIP);
                row.CreateCell(i++).SetCellValue(details.LegalForm);
                row.CreateCell(i++).SetCellValue(details.GroupMember);
                row.CreateCell(i++).SetCellValue(details.Group);
                row.CreateCell(i++).SetCellValue(details.Owner);
                row.CreateCell(i++).SetCellValue(details.OwnerPhone);
                row.CreateCell(i++).SetCellValue(details.Phone1);
                row.CreateCell(i++).SetCellValue(details.Phone2);
                row.CreateCell(i++).SetCellValue(details.Phone3);
                row.CreateCell(i++).SetCellValue(details.Email);
                row.CreateCell(i++).SetCellValue(details.Website);
                row.CreateCell(i++).SetCellValue(details.Agent);
                row.CreateCell(i++).SetCellValue(details.ContactPerson);
                row.CreateCell(i++).SetCellValue(details.ContactPersonPosition);
                row.CreateCell(i++).SetCellValue(details.ContactPersonEmail);
                row.CreateCell(i++).SetCellValue(details.ContactPersonPhone);
                row.CreateCell(i++).SetCellValue(visit.VisitDate.ToString("yyyy-MM-dd"));
                row.CreateCell(i++).SetCellValue(_context.MarketSegments.Single(s => s.Id == visit.MainMarketSegmentId).Name);

                var step2 = _context.ClientSteps2.Include(s => s.Assortment).Include(s => s.LaxTypes).Include(s => s.PackedTypes).OrderByDescending(s => s.Id).FirstOrDefault(s => s.VisitId == visit.Id);
                if (step2 != null)
                {
                    row.CreateCell(i++).SetCellValue(step2.Assortment.AnnualNeed + " ton");
                    row.CreateCell(i++).SetCellValue(_context.PartsOfCement.Single(p => p.Id == step2.Assortment.PartOfCementId).Name);
                    row.CreateCell(i++).SetCellValue(_context.KindsOfPackage.Single(p => p.Id == step2.Assortment.PackageId).Name);
                    row.CreateCell(i++).SetCellValue(step2.Assortment.CustomBrand);
                    row.CreateCell(i++).SetCellValue(step2.Assortment.CommonMarketing);
                    row.CreateCell(i++).SetCellValue(step2.Assortment.AdvisoryService);
                    row.CreateCell(i++).SetCellValue(step2.Assortment.FreshCement);
                    row.CreateCell(i++).SetCellValue(step2.Assortment.CostlessComplaint);
                    row.CreateCell(i++).SetCellValue(step2.Assortment.Others);
                    row.CreateCell(i++).SetCellValue(step2.Assortment.AffectTheChoice);
                    row.CreateCell(i++).SetCellValue(step2.Assortment.QualityComments);
                    row.CreateCell(i++).SetCellValue("Luz");
                    AddLaxCementType(row, "CEM I", "CEM II,CEM III,CEM IV", "32,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM I", "CEM II,CEM III,CEM IV", "42,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM I", "CEM II,CEM III,CEM IV", "52,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM II", "CEM III", "32,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM II", "CEM III", "42,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM II", "CEM III", "52,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM III", "", "32,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM III", "", "42,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM III", "", "52,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM IV", "", "32,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM IV", "", "42,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM IV", "", "52,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM V", "", "22,5", i++, step2.LaxTypes);
                    AddLaxCementType(row, "CEM V", "", "32,5", i++, step2.LaxTypes);
                    row.CreateCell(i++).SetCellValue("Worek");
                    AddPackedCementType(row, "CEM I", "CEM II,CEM III,CEM IV", "32,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM I", "CEM II,CEM III,CEM IV", "42,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM I", "CEM II,CEM III,CEM IV", "52,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM II", "CEM III", " 32,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM II", "CEM III", " 42,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM II", "CEM III", " 52,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM III", "", " 32,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM III", "", " 42,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM III", "", " 52,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM IV", "", " 32,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM IV", "", " 42,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM IV", "", " 52,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM V", "", " 22,5", i++, step2.PackedTypes);
                    AddPackedCementType(row, "CEM V", "", " 32,5", i++, step2.PackedTypes);
                    var procentLax = 0;
                    var procentPacked = 0;
                    if (step2.Assortment.PackageId == 1)
                    {
                        procentLax = 100;
                    }
                    else if (step2.Assortment.PackageId == 2)
                    {
                        procentPacked = 100;
                    }
                    else
                    {
                        procentLax = step2.Assortment.LaxCementPercent;
                        procentPacked = 100 - step2.Assortment.LaxCementPercent;
                    }
                    row.CreateCell(i++).SetCellValue(procentLax + "%");
                    row.CreateCell(i++).SetCellValue(procentPacked + "%");
                }
                else
                {
                    i += 43;
                }

                var step3 = _context.ClientSteps3.OrderByDescending(s => s.Id).FirstOrDefault(s => s.VisitId == visit.Id);
                if (step3 != null)
                {
                    row.CreateCell(i++).SetCellValue(step3.Cooperation ? "Tak" : "Nie");
                }

                var step35 = _context.ClientSteps3_5.Include(s => s.Distributors).OrderByDescending(s => s.Id).FirstOrDefault(s => s.VisitId == visit.Id);
                row.CreateCell(i++).SetCellValue("Bezpośrednie");
                if (step35 != null)
                {
                    var count = 0;
                    foreach (var groups in step35.ManufacturersGroups)
                    {
                        var group = _context.ManufacturersGroups.SingleOrDefault(g => g.Id == groups.ManufacturersGroupId);
                        groups.Percent = step35.Manufacturers.Any(m => m.GroupId == groups.ManufacturersGroupId) ? step35.Manufacturers.Where(m => m.GroupId == groups.ManufacturersGroupId).Sum(m => m.Percent) : groups.Percent;
                        if (@group != null)
                        {
                            row.CreateCell(i++).SetCellValue(@group.Name);
                            row.CreateCell(i++).SetCellValue(groups.Percent + "%");
                            count++;
                        }
                        if (count == 3) break;
                    }
                    if (count < 3)
                    {
                        foreach (var manufacturer in step35.Manufacturers)
                        {
                            var man = _context.Manufacturers.SingleOrDefault(m => m.Id == manufacturer.ManufacturerId);
                            if (man != null && man.GroupId == 0)
                            {
                                row.CreateCell(i++).SetCellValue(man.Name);
                                row.CreateCell(i++).SetCellValue(manufacturer.Percent + "%");
                                count++;
                            }
                            if (count == 3) break;
                        }
                    }
                    i += 6 - (2 * count);
                    count = 0;
                    row.CreateCell(i++).SetCellValue("Pośrednie");
                    foreach (var distributor in step35.Distributors)
                    {
                        var dist = _context.Distributors.SingleOrDefault(d => d.Id == distributor.DistributorId);
                        if (dist != null)
                        {
                            row.CreateCell(i++).SetCellValue(dist.Name);
                            row.CreateCell(i++).SetCellValue(distributor.Percent + "%");
                            count++;
                        }
                        if (count == 3) break;
                    }
                    i += 6 - (2 * count);
                }
                else
                {
                    i += 6;
                    row.CreateCell(i++).SetCellValue("Pośrednie");
                    i += 6;
                }
                var step4 = _context.ClientSteps4.Include(c => c.Logistic).OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == visit.Id);
                if (step4 != null)
                {
                    var yesNo = new List<string>() {
                        "Brak danych",
                        "Tak",
                        "Nie",
                        "Nie dotyczy"
                    };
                    row.CreateCell(i++).SetCellValue(step4.Logistic.NumberOfSilos);
                    row.CreateCell(i++).SetCellValue(step4.Logistic.CapacitySilos);
                    row.CreateCell(i++).SetCellValue(step4.Logistic.CoveredMagazineCapacity);
                    row.CreateCell(i++).SetCellValue(step4.Logistic.NotCoveredMagazineCapacity);
                    row.CreateCell(i++).SetCellValue(yesNo[step4.Logistic.RailwaySiding - 1]);
                    row.CreateCell(i++).SetCellValue(step4.Logistic.ReceiveCementByRailwaySiding != null ? yesNo[step4.Logistic.ReceiveCementByRailwaySiding.Value - 1] : "");
                    row.CreateCell(i++).SetCellValue(yesNo[step4.Logistic.OwnCementTank - 1]);
                    row.CreateCell(i++).SetCellValue(step4.Logistic.NumberOwnCementTank);
                    row.CreateCell(i++).SetCellValue(yesNo[step4.Logistic.OwnPlatformTypeCar - 1]);
                    row.CreateCell(i++).SetCellValue(step4.Logistic.NumberOwnPlatformTypeCar);
                    row.CreateCell(i++).SetCellValue(yesNo[step4.Logistic.OwnHDSTypeCar - 1]);
                    row.CreateCell(i++).SetCellValue(step4.Logistic.NumberOwnHDSTypeCar);
                    row.CreateCell(i++).SetCellValue(yesNo[step4.Logistic.CountryLicense - 1]);
                    row.CreateCell(i++).SetCellValue(yesNo[step4.Logistic.AbroadLicense - 1]);
                    row.CreateCell(i++).SetCellValue(yesNo[step4.Logistic.LaxDelivery - 1]);
                    row.CreateCell(i++).SetCellValue(step4.Logistic.MaxLaxDelivery);
                    row.CreateCell(i++).SetCellValue(yesNo[step4.Logistic.PackedDelivery - 1]);
                    row.CreateCell(i++).SetCellValue(step4.Logistic.MaxPackedDelivery);
                }

                var step5 = _context.ClientSteps5.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == visit.Id);
                if(step5 != null)
                {
                    var maxClients = _context.ClientSteps5.Include(c => c.Clients).Select(c => c.Clients.Count).Max();
                    row.CreateCell(i++).SetCellValue(step5.Annual);
                    if(step5.Clients != null)
                    {
                        foreach(var c in step5.Clients)
                        {
                            CreateClientCells(_context.Clients.Single(s => s.Id == c.ClientId).Name, c.Percent, ref i, row);
                        }
                        if(step5.Clients.Count < maxClients)
                        {
                            i += ((maxClients - step5.Clients.Count) * 2);
                        }
                    }
                    else
                    {
                        i += (2 * maxClients);
                    }
                }

                var step6 = _context.ClientSteps6.OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == visit.Id);
                if (step6 != null)
                {
                    var yesNo = new List<string>() {
                        "Brak danych",
                        "Tak",
                        "Nie",
                        "Nie dotyczy"
                    };

                    row.CreateCell(i++).SetCellValue(yesNo[step6.OwnLaboratory - 1]);
                    row.CreateCell(i++).SetCellValue(yesNo[step6.ForeignLaboratory - 1]);
                }

                var step8 = _context.ClientSteps8.Include(c => c.Notes).OrderByDescending(c => c.Id).FirstOrDefault(c => c.VisitId == visit.Id);
                if (step8 != null)
                {
                    if (step8.Notes != null)
                    {
                        foreach (var note in step8.Notes)
                        {
                            row.CreateCell(i++).SetCellValue(note.Text);
                        }
                    }
                }
            }
        }

        private void AddLaxCementType(IRow row, string cementClass, string notClass, string number, int i, ICollection<VisitLaxType> types)
        {
            var percent = 0;
            foreach (var item in types)
            {
                if (item.Percent == 0 || item.LaxTypeId == 0) continue;
                var type = _context.LaxCementTypes.SingleOrDefault(d => d.Id == item.LaxTypeId);
                if (type == null || !type.Name.Contains(cementClass) || !type.Name.Contains(number)) continue;
                if (!string.IsNullOrEmpty(notClass))
                {
                    if (notClass.Split(',').Any(t => type.Name.Contains(t)))
                    {
                        break;
                    }
                }
                percent += item.Percent;
            }
            row.CreateCell(i).SetCellValue(percent + "%");

        }

        private void AddPackedCementType(IRow row, string cementClass, string notClass, string number, int i, ICollection<VisitPackedType> types)
        {
            var percent = 0;
            foreach (var item in types)
            {
                if (item.Percent == 0 || item.PackedTypeId == 0) continue;
                var type = _context.PackedCementTypes.SingleOrDefault(d => d.Id == item.PackedTypeId);
                if (type == null || !type.Name.Contains(cementClass) || !type.Name.Contains(number)) continue;
                if (!string.IsNullOrEmpty(notClass))
                {
                    if (notClass.Split(',').Any(t => type.Name.Contains(t)))
                    {
                        break;
                    }
                }
                percent += item.Percent;
            }
            row.CreateCell(i).SetCellValue(percent + "%");
        }

        private static void CreateClientCells(string name, int percent, ref int i, IRow row)
        {
            row.CreateCell(i++).SetCellValue(name);
            row.CreateCell(i++).SetCellValue(percent + "%");
        }
    }

    class CementItem
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string City { get; set; }
        public string User { get; set; }
    }
}
