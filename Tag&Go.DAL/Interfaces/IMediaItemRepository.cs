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
        bool Create(MediaItem mediaItem);
        void CreateMediaItem(MediaItem mediaItem);
        IEnumerable<MediaItem?> GetAllMediaItems();
        MediaItem? GetByIdMediaItem(int mediaItem_Id);
        MediaItem? DeleteMediaItem(int mediaItem_Id);
        MediaItem? UpdateMediaItem(int mediaItem_Id, string mediaType, string urlItem, string description);
    }
}
