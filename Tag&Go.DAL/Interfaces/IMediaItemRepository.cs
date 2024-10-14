using Tag_Go.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Tag_Go.DAL.Entities.MediaItem;

namespace Tag_Go.DAL.Interfaces
{
    public interface IMediaItemRepository
    {
    #nullable disable
        Task<MediaItem> Create(MediaItem mediaItem);
        void CreateMediaItem(MediaItem mediaItem);
        Task<IEnumerable<MediaItem?>> GetAllMediaItems(bool includeInactive = false);
        Task<MediaItem?> GetByIdMediaItem(int mediaItem_Id);
        Task<MediaItem?> DeleteMediaItem(int mediaItem_Id);
        Task<MediaItem?> UpdateMediaItem(MediaItem mediaItem);
    }
}
