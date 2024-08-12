using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;

namespace Tag_Go.DAL.Repositories
{
    public class NUserRepository : INUserRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public NUserRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Create(NUser nUser)
        {
            try
            {
                string sql = "INSERT INTO NUser (Email, Pwd, NPerson_Id, Role_Id, Avatar_Id, Point) VALUES " +
                    "(@email, CONVERT(varbinary(64), @Pwd), @NPerson_Id, @Role_Id, @Avatar_Id, @Point)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Email", nUser.Email);
                parameters.Add("@Pwd", nUser.Pwd);
                parameters.Add("@NPerson_Id", nUser.NPerson_Id);
                parameters.Add("@Role_Id", nUser.Role_Id);
                parameters.Add("@Avatar_Id", nUser.Avatar_Id);
                parameters.Add("@Point", nUser.Point);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Encoding New User : {ex.ToString}");
            }
            return false;
        }

        public void CreateNUser(NUser nUser)
        {
            try
            {
                string sql = "INSERT INTO NUser (Email, Pwd, NPerson_Id, Role_Id, Avatar_Id, Point) " +
                    "VALUES (@email, CONVERT(varbinary(64), @pwd), @Nperson_Id, @role_Id, @avatar_Id, @point)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@email", nUser.Email);
                parameters.Add("@pwd", nUser.Pwd);
                parameters.Add("@Nperson_Id", nUser.NPerson_Id);
                parameters.Add("@role_Id", nUser.Role_Id);
                parameters.Add("@avatar_Id", nUser.Avatar_Id);
                parameters.Add("@point", nUser.Point);
                _connection.Query<NUser?>(sql, parameters);

            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Create New Person : {ex.ToString}");
            }
        }

        public NUser? DeleteNUser(int nUser_Id)
        {
            try
            {
                string sql = "DELETE FROM NUser WHERE NUser_Id = @nUser_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nUser_Id", nUser_Id);
                return _connection.QueryFirst<NUser?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Erro deleting User : {ex.ToString}");
            }
            return null;
        }

        public IEnumerable<NUser?> GetAllNUsers()
        {
            string sql = "SELECT * FROM NUser";
            return _connection.Query<NUser?>(sql);
        }

        public NUser? GetByIdNUser(int nUser_Id)
        {
            try
            {
                string sql = "SELECT * FROM NUser WHERE NUser_Id = @nUser_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nUser_Id", nUser_Id);
                return _connection.QueryFirst<NUser?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting User : {ex.ToString}");
            }
            return null;
        }

        public NUser? LoginNUser(string? email, string? pwd)
        {
            try
            {
                string sqlCheckPassword = "SELECT * FROM NUser WHERE Email = @email, Pwd = CONVERT(varbinary(64), @pwd)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@email", email);
                parameters.Add("@pwd", pwd);
                return _connection.QueryFirst<NUser?>(sqlCheckPassword, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Non-existent user : {ex.ToString}");
            }
            return null;
        }

        public bool RegisterNUser(string? email, string? pwd, int nPerson_Id, string? role_Id, int avatar_Id, string? point)
        {
            try
            {
                string sql = "INSERT INTO NUser (Email, Pwd, NPerson_Id, Role_Id, Avatar_Id, Point) " +
                "VALUES (@email, CONVERT(varbinary(64), @pwd), @nPerson_Id, @role_Id, @avatar_Id, @point)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@email", email);
                parameters.Add("@pwd", pwd);
                parameters.Add("@nPerson_Id", nPerson_Id);
                parameters.Add("@role_Id", role_Id);
                parameters.Add("@avatar_Id", avatar_Id);
                parameters.Add("@point", point);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error Registrating New User : {ex.ToString}");

            }
            return false;
        }

        public void SetRole(int nUser_Id, string? role_Id)
        {
            try
            {
                string sql = "UPDATE NUser SET Role_Id = @role_Id WHERE NUser_Id = @nUser_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nUser_Id", nUser_Id);
                parameters.Add("@role_Id", role_Id);
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error changing rôle : {ex.ToString}");
            }
        }

        public NUser? UpdateNUser(int nUser_Id, string? email, string? pwd, int nPerson_Id, string? role_Id, int avatar_Id, string? point)
        {
            try
            {
                string sql = "UPDATE NUser SET Email = @email, Pwd = CONVERT(varbinary(64), @pwd), NPerson_Id = @nPerson_Id, Role_Id = @role_Id WHERE NUser_Id = @nUser_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nUser_Id", nUser_Id);
                parameters.Add("@email", email);
                parameters.Add("@pwd", pwd);
                parameters.Add("@nPerson_Id", nPerson_Id);
                parameters.Add("@role_Id", role_Id);
                return _connection.QueryFirst<NUser>(sql, parameters);
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating user : {ex}");
            }
            return new NUser();
        }
    }
}
