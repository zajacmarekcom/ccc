namespace webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Agents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Street = c.String(),
                        BuildingNumber = c.String(),
                        PostCode = c.String(),
                        ProvinceId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                        City = c.String(),
                        PhoneNumber = c.String(),
                        Email = c.String(),
                        Trades = c.Boolean(nullable: false),
                        Comments = c.String(),
                        UserId = c.Int(),
                        Used = c.Boolean(nullable: false),
                        ClientStep1_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientStep1", t => t.ClientStep1_Id)
                .Index(t => t.ClientStep1_Id);
            
            CreateTable(
                "dbo.BrandPowers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BusinessDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        CooperationTypeId = c.Int(nullable: false),
                        AddDate = c.DateTime(nullable: false),
                        RecipientName = c.String(),
                        Street = c.String(),
                        BuildingNumber = c.String(),
                        PostCode = c.String(),
                        ProvinceId = c.Int(),
                        DistrictId = c.Int(),
                        City = c.String(),
                        LegalFormId = c.Int(),
                        NIP = c.String(),
                        Sap = c.String(),
                        StartYear = c.Int(nullable: false),
                        GroupMember = c.Boolean(nullable: false),
                        GroupId = c.Int(),
                        Owner = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumber2 = c.String(),
                        PhoneNumber3 = c.String(),
                        OwnerPhoneNumber = c.String(),
                        Emial = c.String(),
                        Website = c.String(),
                        AgentId = c.Int(),
                        ContactPerson = c.String(),
                        ContactPersonPosition = c.String(),
                        ContactPersonEmail = c.String(),
                        ContactPersonPhoneNumber = c.String(),
                        BusinessId = c.Int(nullable: false),
                        Lat = c.String(),
                        Lng = c.String(),
                        IsBranch = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Businesses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientStep1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitMarketSegments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        MarketSegmentId = c.Int(nullable: false),
                        Percent = c.Int(nullable: false),
                        ClientStep1_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MarketSegments", t => t.MarketSegmentId, cascadeDelete: true)
                .ForeignKey("dbo.Visits", t => t.VisitId, cascadeDelete: true)
                .ForeignKey("dbo.ClientStep1", t => t.ClientStep1_Id)
                .Index(t => t.VisitId)
                .Index(t => t.MarketSegmentId)
                .Index(t => t.ClientStep1_Id);
            
            CreateTable(
                "dbo.MarketSegments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Visits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitDate = c.DateTime(nullable: false, storeType: "date"),
                        MainMarketSegmentId = c.Int(nullable: false),
                        Comments = c.String(),
                        BusinessId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientStep2",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        Assortment_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assortments", t => t.Assortment_Id)
                .Index(t => t.Assortment_Id);
            
            CreateTable(
                "dbo.Assortments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AnnualNone = c.Boolean(nullable: false),
                        AnnualNeed = c.Int(nullable: false),
                        PartOfCementId = c.Int(nullable: false),
                        PackageId = c.Int(nullable: false),
                        LaxCementPercent = c.Int(nullable: false),
                        CustomBrand = c.String(),
                        CommonMarketing = c.String(),
                        AdvisoryService = c.String(),
                        FreshCement = c.String(),
                        CostlessComplaint = c.String(),
                        Others = c.String(),
                        AffectTheChoice = c.String(),
                        QualityComments = c.String(),
                        BrandPowerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.KindOfPackages", t => t.PackageId, cascadeDelete: true)
                .ForeignKey("dbo.PartOfCements", t => t.PartOfCementId, cascadeDelete: true)
                .Index(t => t.PartOfCementId)
                .Index(t => t.PackageId);
            
            CreateTable(
                "dbo.KindOfPackages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PartOfCements",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitLaxTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        LaxTypeId = c.Int(nullable: false),
                        Percent = c.Int(nullable: false),
                        ClientStep2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LaxCementTypes", t => t.LaxTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Visits", t => t.VisitId, cascadeDelete: true)
                .ForeignKey("dbo.ClientStep2", t => t.ClientStep2_Id)
                .Index(t => t.VisitId)
                .Index(t => t.LaxTypeId)
                .Index(t => t.ClientStep2_Id);
            
            CreateTable(
                "dbo.LaxCementTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProducerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitPackedTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        PackedTypeId = c.Int(nullable: false),
                        Percent = c.Int(nullable: false),
                        ClientStep2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PackedCementTypes", t => t.PackedTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Visits", t => t.VisitId, cascadeDelete: true)
                .ForeignKey("dbo.ClientStep2", t => t.ClientStep2_Id)
                .Index(t => t.VisitId)
                .Index(t => t.PackedTypeId)
                .Index(t => t.ClientStep2_Id);
            
            CreateTable(
                "dbo.PackedCementTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProducerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientStep3",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        Cooperation = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitManufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Int(nullable: false),
                        GroupId = c.Int(nullable: false),
                        Name = c.String(),
                        Percent = c.Int(nullable: false),
                        ClientStep3_Id = c.Int(),
                        ClientStep3_5_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientStep3", t => t.ClientStep3_Id)
                .ForeignKey("dbo.ClientStep3_5", t => t.ClientStep3_5_Id)
                .Index(t => t.ClientStep3_Id)
                .Index(t => t.ClientStep3_5_Id);
            
            CreateTable(
                "dbo.VisitManufacturersGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturersGroupId = c.Int(nullable: false),
                        Name = c.String(),
                        SelectedManufacturers = c.Boolean(nullable: false),
                        Percent = c.Int(nullable: false),
                        ClientStep3_Id = c.Int(),
                        ClientStep3_5_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientStep3", t => t.ClientStep3_Id)
                .ForeignKey("dbo.ClientStep3_5", t => t.ClientStep3_5_Id)
                .Index(t => t.ClientStep3_Id)
                .Index(t => t.ClientStep3_5_Id);
            
            CreateTable(
                "dbo.VisitSuppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SupplierId = c.Int(nullable: false),
                        Text = c.String(),
                        ClientStep3_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientStep3", t => t.ClientStep3_Id)
                .Index(t => t.ClientStep3_Id);
            
            CreateTable(
                "dbo.ClientStep3_5",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        ReceiptId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitDistributors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistributorId = c.Int(nullable: false),
                        MarketAddressId = c.Int(),
                        Percent = c.Int(nullable: false),
                        ClientStep3_5_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MarketAddresses", t => t.MarketAddressId)
                .ForeignKey("dbo.ClientStep3_5", t => t.ClientStep3_5_Id)
                .Index(t => t.MarketAddressId)
                .Index(t => t.ClientStep3_5_Id);
            
            CreateTable(
                "dbo.VisitDistManufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Int(nullable: false),
                        Percent = c.Int(nullable: false),
                        VisitDistributor_Id = c.Int(),
                        VisitDistManufacturersGroup_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VisitDistributors", t => t.VisitDistributor_Id)
                .ForeignKey("dbo.VisitDistManufacturersGroups", t => t.VisitDistManufacturersGroup_Id)
                .Index(t => t.VisitDistributor_Id)
                .Index(t => t.VisitDistManufacturersGroup_Id);
            
            CreateTable(
                "dbo.VisitDistManufacturersGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturersGroupId = c.Int(nullable: false),
                        SelectedManufacturers = c.Boolean(nullable: false),
                        Percent = c.Int(nullable: false),
                        VisitDistributor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VisitDistributors", t => t.VisitDistributor_Id)
                .Index(t => t.VisitDistributor_Id);
            
            CreateTable(
                "dbo.MarketAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DistributorId = c.Int(nullable: false),
                        City = c.String(nullable: false),
                        BuildingNumber = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        PostCode = c.String(nullable: false),
                        ProvinceId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Districts", t => t.DistrictId, cascadeDelete: true)
                .ForeignKey("dbo.Provinces", t => t.ProvinceId, cascadeDelete: true)
                .ForeignKey("dbo.Distributors", t => t.DistributorId, cascadeDelete: true)
                .Index(t => t.DistributorId)
                .Index(t => t.ProvinceId)
                .Index(t => t.DistrictId);
            
            CreateTable(
                "dbo.Districts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ProvinceId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Provinces",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientStep4",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        Logistic_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Logistics", t => t.Logistic_Id)
                .Index(t => t.Logistic_Id);
            
            CreateTable(
                "dbo.Logistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NumberOfSilos = c.Int(nullable: false),
                        CapacitySilos = c.Int(nullable: false),
                        CoveredMagazineCapacity = c.Int(nullable: false),
                        NotCoveredMagazineCapacity = c.Int(nullable: false),
                        RailwaySiding = c.Int(nullable: false),
                        ReceiveCementByRailwaySiding = c.Int(),
                        OwnCementTank = c.Int(nullable: false),
                        NumberOwnCementTank = c.Int(nullable: false),
                        OwnPlatformTypeCar = c.Int(nullable: false),
                        NumberOwnPlatformTypeCar = c.Int(nullable: false),
                        OwnHDSTypeCar = c.Int(nullable: false),
                        NumberOwnHDSTypeCar = c.Int(nullable: false),
                        CountryLicense = c.Int(nullable: false),
                        AbroadLicense = c.Int(nullable: false),
                        LaxDelivery = c.Int(nullable: false),
                        MaxLaxDelivery = c.Int(nullable: false),
                        PackedDelivery = c.Int(nullable: false),
                        MaxPackedDelivery = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitUnloadTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UnloadTypeId = c.Int(nullable: false),
                        ClientStep4_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientStep4", t => t.ClientStep4_Id)
                .Index(t => t.ClientStep4_Id);
            
            CreateTable(
                "dbo.ClientStep5",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        Annual = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.VisitClients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Percent = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        ClientStep5_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.ClientStep5", t => t.ClientStep5_Id)
                .Index(t => t.ClientId)
                .Index(t => t.ClientStep5_Id);
            
            CreateTable(
                "dbo.ClientStep6",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        OwnLaboratory = c.Int(nullable: false),
                        ForeignLaboratory = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ForeignLaboratoryInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        AnnualAmount = c.Int(nullable: false),
                        Barter = c.Boolean(nullable: false),
                        ClientStep6_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientStep6", t => t.ClientStep6_Id)
                .Index(t => t.ClientStep6_Id);
            
            CreateTable(
                "dbo.ClientStep7",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClientLaxes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CementId = c.Int(nullable: false),
                        BuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceType = c.Int(nullable: false),
                        SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientStep7_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientStep7", t => t.ClientStep7_Id)
                .Index(t => t.ClientStep7_Id);
            
            CreateTable(
                "dbo.ClientPackeds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CementId = c.Int(nullable: false),
                        BuyPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceType = c.Int(nullable: false),
                        SellPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ClientStep7_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientStep7", t => t.ClientStep7_Id)
                .Index(t => t.ClientStep7_Id);
            
            CreateTable(
                "dbo.ClientStep8",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VisitId = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        NewVisitDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ClientStep8_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClientStep8", t => t.ClientStep8_Id)
                .Index(t => t.ClientStep8_Id);
            
            CreateTable(
                "dbo.CooperationTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Distributors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Street = c.String(nullable: false),
                        BuildingNumber = c.String(nullable: false),
                        PostCode = c.String(nullable: false),
                        City = c.String(nullable: false),
                        NIP = c.String(nullable: false),
                        Lat = c.String(),
                        Lng = c.String(),
                        IsMarket = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LegalForms",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Manufacturers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        GroupId = c.Int(nullable: false),
                        CountryId = c.Int(nullable: false),
                        Street = c.String(),
                        BuildingNumber = c.String(),
                        City = c.String(),
                        Lat = c.String(),
                        Lng = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ManufacturersGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostCodes",
                c => new
                    {
                        PostCodeNumber = c.String(nullable: false, maxLength: 128),
                        City = c.String(),
                        ProvinceId = c.Int(nullable: false),
                        DistrictId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PostCodeNumber);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.Boolean(nullable: false),
                        Group = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Receipts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Settings",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
            CreateTable(
                "dbo.Suppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UnloadTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(maxLength: 200),
                        Password = c.String(),
                        FirstName = c.String(maxLength: 200),
                        LastName = c.String(maxLength: 200),
                        Email = c.String(maxLength: 300),
                        PhoneNumber = c.String(maxLength: 50),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MarketAddresses", "DistributorId", "dbo.Distributors");
            DropForeignKey("dbo.Notes", "ClientStep8_Id", "dbo.ClientStep8");
            DropForeignKey("dbo.ClientPackeds", "ClientStep7_Id", "dbo.ClientStep7");
            DropForeignKey("dbo.ClientLaxes", "ClientStep7_Id", "dbo.ClientStep7");
            DropForeignKey("dbo.ForeignLaboratoryInfoes", "ClientStep6_Id", "dbo.ClientStep6");
            DropForeignKey("dbo.VisitClients", "ClientStep5_Id", "dbo.ClientStep5");
            DropForeignKey("dbo.VisitClients", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.VisitUnloadTypes", "ClientStep4_Id", "dbo.ClientStep4");
            DropForeignKey("dbo.ClientStep4", "Logistic_Id", "dbo.Logistics");
            DropForeignKey("dbo.VisitManufacturersGroups", "ClientStep3_5_Id", "dbo.ClientStep3_5");
            DropForeignKey("dbo.VisitManufacturers", "ClientStep3_5_Id", "dbo.ClientStep3_5");
            DropForeignKey("dbo.VisitDistributors", "ClientStep3_5_Id", "dbo.ClientStep3_5");
            DropForeignKey("dbo.VisitDistributors", "MarketAddressId", "dbo.MarketAddresses");
            DropForeignKey("dbo.MarketAddresses", "ProvinceId", "dbo.Provinces");
            DropForeignKey("dbo.MarketAddresses", "DistrictId", "dbo.Districts");
            DropForeignKey("dbo.VisitDistManufacturersGroups", "VisitDistributor_Id", "dbo.VisitDistributors");
            DropForeignKey("dbo.VisitDistManufacturers", "VisitDistManufacturersGroup_Id", "dbo.VisitDistManufacturersGroups");
            DropForeignKey("dbo.VisitDistManufacturers", "VisitDistributor_Id", "dbo.VisitDistributors");
            DropForeignKey("dbo.VisitSuppliers", "ClientStep3_Id", "dbo.ClientStep3");
            DropForeignKey("dbo.VisitManufacturersGroups", "ClientStep3_Id", "dbo.ClientStep3");
            DropForeignKey("dbo.VisitManufacturers", "ClientStep3_Id", "dbo.ClientStep3");
            DropForeignKey("dbo.VisitPackedTypes", "ClientStep2_Id", "dbo.ClientStep2");
            DropForeignKey("dbo.VisitPackedTypes", "VisitId", "dbo.Visits");
            DropForeignKey("dbo.VisitPackedTypes", "PackedTypeId", "dbo.PackedCementTypes");
            DropForeignKey("dbo.VisitLaxTypes", "ClientStep2_Id", "dbo.ClientStep2");
            DropForeignKey("dbo.VisitLaxTypes", "VisitId", "dbo.Visits");
            DropForeignKey("dbo.VisitLaxTypes", "LaxTypeId", "dbo.LaxCementTypes");
            DropForeignKey("dbo.ClientStep2", "Assortment_Id", "dbo.Assortments");
            DropForeignKey("dbo.Assortments", "PartOfCementId", "dbo.PartOfCements");
            DropForeignKey("dbo.Assortments", "PackageId", "dbo.KindOfPackages");
            DropForeignKey("dbo.VisitMarketSegments", "ClientStep1_Id", "dbo.ClientStep1");
            DropForeignKey("dbo.VisitMarketSegments", "VisitId", "dbo.Visits");
            DropForeignKey("dbo.VisitMarketSegments", "MarketSegmentId", "dbo.MarketSegments");
            DropForeignKey("dbo.Branches", "ClientStep1_Id", "dbo.ClientStep1");
            DropIndex("dbo.Notes", new[] { "ClientStep8_Id" });
            DropIndex("dbo.ClientPackeds", new[] { "ClientStep7_Id" });
            DropIndex("dbo.ClientLaxes", new[] { "ClientStep7_Id" });
            DropIndex("dbo.ForeignLaboratoryInfoes", new[] { "ClientStep6_Id" });
            DropIndex("dbo.VisitClients", new[] { "ClientStep5_Id" });
            DropIndex("dbo.VisitClients", new[] { "ClientId" });
            DropIndex("dbo.VisitUnloadTypes", new[] { "ClientStep4_Id" });
            DropIndex("dbo.ClientStep4", new[] { "Logistic_Id" });
            DropIndex("dbo.MarketAddresses", new[] { "DistrictId" });
            DropIndex("dbo.MarketAddresses", new[] { "ProvinceId" });
            DropIndex("dbo.MarketAddresses", new[] { "DistributorId" });
            DropIndex("dbo.VisitDistManufacturersGroups", new[] { "VisitDistributor_Id" });
            DropIndex("dbo.VisitDistManufacturers", new[] { "VisitDistManufacturersGroup_Id" });
            DropIndex("dbo.VisitDistManufacturers", new[] { "VisitDistributor_Id" });
            DropIndex("dbo.VisitDistributors", new[] { "ClientStep3_5_Id" });
            DropIndex("dbo.VisitDistributors", new[] { "MarketAddressId" });
            DropIndex("dbo.VisitSuppliers", new[] { "ClientStep3_Id" });
            DropIndex("dbo.VisitManufacturersGroups", new[] { "ClientStep3_5_Id" });
            DropIndex("dbo.VisitManufacturersGroups", new[] { "ClientStep3_Id" });
            DropIndex("dbo.VisitManufacturers", new[] { "ClientStep3_5_Id" });
            DropIndex("dbo.VisitManufacturers", new[] { "ClientStep3_Id" });
            DropIndex("dbo.VisitPackedTypes", new[] { "ClientStep2_Id" });
            DropIndex("dbo.VisitPackedTypes", new[] { "PackedTypeId" });
            DropIndex("dbo.VisitPackedTypes", new[] { "VisitId" });
            DropIndex("dbo.VisitLaxTypes", new[] { "ClientStep2_Id" });
            DropIndex("dbo.VisitLaxTypes", new[] { "LaxTypeId" });
            DropIndex("dbo.VisitLaxTypes", new[] { "VisitId" });
            DropIndex("dbo.Assortments", new[] { "PackageId" });
            DropIndex("dbo.Assortments", new[] { "PartOfCementId" });
            DropIndex("dbo.ClientStep2", new[] { "Assortment_Id" });
            DropIndex("dbo.VisitMarketSegments", new[] { "ClientStep1_Id" });
            DropIndex("dbo.VisitMarketSegments", new[] { "MarketSegmentId" });
            DropIndex("dbo.VisitMarketSegments", new[] { "VisitId" });
            DropIndex("dbo.Branches", new[] { "ClientStep1_Id" });
            DropTable("dbo.Users");
            DropTable("dbo.UnloadTypes");
            DropTable("dbo.Suppliers");
            DropTable("dbo.Settings");
            DropTable("dbo.Roles");
            DropTable("dbo.Receipts");
            DropTable("dbo.Producers");
            DropTable("dbo.PostCodes");
            DropTable("dbo.ManufacturersGroups");
            DropTable("dbo.Manufacturers");
            DropTable("dbo.LegalForms");
            DropTable("dbo.Groups");
            DropTable("dbo.Distributors");
            DropTable("dbo.Countries");
            DropTable("dbo.CooperationTypes");
            DropTable("dbo.Notes");
            DropTable("dbo.ClientStep8");
            DropTable("dbo.ClientPackeds");
            DropTable("dbo.ClientLaxes");
            DropTable("dbo.ClientStep7");
            DropTable("dbo.ForeignLaboratoryInfoes");
            DropTable("dbo.ClientStep6");
            DropTable("dbo.VisitClients");
            DropTable("dbo.ClientStep5");
            DropTable("dbo.VisitUnloadTypes");
            DropTable("dbo.Logistics");
            DropTable("dbo.ClientStep4");
            DropTable("dbo.Provinces");
            DropTable("dbo.Districts");
            DropTable("dbo.MarketAddresses");
            DropTable("dbo.VisitDistManufacturersGroups");
            DropTable("dbo.VisitDistManufacturers");
            DropTable("dbo.VisitDistributors");
            DropTable("dbo.ClientStep3_5");
            DropTable("dbo.VisitSuppliers");
            DropTable("dbo.VisitManufacturersGroups");
            DropTable("dbo.VisitManufacturers");
            DropTable("dbo.ClientStep3");
            DropTable("dbo.PackedCementTypes");
            DropTable("dbo.VisitPackedTypes");
            DropTable("dbo.LaxCementTypes");
            DropTable("dbo.VisitLaxTypes");
            DropTable("dbo.PartOfCements");
            DropTable("dbo.KindOfPackages");
            DropTable("dbo.Assortments");
            DropTable("dbo.ClientStep2");
            DropTable("dbo.Visits");
            DropTable("dbo.MarketSegments");
            DropTable("dbo.VisitMarketSegments");
            DropTable("dbo.ClientStep1");
            DropTable("dbo.Clients");
            DropTable("dbo.Businesses");
            DropTable("dbo.BusinessDatas");
            DropTable("dbo.BrandPowers");
            DropTable("dbo.Branches");
            DropTable("dbo.Agents");
        }
    }
}
