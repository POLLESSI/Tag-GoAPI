using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Map;

namespace Tag_Go.BLL.Interfaces
{
    public interface IMapService
    {
    #nullable disable
        Task<Map> Create(Map map);
        void CreateMap(Map map);
        Task<IEnumerable<Map?>> GetAllMaps(bool includeInactive = false);
        Task<Map?> GetByIdMap(int map_Id);
        Task<Map?> DeleteMap(int map_Id);
        Task<Map?> UpdateMap(Map map);
    }
}
