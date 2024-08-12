using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class ActivityHub : Hub
    {
    #nullable disable
        private static List<Activity> _activities = new List<Activity>();

        public ActivityHub()
        {
        }

        public async Task RefreshActivity()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewactivity");
            }
        }
        public async Task submitActivity(Activity activity)
        {
            if (Clients is not null)
                _activities.Add(activity);
            await Clients.All.SendAsync("ReceiveActivity", activity);
        }
        public async Task DeleteActivity(int activity_Id)
        {
            var activity = _activities.Find(a => a.Activity_Id == activity_Id);
            if (activity != null)
            {
                _activities.Remove(activity);
                await Clients.All.SendAsync("ActivityDeleted", activity_Id);
            }
        }
        public async Task GetAllActivities()
        {
            if (Clients is not null)
                await Clients.All.SendAsync("ReceiveAllActivities", _activities);
        }
        public async Task GetByIdActivity(int activity_Id)
        {
            var activity = _activities.Find(a => a.Activity_Id == activity_Id);
            await Clients.Caller.SendAsync("ReceiveActivity", activity);
        }
        public async Task UpdateActivity(Activity updateActivity)
        {
            var activity = _activities.Find(a => a.Activity_Id == updateActivity.Activity_Id);
            if (activity != null)
            {
                activity.ActivityName = updateActivity.ActivityName;
                activity.ActivityAddress = updateActivity.ActivityAddress;
                activity.ActivityDescription = updateActivity.ActivityDescription;
                activity.ComplementareInformation = updateActivity.ComplementareInformation;
                activity.PosLat = updateActivity.PosLat;
                activity.PosLong = updateActivity.PosLong;
                activity.Organisateur_Id = updateActivity.Organisateur_Id;
                await Clients.All.SendAsync("ReceiveActivity", activity);
            }
        }
    }
}
