using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace webapp.Models
{
    public class WebappContext : DbContext
    {
        public WebappContext()
        {
           // Database.SetInitializer(new MigrateDatabaseToLatestVersion<WebappContext, Migrations.Configuration>("WebappContext"));
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<BusinessData> BusinessDatas { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<ClientStep1> ClientSteps1 { get; set; }
        public DbSet<ClientStep2> ClientSteps2 { get; set; }
        public DbSet<ClientStep3> ClientSteps3 { get; set; }
        public DbSet<ClientStep3_5> ClientSteps3_5 { get; set; }
        public DbSet<ClientStep4> ClientSteps4 { get; set; }
        public DbSet<ClientStep5> ClientSteps5 { get; set; }
        public DbSet<ClientStep6> ClientSteps6 { get; set; }
        public DbSet<ClientStep7> ClientSteps7 { get; set; }
        public DbSet<ClientStep8> ClientSteps8 { get; set; }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<MarketSegment> MarketSegments { get; set; }
        public DbSet<CooperationType> CooperationTypes { get; set; }
        public DbSet<LegalForm> LegalForms { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<ManufacturersGroup> ManufacturersGroups { get; set; }
        public DbSet<Distributor> Distributors { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<Province> Provinces { get; set; }
        public DbSet<District> Districts { get; set; }

        public DbSet<Clients> Clients { get; set; }

        public DbSet<Agent> Agents { get; set; }
        public DbSet<Group> Groups { get; set; }

        public DbSet<PartOfCement> PartsOfCement { get; set; }
        public DbSet<KindOfPackage> KindsOfPackage { get; set; }
        public DbSet<LaxCementType> LaxCementTypes { get; set; }
        public DbSet<PackedCementType> PackedCementTypes { get; set; }
        public DbSet<UnloadType> UnloadTypes { get; set; }
        public DbSet<Producer> Producers { get; set; }

        public DbSet<BrandPower> BrandPowers { get; set; }

        public DbSet<MarketAddress> MarketAddresses { get; set; }

        public DbSet<Settings> Settings { get; set; }
        public DbSet<PostCode> PostCodes { get; set; }
    }
}