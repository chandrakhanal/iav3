using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IndianArmyWeb.Infrastructure.Helpers.Menu
{
    public interface ISiteLink
    {
        int Id { get; }
        int ParentId { get; }
        string Text { get; }
        string Url { get; }
        bool OpenInNewWindow { get; }
        int SortOrder { get; }
    }
}
