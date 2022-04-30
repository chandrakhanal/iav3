using IndianArmyWeb.Core.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;


namespace IndianArmyWeb.Core
{
    public interface IUnitOfWork : IDisposable
    {
        IMenuItemMasterRepository MenuItemMstrRepo { get; }
        IMenuUrlMasterRepository MenuUrlMstrRepo { get; }
        IPageContentRepository PageContentRepo { get; }
        IMenuRoleRepository MenuRoleRepo { get; }
        IMediaCategoryMasterRepository MediaCategoryMstrRepo { get; }
        INewsArticleRepository NewsArticleRepo { get; }
        IMediaGalleryRepository MediaGalleryRepo { get; }
       
        ICSConfRepository CSConfRepo { get; }
        int Complete();
        int Complete(Controller controller);
        void Commit();
        Task CommitAsync();
    }
}
