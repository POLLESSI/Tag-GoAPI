using Tag_Go.BLL.Interfaces;
using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using Tag_Go.DAL.Repositories;
using Tag_Go.BLL;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;

namespace Tag_Go.BLL.Services
{
    public class OrganisateurService : IOrganisateurService
    {
    #nullable disable
        private readonly IOrganisateurRepository _organisateurRepository;

        public OrganisateurService(IOrganisateurRepository organisateurRepository)
        {
            _organisateurRepository = organisateurRepository;
        }

        public bool Create(Organisateur organisateur)
        {
            try
            {
                return _organisateurRepository.Create(organisateur);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating organisateur : {ex.ToString}");
            }
            return false;
        }

        public void CreateOrganisateur(Organisateur organisateur)
        {
            try
            {
                _organisateurRepository.CreateOrganisateur(organisateur);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error createOrganisateur : {ex.ToString}");
            }
        }

        public Task<Organisateur?> DeleteOrganisateur(int organisateur_Id)
        {
            try
            {
                return _organisateurRepository.DeleteOrganisateur(organisateur_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting organisator : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<Organisateur?>> GetAllOrganisateurs()
        {
            return _organisateurRepository.GetAllOrganisateurs();
        }

        public Task<Organisateur?> GetByIdOrganisateur(int organisateur_Id)
        {
            try
            {
                return _organisateurRepository.GetByIdOrganisateur(organisateur_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting organisator : {ex.ToString}");
            }
            return null;
        }

        public Task<Organisateur?> UpdateOrganisateur(Organisateur organisateur)
        {
            try
            {
                var updateOrganisateur = _organisateurRepository.UpdateOrganisateur(organisateur);
                return updateOrganisateur;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating organisator : {ex}");
            }
            return null;
        }
    }
}
