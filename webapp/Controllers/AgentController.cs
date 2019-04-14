using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using AutoMapper;
using webapp.Enums;
using webapp.Helpers.Builders;
using webapp.Models;
using webapp.Models.ViewModels;
using webapp.Models.ViewModels.Json;
using webapp.Repository;
using webapp.Security;
using webapp.Utils;
using webapp.Utils.ControllersHelpers;
using webapp.ViewModels.Builders.ClientEdit;
using webapp.ViewModels.Client;
using WebGrease.Css.Extensions;

namespace webapp.Controllers
{
    public class AgentController : Controller
    {
        readonly WebappContext _context;

        private readonly BusinessRepository _businessRepository;
        private readonly CementRepository _cementRepository;
        private readonly VisitRepository _visitRepository;
        private readonly StepRepository _stepRepository;

        public AgentController()
        {
            _context = new WebappContext();
            _businessRepository = new BusinessRepository(_context);
            _cementRepository = new CementRepository(_context);
            _visitRepository = new VisitRepository(_context);
            _stepRepository = new StepRepository(_context);
        }

        [Authorize(Roles = "agent")]
        public ActionResult Index(string sort, int? page)
        {
            var model = IndexViewModelBuilder.CreateIndexiViewModel(User, _context, sort, page);

            return View(model);
        }

        [HttpGet]
        [Authorize(Roles = "agent")]
        public ActionResult Add(bool? edit, int? businessId, bool? newBranch)
        {
            BusinessData model = null;
            if (newBranch != null && newBranch.Value && TempData["BranchData"] != null)
            {
                var data = (BranchDataViewModel)TempData["BranchData"];
                model = BusinessDataBuilder.CreateBusinessDataFromBranchDataViewModel(data);

                TempData["BranchId"] = data.BranchId;

                ViewBag.Data = AddHelper.FillAdditionalData(_context, model.ProvinceId ?? 0);

                return View(model);
            }

            int? provinceId = 0;
            if (edit.HasValue && edit.Value)
            {
                model = _businessRepository.GetData(businessId);
                provinceId = model.ProvinceId;
            }
            ViewBag.Data = AddHelper.FillAdditionalData(_context, provinceId ?? 0);
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "agent")]
        public ActionResult Add(BusinessData data, bool? edit)
        {
            Business business = null;
            if (edit == null || edit.Value == false)
            {
                data.AddDate = DateTime.Now;
                business = new Business { CreatorId = ((CustomPrincipal)User).CustomIdentity.UserId };
            }
            if (!ModelState.IsValid)
            {
                ViewBag.Data = AddHelper.FillAdditionalData(_context, data.ProvinceId);
                return View(data);
            }
            if ((!edit.HasValue || edit.Value == false) && !data.IsBranch && _businessRepository.NipExist(data.NIP))
            {
                ModelState.AddModelError("NIP", "Firma o podanym numerze NIP już została dodana");
                ViewBag.Data = AddHelper.FillAdditionalData(_context, data.ProvinceId ?? 0);
                return View(data);
            }
            if (TempData["BranchId"] != null && (int)(TempData["BranchId"]) != 0)
            {
                var id = (int)TempData["BranchId"];
                var branch = _context.Branches.Single(b => b.Id == id);
                branch.Used = true;
                _context.Entry(branch).State = EntityState.Modified;
            }
            if (edit.HasValue && edit.Value)
            {
                DataController.GetPosition(ref data);
                _context.Entry(data).State = EntityState.Modified;
            }
            else
            {
                _context.Businesses.Add(business);
                _context.SaveChanges();
                data.BusinessId = business.Id;
                DataController.GetPosition(ref data);
                _context.BusinessDatas.Add(data);

            }
            _context.SaveChanges();
            return RedirectToAction("AddVisit_Step1", "Agent", new { businessId = data.BusinessId, edit });
        }

        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step1(int? businessId, bool? edit)
        {
            if (!_businessRepository.Exist(businessId))
            {
                return HttpNotFound();
            }
            NewVisitStep1 model = null;
            ViewBag.MarketSegments = _context.MarketSegments.ToList();
            if (edit != null && edit.Value)
            {
                var visit = _visitRepository.GetVisitByBusinessId(businessId);
                var step1 = _stepRepository.GetStep1(visit.Id);

                var marketSegments = ((ICollection<MarketSegment>)ViewBag.MarketSegments).Select(segment => new SelectedMarketSegment()
                {
                    Id = segment.Id,
                    Name = segment.Name,
                    Checked = step1.VisitMarketSegments.Any(x => x.MarketSegmentId == segment.Id),
                    Percent = step1.VisitMarketSegments.Any(x => x.MarketSegmentId == segment.Id) ? step1.VisitMarketSegments.Last(x => x.MarketSegmentId == segment.Id).Percent : 0,
                    EId = step1.VisitMarketSegments.Any(x => x.MarketSegmentId == segment.Id) ? step1.VisitMarketSegments.Last(x => x.MarketSegmentId == segment.Id).Id : 0
                });
                model = new NewVisitStep1
                {
                    Branches = step1.Branches.ToList(),
                    Visit = visit,
                    MarketSegments = marketSegments
                };
            }
            ViewBag.BusinessId = businessId;
            ViewBag.Provinces = _context.Provinces.ToList();
            ViewBag.Districts = _context.Districts.ToList();

            var selectedSegments =
                ((IEnumerable<MarketSegment>)ViewBag.MarketSegments).Select(
                    SelectedMarketSegmentBuilder.CreateSelectedMarketSegmentFromMarketSegment).ToList();

            ViewBag.SelectedSegments = selectedSegments;
            ViewBag.Users = _context.Users.Where(u => u.RoleId == 3).ToList();

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step1(NewVisitStep1 data, bool? edit)
        {
            ViewBag.BusinessId = data.Visit.BusinessId;
            ViewBag.Provinces = _context.Provinces.ToList();
            ViewBag.Districts = _context.Districts.ToList();
            ViewBag.Users = _context.Users.Where(u => u.RoleId == 3).ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.MarketSegments = _context.MarketSegments.ToList();
                ViewBag.SelectedSegments = ((IEnumerable<MarketSegment>)ViewBag.MarketSegments).Select(
                        SelectedMarketSegmentBuilder.CreateSelectedMarketSegmentFromMarketSegment);

                return View(data);
            }

            var bData = _businessRepository.GetData(data.Visit.BusinessId);
            var status = bData != null ? bData.Status : 1;

            var sum = data.MarketSegments != null && data.MarketSegments.Any(s => s.Checked)
                ? data.MarketSegments.Where(s => s.Checked).Sum(s => s.Percent)
                : 0;
            var max = data.MarketSegments != null && data.MarketSegments.Any(s => s.Checked)
                ? data.MarketSegments.Where(s => s.Checked).Max(s => s.Percent)
                : 0;
            var maxId = data.MarketSegments != null && data.MarketSegments.Any(s => s.Checked)
                ? data.MarketSegments.First(s => s.Checked && s.Percent == max).Id
                : 0;

            Visit visit;
            if (status == 1 && (sum != 100 || maxId != data.Visit.MainMarketSegmentId))
            {
                ViewBag.MarketSegments = _context.MarketSegments.ToList();
                var selectedSegments =
                    ((IEnumerable<MarketSegment>)ViewBag.MarketSegments).Select(
                        SelectedMarketSegmentBuilder.CreateSelectedMarketSegmentFromMarketSegment).ToList();

                ViewBag.SelectedSegments = selectedSegments;

                if (sum != 100)
                {
                    ModelState.AddModelError("", "Całkowita liczba procent musi wynosić 100%");
                }
                if (maxId != data.Visit.MainMarketSegmentId)
                {
                    ModelState.AddModelError("", "Wiodący segment rynku jest niż procentowy wybór sementów rynku");
                }

                return View(data);
            }

            if (!edit.HasValue || edit.Value == false)
            {
                visit = data.Visit;
                _context.Visits.Add(visit);
                _context.SaveChanges();
            }
            else
            {
                visit = _visitRepository.GetVisit(data.Visit.Id);
            }

            var segments = data.MarketSegments.Where(s => s.Checked).Select(s => new VisitMarketSegment
            {
                MarketSegmentId = s.Id,
                VisitId = visit.Id,
                Percent = s.Percent
            }).ToList();
            if (!edit.HasValue || edit.Value == false)
            {
                _context.ClientSteps1.Add(new ClientStep1
                {
                    Branches = data.Branches != null ? data.Branches.ToList() : null,
                    VisitMarketSegments = segments,
                    VisitId = visit.Id
                });
            }
            else
            {
                var step = _stepRepository.GetStep1(visit.Id);
                step.VisitMarketSegments.Clear();
                _context.SaveChanges();
                foreach (var b in (data.Branches ?? new List<Branch>()))
                {
                    if (b.Id != 0)
                    {
                        _context.Entry(b).State = EntityState.Modified;
                    }
                    else
                    {
                        step.Branches.Add(b);
                    }
                }
                step.VisitMarketSegments = segments;
                _context.Entry(step).State = EntityState.Modified;
                _context.Entry(visit).State = EntityState.Modified;
            }
            _context.SaveChanges();

            return RedirectToAction(status != 1 ? "AddVisit_Step8" : "AddVisit_Step2", "Agent",
                new { currentVisit = data.Visit.Id, edit });
        }

        public ActionResult RemoveBranch(int id)
        {
            var branch = _context.Branches.SingleOrDefault(b => b.Id == id);
            if (branch == null) return null;
            _context.Branches.Remove(branch);
            _context.SaveChanges();

            return null;
        }

        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step2(int? currentVisit, bool? edit)
        {
            if (currentVisit == null) return RedirectToAction("Index");

            ViewBag.VisitId = currentVisit;
            ViewBag.PartOfCement = _context.PartsOfCement.ToList();
            ViewBag.KindOfPackage = _context.KindsOfPackage.ToList();
            ViewBag.BrandPower = _context.BrandPowers.ToList();
            ViewBag.LaxProducers = _cementRepository.GetLaxProducers();
            ViewBag.PackedProducers = _cementRepository.GetPackedProducers();
            ViewBag.LaxTypes = _context.LaxCementTypes.ToList();
            ViewBag.PackedTypes = _context.PackedCementTypes.ToList();

            if (!edit.HasValue || edit.Value != true) return View((NewVisitStep2)null);
            var step = _stepRepository.GetStep2(currentVisit);
            if (step == null) return RedirectToAction("AddVisit_Step2", new { currentVisit });
            var data = step.Assortment != null ? new NewVisitStep2 { Assortment = step.Assortment } : null;

            data.LaxTypes = step.LaxTypes.Select(t => new SelectedLaxCementType
            {
                Checked = true,
                Id = t.LaxTypeId,
                Name = t.LaxType.Name,
                Percent = t.Percent
            }).ToList();

            data.PackedTypes = step.PackedTypes.Select(t => new SelectedPackedCementType
            {
                Checked = true,
                Id = t.PackedTypeId,
                Name = t.PackedType.Name,
                Percent = t.Percent
            }).ToList();

            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step2(NewVisitStep2 data, bool? edit)
        {
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                ViewBag.LaxProducers = _cementRepository.GetLaxProducers();
                ViewBag.PackedProducers = _cementRepository.GetPackedProducers();
                ViewBag.LaxTypes = _context.LaxCementTypes.ToList();
                ViewBag.PackedTypes = _context.PackedCementTypes.ToList();
                ViewBag.VisitId = data.VisitId;
                ViewBag.PartOfCement = _context.PartsOfCement.ToList();
                ViewBag.KindOfPackage = _context.KindsOfPackage.ToList();
                ViewBag.BrandPower = _context.BrandPowers.ToList();

                return View(data);
            }

            var step = new ClientStep2();
            if (edit.HasValue && edit.Value)
            {
                step = _stepRepository.GetStep2(data.VisitId);
                step.LaxTypes.Clear();
                step.PackedTypes.Clear();
                step.Assortment = null;
                _context.Entry(step).State = EntityState.Modified;
                _context.SaveChanges();
            }
            step.Assortment = data.Assortment;
            step.LaxTypes = _cementRepository.GetLaxCementTypes(data);
            step.PackedTypes = _cementRepository.GetPackedCementTypes(data);
            step.VisitId = data.VisitId;

            if (!edit.HasValue || edit.Value == false)
            {
                _context.ClientSteps2.Add(step);
            }
            else
            {
                _context.Entry(step).State = EntityState.Modified;
            }
            _context.SaveChanges();

            return RedirectToAction("AddVisit_Step3", new { currentVisit = data.VisitId, edit });
        }


        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step3(int? currentVisit, bool? edit)
        {
            if (currentVisit == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.VisitId = currentVisit;
            var manufacturers = _context.Manufacturers.ToList();
            var manufacturersGroups = _context.ManufacturersGroups.ToList();
            ViewBag.Suppliers = _context.Suppliers.OrderBy(s => s.Name).ToList();
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.Manufacturers = manufacturers;
            ViewBag.ManufacturersGroups = manufacturersGroups;

            NewVisitStep3 data = null;
            if (edit.HasValue && edit.Value)
            {
                var step = _stepRepository.GetStep3(currentVisit);

                if (step == null)
                {
                    return RedirectToAction("AddVisit_Step3", new { currentVisit });
                }
                data = new NewVisitStep3
                {
                    Cooperation = step.Cooperation,
                    Suppliers = step.Suppliers.Select(s => new SelectedSuppliers
                    {
                        Id = s.SupplierId,
                        Name = _context.Suppliers.AsEnumerable().Last(su => su.Id == s.SupplierId).Name,
                        Text = s.Text
                    }).ToList()
                };

                var manufs = new List<SelectedManufacturers>();
                foreach (var man in manufacturers.Select(m => new SelectedManufacturers
                {
                    Checked = false,
                    GroupId = m.GroupId,
                    Id = m.Id
                }))
                {
                    foreach (var vm in step.Manufacturers.Where(ma => ma.ManufacturerId == man.Id))
                    {
                        man.Percent = vm.Percent;
                        man.Checked = true;
                    }
                    manufs.Add(man);
                }

                data.Manufacturers = manufs;

                var mGroups = new List<SelectedManufacturersGroups>();
                foreach (var g in manufacturersGroups)
                {
                    var group = new SelectedManufacturersGroups
                    {
                        Checked = false,
                        Id = g.Id,
                        SelectedManufacturers = manufs.Any(m => m.GroupId == g.Id)
                    };
                    group.Checked = manufs.Any(m => m.GroupId == g.Id && m.Checked) ||
                                     step.ManufacturersGroups.Any(gr => gr.ManufacturersGroupId == g.Id);
                    group.Percent = group.SelectedManufacturers
                        ? manufs.Where(m => m.GroupId == g.Id).Sum(m => m.Percent)
                        : (step.ManufacturersGroups.Any(gr => gr.ManufacturersGroupId == g.Id)
                            ? step.ManufacturersGroups.First(gr => gr.ManufacturersGroupId == g.Id).Percent
                            : 0);

                    mGroups.Add(group);
                }

                data.ManufacturersGroups = mGroups;
            }
            else
            {
                var step2 = _stepRepository.GetStep2(currentVisit);

                if (step2 == null) return View((NewVisitStep3)null);
                var laxPercent = step2.Assortment.PackageId == 1
                    ? 100
                    : (step2.Assortment.PackageId == 3 ? step2.Assortment.LaxCementPercent : 0);

                var packedPercent = 100 - laxPercent;

                var lax = new List<SelectedManufacturersGroups>();
                if (step2.LaxTypes != null)
                {
                    step2.LaxTypes.ForEach(
                        s => s.LaxType = _context.LaxCementTypes.FirstOrDefault(t => t.Id == s.LaxTypeId));
                    lax.AddRange(
                        step2.LaxTypes.Where(s => s.LaxType != null)
                            .GroupBy(s => _context.Producers.First(p => p.Id == s.LaxType.ProducerId).Group)
                            .Select(@group => new SelectedManufacturersGroups
                            {
                                Id = group.Key,
                                Percent = (int)Math.Round(group.Sum(s => s.Percent * (laxPercent / 100.0)))
                            }));
                }

                if (step2.PackedTypes != null)
                {
                    step2.PackedTypes.ForEach(
                        s => s.PackedType = _context.PackedCementTypes.FirstOrDefault(t => t.Id == s.PackedTypeId));
                    foreach (var item in step2.PackedTypes.Where(s => s.PackedType != null)
                        .GroupBy(s => _context.Producers.First(p => p.Id == s.PackedType.ProducerId).Group).Select(group => new SelectedManufacturersGroups()
                        {
                            Id = group.Key,
                            Percent = (int)Math.Round(group.Sum(s => s.Percent * (packedPercent / 100.0)))
                        }))
                    {
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

                ViewBag.Step2 = lax;
            }
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step3(NewVisitStep3 data, bool? edit)
        {
            if (data == null) return RedirectToAction("Index");
            var sum = 0;

            var suppliers = Step3Helper.GetSuppliers(data);
            var manufacturers = Step3Helper.GetManufacturers(data, ref sum);
            var manufacturersGroups = Step3Helper.GetManufacturersGroups(data, ref sum);

            if (data.Cooperation == false)
            {
                data.Suppliers = null;
            }

            if (sum == 100 || sum == 0)
            {
                ClientStep3 step;
                if (!edit.HasValue || edit.Value == false)
                {
                    step = new ClientStep3();
                    step = Step3Helper.AddDataToStep(step, data.Cooperation, manufacturers, manufacturersGroups,
                        suppliers, data.VisitId);
                    _context.ClientSteps3.Add(step);
                }
                else
                {
                    step = Step3Helper.GetAndClearStep(data.VisitId, _context);
                    step = Step3Helper.AddDataToStep(step, data.Cooperation, manufacturers, manufacturersGroups,
                        suppliers, data.VisitId);
                    _context.Entry(step).State = EntityState.Modified;
                }

                _context.SaveChanges();
                return RedirectToAction("AddVisit_Step3_5", new { currentVisit = data.VisitId, edit });
            }

            ModelState.AddModelError("", "Całkowita liczba procent musi wynosić 100%");
            ViewBag.VisitId = data.VisitId;
            ViewBag.Suppliers = _context.Suppliers.ToList();
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.Manufacturers = _context.Manufacturers.ToList();
            ViewBag.ManufacturersGroups = _context.ManufacturersGroups.ToList();
            data.Suppliers = data.Suppliers ?? new List<SelectedSuppliers>();

            return View(data);
        }

        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step3_5(int? currentVisit, bool? edit)
        {
            if (!currentVisit.HasValue)
            {
                return RedirectToAction("Index");
            }

            ViewBag.VisitId = currentVisit;
            ViewBag.Suppliers = _context.Suppliers.ToList();
            ViewBag.Countries = _context.Countries.ToList();
            ViewBag.Manufacturers = _context.Manufacturers.ToList();
            ViewBag.ManufacturersGroups = _context.ManufacturersGroups.ToList();
            ViewBag.Distributors = _context.Distributors.OrderBy(d => d.City).ThenBy(d => d.Name).ToList();
            var data = new NewVisitStep3_5
            {
                Receipts = _context.Receipts.ToList(),
                ReceiptId = 1
            };
            if (!edit.HasValue || edit.Value != true) return View(data);
            var step = _stepRepository.GetStep35(currentVisit);
            if (step == null)
            {
                return RedirectToAction("AddVisit_Step3_5", new { currentVisit });
            }
            data.ReceiptId = step.ReceiptId;
            if (step.Manufacturers != null)
            {
                data.R1 = new Rec1
                {
                    Manufacturers = step.Manufacturers.Select(m => new SelectedManufacturers
                    {
                        Checked = true,
                        GroupId = m.GroupId,
                        Id = m.ManufacturerId,
                        Percent = m.Percent
                    }).ToList()
                };
            }

            if (step.ManufacturersGroups != null)
            {
                var r1Groups = step.ManufacturersGroups.Select(g => new SelectedManufacturersGroups()
                {
                    Checked = true,
                    Id = g.ManufacturersGroupId,

                    SelectedManufacturers =
                        data.R1.Manufacturers.Any(m => m.Checked && m.GroupId == g.ManufacturersGroupId),
                    Percent = data.R1.Manufacturers.All(m => m.GroupId != g.ManufacturersGroupId)
                        ? g.Percent
                        : data.R1.Manufacturers.Where(m => m.GroupId == g.ManufacturersGroupId).Sum(m => m.Percent)
                }).ToList();
                data.R1.ManufacturersGroups = r1Groups;
            }

            if (step.Distributors == null) return View(data);
            data.R2 = new Rec2 { Manufacturers = new List<SelectedManufacturers>() };
            var r2Dists = new List<SelectedDistributor>();
            foreach (var d in step.Distributors)
            {
                var dist = new SelectedDistributor
                {
                    Id = d.DistributorId,
                    Percent = d.Percent,
                    IsMarket = _context.Distributors.Single(ds => ds.Id == d.DistributorId).IsMarket,
                    MarketAddressId = d.MarketAddressId,
                    Addresses =
                        _context.Distributors.Include(ds => ds.MarketAddresses)
                            .Single(ds => ds.Id == d.DistributorId)
                            .MarketAddresses,
                    Name = _context.Distributors.Single(ds => ds.Id == d.DistributorId).Name,
                    Checked = true,
                    Manufacturers = d.Manufacturers != null ? d.Manufacturers.Select(dm => new SelectedManufacturers
                    {
                        Percent = dm.Percent,
                        Id = dm.ManufacturerId,
                        Checked = true
                    }).ToList() : null
                };

                if (d.ManufacturersGroups != null)
                {
                    var grs = new List<SelectedManufacturersGroups>();
                    foreach (var g in d.ManufacturersGroups)
                    {
                        var gr = new SelectedManufacturersGroups
                        {
                            SelectedManufacturers = g.SelectedManufacturers,
                            Percent = g.Percent,
                            Id = g.ManufacturersGroupId,
                            Checked = true
                        };
                        if (g.Mans != null && g.Mans.Any())
                        {
                            gr.Percent = 0;
                            var mans = new List<SelectedManufacturers>();
                            foreach (var m in g.Mans)
                            {
                                gr.Percent += m.Percent;
                                mans.Add(new SelectedManufacturers
                                {
                                    Checked = true,
                                    GroupId = g.ManufacturersGroupId,
                                    Id = m.ManufacturerId,
                                    Percent = m.Percent
                                });
                            }
                            gr.Manufacturers = mans;
                        }
                        grs.Add(gr);
                    }

                    dist.ManufsGroups = grs;
                }
                r2Dists.Add(dist);
            }
            data.R2.Distributors = r2Dists;
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step3_5(NewVisitStep3_5 data, bool? edit)
        {
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            if (!ModelState.IsValid)
            {
                return View(data);
            }

            var err2 = false;

            var sum = 0;
            var manufacturers = new List<VisitManufacturers>();
            var manuGroups = new List<VisitManufacturersGroup>();

            if (data.ReceiptId == 1 || data.ReceiptId == 3)
            {
                if (data.R1 != null && data.R1.Manufacturers != null)
                {
                    foreach (var m in data.R1.Manufacturers.Where(x => x.Checked))
                    {
                        var group = data.R1.ManufacturersGroups.Any(g => g.Id == m.GroupId);
                        if (group) continue;
                        sum += m.Percent;
                        manufacturers.Add(new VisitManufacturers
                        {
                            ManufacturerId = m.Id,
                            Percent = m.Percent
                        });
                    }
                }

                if (data.R1 != null && data.R1.ManufacturersGroups != null)
                {
                    foreach (var g in data.R1.ManufacturersGroups.Where(g => g.Checked))
                    {
                        sum += g.Percent;
                        manuGroups.Add(new VisitManufacturersGroup
                        {
                            ManufacturersGroupId = g.Id,
                            Percent = g.Percent,
                            SelectedManufacturers = data.R1.Manufacturers.Any(m => m.GroupId == g.Id && m.Checked)
                        });
                    }
                }
            }

            var distributors = new List<VisitDistributor>();
            if (data.ReceiptId == 2 || data.ReceiptId == 3)
            {
                var distSum = 0;
                if (data.R2 != null && data.R2.Distributors != null)
                {
                    foreach (var d in data.R2.Distributors)
                    {
                        distSum = 0;
                        if (d.Id == 0)
                        {
                            var dist = new Distributor
                            {
                                Name = d.Name,
                                Street = d.Street,
                                BuildingNumber = d.BuildingNumber,
                                City = d.City,
                                NIP = d.NIP,
                                PostCode = d.PostCode
                            };
                            _context.Distributors.Add(dist);
                            _context.SaveChanges();
                            d.Id = dist.Id;
                        }
                        if (!d.Checked) continue;
                        var distr = new VisitDistributor
                        {
                            Manufacturers = new List<VisitDistManufacturer>(),
                            ManufacturersGroups = new List<VisitDistManufacturersGroup>(),
                            Percent = d.Percent,
                            DistributorId = d.Id,
                            MarketAddressId = d.MarketAddressId
                        };
                        sum += d.Percent;
                        if (d.Manufacturers != null)
                        {
                            foreach (var m in d.Manufacturers.Where(m => m.Checked))
                            {
                                distSum += m.Percent;
                                distr.Manufacturers.Add(new VisitDistManufacturer
                                {
                                    ManufacturerId = m.Id,
                                    Percent = m.Percent
                                });
                            }
                        }
                        if (d.ManufsGroups != null)
                        {
                            foreach (var g in d.ManufsGroups)
                            {
                                foreach (var mm in g.Manufacturers.Where(mm => mm.Checked))
                                {
                                    g.Checked = true;
                                    break;
                                }
                                if (!g.Checked) continue;
                                var gm = new VisitDistManufacturersGroup
                                {
                                    ManufacturersGroupId = g.Id,
                                    Mans = new List<VisitDistManufacturer>()
                                };
                                foreach (var m in g.Manufacturers.Where(m => m.Checked && m.GroupId == g.Id))
                                {
                                    distSum += m.Percent;
                                    gm.Mans.Add(new VisitDistManufacturer
                                    {
                                        ManufacturerId = m.Id,
                                        Percent = m.Percent
                                    });
                                }
                                if (!g.Manufacturers.Any(m => m.Checked && m.GroupId == g.Id))
                                {
                                    distSum += g.Percent;
                                    gm.Percent = g.Percent;
                                    gm.SelectedManufacturers = false;
                                }
                                else
                                {
                                    gm.SelectedManufacturers = true;
                                    gm.Percent = g.Manufacturers.Where(m => m.Checked).Sum(m => m.Percent);
                                }
                                distr.ManufacturersGroups.Add(gm);
                            }
                        }

                        distributors.Add(distr);
                    }
                    if (err2 != true)
                    {
                        err2 = distSum != 100 && distSum != 0;
                    }
                }
            }
            var err = sum != 100 && sum != 0;

            if (err || err2)
            {
                ModelState.AddModelError("", "Całkowita liczba procent musi wynosić 100%");
                ViewBag.VisitId = data.VisitId;
                ViewBag.Suppliers = _context.Suppliers.ToList();
                ViewBag.Countries = _context.Countries.ToList();
                ViewBag.Manufacturers = _context.Manufacturers.ToList();
                ViewBag.ManufacturersGroups = _context.ManufacturersGroups.ToList();
                ViewBag.Distributors = _context.Distributors.OrderBy(d => d.City).ThenBy(d => d.Name).ToList();
                data.Receipts = _context.Receipts.ToList();

                return View(data);
            }

            var step = new ClientStep3_5();
            if (edit.HasValue && edit.Value)
            {
                step = _stepRepository.GetStep35(data.VisitId);
                if (step.Manufacturers != null)
                    step.Manufacturers.Clear();
                if (step.ManufacturersGroups != null)
                    step.ManufacturersGroups.Clear();
                if (step.Distributors != null)
                    step.Distributors.Clear();

                _context.Entry(step).State = EntityState.Modified;
                _context.SaveChanges();
            }

            step.Manufacturers = manufacturers;
            step.ManufacturersGroups = manuGroups;
            step.Distributors = distributors;
            step.ReceiptId = data.ReceiptId;
            step.VisitId = data.VisitId;
            if (edit.HasValue && edit.Value)
            {
                _context.Entry(step).State = EntityState.Modified;
            }
            else
            {
                _context.ClientSteps3_5.Add(step);
            }
            _context.SaveChanges();
            ViewBag.VisitId = data.VisitId;
            return RedirectToAction("AddVisit_Step4", new { currentVisit = data.VisitId, edit });
        }

        [HttpPost]
        public JsonResult AddDistributor(Distributor data)
        {
            var response = new NewDistributorJson();
            if (!ModelState.IsValid)
            {
                response.Error = true;
                response.ErrorsList.Add("Wszystkie pola muszą być uzupełnione");
                return Json(response);
            }
            var nip = new Regex("^((\\d{3}-\\d{2}-\\d{2}-\\d{3})|(\\d{3}-\\d{3}-\\d{2}-\\d{2}))$");
            var postCode = new Regex("^\\d{2}-\\d{3}$");
            if (!nip.IsMatch(data.NIP))
            {
                response.Error = true;
                response.ErrorsList.Add("Nieprawidłowy NIP");
            }
            if (!postCode.IsMatch(data.PostCode))
            {
                response.Error = true;
                response.ErrorsList.Add("Nieprawidłowy kod pocztowy");
            }
            if (_context.Distributors.Any(d => d.NIP == data.NIP))
            {
                response.Error = true;
                response.ErrorsList.Add("Dystrybutor z podanym numerem NIP już istnieje");
            }
            if (response.Error)
            {
                return Json(response);
            }
            _context.Distributors.Add(data);
            _context.SaveChanges();
            response.Error = false;
            response.Id = data.Id;
            response.Name = data.Name;
            return Json(response);
        }

        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step4(int? currentVisit, bool? edit)
        {
            if (currentVisit == null)
            {
                return RedirectToAction("Index");
            }
            NewVisitStep4 model = null;
            ViewBag.VisitId = currentVisit;
            var unloadTypes = _context.UnloadTypes.Select(t => new SelectedUnloadType
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();
            ViewBag.UnloadTypes = unloadTypes;
            if (edit.HasValue && edit.Value)
            {
                var step = _stepRepository.GetStep4(currentVisit);
                if (step == null)
                {
                    return RedirectToAction("AddVisit_Step4", new { currentVisit });
                }
                model = new NewVisitStep4
                {
                    Logistic = step.Logistic,
                    UnloadTypes =
                        ((IEnumerable<SelectedUnloadType>) ViewBag.UnloadTypes).Select(t => new SelectedUnloadType
                        {
                            Checked = step.UnloadTypes.Any(vt => t.Id == vt.UnloadTypeId),
                            Id = t.Id,
                            Name = t.Name
                        })
                };
            }
            ViewBag.Step2Type = _stepRepository.GetStep2(currentVisit).Assortment.PackageId;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step4(NewVisitStep4 data, bool? edit)
        {
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            switch (_stepRepository.GetStep2(data.VisitId).Assortment.PackageId)
            {
                case 2:
                    data.Logistic.NumberOfSilos = 0;
                    data.Logistic.CapacitySilos = 0;
                    data.Logistic.RailwaySiding = 3;
                    data.Logistic.ReceiveCementByRailwaySiding = 3;
                    data.Logistic.OwnCementTank = 3;
                    data.Logistic.CountryLicense = 4;
                    data.Logistic.AbroadLicense = 4;
                    data.Logistic.LaxDelivery = 4;
                    break;
                case 1:
                    data.Logistic.CoveredMagazineCapacity = 0;
                    data.Logistic.NotCoveredMagazineCapacity = 0;
                    data.Logistic.OwnHDSTypeCar = 3;
                    data.Logistic.OwnPlatformTypeCar = 3;
                    data.Logistic.PackedDelivery = 4;
                    data.UnloadTypes = new List<SelectedUnloadType>();
                    break;
            }

            switch (data.Logistic.RailwaySiding)
            {
                case 1:
                    data.Logistic.ReceiveCementByRailwaySiding = 1;
                    break;
                case 3:
                    data.Logistic.ReceiveCementByRailwaySiding = 3;
                    break;
            }

            switch (data.Logistic.OwnCementTank)
            {
                case 1:
                    data.Logistic.AbroadLicense = 1;
                    data.Logistic.CountryLicense = 1;
                    break;
                case 3:
                    data.Logistic.AbroadLicense = 3;
                    data.Logistic.CountryLicense = 3;
                    break;
            }

            var step = new ClientStep4(); ;
            if (edit.HasValue && edit.Value)
            {
                step = _stepRepository.GetStep4(data.VisitId);
                step.UnloadTypes.Clear();
                step.Logistic = null;
                _context.Entry(step).State = EntityState.Modified;
                _context.SaveChanges();
            }

            step.Logistic = data.Logistic;
            step.VisitId = data.VisitId;
            if (!edit.HasValue || edit.Value == false)
            {
                _context.ClientSteps4.Add(step);
            }
            else
            {
                _context.Entry(step).State = EntityState.Modified;
            }

            step.UnloadTypes = data.UnloadTypes.Where(t => t.Checked).Select(t => new VisitUnloadType() { UnloadTypeId = t.Id }).ToList();

            _context.SaveChanges();

            ViewBag.VisitId = data.VisitId;
            return RedirectToAction("AddVisit_Step5", new { currentVisit = data.VisitId, edit });
        }

        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step5(int? currentVisit, bool? edit)
        {
            if (currentVisit == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.VisitId = currentVisit;
            ViewBag.Clients = _context.Clients.ToList();
            if (!edit.HasValue || edit.Value != true) return View((NewVisitStep5)null);

            var step = _stepRepository.GetStep5(currentVisit);
            if (step == null)
            {
                return RedirectToAction("AddVisit_Step5", new { currentVisit });
            }
            var data = new NewVisitStep5
            {
                Annual = step.Annual,
                Clients = step.Clients.Select(c => new SelectedClients
                {
                    Checked = true,
                    Id = c.ClientId,
                    Percent = c.Percent
                }).ToList()
            };

            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step5(NewVisitStep5 data, bool? edit)
        {
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            var step = new ClientStep5();
            if (edit.HasValue && edit.Value)
            {
                step = _stepRepository.GetStep5(data.VisitId);
                step.Clients.Clear();
                _context.Entry(step).State = EntityState.Modified;
                _context.SaveChanges();
            }
            step.Annual = data.Annual;
            step.VisitId = data.VisitId;
            var sum = data.Clients.Where(x => x.Checked).Sum(x => x.Percent);

            if (sum != 100)
            {
                ViewBag.VisitId = data.VisitId;
                ViewBag.Clients = _context.Clients.ToList();
                ModelState.AddModelError("", "Całkowita liczba procent musi wynosić 100%");
                return View(data);
            }

            step.Clients = data.Clients.Where(x => x.Checked).Select(c => new VisitClients
            {
                ClientId = c.Id, Percent = c.Percent
            }).ToList();
            if (edit.HasValue && edit.Value)
            {
                _context.Entry(step).State = EntityState.Modified;
            }
            else
            {
                _context.ClientSteps5.Add(step);
            }
            _context.SaveChanges();

            ViewBag.VisitId = data.VisitId;
            return RedirectToAction("AddVisit_Step6", new { currentVisit = data.VisitId, edit });
        }

        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step6(int? currentVisit, bool? edit)
        {
            if (currentVisit == null)
            {
                return RedirectToAction("Index");
            }
            ClientStep6 model = null;
            if (edit.HasValue && edit.Value)
            {
                model = _stepRepository.GetStep6(currentVisit);
                if (model == null)
                {
                    return RedirectToAction("AddVisit_Step6", new { currentVisit });
                }
            }

            ViewBag.VisitId = currentVisit;
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step6(ClientStep6 data, bool? edit)
        {
            if (data == null)
            {
                return RedirectToAction("Index");
            }
            if (!edit.HasValue || edit.Value == false)
            {
                _context.ClientSteps6.Add(data);
            }
            else
            {
                var step = _stepRepository.GetStep6(data.VisitId);
                step.ForeignLaboratories.Clear();
                _context.Entry(step).State = EntityState.Modified;
                _context.SaveChanges();
                step.ForeignLaboratories = data.ForeignLaboratories;
                step.ForeignLaboratory = data.ForeignLaboratory;
                step.OwnLaboratory = data.OwnLaboratory;
                _context.Entry(step).State = EntityState.Modified;
            }
            _context.SaveChanges();

            ViewBag.VisitId = data.VisitId;
            return RedirectToAction("AddVisit_Step7", new { currentVisit = data.VisitId, edit });
        }

        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step7(int? currentVisit, bool? edit)
        {
            if (currentVisit == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.LaxProducers = _cementRepository.GetLaxProducers();
            ViewBag.PackedProducers = _cementRepository.GetPackedProducers();
            ViewBag.LaxTypes = _context.LaxCementTypes.ToList();
            ViewBag.PackedTypes = _context.PackedCementTypes.ToList();

            ViewBag.VisitId = currentVisit;
            ClientStep7 data = null;
            if (edit.HasValue && edit.Value)
            {
                var step = _stepRepository.GetStep7(currentVisit);
                    
                if (step == null)
                {
                    return RedirectToAction("AddVisit_Step7", new { currentVisit });
                }
                data = step;
            }

            var step2 = _stepRepository.GetStep2(currentVisit);
            ViewBag.Step2Type = step2.Assortment.PackageId;
            ViewBag.Step2LaxCements = step2.LaxTypes;
            ViewBag.Step2PackedCements = step2.PackedTypes;

            if (data == null)
            {
                data = new ClientStep7();
            }
            if (data.LaxTypes == null)
            {
                data.LaxTypes = new List<ClientLax>();
            }
            if (data.PackedTypes == null)
            {
                data.PackedTypes = new List<ClientPacked>();
            }
            return View(data);
        }

        [HttpPost]
        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step7(ClientStep7 data, bool? edit)
        {
            if (data == null)
            {
                return RedirectToAction("Index");
            }

            switch (_stepRepository.GetStep2(data.VisitId).Assortment.PackageId)
            {
                case 1:
                    data.PackedTypes = null;
                    break;
                case 2:
                    data.LaxTypes = null;
                    break;
            }

            if (edit.HasValue && edit.Value)
            {
                var step = _stepRepository.GetStep7(data.VisitId);
                if (step.LaxTypes != null)
                {
                    step.LaxTypes.Clear();
                }
                if (step.PackedTypes != null)
                {
                    step.PackedTypes.Clear();
                }
                _context.Entry(step).State = EntityState.Modified;
                _context.SaveChanges();
                step.PackedTypes = data.PackedTypes;
                step.LaxTypes = data.LaxTypes;
            }
            else
            {
                _context.ClientSteps7.Add(data);
            }

            _context.SaveChanges();

            ViewBag.VisitId = data.VisitId;
            return RedirectToAction("AddVisit_Step8", new { currentVisit = data.VisitId, edit });
        }

        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step8(int? currentVisit)
        {
            if (!currentVisit.HasValue) return RedirectToAction("Index");

            var data = _stepRepository.GetStep8(currentVisit);
            var canBeGreen = ClientData.CanBeGreen(currentVisit.Value, _context);
            var model = ClientViewModelBuilder.CreateStatusNotesViewModel(data, currentVisit, canBeGreen);

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "agent")]
        public ActionResult AddVisit_Step8(StatusNotesViewModel model)
        {
            if (model == null) return RedirectToAction("Index");

            var data = Mapper.Map<ClientStep8>(model);
            if (model.Edit)
            {
                _stepRepository.EditStep8(data);
            }
            else
            {
                _stepRepository.AddStep8(data);
            }

            return RedirectToAction("Index");
        }

        public ActionResult Complete(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");
            var business = _context.Businesses.FirstOrDefault(b => b.Id == id);
            if (business == null) return RedirectToAction("Index");
            var visits = _context.Visits.Where(v => v.BusinessId == id);
            var lastAddedVisit =
                visits.Where(v => v.VisitDate == visits.Max(a => a.VisitDate)).AsEnumerable().LastOrDefault();
            if (lastAddedVisit == null) return RedirectToAction("AddVisit_Step1", new { businessId = id });
            var step1 = _stepRepository.GetStep1(lastAddedVisit.Id);
            var step2 = _stepRepository.GetStep2(lastAddedVisit.Id);
            var step3 = _stepRepository.GetStep3(lastAddedVisit.Id);
            var step35 = _stepRepository.GetStep35(lastAddedVisit.Id);
            var step4 = _stepRepository.GetStep4(lastAddedVisit.Id);
            var step5 = _stepRepository.GetStep5(lastAddedVisit.Id);
            var step6 = _stepRepository.GetStep6(lastAddedVisit.Id);
            var step7 = _stepRepository.GetStep7(lastAddedVisit.Id);
            var step8 = _stepRepository.GetStep8(lastAddedVisit.Id);

            if (step1 == null)
            {
                return RedirectToAction("AddVisit_Step1", new { businessId = id });
            }
            if (step2 == null)
            {
                return RedirectToAction("AddVisit_Step2", new { currentVisit = lastAddedVisit.Id });
            }
            if (step3 == null)
            {
                return RedirectToAction("AddVisit_Step3", new { currentVisit = lastAddedVisit.Id });
            }
            if (step35 == null)
            {
                return RedirectToAction("AddVisit_Step3_5", new { currentVisit = lastAddedVisit.Id });
            }
            if (step4 == null)
            {
                return RedirectToAction("AddVisit_Step4", new { currentVisit = lastAddedVisit.Id });
            }
            if (step5 == null)
            {
                return RedirectToAction("AddVisit_Step5", new { currentVisit = lastAddedVisit.Id });
            }
            if (step6 == null)
            {
                return RedirectToAction("AddVisit_Step6", new { currentVisit = lastAddedVisit.Id });
            }
            if (step7 == null)
            {
                return RedirectToAction("AddVisit_Step7", new { currentVisit = lastAddedVisit.Id });
            }
            return step8 == null
                ? RedirectToAction("AddVisit_Step8", new { currentVisit = lastAddedVisit.Id })
                : RedirectToAction("Index");
        }

        public ActionResult Search(string query)
        {
            var businessItems = _businessRepository.Search(query, User).Select(b => new BusinessItem
            {
                Id = b.BusinessId,
                Name = b.RecipientName, 
                Status = (BusinessStatus) _stepRepository.GetStatus(b.BusinessId)
            }).ToList();

            return View(businessItems);
        }

        public ActionResult Tasks()
        {
            var branches =
                _context.Branches.Where(
                    b => b.Used == false && b.UserId == ((CustomPrincipal)User).CustomIdentity.UserId).ToList();

            var model = branches.Select(branch => new BusinessItem
            {
                Id = branch.Id,
                Name = branch.Name
            }).ToList();

            return View(model);
        }

        public ActionResult UseBranch(int id)
        {
            var branch = _context.Branches.Single(b => b.Id == id);

            TempData["BranchData"] = new BranchDataViewModel
            {
                BuildingNumber = branch.BuildingNumber,
                City = branch.City,
                DistrictId = branch.DistrictId,
                ProvinceId = branch.ProvinceId,
                Email = branch.Email,
                PhoneNumber = branch.PhoneNumber,
                PostCode = branch.PostCode,
                Street = branch.Street,
                Name = branch.Name,
                BranchId = branch.Id
            };

            return RedirectToAction("Add", new { newBranch = true });
        }



        public JsonResult GetMarkets(int id)
        {
            var distributor = _context.Distributors.Include(d => d.MarketAddresses).Single(d => d.Id == id);

            return Json(distributor.MarketAddresses, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AllUndone()
        {
            var datas = _businessRepository.GetWithStatus(BusinessStatus.Undone, User);

            return View(datas);
        }

        public ActionResult AllBrown()
        {
            var datas = _businessRepository.GetWithStatus(BusinessStatus.Brown, User);

            return View(datas);
        }

        public ActionResult AllGreen()
        {
            var datas = _businessRepository.GetWithStatus(BusinessStatus.Green, User);

            return View(datas);
        }

        public ActionResult AllYellow()
        {
            var datas = _businessRepository.GetWithStatus(BusinessStatus.Yellow, User);

            return View(datas);
        }
    }
}
