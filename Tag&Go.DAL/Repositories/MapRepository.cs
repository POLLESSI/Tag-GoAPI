﻿using Tag_Go.DAL.Entities;
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

        public async Task <Map> Create(Map map)
        {
            try
            {
                string sql = "INSERT INTO Map(DateCreation, MapUrl, Description) VALUES" +
                    "(@DateCreation, @MapUrl, @Description)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@DateCreation", map.DateCreation);
                parameters.Add("@MapUrl", map.MapUrl);
                parameters.Add("@Description", map.Description);
                //return _connection.Execute(sql, parameters) > 0;
                var newId = _connection.QuerySingle<int>(sql, parameters);
                map.Map_Id = newId;

                return map;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating Map : {ex.ToString}");
                return null;
            }
            
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

        public async Task<Map?> DeleteMap(int map_Id)
        {
            try
            {
                string sql = "SELECT * FROM Map WHERE Map_Id = @map_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@map_Id", map_Id);

                var map = await _connection.QueryFirstOrDefaultAsync<Map?>(sql, new { map_Id });

                if (map == null)
                {
                    return null;
                }

                string deleteSql = "DELETE FROM Map WHERE Map_Id = @map_Id";

                await _connection.ExecuteAsync(deleteSql, parameters);

                return map;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Map : {ex.ToString}");
                return null;
            }
        }

        public async Task<IEnumerable<Map?>> GetAllMaps(bool includeInactive = false)
        {
            try
            {
                string sql = includeInactive ? "SELECT * FROM Map": "SELECT * FROM Map WHERE Active = 1";

                var maps = await _connection.QueryAsync<Map?>(sql);
                return maps;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving maps: {ex.Message}");
                return Enumerable.Empty<Map>();
            }
            
        }

        public async Task<Map?> GetByIdMap(int map_Id)
        {
            try
            {
                string sql = "SELECT * FROM Map WHERE Map_Id = @map_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@map_Id", map_Id);

                var map = await _connection.QueryFirstAsync<Map?>(sql, parameters);

                return map ?? new Map();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Map : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<Map?> UpdateMap(Map map)
        {
            try
            {
                string sql = @"
                    UPDATE Map  
                    SET 
                        DateCreation = @dateCreation, 
                        MapUrl = @mapUrl,   
                        Description = @description 
                    WHERE 
                        Map_Id = @map_Id";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@dateCreation", map.DateCreation);
                parameters.Add("@mapUrl", map.MapUrl);
                parameters.Add("@description", map.Description);
                parameters.Add("@map_Id", map.Map_Id);

                await _connection.QueryFirstAsync<Map?>(sql, parameters);

                return map;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Updating Map : {ex}");
                return null;
            }
            
        }
    }
}
