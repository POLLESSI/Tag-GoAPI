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
        bool Create(Activity activity);
        void CreateActivity(Activity activity);
        IEnumerable<Activity?> GetAllActivities();
        Activity? GetByIdActivity(int activity_Id);
        Activity? DeleteActivity(int activity_Id);
        Activity? UpdateActivity(int activity_Id, string activityName, string activityAddress, string activityDescription, string complementareInformation, string posLat, string posLong, int organisateur_Id);
    }
}
