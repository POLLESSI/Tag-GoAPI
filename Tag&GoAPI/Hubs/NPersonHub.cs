using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;

namespace Tag_GoAPI.Hubs
{
    public class NPersonHub : Hub
    {
    #nullable disable
        private static List<NPerson> _nPersons = new List<NPerson>();

        public NPersonHub()
        {
        }

        public async Task SubmitNPerson(NPerson nPerson)
        {
            if (Clients is not null)
            {
                _nPersons.Add(nPerson);
                await Clients.All.SendAsync("receiveNPerson", nPerson);
            }
        }
        public async Task RefreshPerson()
        {
            if (Clients is not null)
            {
                await Clients.All.SendAsync("notifynewnperson");
            }
        }
        public async Task DeleteNPerson(int nPerson_Id)
        {
            var nPerson = _nPersons.FirstOrDefault(np => np.NPerson_Id == nPerson_Id);
            if (nPerson is not null)
            {
                _nPersons.Remove(nPerson);
                await Clients.All.SendAsync("NPersonDeleted", nPerson_Id);
            }
        }
        public async Task GetAllNPersons()
        {
            if (Clients is not null)
                await Clients.Caller.SendAsync("ReceiveNPerson", _nPersons);
        }
        public async Task GetByIdNPerson(int nPerson_Id)
        {
            if (Clients is not null)
            {
                var nperson = _nPersons.FirstOrDefault(np => np.NPerson_Id == nPerson_Id);
                await Clients.Caller.SendAsync("ReceiveNPerson", nperson);
            }
        }
        public async Task UpdateNPerson(NPerson updatedNPerson)
        {
            var nperson = _nPersons.FirstOrDefault(np => np.NPerson_Id == updatedNPerson.NPerson_Id);
            if (Clients is not null)
            {
                nperson.Lastname = updatedNPerson.Lastname;
                nperson.Firstname = updatedNPerson.Firstname;
                nperson.Email = updatedNPerson.Email;
                nperson.Address_Street = updatedNPerson.Address_Street;
                nperson.Address_Nbr = updatedNPerson.Address_Nbr;
                nperson.PostalCode = updatedNPerson.PostalCode;
                nperson.Address_City = updatedNPerson.Address_City;
                nperson.Address_Country = updatedNPerson.Address_Country;
                nperson.Telephone = updatedNPerson.Telephone;
                nperson.Gsm = updatedNPerson.Gsm;

                await Clients.All.SendAsync("ReceiveNPerson", nperson);
            }
        }
    }
}
