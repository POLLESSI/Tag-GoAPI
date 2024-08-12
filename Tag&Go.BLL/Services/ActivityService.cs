using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Repositories;
using Tag_Go.BLL;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Tag_Go.DAL.Interfaces;
using Tag_Go.BLL.Interfaces;

namespace Tag_Go.BLL.Services
{
    public class ActivityService : IActivityService
    {
    #nullable disable
        private readonly IActivityRepository _activityRepository;

        public ActivityService(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        
        public bool Create(Activity activity)
        {
            try
            {
                return _activityRepository.Create(activity);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating activity: {ex.ToString}");
            }
            return false;
        }

        

        public void CreateActivity(Activity activity)
        {
            try
            {
                _activityRepository.CreateActivity(activity);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateActivity: {ex.ToString}");
            }
        }

        
        public Activity? DeleteActivity(int activity_Id)
        {
            try
            {
                return _activityRepository.DeleteActivity(activity_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting activity: {ex.ToString}");
            }
            return null;
        }

        public IEnumerable<Activity?> GetAllActivities()
        {
            return _activityRepository.GetAllActivities();
        }

        public Activity? GetByIdActivity(int activity_Id)
        {
            try
            {
                return _activityRepository.GetByIdActivity(activity_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Activity: {ex.ToString}");
            }
            return null;
        }

        public Activity? UpdateActivity(int activity_Id, string activityName, string activityAddress, string activityDescription, string complementareInformation, string posLat, string posLong, int organisateur_Id)
        {
            try
            {
                var UpdateActivity = _activityRepository.UpdateActivity(activity_Id, activityName, activityAddress, activityDescription, complementareInformation, posLat, posLong, organisateur_Id);
                return UpdateActivity;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating activity : {ex}");
            }
            return new Activity();
        }
    }
}
