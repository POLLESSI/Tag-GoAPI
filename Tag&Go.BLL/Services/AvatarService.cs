using Tag_Go.BLL.Interfaces;
using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Repositories;
using Tag_Go.BLL;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using Tag_Go.DAL.Interfaces;

namespace Tag_Go.BLL.Services
{
    public class AvatarService : IAvatarService
    {
    #nullable disable
        private readonly IAvatarRepository _avatarRepository;

        public AvatarService(IAvatarRepository avatarRepository)
        {
            _avatarRepository = avatarRepository;
        }

        public bool Create(Avatar avatar)
        {
            try
            {
                return _avatarRepository.Create(avatar);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating avatar : {ex.ToString}");
            }
            return false;
        }

        public void CreateAvatar(Avatar avatar)
        {
            try
            {
                _avatarRepository.CreateAvatar(avatar);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateAddres : {ex.ToString}");
            }
        }

        public Avatar? DeleteAvatar(int avatar_Id)
        {
            try
            {
                return _avatarRepository.DeleteAvatar(avatar_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting avatar : {ex.ToString}");
            }
            return null;
        }

        public IEnumerable<Avatar?> GetAllAvatars()
        {
            return _avatarRepository.GetAllAvatars();
        }

        public Avatar? GetByIdAvatar(int avatar_Id)
        {
            try
            {
                return _avatarRepository.GetByIdAvatar(avatar_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Avatar : {ex.ToString}");
            }
            return null;
        }

        public Avatar? UpdateAvatar(int avatar_Id, string avatarName, string avatarUrl, string description)
        {
            try
            {
                var UpdateAvatar = _avatarRepository.UpdateAvatar(avatar_Id, avatarName, avatarUrl, description);
                return UpdateAvatar;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating avatar : {ex}");
            }
            return new Avatar();
        }
    }
}
