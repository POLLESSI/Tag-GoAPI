using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Map;

namespace Tag_Go.DAL.Interfaces
{
    public interface IMapRepository
    {
    #nullable disable
        bool Create(Map map);
        void CreateMap(Map map);
        Task<IEnumerable<Map?>> GetAllMaps();
        Task<Map?> GetByIdMap(int map_Id);
        Task<Map?> DeleteMap(int map_Id);
        Task<Map?> UpdateMap(int map_Id, DateTime dateCreation, string mapUrl, string description);
    }
}
