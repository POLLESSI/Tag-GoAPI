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
    public class NVoteRepository : INVoteRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public NVoteRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Create(NVote nVote)
        {
            try
            {
                string sql = "INSERT INTO NVote (NEvenement_Id, FunOrNot, Comment) VALUES " +
                    "(@NEvenement_Id, @FunOrNot, @Comment)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NEvenement_Id", nVote.NEvenement_Id);
                parameters.Add("@FunOrNot", nVote.FunOrNot);
                parameters.Add("@Comment", nVote.Comment);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding New Vote : {ex.ToString}");
            }
            return false;
        }

        public void CreateVote(NVote nVote)
        {
            try
            {
                string sql = "INSERT INTO NVote (NEvenement_Id, FunOrNot, Comment) " +
                    "VALUES (@nEvenement_Id, @funOrNot, @comment)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nEvenement_Id", nVote.NEvenement_Id);
                parameters.Add("@funOrNot", nVote.FunOrNot);
                parameters.Add("@comment", nVote.Comment);
                _connection.Query<NVote?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating new Vote : {ex.ToString}");
            }
        }

        public Task<NVote?> DeleteNVote(int nVote_Id)
        {
            try
            {
                string sql = "DELETE FROM NVote WHERE NVote_Id = @nVote_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nVote_Id", nVote_Id);
                return _connection.QueryFirstAsync<NVote?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting vote : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<NVote?>> GetAllNVotes()
        {
            string sql = "SELECT * FROM NVote";
            return _connection.QueryAsync<NVote?>(sql);
        }

        public Task<NVote?> GetByIdNVote(int nVote_Id)
        {
            try
            {
                string sql = "SELECT * FROM NVote WHERE NVote_Id = @nVote_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nVote_Id", nVote_Id);
                return _connection.QueryFirstAsync<NVote?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Vote : {ex.ToString}");
            }
            return null;
        }
    }
}
