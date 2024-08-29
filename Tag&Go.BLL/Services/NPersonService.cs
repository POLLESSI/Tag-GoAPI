using Tag_Go.BLL.Interfaces;
using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using Tag_Go.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;

namespace Tag_Go.BLL.Services
{
    public class NPersonService : INPersonService
    {
    #nullable disable
        private readonly INPersonRepository _nPersonRepository;

        public NPersonService(INPersonRepository nPersonRepository)
        {
            _nPersonRepository = nPersonRepository;
        }

        public bool Create(NPerson nPerson)
        {
            try
            {
                return _nPersonRepository.Create(nPerson);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating new person : {ex.ToString}");
            }
            return false;
        }

        public void CreatePerson(NPerson nPerson)
        {
            try
            {
                _nPersonRepository.CreatePerson(nPerson);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreatePerson : {ex.ToString}");
            }
        }

        public Task<NPerson?> DeleteNPerson(int nPerson_Id)
        {
            try
            {
                return _nPersonRepository.DeleteNPerson(nPerson_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting person : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<NPerson?>> GetAllNPersons()
        {
            return _nPersonRepository.GetAllNPersons();
        }

        public Task<NPerson?> GetByIdNPerson(int nPerson_Id)
        {
            try
            {
                return _nPersonRepository.GetByIdNPerson(nPerson_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting person : {ex.ToString}");
            }
            return null;
        }

        public Task<NPerson?> UpdateNPerson(string lastname, string firstname, string email, string address_Street, string address_Nbr, string postalCode, string address_City, string address_Country, string telephone, string gsm, int nPerson_Id)
        {
            try
            {
                var updatePerson = _nPersonRepository.UpdateNPerson(lastname, firstname, email, address_Street, address_Nbr, postalCode, address_City, address_Country, telephone, gsm, nPerson_Id);
                return updatePerson;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating person : {ex.Message}");
            }
            return null;
        }
    }
}
