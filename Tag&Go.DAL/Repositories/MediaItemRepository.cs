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

        public bool Create(MediaItem mediaItem)
        {
            try
            {
                string sql = "INSERT INTO MediaItem (MediaType, UrlItem, Description) VALUES " +
                    "(@MediaType, @UrlItem, @Description)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MediaType", mediaItem.MediaType);
                parameters.Add("@UrlItem", mediaItem.UrlItem);
                parameters.Add("@Description", mediaItem.Description);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Encoding New Media Item : {ex.ToString}");
            }
            return false;
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

        public Task<MediaItem?> DeleteMediaItem(int mediaItem_Id)
        {
            try
            {
                string sql = "DELETE FROM MediaItem WHERE MediaItem_Id = @mediaItem_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@mediaItem_Id", mediaItem_Id);
                return _connection.QueryFirstAsync<MediaItem?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Media Item : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<MediaItem?>> GetAllMediaItems()
        {
            string sql = "SELECT * FROM MediaItem";
            return _connection.QueryAsync<MediaItem?>(sql);
        }

        public Task<MediaItem?> GetByIdMediaItem(int mediaItem_Id)
        {
            try
            {
                string sql = "SELECT * FROM MediaItem WHERE MediaItem_Id = @mediaItem_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@mediaItem_Id", mediaItem_Id);
                return _connection.QueryFirstAsync<MediaItem?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Media Item : {ex.ToString}");
            }
            return null;
        }

        public Task<MediaItem?> UpdateMediaItem(int mediaItem_Id, string mediaType, string urlItem, string description)
        {
            try
            {
                string sql = "UPDATE MediaItem SET MediaType = @mediaType, UrlItem = @urlItem, Description = @description WHERE MediaItem_Id = @mediaItem_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@mediaType", mediaType);
                parameters.Add("@urlItem", urlItem);
                parameters.Add("description", description);
                return _connection.QueryFirstAsync<MediaItem?>(sql, parameters);
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
