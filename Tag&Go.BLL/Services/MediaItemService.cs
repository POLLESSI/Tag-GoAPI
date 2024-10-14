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
using System.Security.Cryptography.X509Certificates;

namespace Tag_Go.BLL.Services
{
    public class MediaItemService : IMediaItemService
    {
#nullable disable
        private readonly IMediaItemRepository _mediaItemRepository;

        public MediaItemService(IMediaItemRepository mediaItemRepository)
        {
            _mediaItemRepository = mediaItemRepository;
        }

        public async Task<MediaItem> Create(MediaItem mediaItem)
        {
            try
            {
                return await _mediaItemRepository.Create(mediaItem);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating Media Item : {ex.ToString}");
                return null;
            }
        }

        public void CreateMediaItem(MediaItem mediaItem)
        {
            try
            {
                _mediaItemRepository.CreateMediaItem(mediaItem);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error createMediaItem : {ex.ToString}");
            }
        }

        public Task<MediaItem?> DeleteMediaItem(int mediaItem_Id)
        {
            try
            {
                return _mediaItemRepository.DeleteMediaItem(mediaItem_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Media Item : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<MediaItem?>> GetAllMediaItems(bool includeInactive = false)
        {
            try
            {
                return _mediaItemRepository.GetAllMediaItems();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error return Medias Items : {ex.Message}");
                return null;
            }
            
        }

        public Task<MediaItem?> GetByIdMediaItem(int mediaItem_Id)
        {
            try
            {
                return _mediaItemRepository.GetByIdMediaItem(mediaItem_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Media Item : {ex.ToString}");
            }
            return null;
        }

        public Task<MediaItem?> UpdateMediaItem(MediaItem mediaItem)
        {
            try
            {
                var updateMediaItem = _mediaItemRepository.UpdateMediaItem(mediaItem);
                return updateMediaItem;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Media Item : {ex.Message}");
            }
            return null;
        }
    }
}
