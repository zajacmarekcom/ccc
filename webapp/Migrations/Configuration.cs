using Microsoft.Ajax.Utilities;

namespace webapp.Migrations
{
    using webapp.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<webapp.Models.WebappContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(webapp.Models.WebappContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            List<MarketSegment> segments = new List<MarketSegment>
            {
                new MarketSegment { Id = 1, Name = "Beton towarowy"},
                new MarketSegment { Id = 2, Name = "Prefabrykaty"},
                new MarketSegment { Id = 3, Name = "Dachówka cementowa"},
                new MarketSegment { Id = 4, Name = "Kostka brukowa"},
                new MarketSegment { Id = 5, Name = "Suche mieszanki"},
                new MarketSegment { Id = 6, Name = "Stabilizacja"},
                new MarketSegment { Id = 7, Name = "Firma handlowa"}
            };

            foreach (MarketSegment segment in segments)
            {
                context.MarketSegments.AddOrUpdate(segment);
            }

            List<Receipt> receipts = new List<Receipt>
            {
                new Receipt { Id = 1, Name = "Odbiory bezpośrednie"},
                new Receipt { Id = 2, Name = "Odbiory pośrednie"},
                new Receipt { Id = 3, Name = "Odbiory bezpośrednie i pośrednie"}
            };

            foreach (Receipt c in receipts)
            {
                context.Receipts.AddOrUpdate(c);
            }

            List<CooperationType> cooperationTypes = new List<CooperationType>
            {
                new CooperationType { Id = 1, Name = "Bezpośrednia"},
                new CooperationType { Id = 2, Name = "Pośrednia 2"},
                new CooperationType { Id = 3, Name = "Pośrednia 3"},
                new CooperationType { Id = 4, Name = "Brak"}
            };

            foreach (CooperationType c in cooperationTypes)
            {
                context.CooperationTypes.AddOrUpdate(c);
            }

            List<LegalForm> legalForms = new List<LegalForm>
            {
                new LegalForm { Id = 1, Name = "Spółka jawna"},
                new LegalForm { Id = 2, Name = "Spółka partnerska"},
                new LegalForm { Id = 3, Name = "Spółka komandytowa"},
                new LegalForm { Id = 4, Name = "Spółka komandytowo-akcyjna"},
                new LegalForm { Id = 5, Name = "Spółka z o.o."},
                new LegalForm { Id = 6, Name = "Spółka akcyjna"},
                new LegalForm { Id = 7, Name = "Spółka cywilna"},
                new LegalForm { Id = 8, Name = "Przedsiębiorstwo prywatne osoby fizycznej"},
                new LegalForm { Id = 9, Name = "Przedsiębiorstwo państwowe"},
                new LegalForm { Id = 10, Name = "Stowarzyszenie"},
                new LegalForm { Id = 11, Name = "Spółdzielnia"},
                new LegalForm { Id = 12, Name = "Fundacja"},
            };

            foreach (LegalForm l in legalForms)
            {
                context.LegalForms.AddOrUpdate(l);
            }

            List<Province> provinces = new List<Province>()
            {
                new Province { Id = 1, Name = "Dolnośląskie"},
                new Province { Id = 2, Name = "Małopolskie"},
                new Province { Id = 3, Name = "Lubuskie"},
                new Province { Id = 4, Name = "Łódzkie"},
                new Province { Id = 5, Name = "Opolskie"},
                new Province { Id = 6, Name = "Śląskie"},
                new Province { Id = 7, Name = "Wielkopolskie"}
            };

            foreach (Province p in provinces)
            {
                context.Provinces.AddOrUpdate(p);
            }

            List<Agent> agents = new List<Agent>()
            {
                new Agent {Id = 1, FullName = "Aaaa Aaaa" },
                new Agent {Id = 2, FullName = "Bbbb Bbbb" },
                new Agent {Id = 3, FullName = "Cccc Cccc" },
                new Agent {Id = 4, FullName = "Dddd Dddd" },
                new Agent {Id = 5, FullName = "Eeee Eeee" },
                new Agent {Id = 6, FullName = "Ffff Ffff" },
                new Agent {Id = 7, FullName = "Gggg Gggg" },
                new Agent {Id = 8, FullName = "Hhhh Hhhh" },
                new Agent {Id = 9, FullName = "Iiii Iiii" },
                new Agent {Id = 10, FullName = "Jjjj Jjjj" }
            };

            foreach (Agent a in agents)
            {
                context.Agents.AddOrUpdate(a);
            }

            List<District> districts = new List<District>
            {
                new District { Id = 1, ProvinceId = 1, Name = "Bolesławiecki"},
                new District { Id = 2, ProvinceId = 1, Name = "Dzierżoniowski"},
                new District { Id = 3, ProvinceId = 1, Name = "Głogowski"},
                new District { Id = 4, ProvinceId = 1, Name = "Górowski"},
                new District { Id = 5, ProvinceId = 1, Name = "Jaworski"},
                new District { Id = 6, ProvinceId = 1, Name = "Jelenia Góra"},
                new District { Id = 7, ProvinceId = 1, Name = "Jeleniogórski"},
                new District { Id = 8, ProvinceId = 1, Name = "Kamiennogórski"},
                new District { Id = 9, ProvinceId = 1, Name = "Kłodzki"},
                new District { Id = 10, ProvinceId = 1, Name = "Legnica"},
                new District { Id = 11, ProvinceId = 1, Name = "Legnicki"},
                new District { Id = 12, ProvinceId = 1, Name = "Lubański"},
                new District { Id = 13, ProvinceId = 1, Name = "Lubiński"},
                new District { Id = 14, ProvinceId = 1, Name = "Lwówecki"},
                new District { Id = 15, ProvinceId = 1, Name = "Milicki"},
                new District { Id = 16, ProvinceId = 1, Name = "Oleśnicki"},
                new District { Id = 17, ProvinceId = 1, Name = "Oławski"},
                new District { Id = 18, ProvinceId = 1, Name = "Polkowicki"},
                new District { Id = 19, ProvinceId = 1, Name = "Strzeliński"},
                new District { Id = 20, ProvinceId = 1, Name = "Trzebnicki"},
                new District { Id = 21, ProvinceId = 1, Name = "Wałbrzych"},
                new District { Id = 22, ProvinceId = 1, Name = "Wałbrzyski"},
                new District { Id = 23, ProvinceId = 1, Name = "Wołoński"},
                new District { Id = 24, ProvinceId = 1, Name = "Wrocław"},
                new District { Id = 25, ProvinceId = 1, Name = "Wrocławski"},
                new District { Id = 26, ProvinceId = 1, Name = "Zgorzelecki"},
                new District { Id = 27, ProvinceId = 1, Name = "Złotoryjski"},
                new District { Id = 28, ProvinceId = 1, Name = "Ząbkowicki"},
                new District { Id = 29, ProvinceId = 1, Name = "średzki (śląski)"},
                new District { Id = 30, ProvinceId = 1, Name = "świdnicki (Dolnośląski)"},
                new District { Id = 31, ProvinceId = 2, Name = "Bocheński"},
                new District { Id = 32, ProvinceId = 2, Name = "Brzeski (Małopolski)"},
                new District { Id = 33, ProvinceId = 2, Name = "Chrzanowski"},
                new District { Id = 34, ProvinceId = 2, Name = "Dąbrowski"},
                new District { Id = 35, ProvinceId = 2, Name = "Gorlicki"},
                new District { Id = 36, ProvinceId = 2, Name = "Krakowski"},
                new District { Id = 37, ProvinceId = 2, Name = "Kraków"},
                new District { Id = 38, ProvinceId = 2, Name = "Limanowski"},
                new District { Id = 39, ProvinceId = 2, Name = "Miechowski"},
                new District { Id = 40, ProvinceId = 2, Name = "Myślenicki"},
                new District { Id = 41, ProvinceId = 2, Name = "Nowosądecki"},
                new District { Id = 42, ProvinceId = 2, Name = "Nowotarski"},
                new District { Id = 43, ProvinceId = 2, Name = "Nowy Sącz"},
                new District { Id = 44, ProvinceId = 2, Name = "Olkuski"},
                new District { Id = 45, ProvinceId = 2, Name = "Oświęcimski"},
                new District { Id = 46, ProvinceId = 2, Name = "Proszowicki"},
                new District { Id = 47, ProvinceId = 2, Name = "Suski"},
                new District { Id = 48, ProvinceId = 2, Name = "Tarnowski"},
                new District { Id = 49, ProvinceId = 2, Name = "Tarnów"},
                new District { Id = 50, ProvinceId = 2, Name = "Tatrzański"},
                new District { Id = 51, ProvinceId = 2, Name = "Wadowicki"},
                new District { Id = 52, ProvinceId = 2, Name = "Wielicki"},
                new District { Id = 53, ProvinceId = 3, Name = "Gorzowski"},
                new District { Id = 54, ProvinceId = 3, Name = "Gorzów Wielkopolski"},
                new District { Id = 55, ProvinceId = 3, Name = "Krośnieński"},
                new District { Id = 56, ProvinceId = 3, Name = "Międzyrzecki"},
                new District { Id = 57, ProvinceId = 3, Name = "Nowosolski"},
                new District { Id = 58, ProvinceId = 3, Name = "Słubicki"},
                new District { Id = 59, ProvinceId = 3, Name = "Strzelecko-drezdenecki"},
                new District { Id = 60, ProvinceId = 3, Name = "Sulęciński"},
                new District { Id = 61, ProvinceId = 3, Name = "Wschowski"},
                new District { Id = 62, ProvinceId = 3, Name = "Zielona Góra"},
                new District { Id = 63, ProvinceId = 3, Name = "Zielonogórski"},
                new District { Id = 64, ProvinceId = 3, Name = "Żagański"},
                new District { Id = 65, ProvinceId = 3, Name = "Żarski"},
                new District { Id = 66, ProvinceId = 3, Name = "świebodziński"},
                new District { Id = 67, ProvinceId = 4, Name = "Bełchatowski"},
                new District { Id = 68, ProvinceId = 4, Name = "Brzeziński"},
                new District { Id = 69, ProvinceId = 4, Name = "Kutnowski"},
                new District { Id = 70, ProvinceId = 4, Name = "Łaski"},
                new District { Id = 71, ProvinceId = 4, Name = "Łęczycki"},
                new District { Id = 72, ProvinceId = 4, Name = "Łowicki"},
                new District { Id = 73, ProvinceId = 4, Name = "Łódzki-wschodni"},
                new District { Id = 74, ProvinceId = 4, Name = "Łódź"},
                new District { Id = 75, ProvinceId = 4, Name = "Opoczyński"},
                new District { Id = 76, ProvinceId = 4, Name = "Pabianicki"},
                new District { Id = 77, ProvinceId = 4, Name = "Pajęczański"},
                new District { Id = 78, ProvinceId = 4, Name = "Piotrkowski"},
                new District { Id = 79, ProvinceId = 4, Name = "Piotrków Trybunalski"},
                new District { Id = 80, ProvinceId = 4, Name = "Poddębicki"},
                new District { Id = 81, ProvinceId = 4, Name = "Radomszczański"},
                new District { Id = 82, ProvinceId = 4, Name = "Rawski"},
                new District { Id = 83, ProvinceId = 4, Name = "Sieradzki"},
                new District { Id = 84, ProvinceId = 4, Name = "Skierniewice"},
                new District { Id = 85, ProvinceId = 4, Name = "Skierniewicki"},
                new District { Id = 86, ProvinceId = 4, Name = "Tomaszowski (Mazowiecki)"},
                new District { Id = 87, ProvinceId = 4, Name = "Wieluński"},
                new District { Id = 88, ProvinceId = 4, Name = "Wieruszowski"},
                new District { Id = 89, ProvinceId = 4, Name = "Zduńskowolski"},
                new District { Id = 90, ProvinceId = 4, Name = "Zgierski"},
                new District { Id = 91, ProvinceId = 5, Name = "Brzeski (Opolski)"},
                new District { Id = 92, ProvinceId = 5, Name = "Głubczycki"},
                new District { Id = 93, ProvinceId = 5, Name = "Kędzierzyńsko-kozielski"},
                new District { Id = 94, ProvinceId = 5, Name = "Kluczborski"},
                new District { Id = 95, ProvinceId = 5, Name = "Krapkowicki"},
                new District { Id = 96, ProvinceId = 5, Name = "Namysłowski"},
                new District { Id = 97, ProvinceId = 5, Name = "Nyski"},
                new District { Id = 98, ProvinceId = 5, Name = "Oleski"},
                new District { Id = 99, ProvinceId = 5, Name = "Opole"},
                new District { Id = 100, ProvinceId = 5, Name = "Opolski"},
                new District { Id = 101, ProvinceId = 5, Name = "Prudnicki"},
                new District { Id = 102, ProvinceId = 5, Name = "Strzelecki"},
                new District { Id = 103, ProvinceId = 6, Name = "Będziński"},
                new District { Id = 104, ProvinceId = 6, Name = "Bielski"},
                new District { Id = 105, ProvinceId = 6, Name = "Bielsko-Biała"},
                new District { Id = 106, ProvinceId = 6, Name = "Bieruńsko-łędziński"},
                new District { Id = 107, ProvinceId = 6, Name = "Bytom"},
                new District { Id = 108, ProvinceId = 6, Name = "Chorzów"},
                new District { Id = 109, ProvinceId = 6, Name = "Cieszyński"},
                new District { Id = 110, ProvinceId = 6, Name = "Częstochowa"},
                new District { Id = 111, ProvinceId = 6, Name = "Częstochowski"},
                new District { Id = 112, ProvinceId = 6, Name = "Dąbrowa Górnicza"},
                new District { Id = 113, ProvinceId = 6, Name = "Gliwice"},
                new District { Id = 114, ProvinceId = 6, Name = "Gliwicki"},
                new District { Id = 115, ProvinceId = 6, Name = "Jastrzębie-Zdrój"},
                new District { Id = 116, ProvinceId = 6, Name = "Jaworzno"},
                new District { Id = 117, ProvinceId = 6, Name = "Katowice"},
                new District { Id = 118, ProvinceId = 6, Name = "Kłobucki"},
                new District { Id = 119, ProvinceId = 6, Name = "Lubliniecki"},
                new District { Id = 120, ProvinceId = 6, Name = "Mikołowski"},
                new District { Id = 121, ProvinceId = 6, Name = "Mysłowice"},
                new District { Id = 122, ProvinceId = 6, Name = "Myszkowski"},
                new District { Id = 123, ProvinceId = 6, Name = "Piekary śląskie"},
                new District { Id = 124, ProvinceId = 6, Name = "Pszczyński"},
                new District { Id = 125, ProvinceId = 6, Name = "Raciborski"},
                new District { Id = 126, ProvinceId = 6, Name = "Ruda śląska"},
                new District { Id = 127, ProvinceId = 6, Name = "Rybnicki"},
                new District { Id = 128, ProvinceId = 6, Name = "Rybnik"},
                new District { Id = 129, ProvinceId = 6, Name = "Siemianowice śląskie"},
                new District { Id = 130, ProvinceId = 6, Name = "Sosnowiec"},
                new District { Id = 131, ProvinceId = 6, Name = "Tarnogórski"},
                new District { Id = 132, ProvinceId = 6, Name = "Tychy"},
                new District { Id = 133, ProvinceId = 6, Name = "Wodzisławski"},
                new District { Id = 134, ProvinceId = 6, Name = "Zabrze"},
                new District { Id = 135, ProvinceId = 6, Name = "Zawierciański"},
                new District { Id = 136, ProvinceId = 6, Name = "Żory"},
                new District { Id = 137, ProvinceId = 6, Name = "Żywiecki"},
                new District { Id = 138, ProvinceId = 6, Name = "świętochłowice"},
                new District { Id = 139, ProvinceId = 7, Name = "Chodzieski"},
                new District { Id = 140, ProvinceId = 7, Name = "Czamkowsko-trzcianiecki"},
                new District { Id = 141, ProvinceId = 7, Name = "Gnieźnieński"},
                new District { Id = 142, ProvinceId = 7, Name = "Gostyński"},
                new District { Id = 143, ProvinceId = 7, Name = "Grodziski (Wielkopolski)"},
                new District { Id = 144, ProvinceId = 7, Name = "Jarociński"},
                new District { Id = 145, ProvinceId = 7, Name = "Kaliski"},
                new District { Id = 146, ProvinceId = 7, Name = "Kalisz"},
                new District { Id = 147, ProvinceId = 7, Name = "Kępiński"},
                new District { Id = 148, ProvinceId = 7, Name = "Kolski"},
                new District { Id = 149, ProvinceId = 7, Name = "Konin"},
                new District { Id = 150, ProvinceId = 7, Name = "Koniński"},
                new District { Id = 151, ProvinceId = 7, Name = "Kościański"},
                new District { Id = 152, ProvinceId = 7, Name = "Krotoszyński"},
                new District { Id = 153, ProvinceId = 7, Name = "Leszczyński"},
                new District { Id = 154, ProvinceId = 7, Name = "Leszno"},
                new District { Id = 155, ProvinceId = 7, Name = "Międzychodzki"},
                new District { Id = 156, ProvinceId = 7, Name = "Nowotomyski"},
                new District { Id = 157, ProvinceId = 7, Name = "Obornicki"},
                new District { Id = 158, ProvinceId = 7, Name = "Ostrowski (Wielkopolski)"},
                new District { Id = 159, ProvinceId = 7, Name = "Ostrzeniowski"},
                new District { Id = 160, ProvinceId = 7, Name = "Pilski"},
                new District { Id = 161, ProvinceId = 7, Name = "Pleszewski"},
                new District { Id = 162, ProvinceId = 7, Name = "Poznań"},
                new District { Id = 163, ProvinceId = 7, Name = "Poznański"},
                new District { Id = 164, ProvinceId = 7, Name = "Rawicki"},
                new District { Id = 165, ProvinceId = 7, Name = "Słupecki"},
                new District { Id = 166, ProvinceId = 7, Name = "Szamotulski"},
                new District { Id = 167, ProvinceId = 7, Name = "Turecki"},
                new District { Id = 168, ProvinceId = 7, Name = "Wolsztyński"},
                new District { Id = 169, ProvinceId = 7, Name = "Wrzesiński"},
                new District { Id = 170, ProvinceId = 7, Name = "Wągrowiecki"},
                new District { Id = 171, ProvinceId = 7, Name = "Złotowski"},
                new District { Id = 172, ProvinceId = 7, Name = "Średzki (Wielkopolski)"},
                new District { Id = 173, ProvinceId = 7, Name = "Śremski"},
            };

            foreach (District d in districts)
            {
                context.Districts.AddOrUpdate(d);
            }

            List<Roles> roles = new List<Roles>
            {
                new Roles { Id = 1, Name = "admin", Description = "Administrator"},
                new Roles { Id = 2, Name = "unactive", Description = "Nieaktywny"},
                new Roles { Id = 3, Name = "agent", Description = "Przedstawiciel"},
                new Roles { Id = 4, Name = "manager", Description = "Kierownik"}
            };

            foreach (Roles r in roles)
            {
                context.Roles.AddOrUpdate(r);
            }

            List<PartOfCement> parts = new List<PartOfCement>
            {
                new PartOfCement { Id = 1, Name = "Brak danych"},
                new PartOfCement { Id = 2, Name = "1-10"},
                new PartOfCement { Id = 3, Name = "11-20"},
                new PartOfCement { Id = 4, Name = "21-30"},
                new PartOfCement { Id = 5, Name = "31-40"},
                new PartOfCement { Id = 6, Name = "41-50"},
                new PartOfCement { Id = 7, Name = "51-60"},
                new PartOfCement { Id = 8, Name = "61-70"},
                new PartOfCement { Id = 9, Name = "71-80"},
                new PartOfCement { Id = 10, Name = "81-90"},
                new PartOfCement { Id = 11, Name = "91-100"}       
            };

            foreach (PartOfCement p in parts)
            {
                context.PartsOfCement.AddOrUpdate(p);
            }

            List<KindOfPackage> packages = new List<KindOfPackage>()
            {
                new KindOfPackage { Id = 1, Name = "luz"},
                new KindOfPackage { Id = 2, Name = "worek"},
                new KindOfPackage { Id = 3, Name = "luz i worek"},
            };

            foreach (KindOfPackage k in packages)
            {
                context.KindsOfPackage.AddOrUpdate(k);
            }

            List<BrandPower> powers = new List<BrandPower>()
            {
                new BrandPower { Id = 1, Value = "Nie ma znaczenia"},
                new BrandPower { Id = 2, Value = "Na równi z innymi"},
                new BrandPower { Id = 3, Value = "Ma przewagę"}
            };

            foreach (BrandPower b in powers)
            {
                context.BrandPowers.AddOrUpdate(b);
            }

            List<Producer> producers = new List<Producer>()
            {
                new Producer { Id = 1,Country = true, Name = "PROD 1", Group = 1},
                new Producer { Id = 2,Country = true, Name = "PROD 2", Group = 5},
                new Producer { Id = 3,Country = true, Name = "PROD 3", Group = 7},
                new Producer { Id = 4,Country = true, Name = "PROD 4", Group = 2},
                new Producer { Id = 5,Country = true, Name = "PROD 5", Group = 0},
                new Producer { Id = 6,Country = true, Name = "PROD 6", Group = 6},
                new Producer { Id = 7,Country = true, Name = "PROD 7", Group = 3},
                new Producer { Id = 8,Country = true, Name = "PROD 8", Group = 3},
                new Producer { Id = 9,Country = true, Name = "PROD 9", Group = 5},
                new Producer { Id = 10,Country = true, Name = "PROD 10", Group = 0},
                new Producer { Id = 11,Country = true, Name = "PROD 11", Group = 0},
                new Producer { Id = 12,Country = true, Name = "PROD 12", Group = 0},
                new Producer { Id = 13,Country = true, Name = "PROD 13", Group = 0},
                new Producer { Id = 14,Country = true, Name = "PROD 14", Group = 0},
                new Producer { Id = 15,Country = true, Name = "PROD 15", Group = 0},
                new Producer { Id = 16,Country = true, Name = "PROD 16", Group = 0},
                new Producer { Id = 17,Country = true, Name = "PROD 17", Group = 0},
                new Producer { Id = 18,Country = true, Name = "PROD 18", Group = 0},
                new Producer { Id = 19,Country = true, Name = "PROD 19", Group = 0},
                new Producer { Id = 20,Country = true, Name = "PROD 20", Group = 0},
                new Producer { Id = 21,Country = false, Name = "PROD 21", Group = 0},
                new Producer { Id = 22,Country = false, Name = "PROD 22", Group = 8},
                new Producer { Id = 23,Country = false, Name = "PROD 23", Group = 8},
                new Producer { Id = 24,Country = false, Name = "PROD 34", Group = 0},
                new Producer { Id = 25,Country = false, Name = "PROD 25", Group = 6},
                new Producer { Id = 26,Country = false, Name = "PROD 26", Group = 2},
                new Producer { Id = 27,Country = false, Name = "PROD 27", Group = 0},
                new Producer { Id = 28,Country = false, Name = "PROD 28", Group = 5},
                new Producer { Id = 29,Country = false, Name = "PROD 29"},
                new Producer { Id = 30, Country = true, Name = "PROD 30", Group = 0},
                new Producer { Id = 31, Country = true, Name = "PROD 31", Group = 0},
                new Producer { Id = 32,Country = false, Name = "PROD 32", Group = 0}
            };
            if (context.Producers.Any())
            {
                foreach (var p in producers)
                {
                    context.Producers.AddOrUpdate(p);
                }
            }
            else if (context.Producers.AsEnumerable().All(p => p.Group == 0))
            {
                foreach (var item in producers)
                {
                    var p = context.Producers.FirstOrDefault(d => d.Name == item.Name);
                    if (p != null)
                    {
                        p.Group = item.Group;
                        context.Entry(p).State = EntityState.Modified;
                    }
                }
            }

            List<LaxCementType> laxTypes = new List<LaxCementType>()
            {
                new LaxCementType { Id = 1, Name = "CEM 1", ProducerId = 2},
                new LaxCementType { Id = 2, Name = "CEM 2", ProducerId = 2},
                new LaxCementType { Id = 3, Name = "CEM 3", ProducerId = 2},
                new LaxCementType { Id = 4, Name = "CEM 4", ProducerId = 2},
                new LaxCementType { Id = 5, Name = "CEM 5", ProducerId = 2},
                new LaxCementType { Id = 6, Name = "CEM 6", ProducerId = 2},
                new LaxCementType { Id = 7, Name = "CEM 7", ProducerId = 2},
                new LaxCementType { Id = 8, Name = "CEM 8", ProducerId = 2},
                new LaxCementType { Id = 9, Name = "CEM 9", ProducerId = 2},
                new LaxCementType { Id = 10, Name = "CEM 10", ProducerId = 2},
                new LaxCementType { Id = 11, Name = "CEM 11", ProducerId = 2},
                new LaxCementType { Id = 12, Name = "CEM 12", ProducerId = 2},
                new LaxCementType { Id = 13, Name = "CEM 13", ProducerId = 4},
                new LaxCementType { Id = 14, Name = "CEM 14", ProducerId = 4},
                new LaxCementType { Id = 15, Name = "CEM 15", ProducerId = 4},
                new LaxCementType { Id = 16, Name = "CEM 16", ProducerId = 4},
                new LaxCementType { Id = 17, Name = "CEM 17", ProducerId = 4},
                new LaxCementType { Id = 18, Name = "CEM 18", ProducerId = 4},
                new LaxCementType { Id = 19, Name = "CEM 19", ProducerId = 4},
                new LaxCementType { Id = 20, Name = "CEM 20", ProducerId = 4},
                new LaxCementType { Id = 21, Name = "CEM 21", ProducerId = 4},
                new LaxCementType { Id = 22, Name = "CEM 22", ProducerId = 4},
                new LaxCementType { Id = 23, Name = "CEM 23", ProducerId = 4},
                new LaxCementType { Id = 24, Name = "CEM 24", ProducerId = 5},
                new LaxCementType { Id = 25, Name = "CEM 25", ProducerId = 5},
                new LaxCementType { Id = 26, Name = "CEM 26", ProducerId = 5},
                new LaxCementType { Id = 27, Name = "CEM 27", ProducerId = 5},
                new LaxCementType { Id = 28, Name = "CEM 28", ProducerId = 5},
                new LaxCementType { Id = 29, Name = "CEM 29", ProducerId = 5},
                new LaxCementType { Id = 30, Name = "CEM 30", ProducerId = 5},
                new LaxCementType { Id = 31, Name = "CEM 31", ProducerId = 5},
                new LaxCementType { Id = 32, Name = "CEM 32", ProducerId = 6},
                new LaxCementType { Id = 33, Name = "CEM 33", ProducerId = 6},
                new LaxCementType { Id = 34, Name = "CEM 34", ProducerId = 6},
                new LaxCementType { Id = 35, Name = "CEM 35", ProducerId = 6},
                new LaxCementType { Id = 36, Name = "CEM 36", ProducerId = 6},
                new LaxCementType { Id = 37, Name = "CEM 37", ProducerId = 7},
                new LaxCementType { Id = 38, Name = "CEM 38", ProducerId = 7},
                new LaxCementType { Id = 39, Name = "CEM 39", ProducerId = 7},
                new LaxCementType { Id = 40, Name = "CEM 40", ProducerId = 7},
                new LaxCementType { Id = 41, Name = "CEM 41", ProducerId = 7},
                new LaxCementType { Id = 42, Name = "CEM 42", ProducerId = 7},
                new LaxCementType { Id = 43, Name = "CEM 43", ProducerId = 7},
                new LaxCementType { Id = 44, Name = "CEM 44", ProducerId = 7},
                new LaxCementType { Id = 45, Name = "CEM 45", ProducerId = 7},
                new LaxCementType { Id = 46, Name = "CEM 46", ProducerId = 8},
                new LaxCementType { Id = 47, Name = "CEM 47", ProducerId = 8},
                new LaxCementType { Id = 48, Name = "CEM 48", ProducerId = 8},
                new LaxCementType { Id = 49, Name = "CEM 49", ProducerId = 8},
                new LaxCementType { Id = 50, Name = "CEM 50", ProducerId = 8},
                new LaxCementType { Id = 51, Name = "CEM 51", ProducerId = 1},
                new LaxCementType { Id = 52, Name = "CEM 52", ProducerId = 1},
                new LaxCementType { Id = 53, Name = "CEM 53", ProducerId = 1},
                new LaxCementType { Id = 54, Name = "CEM 54", ProducerId = 1},
                new LaxCementType { Id = 55, Name = "CEM 55", ProducerId = 1},
                new LaxCementType { Id = 56, Name = "CEM 56", ProducerId = 1},
                new LaxCementType { Id = 57, Name = "CEM 57", ProducerId = 1},
                new LaxCementType { Id = 58, Name = "CEM 58", ProducerId = 1},
                new LaxCementType { Id = 59, Name = "CEM 59", ProducerId = 1},
                new LaxCementType { Id = 60, Name = "CEM 60", ProducerId = 1},
                new LaxCementType { Id = 61, Name = "CEM 61", ProducerId = 1},
                new LaxCementType { Id = 62, Name = "CEM 62", ProducerId = 1},
                new LaxCementType { Id = 63, Name = "CEM 63", ProducerId = 21},
                new LaxCementType { Id = 64, Name = "CEM 64", ProducerId = 21},
                new LaxCementType { Id = 65, Name = "CEM 65", ProducerId = 21},
                new LaxCementType { Id = 66, Name = "CEM 66", ProducerId = 21},
                new LaxCementType { Id = 67, Name = "CEM 67", ProducerId = 21},
                new LaxCementType { Id = 68, Name = "CEM 68", ProducerId = 21},
                new LaxCementType { Id = 69, Name = "CEM 69", ProducerId = 21},
                new LaxCementType { Id = 70, Name = "CEM 70", ProducerId = 21},
                new LaxCementType { Id = 71, Name = "CEM 71", ProducerId = 21},
                new LaxCementType { Id = 72, Name = "CEM 72", ProducerId = 21},
                new LaxCementType { Id = 73, Name = "CEM 73", ProducerId = 21},
                new LaxCementType { Id = 74, Name = "CEM 74", ProducerId = 21},
                new LaxCementType { Id = 75, Name = "CEM 75", ProducerId = 21},
                new LaxCementType { Id = 76, Name = "CEM 76", ProducerId = 21},
                new LaxCementType { Id = 77, Name = "CEM 77", ProducerId = 21},
                new LaxCementType { Id = 78, Name = "CEM 78", ProducerId = 21},
                new LaxCementType { Id = 79, Name = "CEM 79", ProducerId = 21},
                new LaxCementType { Id = 80, Name = "CEM 80", ProducerId = 22},
                new LaxCementType { Id = 81, Name = "CEM 81", ProducerId = 22},
                new LaxCementType { Id = 82, Name = "CEM 82", ProducerId = 22},
                new LaxCementType { Id = 83, Name = "CEM 83", ProducerId = 22},
                new LaxCementType { Id = 84, Name = "CEM 84", ProducerId = 22},
                new LaxCementType { Id = 85, Name = "CEM 85", ProducerId = 22},
                new LaxCementType { Id = 86, Name = "CEM 86", ProducerId = 22},
                new LaxCementType { Id = 87, Name = "CEM 87", ProducerId = 22},
                new LaxCementType { Id = 88, Name = "CEM 88", ProducerId = 22},
                new LaxCementType { Id = 89, Name = "CEM 89", ProducerId = 22},
                new LaxCementType { Id = 90, Name = "CEM 90", ProducerId = 22},
                new LaxCementType { Id = 91, Name = "CEM 91", ProducerId = 22},
                new LaxCementType { Id = 92, Name = "CEM 92", ProducerId = 23},
                new LaxCementType { Id = 93, Name = "CEM 93", ProducerId = 23},
                new LaxCementType { Id = 94, Name = "CEM 94", ProducerId = 23},
                new LaxCementType { Id = 95, Name = "CEM 95", ProducerId = 23},
                new LaxCementType { Id = 96, Name = "CEM 96", ProducerId = 23},
                new LaxCementType { Id = 97, Name = "CEM 97", ProducerId = 23},
                new LaxCementType { Id = 98, Name = "CEM 98", ProducerId = 23},
                new LaxCementType { Id = 99, Name = "CEM 99", ProducerId = 23},
                new LaxCementType { Id = 100, Name = "CEM 100", ProducerId = 24},
                new LaxCementType { Id = 101, Name = "CEM 101", ProducerId = 24},
                new LaxCementType { Id = 102, Name = "CEM 102", ProducerId = 24},
                new LaxCementType { Id = 103, Name = "CEM 103", ProducerId = 24},
                new LaxCementType { Id = 104, Name = "CEM 104", ProducerId = 24},
                new LaxCementType { Id = 105, Name = "CEM 105", ProducerId = 25},
                new LaxCementType { Id = 106, Name = "CEM 106", ProducerId = 25},
                new LaxCementType { Id = 107, Name = "CEM 107", ProducerId = 25},
                new LaxCementType { Id = 108, Name = "CEM 108", ProducerId = 25},
                new LaxCementType { Id = 109, Name = "CEM 109", ProducerId = 25},
                new LaxCementType { Id = 110, Name = "CEM 110", ProducerId = 25},
                new LaxCementType { Id = 111, Name = "CEM 111", ProducerId = 26},
                new LaxCementType { Id = 112, Name = "CEM 112", ProducerId = 26},
                new LaxCementType { Id = 113, Name = "CEM 113", ProducerId = 26},
                new LaxCementType { Id = 114, Name = "CEM 114", ProducerId = 27},
                new LaxCementType { Id = 115, Name = "CEM 115", ProducerId = 27},
                new LaxCementType { Id = 116, Name = "CEM 116", ProducerId = 27},
                new LaxCementType { Id = 117, Name = "CEM 117", ProducerId = 27},
                new LaxCementType { Id = 118, Name = "CEM 118", ProducerId = 27},
                new LaxCementType { Id = 119, Name = "CEM 119", ProducerId = 27},
                new LaxCementType { Id = 120, Name = "CEM 120", ProducerId = 27},
                new LaxCementType { Id = 121, Name = "CEM 121", ProducerId = 27},
                new LaxCementType { Id = 122, Name = "CEM 122", ProducerId = 27},
                new LaxCementType { Id = 123, Name = "CEM 123", ProducerId = 27},
                new LaxCementType { Id = 124, Name = "CEM 124", ProducerId = 27},
                new LaxCementType { Id = 125, Name = "CEM 125", ProducerId = 28},
                new LaxCementType { Id = 126, Name = "CEM 126", ProducerId = 28},
                new LaxCementType { Id = 127, Name = "CEM 127", ProducerId = 28},
                new LaxCementType { Id = 128, Name = "CEM 128", ProducerId = 28},
                new LaxCementType { Id = 129, Name = "CEM 129", ProducerId = 28},
                new LaxCementType { Id = 130, Name = "CEM 130", ProducerId = 28},
                new LaxCementType { Id = 131, Name = "CEM 131", ProducerId = 28},
                new LaxCementType { Id = 132, Name = "CEM 132", ProducerId = 28},
                new LaxCementType { Id = 133, Name = "CEM 133", ProducerId = 28},
                new LaxCementType { Id = 134, Name = "CEM 134", ProducerId = 28},
                new LaxCementType { Id = 135, Name = "CEM 135", ProducerId = 28},
                new LaxCementType { Id = 136, Name = "CEM 136", ProducerId = 28},
                new LaxCementType { Id = 137, Name = "CEM 137", ProducerId = 28},
                new LaxCementType { Id = 138, Name = "CEM 138", ProducerId = 28},
                new LaxCementType { Id = 139, Name = "CEM 139", ProducerId = 28},
                new LaxCementType { Id = 140, Name = "CEM 140", ProducerId = 28},
                new LaxCementType { Id = 141, Name = "CEM 141", ProducerId = 28},
                new LaxCementType { Id = 142, Name = "CEM 142", ProducerId = 28},
                new LaxCementType { Id = 143, Name = "CEM 143", ProducerId = 28},
                new LaxCementType { Id = 144, Name = "CEM 144", ProducerId = 28},
                new LaxCementType { Id = 145, Name = "CEM 145", ProducerId = 28},
                new LaxCementType { Id = 146, Name = "CEM 146", ProducerId = 29},
                new LaxCementType { Id = 147, Name = "CEM 147", ProducerId = 29},
                new LaxCementType { Id = 148, Name = "CEM 148", ProducerId = 29},
                new LaxCementType { Id = 149, Name = "CEM 149", ProducerId = 29},
                new LaxCementType { Id = 150, Name = "CEM 150", ProducerId = 29},
                new LaxCementType { Id = 151, Name = "CEM 151", ProducerId = 29},
                new LaxCementType { Id = 152, Name = "CEM 152", ProducerId = 29},
                new LaxCementType { Id = 153, Name = "CEM 153", ProducerId = 29},
                new LaxCementType { Id = 154, Name = "CEM 154", ProducerId = 29},
                new LaxCementType { Id = 155, Name = "CEM 155", ProducerId = 29},
                new LaxCementType { Id = 156, Name = "CEM 156", ProducerId = 29},
                new LaxCementType { Id = 157, Name = "CEM 157", ProducerId = 29},
                new LaxCementType { Id = 158, Name = "CEM 158", ProducerId = 29},
                new LaxCementType { Id = 159, Name = "CEM 159", ProducerId = 29},
                new LaxCementType { Id = 160, Name = "CEM 160", ProducerId = 29},
                new LaxCementType { Id = 161, Name = "CEM 161", ProducerId = 29},
                new LaxCementType { Id = 162, Name = "CEM 162", ProducerId = 29},
                new LaxCementType { Id = 163, Name = "CEM 163", ProducerId = 32}
            };

            if (context.LaxCementTypes.Count() <= 0)
            {
                foreach (LaxCementType l in laxTypes)
                {
                    context.LaxCementTypes.AddOrUpdate(l);
                }
            }

            List<PackedCementType> packedTypes = new List<PackedCementType>()
            {
                new PackedCementType { Id = 1, Name = "CEM 1", ProducerId = 2},
                new PackedCementType { Id = 2, Name = "CEM 2", ProducerId = 2},
                new PackedCementType { Id = 3, Name = "CEM 3", ProducerId = 2},
                new PackedCementType { Id = 4, Name = "CEM 4", ProducerId = 2},
                new PackedCementType { Id = 5, Name = "CEM 5", ProducerId = 2},
                new PackedCementType { Id = 6, Name = "CEM 6", ProducerId = 3},
                new PackedCementType { Id = 7, Name = "CEM 7", ProducerId = 3},
                new PackedCementType { Id = 8, Name = "CEM 8", ProducerId = 4},
                new PackedCementType { Id = 9, Name = "CEM 9", ProducerId = 4},
                new PackedCementType { Id = 10, Name = "CEM 10", ProducerId = 4},
                new PackedCementType { Id = 11, Name = "CEM 11", ProducerId = 4},
                new PackedCementType { Id = 12, Name = "CEM 12", ProducerId = 4},
                new PackedCementType { Id = 13, Name = "CEM 13", ProducerId = 5},
                new PackedCementType { Id = 14, Name = "CEM 14", ProducerId = 5},
                new PackedCementType { Id = 15, Name = "CEM 15", ProducerId = 6},
                new PackedCementType { Id = 16, Name = "CEM 16", ProducerId = 6},
                new PackedCementType { Id = 17, Name = "CEM 17", ProducerId = 6},
                new PackedCementType { Id = 18, Name = "CEM 18", ProducerId = 7},
                new PackedCementType { Id = 19, Name = "CEM 19", ProducerId = 7},
                new PackedCementType { Id = 20, Name = "CEM 20", ProducerId = 7},
                new PackedCementType { Id = 21, Name = "CEM 21", ProducerId = 8},
                new PackedCementType { Id = 22, Name = "CEM 22", ProducerId = 8},
                new PackedCementType { Id = 23, Name = "CEM 23", ProducerId = 8},
                new PackedCementType { Id = 24, Name = "CEM 24", ProducerId = 8},
                new PackedCementType { Id = 25, Name = "CEM 25", ProducerId = 9},
                new PackedCementType { Id = 26, Name = "CEM 26", ProducerId = 9},
                new PackedCementType { Id = 27, Name = "CEM 27", ProducerId = 9},
                new PackedCementType { Id = 28, Name = "CEM 28", ProducerId = 10},
                new PackedCementType { Id = 29, Name = "CEM 29", ProducerId = 10},
                new PackedCementType { Id = 30, Name = "CEM 30", ProducerId = 10},
                new PackedCementType { Id = 31, Name = "CEM 31", ProducerId = 10},
                new PackedCementType { Id = 32, Name = "CEM 32", ProducerId = 11},
                new PackedCementType { Id = 33, Name = "CEM 33", ProducerId = 11},
                new PackedCementType { Id = 34, Name = "CEM 34", ProducerId = 11},
                new PackedCementType { Id = 35, Name = "CEM 35", ProducerId = 12},
                new PackedCementType { Id = 36, Name = "CEM 36", ProducerId = 13},
                new PackedCementType { Id = 37, Name = "CEM 37", ProducerId = 14},
                new PackedCementType { Id = 38, Name = "CEM 38", ProducerId = 14},
                new PackedCementType { Id = 39, Name = "CEM 39", ProducerId = 15},
                new PackedCementType { Id = 40, Name = "CEM 40", ProducerId = 16},
                new PackedCementType { Id = 41, Name = "CEM 41", ProducerId = 16},
                new PackedCementType { Id = 42, Name = "CEM 42", ProducerId = 17},
                new PackedCementType { Id = 43, Name = "CEM 43", ProducerId = 1},
                new PackedCementType { Id = 44, Name = "CEM 44", ProducerId = 1},
                new PackedCementType { Id = 45, Name = "CEM 45", ProducerId = 1},
                new PackedCementType { Id = 46, Name = "CEM 46", ProducerId = 1},
                new PackedCementType { Id = 47, Name = "CEM 47", ProducerId = 1},
                new PackedCementType { Id = 48, Name = "CEM 48", ProducerId = 1},
                new PackedCementType { Id = 49, Name = "CEM 49", ProducerId = 4},
                new PackedCementType { Id = 50, Name = "CEM 50", ProducerId = 10},
                new PackedCementType { Id = 51, Name = "CEM 51", ProducerId = 18},
                new PackedCementType { Id = 52, Name = "CEM 52", ProducerId = 19},
                new PackedCementType { Id = 53, Name = "CEM 53", ProducerId = 20},
                new PackedCementType { Id = 54, Name = "CEM 54", ProducerId = 1},
                new PackedCementType { Id = 55, Name = "CEM 55", ProducerId = 21},
                new PackedCementType { Id = 56, Name = "CEM 56", ProducerId = 21},
                new PackedCementType { Id = 57, Name = "CEM 57", ProducerId = 21},
                new PackedCementType { Id = 58, Name = "CEM 58", ProducerId = 21},
                new PackedCementType { Id = 59, Name = "CEM 59", ProducerId = 22},
                new PackedCementType { Id = 60, Name = "CEM 60", ProducerId = 22},
                new PackedCementType { Id = 61, Name = "CEM 61", ProducerId = 22},
                new PackedCementType { Id = 62, Name = "CEM 62", ProducerId = 22},
                new PackedCementType { Id = 63, Name = "CEM 63", ProducerId = 22},
                new PackedCementType { Id = 64, Name = "CEM 64", ProducerId = 22},
                new PackedCementType { Id = 65, Name = "CEM 65", ProducerId = 23},
                new PackedCementType { Id = 66, Name = "CEM 66", ProducerId = 23},
                new PackedCementType { Id = 67, Name = "CEM 67", ProducerId = 23},
                new PackedCementType { Id = 68, Name = "CEM 68", ProducerId = 24},
                new PackedCementType { Id = 69, Name = "CEM 69", ProducerId = 24},
                new PackedCementType { Id = 70, Name = "CEM 70", ProducerId = 24},
                new PackedCementType { Id = 71, Name = "CEM 71", ProducerId = 24},
                new PackedCementType { Id = 72, Name = "CEM 72", ProducerId = 24},
                new PackedCementType { Id = 73, Name = "CEM 73", ProducerId = 25},
                new PackedCementType { Id = 74, Name = "CEM 74", ProducerId = 25},
                new PackedCementType { Id = 75, Name = "CEM 75", ProducerId = 25},
                new PackedCementType { Id = 76, Name = "CEM 76", ProducerId = 27},
                new PackedCementType { Id = 77, Name = "CEM 77", ProducerId = 27},
                new PackedCementType { Id = 78, Name = "CEM 78", ProducerId = 27},
                new PackedCementType { Id = 79, Name = "CEM 79", ProducerId = 27},
                new PackedCementType { Id = 80, Name = "CEM 80", ProducerId = 27},
                new PackedCementType { Id = 81, Name = "CEM 81", ProducerId = 27},
                new PackedCementType { Id = 82, Name = "CEM 82", ProducerId = 27},
                new PackedCementType { Id = 83, Name = "CEM 83", ProducerId = 27},
                new PackedCementType { Id = 84, Name = "CEM 84", ProducerId = 28},
                new PackedCementType { Id = 85, Name = "CEM 85", ProducerId = 28},
                new PackedCementType { Id = 86, Name = "CEM 86", ProducerId = 28},
                new PackedCementType { Id = 87, Name = "CEM 87", ProducerId = 28},
                new PackedCementType { Id = 88, Name = "CEM 88", ProducerId = 28},
                new PackedCementType { Id = 89, Name = "CEM 89", ProducerId = 28},
                new PackedCementType { Id = 90, Name = "CEM 90", ProducerId = 29},
                new PackedCementType { Id = 91, Name = "CEM 91", ProducerId = 29},
                new PackedCementType { Id = 92, Name = "CEM 92", ProducerId = 10},
                new PackedCementType { Id = 93, Name = "CEM 93", ProducerId = 30},
                new PackedCementType { Id = 94, Name = "CEM 94", ProducerId = 31},
                new PackedCementType { Id = 95, Name = "CEM 95", ProducerId = 31},
                new PackedCementType { Id = 96, Name = "CEM 96", ProducerId = 32}
            };

            if (context.PackedCementTypes.Count() <= 0)
            {
                foreach (PackedCementType p in packedTypes)
                {
                    context.PackedCementTypes.AddOrUpdate(p);
                }
            }

            List<UnloadType> unloadTypes = new List<UnloadType>()
            {
                new UnloadType { Id = 1, Name = "HDS"},
                new UnloadType { Id = 2, Name = "Wózek widłowy"},
                new UnloadType { Id = 3, Name = "Inne"}
            };

            foreach (UnloadType u in unloadTypes)
            {
                context.UnloadTypes.AddOrUpdate(u);
            }

            List<Clients> clients = new List<Clients>()
            {
                new Clients { Id = 1, Name = "Firmy budowlane"},
                new Clients { Id = 2, Name = "Producenci"},
                new Clients { Id = 3, Name = "Firmy handlowe"},
                new Clients { Id = 4, Name = "Odbiorcy indywidualni"}
            };

            foreach (Clients c in clients)
            {
                context.Clients.AddOrUpdate(c);
            }

            List<Supplier> suppliers = new List<Supplier>()
            {
                new Supplier { Id = 1, Name = "SUP 1"},
                new Supplier { Id = 2, Name = "SUP 2"},
                new Supplier { Id = 3, Name = "SUP 3"},
                new Supplier { Id = 4, Name = "SUP 4"},
                new Supplier { Id = 5, Name = "SUP 5"},
                new Supplier { Id = 6, Name = "SUP 6"},
                new Supplier { Id = 7, Name = "SUP 7"},
                new Supplier { Id = 8, Name = "SUP 8"},
                new Supplier { Id = 9, Name = "SUP 9"},
                new Supplier { Id = 10, Name = "SUP 10"},
                new Supplier { Id = 11, Name = "SUP 11"},
                new Supplier { Id = 12, Name = "SUP 12"},
                new Supplier { Id = 13, Name = "SUP 13"},
                new Supplier { Id = 14, Name = "SUP 14"},
                new Supplier { Id = 15, Name = "SUP 15"},
                new Supplier { Id = 16, Name = "SUP 16"},
                new Supplier { Id = 17, Name = "SUP 17"},
                new Supplier { Id = 18, Name = "SUP 18"},
                new Supplier { Id = 19, Name = "SUP 19"},
                new Supplier { Id = 20, Name = "SUP 20"},
                new Supplier { Id = 21, Name = "SUP 21"},
                new Supplier { Id = 22, Name = "SUP 22"},
                new Supplier { Id = 23, Name = "SUP 23"},
                new Supplier { Id = 24, Name = "SUP 24"},
                new Supplier { Id = 25, Name = "SUP 25"},
                new Supplier { Id = 26, Name = "SUP 26"},
                new Supplier { Id = 27, Name = "SUP 27"},
                new Supplier { Id = 28, Name = "SUP 28"},
                new Supplier { Id = 29, Name = "SUP 29"},
                new Supplier { Id = 30, Name = "SUP 30"},
                new Supplier { Id = 31, Name = "SUP 31"},
                new Supplier { Id = 32, Name = "SUP 32"},
                new Supplier { Id = 33, Name = "SUP 33"},
                new Supplier { Id = 34, Name = "SUP 34"},
                new Supplier { Id = 35, Name = "SUP 35"},
                new Supplier { Id = 36, Name = "SUP 36"},
                new Supplier { Id = 37, Name = "SUP 37"},
                new Supplier { Id = 38, Name = "SUP 38"},
                new Supplier { Id = 39, Name = "SUP 39"},
                new Supplier { Id = 40, Name = "SUP 40"},
                new Supplier { Id = 41, Name = "SUP 41"},
                new Supplier { Id = 42, Name = "SUP 42"},
                new Supplier { Id = 43, Name = "SUP 43"},
                new Supplier { Id = 44, Name = "SUP 44"},
                new Supplier { Id = 45, Name = "SUP 45"},
                new Supplier { Id = 46, Name = "SUP 46"},
                new Supplier { Id = 47, Name = "SUP 47"},
                new Supplier { Id = 48, Name = "SUP 48"},
                new Supplier { Id = 49, Name = "SUP 49"},
                new Supplier { Id = 50, Name = "SUP 50"},
                new Supplier { Id = 51, Name = "SUP 51"},
                new Supplier { Id = 52, Name = "SUP 52"},
                new Supplier { Id = 53, Name = "SUP 53"},
                new Supplier { Id = 54, Name = "SUP 54"},
                new Supplier { Id = 55, Name = "SUP 55"},
                new Supplier { Id = 56, Name = "SUP 56"},
                new Supplier { Id = 57, Name = "SUP 57"},
                new Supplier { Id = 58, Name = "SUP 58"}
            };

            if (context.Suppliers.Count() <= 0)
            {
                foreach (Supplier s in suppliers)
                {
                    context.Suppliers.AddOrUpdate(s);
                }
            }

            List<Country> countries = new List<Country>()
            {
                new Country { Id = 1, Name = "Polska"},
                new Country { Id = 2, Name = "Słowacja"},
                new Country { Id = 3, Name = "Czechy"},
                new Country { Id = 4, Name = "Niemcy"}
            };

            foreach (Country c in countries)
            {
                context.Countries.AddOrUpdate(c);
            }

            List<ManufacturersGroup> manuGroups = new List<ManufacturersGroup>()
            {
                new ManufacturersGroup { Id = 1, CountryId = 1, Name = "MAN GROUP 1"},
                new ManufacturersGroup { Id = 2, CountryId = 1, Name = "MAN GROUP 2"},
                new ManufacturersGroup { Id = 3, CountryId = 1, Name = "MAN GROUP 3"},
                new ManufacturersGroup { Id = 4, CountryId = 1, Name = "MAN GROUP 4"},
                new ManufacturersGroup { Id = 5, CountryId = 1, Name = "MAN GROUP 5"},
                new ManufacturersGroup { Id = 6, CountryId = 1, Name = "MAN GROUP 6"},
                new ManufacturersGroup { Id = 7, CountryId = 1, Name = "MAN GROUP 7"},
                new ManufacturersGroup { Id = 8, CountryId = 2, Name = "MAN GROUP 8"},
            };

            if (context.ManufacturersGroups.Count() <= 0)
            {
                foreach (var g in manuGroups)
                {
                    context.ManufacturersGroups.AddOrUpdate(g);
                }
            }

            List<Manufacturer> manufacturers = new List<Manufacturer>()
            {
                new Manufacturer { Id = 1, Name = "MAN 1", GroupId = 1, CountryId = 1, Street = "Ulica 1", BuildingNumber = "1", City = "Miasto 1", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 2, Name = "MAN 2", GroupId = 1, CountryId = 1, Street = "Ulica 2", BuildingNumber = "14", City = "Miasto 2", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 3, Name = "MAN 3", GroupId = 2, CountryId = 1, Street = "Ulica 3", BuildingNumber = "10", City = "Miasto 3", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 4, Name = "MAN 4", GroupId = 2, CountryId = 1, Street = "Ulica 4", BuildingNumber = "6", City = "Miasto 4", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 5, Name = "MAN 5", GroupId = 3, CountryId = 1, Street = "Ulica 5", BuildingNumber = "9", City = "Miasto 5", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 6, Name = "MAN 6", GroupId = 3, CountryId = 1, Street = "Ulica 6", BuildingNumber = "17", City = "Miasto 6", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 7, Name = "MAN 7", GroupId = 4, CountryId = 1, Street = "Ulica 7", BuildingNumber = "77", City = "Miasto 7", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 8, Name = "MAN 8", GroupId = 4, CountryId = 1, Street = "Ulica 8", BuildingNumber = "1", City = "Miasto 8", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 9, Name = "MAN 9", GroupId = 5, CountryId = 1, Street = "Ulica 9", BuildingNumber = "", City = "Miasto 9", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 10, Name = "MAN 10", GroupId = 5, CountryId = 1, Street = "Ulica 10", BuildingNumber = "110", City = "Miasto 10", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 11, Name = "MAN 11", GroupId = 6, CountryId = 1, Street = "Ulica 11", BuildingNumber = "3", City = "Miasto 11", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 12, Name = "MAN 12", GroupId = 7, CountryId = 1, Street = "Ulica 12", BuildingNumber = "", City = "Miasto 12", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 13, Name = "MAN 13", GroupId = 0, CountryId = 2, Street = "Ulica 13", BuildingNumber = "", City = "Miasto 13", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 14, Name = "MAN 14", GroupId = 8, CountryId = 2, Street = "Ulica 14", BuildingNumber = "", City = "Miasto 14", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 15, Name = "MAN 15", GroupId = 8, CountryId = 2, Street = "Ulica 15", BuildingNumber = "", City = "Miasto 15", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 16, Name = "MAN 16", GroupId = 0, CountryId = 2, Street = "Ulica 16", BuildingNumber = "14", City = "Miasto 16", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 17, Name = "MAN 17", GroupId = 0, CountryId = 3, Street = "Ulica 17", BuildingNumber = "288", City = "Miasto 17", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 18, Name = "MAN 18", GroupId = 0, CountryId = 3, Street = "Ulica 18", BuildingNumber = "1234", City = "Miasto 18", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 19, Name = "MAN 19", GroupId = 0, CountryId = 4, Street = "Ulica 19", BuildingNumber = "", City = "Miasto 19", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 20, Name = "MAN 20", GroupId = 0, CountryId = 4, Street = "Ulica 20", BuildingNumber = "9", City = "Miasto 20", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 21, Name = "MAN 21", GroupId = 0, CountryId = 4, Street = "Ulica 21", BuildingNumber = "25", City = "Miasto 21", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 22, Name = "MAN 22", GroupId = 0, CountryId = 4, Street = "Ulica 22", BuildingNumber = "67", City = "Miasto 22", Lat = "0.0", Lng = "0.0"},
                new Manufacturer { Id = 23, Name = "Mieszalnie", GroupId = 0, CountryId = 0}
            };

            if (context.Manufacturers.Count() <= 0)
            {
                foreach (Manufacturer m in manufacturers)
                {
                    context.Manufacturers.AddOrUpdate(m);
                }
            }

            List<Distributor> distributors = new List<Distributor>()
            {
                new Distributor { Id = 1, Name = "DIST 1", BuildingNumber = "Brak", City = "Brak", NIP = "Brak", PostCode = "Brak", Street = "Brak"},
                new Distributor { Id = 2, Name = "DIST 2", BuildingNumber = "Brak", City = "Brak", NIP = "Brak", PostCode = "Brak", Street = "Brak"},
                new Distributor { Id = 3, Name = "DIST 3", BuildingNumber = "Brak", City = "Brak", NIP = "Brak", PostCode = "Brak", Street = "Brak"},
                new Distributor { Id = 4, Name = "DIST 4", BuildingNumber = "Brak", City = "Brak", NIP = "Brak", PostCode = "Brak", Street = "Brak"},
                new Distributor { Id = 5, Name = "DIST 5", BuildingNumber = "Brak", City = "Brak", NIP = "Brak", PostCode = "Brak", Street = "Brak"},
                new Distributor { Id = 6, Name = "DIST 6", BuildingNumber = "Brak", City = "Brak", NIP = "Brak", PostCode = "Brak", Street = "Brak"},
                new Distributor { Id = 7, Name = "DIST 7", BuildingNumber = "Brak", City = "Brak", NIP = "Brak", PostCode = "Brak", Street = "Brak"}
            };

            if (context.Distributors.Count() <= 0)
            {
                foreach (Distributor d in distributors)
                {
                    context.Distributors.AddOrUpdate(d);
                }
            }

            List<Group> groups = new List<Group>()
            {
                new Group { Id = 1, Name = "GRP 1"},
                new Group { Id = 2, Name = "GRP 2"},
                new Group { Id = 3, Name = "GRP 3"},
                new Group { Id = 4, Name = "GRP 4"},
                new Group { Id = 5, Name = "GRP 5"}
            };

            if (context.Groups.Count() <= 0)
            {
                foreach (var group in groups)
                {
                    context.Groups.AddOrUpdate(group);
                }
            }

            context.SaveChanges();
        }
    }
}
