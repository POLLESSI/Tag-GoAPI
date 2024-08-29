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

        public bool Create(Recompense recompense)
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
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding Recompense : {ex.ToString}");
            }
            return false;
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

        public Task<Recompense?> DeleteRecompense(int recompense_Id)
        {
            try
            {
                string sql = "DELETE FROM Recompense WHERE Recompense_Id = @recompense_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@recompense_Id", recompense_Id);
                return _connection.QueryFirstAsync<Recompense?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Recompense : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<Recompense?>> GetAllRecompenses()
        {
            string sql = "SELECT * FROM Recompense";
            return _connection.QueryAsync<Recompense?>(sql);
        }

        public Task<Recompense?> GetByIdRecompense(int recompense_Id)
        {
            try
            {
                string sql = "SELECT * FROM Recompense WHERE Recompense_Id = @recompense_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@recompense_Id", recompense_Id);
                return _connection.QueryFirstAsync<Recompense?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Recompense : {ex.ToString}");
            }
            return null;
        }

        public Task<Recompense?> UpdateRecompense(string definition, string point, string implication, string granted, int recompense_Id)
        {
            try
            {
                string sql = "UPDATE Recompense SET Definition = @definition, Point = @point, Implication = @implication, Granted = @granted WHERE Recompense_Id = @recompense_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@definition", definition);
                parameters.Add("@point", point);
                parameters.Add("@implication", implication);
                parameters.Add("@granted", granted);
                parameters.Add("@recompense_Id", recompense_Id);
                return _connection.QueryFirstAsync<Recompense?>(sql, parameters);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Recompense : {ex}");
            }
            return null;
        }
    }
}
