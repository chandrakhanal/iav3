namespace IndianArmyWeb.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigrationOfIAWebsite : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContactUs",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Email = c.String(),
                        ContactNo = c.String(),
                        SubjectCategory = c.String(),
                        Description = c.String(storeType: "ntext"),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.CSConf",
                c => new
                    {
                        ConfId = c.Int(nullable: false, identity: true),
                        ConfIP = c.String(),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.ConfId);
            
            CreateTable(
                "dbo.MediaCategoryMaster",
                c => new
                    {
                        MediaCategoryId = c.Int(nullable: false, identity: true),
                        MediaCategoryName = c.String(),
                    })
                .PrimaryKey(t => t.MediaCategoryId);
            
            CreateTable(
                "dbo.MediaFile",
                c => new
                    {
                        GuId = c.Guid(nullable: false),
                        MediaGalleryId = c.Int(nullable: false),
                        FileName = c.String(),
                        Extension = c.String(),
                        FilePath = c.String(),
                    })
                .PrimaryKey(t => t.GuId)
                .ForeignKey("dbo.MediaGallery", t => t.MediaGalleryId, cascadeDelete: true)
                .Index(t => t.MediaGalleryId);
            
            CreateTable(
                "dbo.MediaGallery",
                c => new
                    {
                        MediaGalleryId = c.Int(nullable: false, identity: true),
                        MediaType = c.Int(nullable: false),
                        Caption = c.String(),
                        Description = c.String(unicode: false, storeType: "text"),
                        Archive = c.Boolean(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        ArchiveDate = c.DateTime(nullable: false),
                        MediaCategoryId = c.Int(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.MediaGalleryId)
                .ForeignKey("dbo.MediaCategoryMaster", t => t.MediaCategoryId, cascadeDelete: true)
                .Index(t => t.MediaCategoryId);
            
            CreateTable(
                "dbo.MenuItemMaster",
                c => new
                    {
                        MenuId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(nullable: false),
                        SlugMenu = c.String(),
                        SortOrder = c.Int(nullable: false),
                        MenuName = c.String(),
                        HMenuName = c.String(),
                        PageTitle = c.String(),
                        PageHeading = c.String(),
                        HPageHeading = c.String(),
                        IsVisible = c.Boolean(nullable: false),
                        PositionType = c.Int(nullable: false),
                        Layout = c.Int(nullable: false),
                        ExternalLink = c.Boolean(nullable: false),
                        ExternalUrl = c.String(),
                        MenuUrlId = c.Int(),
                    })
                .PrimaryKey(t => t.MenuId)
                .ForeignKey("dbo.MenuUrlMaster", t => t.MenuUrlId)
                .Index(t => t.MenuUrlId);
            
            CreateTable(
                "dbo.MenuUrlMaster",
                c => new
                    {
                        MenuUrlId = c.Int(nullable: false, identity: true),
                        UrlPrefix = c.String(),
                        Controller = c.String(),
                        Action = c.String(),
                        PageType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MenuUrlId);
            
            CreateTable(
                "dbo.MenuRoles",
                c => new
                    {
                        RoleId = c.Int(nullable: false),
                        MenuId = c.Int(nullable: false),
                        Read = c.Boolean(nullable: false),
                        Write = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.RoleId, t.MenuId })
                .ForeignKey("dbo.MenuItemMaster", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.NewsArticle",
                c => new
                    {
                        NewsArticleId = c.Int(nullable: false, identity: true),
                        NewsCategory = c.Int(nullable: false),
                        Headline = c.String(),
                        Description = c.String(storeType: "ntext"),
                        Highlight = c.Boolean(nullable: false),
                        Archive = c.Boolean(nullable: false),
                        PublishDate = c.DateTime(nullable: false),
                        ArchiveDate = c.DateTime(nullable: false),
                        CreatedAt = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdatedAt = c.DateTime(),
                        LastUpdatedBy = c.String(),
                    })
                .PrimaryKey(t => t.NewsArticleId);
            
            CreateTable(
                "dbo.PageContent",
                c => new
                    {
                        PageContentId = c.Int(nullable: false, identity: true),
                        Content = c.String(storeType: "ntext"),
                        MenuId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PageContentId)
                .ForeignKey("dbo.MenuItemMaster", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.UserActivity",
                c => new
                    {
                        ActivityId = c.Int(nullable: false, identity: true),
                        Url = c.String(),
                        Data = c.String(),
                        UserName = c.String(),
                        IpAddress = c.String(),
                        ActivityDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ActivityId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PageContent", "MenuId", "dbo.MenuItemMaster");
            DropForeignKey("dbo.MenuRoles", "MenuId", "dbo.MenuItemMaster");
            DropForeignKey("dbo.MenuItemMaster", "MenuUrlId", "dbo.MenuUrlMaster");
            DropForeignKey("dbo.MediaFile", "MediaGalleryId", "dbo.MediaGallery");
            DropForeignKey("dbo.MediaGallery", "MediaCategoryId", "dbo.MediaCategoryMaster");
            DropIndex("dbo.PageContent", new[] { "MenuId" });
            DropIndex("dbo.MenuRoles", new[] { "MenuId" });
            DropIndex("dbo.MenuItemMaster", new[] { "MenuUrlId" });
            DropIndex("dbo.MediaGallery", new[] { "MediaCategoryId" });
            DropIndex("dbo.MediaFile", new[] { "MediaGalleryId" });
            DropTable("dbo.UserActivity");
            DropTable("dbo.PageContent");
            DropTable("dbo.NewsArticle");
            DropTable("dbo.MenuRoles");
            DropTable("dbo.MenuUrlMaster");
            DropTable("dbo.MenuItemMaster");
            DropTable("dbo.MediaGallery");
            DropTable("dbo.MediaFile");
            DropTable("dbo.MediaCategoryMaster");
            DropTable("dbo.CSConf");
            DropTable("dbo.ContactUs");
        }
    }
}
