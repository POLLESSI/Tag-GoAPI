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
    public class NEvenementService : INEvenementService
    {
    #nullable disable
        private readonly INEvenementRepository _nEvenementRepository;

        public NEvenementService(INEvenementRepository nEvenementRepository)
        {
            _nEvenementRepository = nEvenementRepository;
        }

        public bool Create(NEvenement nEvenement)
        {
            try
            {
                return _nEvenementRepository.Create(nEvenement);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating New Event : {ex.ToString}");
            }
            return false;
        }

        public void CreateEvenement(NEvenement nEvenement)
        {
            try
            {
                _nEvenementRepository.CreateEvenement(nEvenement);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateEvent : {ex.ToString}");
            }
        }

        public NEvenement? DeleteNEvenement(int nEvenement_Id)
        {
            try
            {
                return _nEvenementRepository.DeleteNEvenement(nEvenement_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting event : {ex.ToString}");
            }
            return null;
        }

        public IEnumerable<NEvenement?> GetAllNEvenements()
        {
            return _nEvenementRepository.GetAllNEvenements();
        }

        public NEvenement? GetByIdNEvenement(int nEvenement_Id)
        {
            try
            {
                return _nEvenementRepository.GetByIdNEvenement(nEvenement_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting event : {ex.ToString}");
            }
            return null;
        }

        public NEvenement? UpdateNEvenement(DateTime nEvenementDate, string nEvenementName, string nEvenementDescription, string posLat, string posLong, string positif, int organisateur_Id, int nIcon_Id, int recompense_Id, int bonus_Id, int mediaItem_Id, int nEvenement_Id)
        {
            try
            {
                var UpdateNEvenement = _nEvenementRepository.UpdateNEvenement(nEvenementDate, nEvenementDescription, posLat, posLong, positif, organisateur_Id, nIcon_Id, recompense_Id, bonus_Id, mediaItem_Id, nEvenement_Id);
                return UpdateNEvenement;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating event : {ex}");
            }
            return new NEvenement();
        }
    }
}
