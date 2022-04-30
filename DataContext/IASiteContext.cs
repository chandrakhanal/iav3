using IndianArmyWeb.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace IndianArmyWeb.DataContexts
{
    public class IASiteContext : DbContext
    {
        public IASiteContext() : base("IASiteConString")
        {
        }

        #region DbSets
        public virtual DbSet<MenuItemMaster> MenuItemMstr { get; set; }
        public virtual DbSet<MenuUrlMaster> MenuUrlMstr { get; set; }
        public virtual DbSet<PageContent> PageContents { get; set; }
        public virtual DbSet<MenuRole> MenuRoles { get; set; }
        public virtual DbSet<NewsArticle> NewsArticles { get; set; }
        public virtual DbSet<MediaCategoryMaster> MediaCategoryMstr { get; set; }
        public virtual DbSet<MediaGallery> MediaGalleries { get; set; }
        public virtual DbSet<MediaFile> MediaFiles { get; set; }
        public virtual DbSet<UserActivity> UserActivities { get; set; }
        public virtual DbSet<ContactUs> Contact { get; set; }
        public virtual DbSet<CSConf> CSConfs { get; set; }
        
        #endregion
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // configures one-to-many relationship
            modelBuilder.Entity<MediaFile>()
                .HasRequired(r => r.MediaGalleries)
                .WithMany(s => s.iMediaFiles)
                .HasForeignKey(f => f.MediaGalleryId);

        }
    }
}