﻿using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.NEvenement;

namespace Tag_Go.BLL.Interfaces
{
    public interface INEvenementService
    {
    #nullable disable
        Task<NEvenement> Create(NEvenement nEvenement);
        void CreateEvenement(NEvenement nEvenement);
        Task<IEnumerable<NEvenement?>> GetAllNEvenements(bool includeInactive = false);
        Task<NEvenement?> GetByIdNEvenement(int nEvenement_Id);
        Task<NEvenement?> DeleteNEvenement(int nEvenement_Id);
        Task<NEvenement?> UpdateNEvenement(NEvenement nEvenement);
    }
}
