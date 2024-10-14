using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.Activity;

namespace Tag_Go.BLL.Interfaces
{
    public interface IActivityService
    {
#nullable disable
        Task<Activity> Create(Activity activity);
        void CreateActivity(Activity activity);
        Task <IEnumerable<Activity?>> GetAllActivities(bool includeInactive = false);
        Task<Activity?> GetByIdActivity(int activity_Id);
        Task<Activity?> DeleteActivity(int activity_Id);
        Task<Activity?> UpdateActivity(Activity activity);
    }
}
