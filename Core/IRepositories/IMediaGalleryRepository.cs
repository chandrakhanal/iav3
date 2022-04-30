using IndianArmyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndianArmyWeb.Core.IRepositories
{
    public interface IMediaGalleryRepository : IRepository<MediaGallery>
    {
        void UpdateRecord(MediaGallery objmediaGallery);
    }
}
