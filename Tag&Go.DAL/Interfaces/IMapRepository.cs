﻿using Tag_Go.DAL.Entities;
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
        IEnumerable<Map?> GetAllMaps();
        Map? GetByIdMap(int map_Id);
        Map? DeleteMap(int map_Id);
        Map? UpdateMap(int map_Id, DateTime dateCreation, string mapUrl, string description);
    }
}
