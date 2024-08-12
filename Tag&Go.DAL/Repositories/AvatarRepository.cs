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
    public class AvatarRepository : IAvatarRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public AvatarRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Create(Avatar avatar)
        {
            try
            {
                string sql = "INSERT INTO Avatar (AvatarName, AvatarUrl, Description) VALUES " +
                    "(@avatarName, @avatarUrl, @description)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@avatarName", avatar.AvatarName);
                parameters.Add("@avatarUrl", avatar.AvatarUrl);
                parameters.Add("@description", avatar.Description);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Encoding New Avatar : {ex.ToString}");
            }
            return false;
        }

        public void CreateAvatar(Avatar avatar)
        {
            try
            {
                string sql = "INSERT INTO Avatar (AvatarName, AvatarUrl, Description)" +
                    "VALUES (@AvatarName, @AvatarUrl, @Description)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@AvatarName", avatar.AvatarName);
                parameters.Add("@AvatarUrl", avatar.AvatarUrl);
                parameters.Add("@Description", avatar.Description);
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error createAvatar : {ex.ToString}");
            }
        }

        public Avatar? DeleteAvatar(int avatar_Id)
        {
            try
            {
                string sql = "DELETE FROM Avatar WHERE Avatar_Id = @avatar_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@avatar_Id", avatar_Id);
                return _connection.QueryFirst<Avatar?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting avatar : {ex.ToString}");
            }
            return null;
        }

        public IEnumerable<Avatar?> GetAllAvatars()
        {
            string sql = "SELECT * FROM Avatar";
            return _connection.Query<Avatar?>(sql);
        }

        public Avatar? GetByIdAvatar(int avatar_Id)
        {
            try
            {
                string sql = "SELECT * FROM Avatar WHERE Avatar_Id = @avatar_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@avatar_Id", avatar_Id);
                return _connection.QueryFirst<Avatar?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Avatar : {ex.ToString}");
            }
            return null;
        }

        public Avatar? UpdateAvatar(int avatar_Id, string avatarName, string avatarUrl, string description)
        {
            try
            {
                string sql = "UPDATE Avatar SET AvatarName = @avatarName, AvatarUrl = @avatarUrl, Description = @description WHERE Avatar_Id = @avatar_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@avatarName", avatarName);
                parameters.Add("@avatarUrl", avatarUrl);
                parameters.Add("@description", description);
                return _connection.QueryFirst<Avatar?>(sql, parameters);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating avatar : {ex}");
            }
            return new Avatar();
        }
    }
}
