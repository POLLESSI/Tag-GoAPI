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
    public class NIconService : INIconService
    {
    #nullable disable
        private readonly INIconRepository _nIconRepository;

        public NIconService(INIconRepository nIconRepository)
        {
            _nIconRepository = nIconRepository;
        }

        public bool Create(NIcon nIcon)
        {
            try
            {
                return _nIconRepository.Create(nIcon);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating Icon : {ex.ToString}");
            }
            return false;
        }

        public void CreateIcon(NIcon nIcon)
        {
            try
            {
                _nIconRepository.CreateIcon(nIcon);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error createIcon : {ex.ToString}");
            }
        }

        public NIcon? DeleteNIcon(int nIcon_Id)
        {
            try
            {
                return _nIconRepository.DeleteNIcon(nIcon_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Icon : {ex.ToString}");
            }
            return null;
        }

        public IEnumerable<NIcon?> GetAllNIcons()
        {
            return _nIconRepository.GetAllNIcons();
        }

        public NIcon? GetByIdNIcon(int nIcon_Id)
        {
            try
            {
                return _nIconRepository.GetByIdNIcon(nIcon_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Icon : {ex.ToString}");
            }
            return null;
        }

        public NIcon? UpdateNIcon(string nIconName, string nIconDescription, string nIconUrl, int nIcon_Id)
        {
            try
            {
                var UpdateNIcon = _nIconRepository.UpdateNIcon(nIconName, nIconDescription, nIconUrl, nIcon_Id);
                return UpdateNIcon;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating icon : {ex}");
            }
            return new NIcon();
        }
    }
}
