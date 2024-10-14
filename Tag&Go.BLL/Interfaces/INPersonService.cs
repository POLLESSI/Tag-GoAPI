﻿using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.NPerson;

namespace Tag_Go.BLL.Interfaces
{
    public interface INPersonService
    {
    #nullable disable
        Task<NPerson> Create(NPerson nPerson);
        void CreatePerson(NPerson nPerson);
        Task<IEnumerable<NPerson?>> GetAllNPersons(bool includeInactive = false);
        Task<NPerson?> GetByIdNPerson(int nPerson_Id);
        Task<NPerson?> DeleteNPerson(int nPerson_Id);
        Task<NPerson?> UpdateNPerson(NPerson nPerson);
    }
}
