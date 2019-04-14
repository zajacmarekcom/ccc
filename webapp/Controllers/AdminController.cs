using webapp.Models;
using webapp.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Web.Hosting;
using webapp.Enums;
using webapp.Repository;
using webapp.Utils.ControllersHelpers;

namespace webapp.Controllers
{
    public class AdminController : Controller
    {
        readonly WebappContext _context = new WebappContext();

        [Authorize(Roles="admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles="admin")]
        public ActionResult Users()
        {
            var users = _context.Users.ToList();
            @ViewBag.Roles = _context.Roles.ToList();
            var data = new AdminUsers
            {
                Users = users
            };
            return View(data);
        }

        [Authorize(Roles="admin")]
        [HttpPost]
        public ActionResult Users(AdminUsers data)
        {
            foreach(var u in data.Users)
            {
                _context.Entry(u).State = EntityState.Modified;
            }
            _context.SaveChanges();
            ViewBag.Roles = _context.Roles.ToList();
            return View(data);
        }

        public ActionResult NoDistributors()
        {
            var step = _context.ClientSteps3_5.Include(d => d.Distributors).Where(d => d.ReceiptId == 2 || d.ReceiptId == 3).ToList();
            step = step.Where(d => d.Distributors == null || d.Distributors.Count == 0).ToList();
            var model = new List<BusinessUserViewModel>();
            foreach(var d in step)
            {
                var visit = _context.Visits.OrderByDescending(o => o.Id).First(s => s.Id == d.VisitId);
                var data = _context.BusinessDatas.OrderByDescending(o => o.Id).First(b => b.BusinessId == visit.BusinessId);
                var business = _context.Businesses.OrderByDescending(o => o.Id).First(b => b.Id == data.BusinessId);
                var user = _context.Users.OrderByDescending(o => o.Id).First(u => u.Id == business.CreatorId);

                model.Add(new BusinessUserViewModel
                {
                    City = data.City,
                    Name = data.RecipientName,
                    User = user.FirstName + " " + user.LastName
                });
            }

            return View(model);
        }

        public FileResult NoDistributorsXLS()
        {
            var step = _context.ClientSteps3_5.Include(d => d.Distributors).Where(d => d.ReceiptId == 2 || d.ReceiptId == 3).ToList();
            step = step.Where(d => d.Distributors == null || d.Distributors.Count == 0).ToList();
            var model = new List<BusinessUserViewModel>();
            foreach (var d in step)
            {
                var visit = _context.Visits.OrderByDescending(o => o.Id).First(s => s.Id == d.VisitId);
                var data = _context.BusinessDatas.OrderByDescending(o => o.Id).First(b => b.BusinessId == visit.BusinessId);
                var business = _context.Businesses.OrderByDescending(o => o.Id).First(b => b.Id == data.BusinessId);
                var user = _context.Users.OrderByDescending(o => o.Id).First(u => u.Id == business.CreatorId);

                model.Add(new BusinessUserViewModel
                {
                    City = data.City,
                    Name = data.RecipientName,
                    User = user.FirstName + " " + user.LastName
                });
            }

            IWorkbook workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Raport");
            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("Nazwa");
            row.CreateCell(1).SetCellValue("Miejscowość");
            row.CreateCell(2).SetCellValue("Użytkownik");
            var i = 1;
            foreach(var item in model)
            {
                row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.Name);
                row.CreateCell(1).SetCellValue(item.City);
                row.CreateCell(2).SetCellValue(item.User);
                i++;
            }

            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport_dystrybutorzy.xls");
        }

        public ActionResult HavePrices()
        {
            var step = _context.ClientSteps7.Include(s => s.LaxTypes).Include(s => s.PackedTypes).ToList();
            step = step.Where(s => (s.LaxTypes != null && s.LaxTypes.Count > 0) || (s.PackedTypes != null && s.PackedTypes.Count > 0)).ToList();
            var model = new List<BusinessUserViewModel>();
            foreach(var d in step)
            {
                var visit = _context.Visits.OrderByDescending(o => o.Id).First(s => s.Id == d.VisitId);
                var data = _context.BusinessDatas.OrderByDescending(o => o.Id).First(b => b.BusinessId == visit.BusinessId);
                var business = _context.Businesses.OrderByDescending(o => o.Id).First(b => b.Id == data.BusinessId);
                var user = _context.Users.OrderByDescending(o => o.Id).First(u => u.Id == business.CreatorId);

                model.Add(new BusinessUserViewModel
                {
                    City = data.City,
                    Name = data.RecipientName,
                    User = user.FirstName + " " + user.LastName
                });
            }

            return View(model);
        }

        public FileResult HavePricesXLS()
        {
            var step = _context.ClientSteps7.Include(s => s.LaxTypes).Include(s => s.PackedTypes).ToList();
            step = step.Where(s => (s.LaxTypes != null && s.LaxTypes.Count > 0) || (s.PackedTypes != null && s.PackedTypes.Count > 0)).ToList();
            var model = new List<BusinessUserViewModel>();
            foreach (var d in step)
            {
                var visit = _context.Visits.OrderByDescending(o => o.Id).First(s => s.Id == d.VisitId);
                var data = _context.BusinessDatas.OrderByDescending(o => o.Id).First(b => b.BusinessId == visit.BusinessId);
                var business = _context.Businesses.OrderByDescending(o => o.Id).First(b => b.Id == data.BusinessId);
                var user = _context.Users.OrderByDescending(o => o.Id).First(u => u.Id == business.CreatorId);

                model.Add(new BusinessUserViewModel
                {
                    City = data.City,
                    Name = data.RecipientName,
                    User = user.FirstName + " " + user.LastName
                });
            }

            IWorkbook workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Raport");
            var row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("Nazwa");
            row.CreateCell(1).SetCellValue("Miejscowość");
            row.CreateCell(2).SetCellValue("Użytkownik");
            var i = 1;
            foreach (var item in model)
            {
                row = sheet.CreateRow(i);
                row.CreateCell(0).SetCellValue(item.Name);
                row.CreateCell(1).SetCellValue(item.City);
                row.CreateCell(2).SetCellValue(item.User);
                i++;
            }

            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);

            var stream = new MemoryStream();
            workbook.Write(stream);

            return File(new MemoryStream(stream.GetBuffer()), "application/vnd.ms-excel", "raport_dystrybutorzy.xls");
        }

        public ActionResult ProducersList()
        {
            var model = _context.Producers.ToList();

            return View(model);
        }

        public ActionResult AddProducer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddProducer(Producer producer)
        {
            _context.Producers.Add(producer);
            _context.SaveChanges();

            return RedirectToAction("ProducersList");
        }

        public ActionResult LaxCementsList()
        {
            var list = _context.LaxCementTypes.ToList();

            var model = list.Select(item => new CementViewModel
            {
                Name = item.Name,
                Producer = _context.Producers.Single(d => d.Id == item.ProducerId).Name
            }).GroupBy(a => a.Producer);

            return View(model);
        }

        public ActionResult AddLaxCement()
        {
            ViewBag.Producers = _context.Producers.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult AddLaxCement(LaxCementType cement)
        {
            _context.LaxCementTypes.Add(cement);
            _context.SaveChanges();

            return RedirectToAction("LaxCementsList");
        }

        public ActionResult PackedCementsList()
        {
            var list = _context.PackedCementTypes.ToList();

            var model = list.Select(item => new CementViewModel
            {
                Name = item.Name, Producer = _context.Producers.Single(d => d.Id == item.ProducerId).Name
            }).GroupBy(a => a.Producer);

            return View(model);
        }

        public ActionResult AddPackedCement()
        {
            ViewBag.Producers = _context.Producers.ToList();

            return View();
        }

        [HttpPost]
        public ActionResult AddPackedCement(PackedCementType cement)
        {
            _context.PackedCementTypes.Add(cement);
            _context.SaveChanges();

            return RedirectToAction("PackedCementsList");
        }

        public ActionResult SearchClient()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SearchResult(string query)
        {
            var businesses = _context.Businesses.Select(b => b.Id).ToList();
            var result = _context.BusinessDatas.Where(b => (b.NIP.Replace("-", "").Contains(query.Replace("-", "")) || b.RecipientName.Contains(query))).ToList();
            var datas = result.Where(d => businesses.Contains(d.BusinessId)).ToList();

            var businessItems = new List<BusinessItem>();
            foreach (var b in datas)
            {
                var status = 0;
                var name = b.RecipientName;
                var visits = _context.Visits.Where(v => v.BusinessId == b.BusinessId);
                var lastAddedVisit = visits.Where(v => v.VisitDate == visits.Max(a => a.VisitDate)).OrderByDescending(v => v.Id).FirstOrDefault();
                if (lastAddedVisit != null)
                {
                    var step1 = _context.ClientSteps1.Where(c => c.VisitId == lastAddedVisit.Id).OrderByDescending(s => s.Id).FirstOrDefault();
                    var step2 = _context.ClientSteps2.Where(c => c.VisitId == lastAddedVisit.Id).OrderByDescending(s => s.Id).FirstOrDefault();
                    var step3 = _context.ClientSteps3.Where(c => c.VisitId == lastAddedVisit.Id).OrderByDescending(s => s.Id).FirstOrDefault();
                    var step4 = _context.ClientSteps4.Where(c => c.VisitId == lastAddedVisit.Id).OrderByDescending(s => s.Id).FirstOrDefault();
                    var step5 = _context.ClientSteps5.Where(c => c.VisitId == lastAddedVisit.Id).OrderByDescending(s => s.Id).FirstOrDefault();
                    var step6 = _context.ClientSteps6.Where(c => c.VisitId == lastAddedVisit.Id).OrderByDescending(s => s.Id).FirstOrDefault();
                    var step7 = _context.ClientSteps7.Where(c => c.VisitId == lastAddedVisit.Id).OrderByDescending(s => s.Id).FirstOrDefault();
                    var step8 = _context.ClientSteps8.Where(c => c.VisitId == lastAddedVisit.Id).OrderByDescending(s => s.Id).FirstOrDefault();
                    if (step1 != null && step2 != null && step3 != null && step4 != null && step5 != null && step6 != null && step7 != null && step8 != null)
                    {
                        status = step8.Status;
                    }
                }
                businessItems.Add(new BusinessItem
                {
                    Id = b.BusinessId,
                    Name = name,
                    Status = (BusinessStatus) status
                });
            }

            return View(businessItems);
        }

        public ActionResult DeleteClient(int id)
        {
            var business = _context.Businesses.Single(b => b.Id == id);
            var data = _context.BusinessDatas.Single(d => d.BusinessId == id);
            _context.BusinessDatas.Remove(data);
            var visits = _context.Visits.Where(v => v.BusinessId == id).ToList();
            foreach(var v in visits)
            {
                var step1 = _context.ClientSteps1.Include(s => s.VisitMarketSegments).Include(s => s.Branches).Where(s => s.VisitId == v.Id).ToList();
                foreach(var s in step1)
                {
                    _context.ClientSteps1.Remove(s);
                }
                var step2 = _context.ClientSteps2.Include(s => s.Assortment).Include(s => s.LaxTypes).Include(s => s.PackedTypes).Where(s => s.VisitId == v.Id).ToList();
                foreach (var s in step2)
                {
                    _context.ClientSteps2.Remove(s);
                }
                var step3 = _context.ClientSteps3.Include(s => s.Manufacturers).Include(s => s.ManufacturersGroups).Include(s => s.Suppliers).Where(s => s.VisitId == v.Id).ToList();
                foreach (var s in step3)
                {
                    _context.ClientSteps3.Remove(s);
                }
                var step3_5 = _context.ClientSteps3_5.Include(s => s.Distributors).Include(s => s.Manufacturers).Include(s => s.ManufacturersGroups).Where(s => s.VisitId == v.Id).ToList();
                foreach (var s in step3_5)
                {
                    _context.ClientSteps3_5.Remove(s);
                }
                var step4 = _context.ClientSteps4.Include(s => s.Logistic).Include(s => s.UnloadTypes).Where(s => s.VisitId == v.Id).ToList();
                foreach (var s in step4)
                {
                    _context.ClientSteps4.Remove(s);
                }
                var step5 = _context.ClientSteps5.Include(s => s.Clients).Where(s => s.VisitId == v.Id).ToList();
                foreach (var s in step5)
                {
                    _context.ClientSteps5.Remove(s);
                }
                var step6 = _context.ClientSteps6.Include(s => s.ForeignLaboratories).Where(s => s.VisitId == v.Id).ToList();
                foreach (var s in step6)
                {
                    _context.ClientSteps6.Remove(s);
                }
                var step7 = _context.ClientSteps7.Include(s => s.PackedTypes).Include(s => s.LaxTypes).Where(s => s.VisitId == v.Id).ToList();
                foreach (var s in step7)
                {
                    _context.ClientSteps7.Remove(s);
                }
                var step8 = _context.ClientSteps8.Include(s => s.Notes).Where(s => s.VisitId == v.Id).ToList();
                foreach (var s in step8)
                {
                    _context.ClientSteps8.Remove(s);
                }
                _context.Visits.Remove(v);
            }
            _context.Businesses.Remove(business);

            _context.SaveChanges();

            return RedirectToAction("SearchClient");
        }

        public ActionResult Distributors(bool? error)
        {
            var model = _context.Distributors.OrderBy(d => d.Name).ToList();

            if(error != null && error.Value)
            {
                ModelState.AddModelError("", "Nie możesz usunąć tego dystrybutora");
            }

            return View(model);
        }

        public ActionResult DeleteDistributor(int id)
        {
            try
            {
                var distributor = _context.Distributors.Single(d => d.Id == id);
                var step = _context.ClientSteps3_5.Include(s => s.Distributors).Where(d => d.Distributors != null).ToList();
                if (step.Any(s => s.Distributors.Any(d => d.DistributorId == distributor.Id)))
                {
                    throw new Exception();
                }
                _context.Distributors.Remove(distributor);
                _context.SaveChanges();
                return RedirectToAction("Distributors");
            }
            catch(Exception ex)
            {
                return RedirectToAction("Distributors", new { error = true });
            }
        }

        public ActionResult AddDistributor()
        {
            ViewBag.Provinces = _context.Provinces.ToList();
            ViewBag.Districts = _context.Districts.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult AddDistributor(Distributor distributor)
        {
            if(ModelState.IsValid)
            {
                _context.Distributors.Add(distributor);
                _context.SaveChanges();
                return RedirectToAction("Distributors");
            }
            ViewBag.Provinces = _context.Provinces.ToList();
            ViewBag.Districts = _context.Districts.ToList();
            return View(distributor);
        }

        public ActionResult DistributorDetails(int id)
        {
            var model = _context.Distributors.Include(d => d.MarketAddresses).SingleOrDefault(d => d.Id == id);
            if(model == null)
            {
                return HttpNotFound();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeMarket(int distributorId)
        {
            var distributor = _context.Distributors.SingleOrDefault(d => d.Id == distributorId);
            if (distributor == null) return RedirectToAction("DistributorDetails", new {id = distributorId});
            distributor.IsMarket = !distributor.IsMarket;
            _context.Entry(distributor).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToAction("DistributorDetails", new { id = distributorId });
        }

        public ActionResult AddMarketAddress(int id)
        {
            var model = new MarketAddress {DistributorId = id};
            var additionalData = AddHelper.FillAdditionalData(_context, 0);
            ViewBag.Data = additionalData;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddMarketAddress(MarketAddress address)
        {
            if (!ModelState.IsValid)
            {
                var additionalData = AddHelper.FillAdditionalData(_context, address.ProvinceId);
                ViewBag.Data = additionalData;
                return View(address);
            }
            var distributor = _context.Distributors.SingleOrDefault(d => d.Id == address.DistributorId);
            if (distributor == null)
                return RedirectToAction("DistributorDetails", new {id = address.DistributorId});
            _context.MarketAddresses.Add(address);
            _context.SaveChanges();
            return RedirectToAction("DistributorDetails", new { id = address.DistributorId });
        }

        [HttpGet]
        public ActionResult AddMissingPositions()
        {
            var datas = _context.BusinessDatas.ToList();
            foreach (var item in datas.Where(x => string.IsNullOrEmpty(x.Lat) || string.IsNullOrEmpty(x.Lng)))
            {
                var data = item;
                DataController.GetPosition(ref data);
                _context.Entry(data);
            }
            _context.SaveChanges();

            return RedirectToAction("PositionsUpdated");
        }

        public ActionResult PositionsUpdated()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult AddFile()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AddFile(HttpPostedFileBase file)
        {
            if (file == null || file.ContentLength <= 0) return RedirectToAction("Index");

            var path = Path.Combine(Server.MapPath("~/Content/"), "data.xls");
            file.SaveAs(path);

            IWorkbook book;
            var count = 0;
            using (var data = new FileStream(HostingEnvironment.MapPath(@"~/Content/data.xls"), FileMode.Open, FileAccess.Read))
            {
                book = WorkbookFactory.Create(data);
            }
            var st = book.GetSheetAt(0);
            for (var b = 0; b <= st.LastRowNum; b++)
            {
                if (st.GetRow(b) != null)
                {
                    ++count;
                }
            }
            var setRepo = new SettingsRepository(_context);
            setRepo.Set("GreenFromFile", count.ToString());

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult LoadPostCodes()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult LoadPostCodes(HttpPostedFileBase file)
        {
            if(file == null || file.ContentLength <= 0) return RedirectToAction("Index");

            IWorkbook book = WorkbookFactory.Create(file.InputStream);
            var st = book.GetSheetAt(0);
            var list = new List<PostCodesTableItem>();
            for (var i = 1; i <= st.LastRowNum; i++)
            {
                var row = st.GetRow(i);
                var item = new PostCodesTableItem
                {
                    PostCode = row.GetCell(0).StringCellValue,
                    City = row.GetCell(1).StringCellValue,
                    District = row.GetCell(3).StringCellValue,
                    Province = row.GetCell(4).StringCellValue
                };
                list.Add(item);
            }

            foreach (var item in list)
            {
                if (_context.Provinces.Any(p => string.Equals(p.Name, item.Province, StringComparison.CurrentCultureIgnoreCase))) continue;
                _context.Provinces.Add(new Province()
                {
                    Name = item.Province
                });
                _context.SaveChanges();
            }
            foreach (var item in list)
            {
                if (_context.Districts.Any(d => d.Name.ToLower() == item.District.ToLower())) continue;
                _context.Districts.Add(new District()
                {
                    Name = item.District,
                    ProvinceId = _context.Provinces.First(p => p.Name.ToLower() == item.Province).Id
                });
                _context.SaveChanges();
            }

            foreach (var item in list)
            {
                if (_context.PostCodes.Any(p => p.PostCodeNumber == item.PostCode)) continue;
                _context.PostCodes.Add(new PostCode()
                {
                    PostCodeNumber = item.PostCode,
                    City = item.City,
                    DistrictId = _context.Districts.First(d => d.Name.ToLower() == item.District.ToLower()).Id,
                    ProvinceId = _context.Provinces.First(p => p.Name.ToLower() == item.Province.ToLower()).Id
                });
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult Groups()
        {
            var groups = _context.ManufacturersGroups.ToList();

            var model = _context.Producers.Select(producer => new ProducerGroupItem()
            {
                Id = producer.Id,
                Name = producer.Name,
                Group = producer.Group != 0 ? groups.First(g => g.Id == producer.Group).Name : string.Empty
            }).ToList();

            return View(model);
        }
    }
}
