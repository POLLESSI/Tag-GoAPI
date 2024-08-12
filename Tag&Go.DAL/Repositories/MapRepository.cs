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
    public class MapRepository : IMapRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public MapRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Create(Map map)
        {
            try
            {
                string sql = "INSERT INTO Map(DateCreation, MapUrl, Description) VALUES" +
                    "(@DateCreation, @MapUrl, @Description)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DateCreation", map.DateCreation);
                parameters.Add("@MapUrl", map.MapUrl);
                parameters.Add("@Description", map.Description);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating Map : {ex.ToString}");
            }
            return false;
        }

        public void CreateMap(Map map)
        {
            try
            {
                string sql = "INSERT INTO Map(DateCreation, MapUrl, Description)" +
                    "VALUES (@dateCreation, @mapUrl, @description)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@dateCreation", map.DateCreation);
                parameters.Add("@mapUrl", map.MapUrl);
                parameters.Add("@description", map.Description);
                _connection.Query<Map?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating new Map : {ex.ToString}");
            }
        }

        public Map? DeleteMap(int map_Id)
        {
            try
            {
                string sql = "DELETE FROM Map WHERE Map_Id = @map_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@map_Id", map_Id);
                return _connection.QueryFirst<Map?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Map : {ex.ToString}");
            }
            return null;
        }

        public IEnumerable<Map?> GetAllMaps()
        {
            string sql = "SELECT * FROM Map";
            return _connection.Query<Map?>(sql);
        }

        public Map? GetByIdMap(int map_Id)
        {
            try
            {
                string sql = "SELECT * FROM Map WHERE Map_Id = @map_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@map_Id", map_Id);
                return _connection.QueryFirst<Map?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Map : {ex.ToString}");
            }
            return null;
        }

        public Map? UpdateMap(int map_Id, DateTime dateCreation, string mapUrl, string description)
        {
            try
            {
                string sql = "UPDATE Map SET DateCreation = @dateCreation, MapUrl = @mapUrl, Description = @dateDescription WHERE Map_Id = @map_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@dateCreation", dateCreation);
                parameters.Add("@mapUrl", mapUrl);
                parameters.Add("description", description);
                return _connection.QueryFirst<Map?>(sql, parameters);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Updating Map : {ex}");
            }
            return new Map();
        }
    }
}
