using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Tag_Go.DAL.Repositories
{
    public class NIconRepository : INIconRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public NIconRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<NIcon> Create(NIcon nIcon)
        {
            try
            {
                string sql = "INSERT INTO NIcon (NIconName, NIconDescription, NIConUrl) VALUES" +
                    "(@NIconName, @NIconDescription, @NIconUrl)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NIconName", nIcon.NIconName);
                parameters.Add("@NIconDescription", nIcon.NIconDescription);
                parameters.Add("@NIconUrl", nIcon.NIconUrl);
                //return _connection.Execute(sql, parameters) > 0;
                var newId = _connection.QuerySingle<int>(sql, parameters);

                nIcon.NIcon_Id = newId;

                return nIcon;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating New Icon : {ex.ToString}");
                return null;
            }
            
        }

        public void CreateIcon(NIcon nIcon)
        {
            try
            {
                string sql = "INSERT INTO NIcon (NIconName, NIconDescription, NIconUrl)" +
                    "VALUES (@nIconName, @nIconDescription, @nIconUrl)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nIconName", nIcon.NIconName);
                parameters.Add("@nIconDescription", nIcon.NIconDescription);
                parameters.Add("@nIonUrl", nIcon.NIconUrl);
                _connection.Query<NIcon?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating new Icon : {ex.ToString}");
            }
        }

        public async Task<NIcon?> DeleteNIcon(int nIcon_Id)
        {
            try
            {
                string sql = "SELECT * FROM NIcon WHERE NIcon_Id = @nIcon_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nIcon_Id", nIcon_Id);

                var nIcon = await _connection.QueryFirstOrDefaultAsync<NIcon?>(sql, new { nIcon_Id });

                if (nIcon == null)
                {
                    return null;
                }

                string deleteSql = "DELETE FROM NIcon WHERE NIcon_Id = @nIcon_Id";

                await _connection.ExecuteAsync(deleteSql, parameters);

                return nIcon;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Icon : {ex.ToString}");
                return null;
            }
        }

        public async Task<IEnumerable<NIcon?>> GetAllNIcons(bool includeInactive = false)
        {
            try
            {
                string sql = includeInactive ? "SELECT * FROM NIcon" : "SELECT * FROM NIcon WHERE Active = 1";
                var nicons = await _connection.QueryAsync<NIcon?>(sql);
                return nicons;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving icons: {ex.Message}");
                return Enumerable.Empty<NIcon>();
            }
            
        }

        public async Task<NIcon?> GetByIdNIcon(int nIcon_Id)
        {
            try
            {
                string sql = "SELECT * FROM NIcon WHERE NIcon_Id = @nIcon_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nIcon_Id", nIcon_Id);

                var nIcon = await _connection.QueryFirstAsync<NIcon?>(sql, parameters);
                return nIcon;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Icon : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<NIcon?> UpdateNIcon(NIcon nIcon)
        {
            try
            {
                string sql = @"
                    UPDATE NIcon 
                    SET 
                        NIconName = @nIconName, 
                        NIconDescription = @nIconDescription, 
                        NIconUrl = @nIconUrl 
                    WHERE 
                        NIcon_Id = @nIcon_Id";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@nIconName", nIcon.NIconName);
                parameters.Add("@nIconDescription", nIcon.NIconDescription);
                parameters.Add("@nIconUrl", nIcon.NIconUrl);
                parameters.Add("@nIcon_Id", nIcon.NIcon_Id);

                await _connection.QueryFirstAsync<NIcon?>(sql, parameters);

                return nIcon;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error Updating Icon : {ex}");
                return null;
            }
            
        }
    }
}
