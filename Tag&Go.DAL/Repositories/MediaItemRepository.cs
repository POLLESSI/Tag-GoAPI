using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tag_Go.DAL.Repositories
{
    public class MediaItemRepository : IMediaItemRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public MediaItemRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<MediaItem> Create(MediaItem mediaItem)
        {
            try
            {
                string sql = "INSERT INTO MediaItem (MediaType, UrlItem, Description) VALUES " +
                    "(@MediaType, @UrlItem, @Description)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MediaType", mediaItem.MediaType);
                parameters.Add("@UrlItem", mediaItem.UrlItem);
                parameters.Add("@Description", mediaItem.Description);
                //return _connection.Execute(sql, parameters) > 0;
                var newId = _connection.QuerySingle<int>(sql, parameters);

                mediaItem.MediaItem_Id = newId;

                return mediaItem;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Encoding New Media Item : {ex.ToString}");
                return null;
            }
        }

        public void CreateMediaItem(MediaItem mediaItem)
        {
            try
            {
                string sql = "INSERT INTO MediaItem (MediaType, UrlItem, Decription)" +
                    "VALUES (@mediaType, @urlItem, @description)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@mediaType", mediaItem.MediaType);
                parameters.Add("@urlItem", mediaItem.UrlItem);
                parameters.Add("@description", mediaItem.Description);
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Create New media Item : {ex.ToString}");
            }
        }

        public async Task<MediaItem?> DeleteMediaItem(int mediaItem_Id)
        {
            try
            {
                string sql = "SELECT * FROM MediaItem WHERE MediaItem_Id = @mediaItem_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@mediaItem_Id", mediaItem_Id);

                var mediaItem = await _connection.QueryFirstOrDefaultAsync<MediaItem?>(sql, new { mediaItem_Id });

                if (mediaItem == null) 
                {
                    return null;
                }

                string deleteSql = "DELETE FROM MediaItem WHERE MediaItem_Id = @mediaItem_Id";

                await _connection.ExecuteAsync(deleteSql, parameters);

                return mediaItem;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Media Item : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<IEnumerable<MediaItem?>> GetAllMediaItems(bool includeInactive = false)
        {
            try
            {
                string sql = includeInactive ? "SELECT * FROM MediaItem" : "SELECT * FROM MediaItem WHERE Active = 1";
                var mediaItems = await _connection.QueryAsync<MediaItem?>(sql);
                return mediaItems;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving medias items: {ex.Message}");
                return Enumerable.Empty<MediaItem>();
            }
            
        }

        public async Task<MediaItem?> GetByIdMediaItem(int mediaItem_Id)
        {
            try
            {
                string sql = "SELECT * FROM MediaItem WHERE MediaItem_Id = @mediaItem_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@mediaItem_Id", mediaItem_Id);

                var mediaItem = await _connection.QueryFirstAsync<MediaItem?>(sql, parameters);

                return mediaItem ?? new MediaItem();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Media Item : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<MediaItem?> UpdateMediaItem(MediaItem mediaItem)
        {
            try
            {
                string sql = @"
                    UPDATE MediaItem 
                    SET 
                        MediaType = @mediaType, 
                        UrlItem = @urlItem, 
                        Description = @description 
                    WHERE 
                        MediaItem_Id = @mediaItem_Id";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@mediaType", mediaItem.MediaType);
                parameters.Add("@urlItem", mediaItem.UrlItem);
                parameters.Add("@description", mediaItem.Description);
                parameters.Add("@mediaItem_Id", mediaItem.MediaItem_Id);

                await _connection.QueryFirstAsync<MediaItem?>(sql, parameters);

                return mediaItem;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Media Item : {ex}");
            }
            return null;
        }
    }
}
