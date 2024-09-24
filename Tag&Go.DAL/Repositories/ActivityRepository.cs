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
    public class ActivityRepository : IActivityRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public ActivityRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Create(Activity activity)
        {
            try
            {
                string sql = "INSERT INTO Activity (ActivityName, ActivityAddress, ActivityDescription, ComplementareInformation, PosLat, PosLong, Organisateur_Id) VALUES " +
                    "(@activityName, @activityAddress, @activityDescription, @complementareInformation, @posLat, @posLong, @organisateur_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@activityName", activity.ActivityName);
                parameters.Add("@activityAddress", activity.ActivityAddress);
                parameters.Add("@activityDescription", activity.ActivityDescription);
                parameters.Add("@complementareInformation", activity.ComplementareInformation);
                parameters.Add("@posLat", activity.PosLat);
                parameters.Add("@posLong", activity.PosLong);
                parameters.Add("@organisateur_Id", activity.Organisateur_Id);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Encoding New Activity : {ex.ToString}");
            }
            return false;
        }

        public void CreateActivity(Activity activity)
        {
            try
            {
                string sql = "INSERT INTO Activity (ActivityName, ActivityAddress, ActivityDescription, ComplementareInformation, PosLat, PosLong, Organisateur_Id)" +
                    "VALUES (@ActivityName, @ActivityAddress, @ActivityDescription, @ComplementareInformation, @PosLat, @PosLong, @Organisateur_id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@ActivityName", activity.ActivityName);
                parameters.Add("@ActivityAddress", activity.ActivityAddress);
                parameters.Add("@ComplementareInformation", activity.ComplementareInformation);
                parameters.Add("@PosLat", activity.PosLat);
                parameters.Add("@PosLong", activity.PosLong);
                parameters.Add("@Organisateur_Id", activity.Organisateur_Id);
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateActivity : {ex.ToString}");
            }
        }

        public async Task<Activity?> DeleteActivity(int activity_Id)
        {
            try
            {
                string sql = "SELECT * FROM Activity WHERE Activity_Id = @activity_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@activity_Id", activity_Id);

                var activity = await _connection.QueryFirstOrDefaultAsync<Activity>(sql, new { activity_Id });

                if (activity == null)
                {
                    return null;
                }

                string deleteSql = "DELETE FROM Activity WHERE Activity_Id = @activity_Id";

                await _connection.ExecuteAsync(deleteSql, parameters);

                return activity;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting activity : {ex.ToString}");
                return null;
            }
            
        }

        public async Task <IEnumerable<Activity?>> GetAllActivities()
        {
            string sql = "SELECT * FROM Activity";
            return await _connection.QueryAsync<Activity?>(sql);
        }

        public async Task<Activity?> GetByIdActivity(int activity_Id)
        {
            try
            {
                string sql = "SELECT * FROM Activity WHERE Activity_Id = @activity_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@activity_Id", activity_Id);

                var activity = await _connection.QueryFirstAsync<Activity?>(sql, parameters);

                return activity ;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Activity : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<Activity?> UpdateActivity(Activity activity)
        {
            try
            {
                string sql = @"
                    UPDATE Activity 
                    SET 
                        ActivityName = @activityName, 
                        ActivityAddress = @activityAddress, 
                        ActivityDescription = @activityDescription, 
                        ComplementareInformation = @complementareInformation, 
                        PosLat = @posLat, 
                        PosLong = @posLong, 
                        Organisateur_Id = @organisateur_Id 
                    WHERE 
                        Activity_Id = @activity_Id";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@activityName", activity.ActivityName);
                parameters.Add("@activityAddress", activity.ActivityAddress);
                parameters.Add("@activityDescription", activity.ActivityDescription);
                parameters.Add("@complementareInformation", activity.ComplementareInformation);
                parameters.Add("@posLat", activity.PosLat);
                parameters.Add("@posLong", activity.PosLong);
                parameters.Add("@organisateur_Id", activity.Organisateur_Id);
                parameters.Add("@activity_Id", activity.Activity_Id);

                await _connection.ExecuteAsync(sql, parameters);

                return activity;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating activity : {ex}");
                return null;
            }
            
        }
    }
}
