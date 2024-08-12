using System;
using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tag_Go.DAL.Repositories
{
    public class BonusRepository : IBonusRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public BonusRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Create(Bonus bonus)
        {
            try
            {
                string sql = "INSERT INTO Bonus (BonusType, BonusDescription, Application, Granted) VALUES" +
                    "(@BonusType, @BonusDescription, @Application, @Granted)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@BonusType", bonus.BonusType);
                parameters.Add("@BonusDescription", bonus.BonusDescription);
                parameters.Add("@Application", bonus.Application);
                parameters.Add("@Granted", bonus.Granted);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding Bonus : {ex.ToString}");
            }
            return false;
        }

        public void CreateBonus(Bonus bonus)
        {
            try
            {
                string sql = "INSERT INTO Bonus (BonusType, BonusDescription, Application, Granted) VALUES" +
                    "(@bonusType, @bonusDescription, @application, @granted)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@bonusType", bonus.BonusType);
                parameters.Add("@bonusDescription", bonus.BonusDescription);
                parameters.Add("@applications", bonus.Application);
                parameters.Add("@granted", bonus.Granted);
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateBonus : {ex.ToString}");
            }
        }

        public Bonus? DeleteBonus(int bonus_Id)
        {
            try
            {
                string sql = "DELETE FROM Bonus WHERE Bonus_Id = @bonus_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@bonus_Id", bonus_Id);
                return _connection.QueryFirst<Bonus?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Bonus : {ex.ToString}");
            }
            return null;
        }

        public IEnumerable<Bonus?> GetAllBonuss()
        {
            string sql = "SELECT * FROM Bonus";
            return _connection.Query<Bonus?>(sql);
        }

        public Bonus? GetByIdBonus(int bonus_Id)
        {
            try
            {
                string sql = "SELECT * FROM Bonus WHERE Bonus_Id = @bonus_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@bonus_Id", bonus_Id);
                return _connection.QueryFirst<Bonus?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Bonus : {ex.ToString}");
            }
            return null;
        }

        public Bonus? UpdateBonus(int bonus_Id, string bonusType, string bonusDescription, string application, string granted)
        {
            try
            {
                string sql = "UPDATE Bonus SET BonusType = @bonusType, BonusDescription = @bonusDescription, Application = @application, Granted = @granted WHERE Bonus_Id = @bonus_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@bonusType", bonusType);
                parameters.Add("@bonusDescription", bonusDescription);
                parameters.Add("@application", application);
                parameters.Add("@granted", granted);
                return _connection.QueryFirst<Bonus?>(sql, parameters);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Bonus : {ex}");
            }
            return new Bonus();
        }
    }
}
