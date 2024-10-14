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

        public async Task<NVote> Create(NVote nVote)
        {
            try
            {
                string sql = "INSERT INTO NVote (NEvenement_Id, FunOrNot, Comment) VALUES " +
                    "(@NEvenement_Id, @FunOrNot, @Comment)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NEvenement_Id", nVote.NEvenement_Id);
                parameters.Add("@FunOrNot", nVote.FunOrNot);
                parameters.Add("@Comment", nVote.Comment);
                //return _connection.Execute(sql, parameters) > 0;
                var newId = _connection.QuerySingle<int>(sql, parameters);

                nVote.NVote_Id = newId;

                return nVote;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding New Vote : {ex.ToString}");
                return null;
            }
            
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

        public async Task<NVote?> DeleteNVote(int nVote_Id)
        {
            try
            {
                string sql = "SELECT * FROM NVote WHERE NVote_Id = @nVote_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nVote_Id", nVote_Id);

                var nVote = await _connection.QueryFirstAsync<NVote?>(sql, new { nVote_Id });
                if (nVote == null)
                {
                    return null;
                }

                string deleteSql = "DELETE FROM NVote WHERE NVote_Id = @nVote_Id";

                await _connection.ExecuteAsync(deleteSql, parameters);
                return nVote;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting vote : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<IEnumerable<NVote?>> GetAllNVotes(bool includeInactive = false)
        {
            try
            {
                string sql = includeInactive ? "SELECT * FROM NVote": "SELECT * FROM NVote WHERE Active = 1";

                var nvotes = await _connection.QueryAsync<NVote?>(sql);
                return nvotes;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving Votes: {ex.Message}");
                return Enumerable.Empty<NVote>();
            }
            
        }

        public async Task<NVote?> GetByIdNVote(int nVote_Id)
        {
            try
            {
                string sql = "SELECT * FROM NVote WHERE NVote_Id = @nVote_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nVote_Id", nVote_Id);
                var nVote = await _connection.QueryFirstAsync<NVote?>(sql, parameters);
                return nVote;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Vote : {ex.ToString}");
                return null;
            }
            
        }
    }
}
