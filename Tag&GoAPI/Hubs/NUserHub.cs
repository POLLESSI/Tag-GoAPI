using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class NUserHub : Hub
    {
    #nullable disable
        private static List<NUser> _nUsers = new List<NUser>();

        public NUserHub()
        {
        }

        public async Task SubmitNUser(NUser nUser)
        {
            if (Clients is not null)
            {
                _nUsers.Add(nUser);
                await Clients.All.SendAsync("ReceiveNUserUpdate", nUser);
            }
        }
        public async Task RefreshNUser()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewnuser");
            }
        }
        public async Task DeleteNUser(int nUser_Id)
        {
            var nuser = _nUsers.Find(nu => nu.NUser_Id == nUser_Id);
            if (Clients is not null)
            {
                _nUsers.Remove(nuser);
                await Clients.All.SendAsync("NUserDeleted", nUser_Id);
            }
        }
        public async Task GetAllNUsers()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllNUsers", _nUsers);
        }
        public async Task GetByIdNUser(int nUser_Id)
        {
            var nuser = _nUsers.Find(nu => nu.NUser_Id == nUser_Id);

            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveNUser", nuser);
        }
        public async Task UpdateNUser(NUser updateNUser)
        {
            var nuser = _nUsers.FirstOrDefault(nu => nu.NUser_Id == updateNUser.NUser_Id);
            if (Clients is not null)
            {
                nuser.Email = updateNUser.Email;
                nuser.Pwd = updateNUser.Pwd;
                nuser.NPerson_Id = updateNUser.NPerson_Id;
                nuser.Role_Id = updateNUser.Role_Id;
                nuser.Avatar_Id = updateNUser.Avatar_Id;
                nuser.Point = updateNUser.Point;

                await Clients.All.SendAsync("ReceiveNUser", nuser);
            }
        }
    }
}
