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

        public Organisateur? DeleteOrganisateur(int organisateur_Id)
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

        public IEnumerable<Organisateur?> GetAllOrganisateurs()
        {
            return _organisateurRepository.GetAllOrganisateurs();
        }

        public Organisateur? GetByIdOrganisateur(int organisateur_Id)
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

        public Organisateur? UpdateOrganisateur(string companyName, string businessNumber, int nUser_Id, string point, int organisateur_Id)
        {
            try
            {
                var UpdateOrganisateur = _organisateurRepository.UpdateOrganisateur(companyName, businessNumber, nUser_Id, point, organisateur_Id);
                return UpdateOrganisateur;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating organisator : {ex}");
            }
            return new Organisateur();
        }
    }
}
