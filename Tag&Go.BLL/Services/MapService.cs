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
    public class MapService : IMapService
    {
    #nullable disable
        private readonly IMapRepository _mapRepository;

        public MapService(IMapRepository mapRepository)
        {
            _mapRepository = mapRepository;
        }

        public async Task<Map> Create(Map map)
        {
            try
            {
                return await _mapRepository.Create(map);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating map : {ex.ToString}");
                return null;
            }
            
        }

        public void CreateMap(Map map)
        {
            try
            {
                _mapRepository.CreateMap(map);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateMap : {ex.ToString}");
            }
        }

        public Task<Map?> DeleteMap(int map_Id)
        {
            try
            {
                return _mapRepository.DeleteMap(map_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting map : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<Map?>> GetAllMaps(bool includeInactive = false)
        {
            try
            {
                return _mapRepository.GetAllMaps();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error return maps : {ex.Message}");
                return null;
            }
            
        }

        public Task<Map?> GetByIdMap(int map_Id)
        {
            try
            {
                return _mapRepository.GetByIdMap(map_Id);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting map : {ex.ToString}");
            }
            return null;
        }

        public Task<Map?> UpdateMap(Map map)
        {
            try
            {
                var updateMap = _mapRepository.UpdateMap(map);
                return updateMap;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating map : {ex}");
            }
            return null;
        }
    }
}
