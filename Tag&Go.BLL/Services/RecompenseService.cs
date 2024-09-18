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
    public class RecompenseService : IRecompenseService
    {
    #nullable disable
        private readonly IRecompenseRepository _recompenseRepository;

        public RecompenseService(IRecompenseRepository recompenseRepository)
        {
            _recompenseRepository = recompenseRepository;
        }

        public bool Create(Recompense recompense)
        {
            try
            {
                return _recompenseRepository.Create(recompense);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating recompense : {ex.ToString}");
            }
            return false;
        }

        public void CreateRecompense(Recompense recompense)
        {
            try
            {
                _recompenseRepository.CreateRecompense(recompense);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error createRecompense : {ex.ToString}");
            }
        }

        public Task<Recompense?> DeleteRecompense(int recompense_Id)
        {
            try
            {
                return _recompenseRepository.DeleteRecompense(recompense_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting recompense : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<Recompense?>> GetAllRecompenses()
        {
            return _recompenseRepository.GetAllRecompenses();
        }

        public Task<Recompense?> GetByIdRecompense(int recompense_Id)
        {
            try
            {
                return _recompenseRepository.GetByIdRecompense(recompense_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting event : {ex.ToString}");
            }
            return null;
        }

        public Task<Recompense?> UpdateRecompense(Recompense recompense)
        {
            try
            {
                var UpdateRecompense = _recompenseRepository.UpdateRecompense(recompense);
                return UpdateRecompense;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating recompense : {ex}");
            }
            return null;
        }
    }
}
