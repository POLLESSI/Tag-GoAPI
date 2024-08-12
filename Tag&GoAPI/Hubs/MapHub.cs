using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class MapHub : Hub
    {
    #nullable disable
        private static List<Map> _maps = new List<Map>();

        public MapHub()
        {
        }

        public async Task SubmitMap(Map map)
        {
            if (Clients is not null)
            {
                _maps.Add(map);
                await Clients.All.SendAsync("receivemap", map);
            }
        }
        public async Task RefreshMap()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewmap");
            }
        }

        public async Task DeleteMap(int map_Id)
        {
            var map = _maps.Find(mp => mp.Map_Id == map_Id);
            if (Clients is not null)
            {
                _maps.Remove(map);
                await Clients.All.SendAsync("MapDeleted", map_Id);
            }
        }
        public async Task GetAllMaps()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllMaps", _maps);
        }
        public async Task GetByIdMap(int map_Id)
        {
            if (Clients is not null)
            {
                var map = _maps.Find(mp => mp.Map_Id == map_Id);
                await Clients.Caller.SendAsync("ReceiveMap", map);
            }
        }
        public async Task updateMap(Map updateMap)
        {
            var map = _maps.Find(ma => ma.Map_Id == updateMap.Map_Id);

            if (Clients is not null)
            {
                map.DateCreation = updateMap.DateCreation;
                map.MapUrl = updateMap.MapUrl;
                map.Description = updateMap.Description;
                await Clients.All.SendAsync("ReceiveMap", map);
            }
        }
    }
}
