using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.NIcon;

namespace Tag_Go.DAL.Interfaces
{
    public interface INIconRepository
    {
    #nullable disable
        Task<NIcon> Create(NIcon nIcon);
        void CreateIcon(NIcon nIcon);
        Task<IEnumerable<NIcon?>> GetAllNIcons(bool includeInactive = false);
        Task<NIcon?> GetByIdNIcon(int nIcon_Id);
        Task<NIcon?> DeleteNIcon(int nIcon_Id);
        Task<NIcon?> UpdateNIcon(NIcon nIcon);
    }
}
