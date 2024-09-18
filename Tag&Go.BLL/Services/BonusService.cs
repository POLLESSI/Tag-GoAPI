using Tag_Go.BLL;
using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Repositories;
using Tag_Go.DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.SignalR.Client;
using Tag_Go.BLL.Interfaces;


namespace Tag_Go.BLL.Services
{
    public class BonusService : IBonusService
    {
    #nullable disable
        private readonly IBonusRepository _bonusRepository;

        public BonusService(IBonusRepository bonusRepository)
        {
            _bonusRepository = bonusRepository;
        }

        public bool Create(Bonus bonus)
        {
            try
            {
                return _bonusRepository.Create(bonus);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating bonus : {ex.ToString}");
            }
            return false;
        }

        public void CreateBonus(Bonus bonus)
        {
            try
            {
                _bonusRepository.CreateBonus(bonus);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateBonus : {ex.ToString}");
            }
        }

        public Task<Bonus?> DeleteBonus(int bonus_Id)
        {
            try
            {
                return _bonusRepository.DeleteBonus(bonus_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting bonus : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<Bonus?>> GetAllBonuss()
        {
            return _bonusRepository.GetAllBonuss();
        }

        public Task<Bonus?> GetByIdBonus(int bonus_Id)
        {
            try
            {
                return _bonusRepository.GetByIdBonus(bonus_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting bonus : {ex.ToString}");
            }
            return null;
        }

        public Task<Bonus?> UpdateBonus(Bonus bonus)
        {
            try
            {
                var UpdateBonus = _bonusRepository.UpdateBonus(bonus);
                return UpdateBonus;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating bonus : {ex}");
            }
            return null;
        }
    }
}
