﻿using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.NIcon;

namespace Tag_Go.BLL.Interfaces
{
    public interface INIconService
    {
        bool Create(NIcon nIcon);
        void CreateIcon(NIcon nIcon);
        Task<IEnumerable<NIcon?>> GetAllNIcons();
        Task<NIcon?> GetByIdNIcon(int nIcon_Id);
        Task<NIcon?> DeleteNIcon(int nIcon_Id);
        Task<NIcon?> UpdateNIcon(NIcon nIcon);
    }
}
