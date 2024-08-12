using Tag_GoAPI.DTOs;
using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class OrganisateurHub : Hub
    {
    #nullable disable
        private static List<Organisateur> _organisateurs = new List<Organisateur>();
        public OrganisateurHub()
        {
        }
        public async Task SubmitOrganisateur(Organisateur organisateur)
        {
            if (Clients is not null)
            {
                _organisateurs.Add(organisateur);
                await Clients.All.SendAsync("receiveOrganisateur");
            }
        }
        public async Task RefreshOrganisateur()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifyneworganisateur");
            }
        }
        public async Task DeleteOrganisateur(int organisateur_Id)
        {
            var organisateur = _organisateurs.FirstOrDefault(or => or.Organisateur_Id == organisateur_Id);
            if (Clients is not null)
                _organisateurs.Remove(organisateur);
            await Clients.All.SendAsync("organisateurDeleted", organisateur_Id);
        }
        public async Task GetAllOrganisateurs()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveAllOrganisateurs", _organisateurs);
        }

        public async Task GetByIdOrganisateur(int organisateur_Id)
        {
            var organisateur = _organisateurs.FirstOrDefault(or => or.Organisateur_Id == organisateur_Id);
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveOrganisateur", organisateur);
        }
        public async Task updateOrganisateur(Organisateur updateOrganisateur)
        {
            var organisateur = _organisateurs.Find(or => or.Organisateur_Id == updateOrganisateur.Organisateur_Id);
            if (organisateur is not null)
            {
                organisateur.CompanyName = updateOrganisateur.CompanyName;
                organisateur.BusinessNumber = updateOrganisateur.BusinessNumber;
                organisateur.NUser_Id = updateOrganisateur.NUser_Id;
                organisateur.Point = updateOrganisateur.Point;

                await Clients.All.SendAsync("ReceiveOrganisateur", organisateur);
            }
        }
    }
}
