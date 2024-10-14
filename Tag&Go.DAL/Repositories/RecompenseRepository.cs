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
    public class RecompenseRepository : IRecompenseRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public RecompenseRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Recompense> Create(Recompense recompense)
        {
            try
            {
                string sql = "INSERT INTO Recompense (Definition, Point, Implication, Granted) VALUES " +
                    "(@Definition, @Point, @Implication, @Granted)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Definition", recompense.Definition);
                parameters.Add("@Point", recompense.Point);
                parameters.Add("@Implication", recompense.Implication);
                parameters.Add("@Granted", recompense.Granted);
                //return _connection.Execute(sql, parameters) > 0;
                var newId = _connection.QuerySingle<int>(sql, parameters);

                recompense.Recompense_Id = newId;

                return recompense;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding Recompense : {ex.ToString}");
                return null;
            }
        }

        public void CreateRecompense(Recompense recompense)
        {
            try
            {
                string sql = "INSERT INTO Recompense (Definition, Point, Implication, Granted) " +
                    "VALUES (@definition, @point, @implication, @granted)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@definition", recompense.Definition);
                parameters.Add("@point", recompense.Point);
                parameters.Add("@implication", recompense.Implication);
                parameters.Add("@granted", recompense.Granted);
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateRecompense : {ex.ToString}");
            }
        }

        public async Task<Recompense?> DeleteRecompense(int recompense_Id)
        {
            try
            {
                string sql = "SELECT * Recompense WHERE Recompense_Id = @recompense_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@recompense_Id", recompense_Id);

                var recompense = await _connection.QueryFirstOrDefaultAsync<Recompense?>(sql, new { recompense_Id });

                if (recompense == null)
                {
                    return null;
                }

                string deleteSql = "DELETE FROM Recompense WHERE Recompense_Id = @recompense_Id";

                await _connection.ExecuteAsync(deleteSql, parameters);
                return recompense;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Recompense : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<IEnumerable<Recompense?>> GetAllRecompenses(bool includeInactive = false)
        {
            string sql = includeInactive ? "SELECT * FROM Recompense": "SELECT * FROM Recompense WHERE Active = 1";
            var recompenses = await _connection.QueryAsync<Recompense?>(sql);
            return recompenses;
        }

        public async Task<Recompense?> GetByIdRecompense(int recompense_Id)
        {
            try
            {
                string sql = "SELECT * FROM Recompense WHERE Recompense_Id = @recompense_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@recompense_Id", recompense_Id);

                var recompense = await _connection.QueryFirstAsync<Recompense?>(sql, parameters);

                return recompense ?? null;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Recompense : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<Recompense?> UpdateRecompense(Recompense recompense)
        {
            try
            {
                string sql = @"
                    UPDATE Recompense 
                    SET 
                        Definition = @definition, 
                        Point = @point, 
                        Implication = @implication, 
                        Granted = @granted 
                    WHERE 
                        Recompense_Id = @recompense_Id";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@definition", recompense.Definition);
                parameters.Add("@point", recompense.Point);
                parameters.Add("@implication", recompense.Implication);
                parameters.Add("@granted", recompense.Granted);
                parameters.Add("@recompense_Id", recompense.Recompense_Id);

                await _connection.QueryFirstAsync<Recompense?>(sql, parameters);

                return recompense;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Recompense : {ex}");
                return null;
            }
            
        }
    }
}
