using Tag_Go.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.MediaItem;

namespace Tag_Go.BLL.Interfaces
{
    public interface IMediaItemService
    {
    #nullable disable
        bool Create(MediaItem mediaItem);
        void CreateMediaItem(MediaItem mediaItem);
        Task<IEnumerable<MediaItem?>> GetAllMediaItems();
        Task<MediaItem?> GetByIdMediaItem(int mediaItem_Id);
        Task<MediaItem?> DeleteMediaItem(int mediaItem_Id);
        Task<MediaItem?> UpdateMediaItem(int mediaItem_Id, string mediaType, string urlItem, string description);
    }
}
