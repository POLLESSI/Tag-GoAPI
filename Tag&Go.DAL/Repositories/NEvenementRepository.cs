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
    public class NEvenementRepository : INEvenementRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public NEvenementRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Create(NEvenement nEvenement)
        {
            try
            {
                string sql = "INSERT INTO NEvenement (NEvenementDate, NEvenementName, NEvenementDescription, PosLat, PosLong, Positif, Organisateur_Id, NIcon_Id, Recompense_Id, Bonus_Id, MediaItem_Id) VALUES " +
                    "(@NEvenementDate, @NEvenementName, @NEvenementDescription, @PosLat, @PosLong, @Positif, @Organisateur_Id, @NIcon_Id, @Recompense_Id, @Bonus_Id, @MediaItem_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NEvenementDate", nEvenement.NEvenementDate);
                parameters.Add("@NEvenementName", nEvenement.NEvenementName);
                parameters.Add("@NEvenementDescription", nEvenement.NEvenementDescription);
                parameters.Add("@PosLat", nEvenement.PosLat);
                parameters.Add("@PosLong", nEvenement.PosLong);
                parameters.Add("@Positif", nEvenement.Positif);
                parameters.Add("@Organisateur_Id", nEvenement.Organisateur_Id);
                parameters.Add("@NIcon_Id", nEvenement.NIcon_Id);
                parameters.Add("@Recompense_Id", nEvenement.Recompense_Id);
                parameters.Add("@Bonus_Id", nEvenement.Bonus_Id);
                parameters.Add("@MediaItem_Id", nEvenement.MediaItem_Id);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error enconding New Event : {ex.ToString}");
            }
            return false;
        }

        public void CreateEvenement(NEvenement nEvenement)
        {
            try
            {
                string sql = "INSERT INTO NEvenement (NEvenementDate, NEvenementName, NEvenementDescription, PosLat, PosLong, Positif, Organisateur_Id, NIcon_Id, Recompense_Id, Bonus_Id, MediaItem_Id)" +
                    "VALUES (@nEvenementDate, @nEvenementName, @nEvenementDescription, @posLat, @posLong, @positif, @organisateur_Id, @nIcon_Id, @recompense_Id, @bonus_id, @mediaItem_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nEvenementDate", nEvenement.NEvenementDate);
                parameters.Add("@nEvenementName", nEvenement.NEvenementName);
                parameters.Add("@nEvenementDescription", nEvenement.NEvenementDescription);
                parameters.Add("@posLat", nEvenement.PosLat);
                parameters.Add("@posLong", nEvenement.PosLong);
                parameters.Add("@positif", nEvenement.Positif);
                parameters.Add("@organisateur_Id", nEvenement.Organisateur_Id);
                parameters.Add("@nIcon_Id", nEvenement.NIcon_Id);
                parameters.Add("@recompense_Id", nEvenement.Recompense_Id);
                parameters.Add("@bonus_Id", nEvenement.Bonus_Id);
                parameters.Add("@mediaItem", nEvenement.MediaItem_Id);
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateNEvent : {ex.ToString}");
            }
        }

        public Task<NEvenement?> DeleteNEvenement(int nEvenement_Id)
        {
            try
            {
                string sql = "DELETE FROM NEvenement WHERE NEvenement_Id = @nEvenement_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nEvenement_Id", nEvenement_Id);
                return _connection.QueryFirstAsync<NEvenement?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting evenement : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<NEvenement?>> GetAllNEvenements()
        {
            string sql = "SELECT * FROM NEvenement";
            return _connection.QueryAsync<NEvenement?>(sql);
        }

        public Task<NEvenement?> GetByIdNEvenement(int nEvenement_Id)
        {
            try
            {
                string sql = "SELECT * FROM NEvenement WHERE NEvenement_Id = @nEvenement_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nEvenement_Id", nEvenement_Id);
                return _connection.QueryFirstAsync<NEvenement?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Event : {ex.ToString}");
            }
            return null;
        }

        public Task<NEvenement?> UpdateNEvenement(NEvenement nEvenement)
        {
            try
            {
                string sql = "UPDATE NEvenement SET NEvenementDate = @nEvenementDate, NEvenementDescription = @nEvenementDescription, PosLat = @posLat, PosLong = @posLong, Positif = @positif, Organisateur_Id = @organisateur_Id, NIcon_Id = @nIcon_Id, Recompense_Id = @recompense_Id, Bonus_Id = @bonus_Id, MediaItem_Id = @mediaItem_Id WHERE NEvenement_Id = @nEvenement_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nEvenementDate", nEvenement.NEvenementDate);
                parameters.Add("@nEvenementDescription", nEvenement.NEvenementDescription);
                parameters.Add("@posLat", nEvenement.PosLat);
                parameters.Add("@posLong", nEvenement.PosLong);
                parameters.Add("@positif", nEvenement.Positif);
                parameters.Add("@organisateur_Id", nEvenement.Organisateur_Id);
                parameters.Add("@nIon_Id", nEvenement.NIcon_Id);
                parameters.Add("@recompense_Id", nEvenement.Recompense_Id);
                parameters.Add("@bonus_Id", nEvenement.Bonus_Id);
                parameters.Add("@mediaItem", nEvenement.MediaItem_Id);
                parameters.Add("@nEvenement_Id", nEvenement.NEvenement_Id);

                return _connection.QueryFirstAsync<NEvenement?>(sql, parameters);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating event : {ex}");
            }
            return null;
        }
    }
}
