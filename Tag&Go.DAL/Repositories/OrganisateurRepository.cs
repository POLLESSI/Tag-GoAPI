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
    public class OrganisateurRepository : IOrganisateurRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public OrganisateurRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<Organisateur> Create(Organisateur organisateur)
        {
            try
            {
                string sql = "INSERT INTO Organisateur (CompanyName, BusinessNumber, NUser_Id, Point) VALUES " +
                    "(@CompanyName, @BusinessNumber, @NUser_Id, @Point)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@CompanyName", organisateur.CompanyName);
                parameters.Add("@BusinessNumber", organisateur.BusinessNumber);
                parameters.Add("@NUser_Id", organisateur.NUser_Id);
                parameters.Add("@Point", organisateur.Point);
                //return _connection.Execute(sql, parameters) > 0;
                var newId = _connection.QuerySingle<int>(sql, parameters);
                organisateur.Organisateur_Id = newId;

                return organisateur;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding Organisator : {ex.ToString}");
                return null;
            }
            
        }

        public void CreateOrganisateur(Organisateur organisateur)
        {
            try
            {
                string sql = "INSERT INTO Organisateur (CompanyName, BusinessNumber, NUser_Id, Point)" +
                    "VALUES (@companyName, @businessNumber, @nUser_Id, @point)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@companyName", organisateur.CompanyName);
                parameters.Add("@businessNumber", organisateur.BusinessNumber);
                parameters.Add("@nUser_Id", organisateur.NUser_Id);
                parameters.Add("@point", organisateur.Point);
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error createOrganisator : {ex.ToString}");
            }
        }

        public async Task<Organisateur?> DeleteOrganisateur(int organisateur_Id)
        {
            try
            {
                string sql = "SELECT * FROM Organisateur WHERE Organisateur_Id = @organisateur_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@organisateur_Id", organisateur_Id);

                var organisateur = await _connection.QueryFirstOrDefaultAsync<Organisateur?>(sql, new { organisateur_Id });

                if (organisateur == null)
                {
                    return null;
                }

                string deleteSql = "DELETE FROM Organisateur WHERE Organisateur_Id = @organisateur_Id";

                await _connection.ExecuteAsync(deleteSql, parameters);

                return organisateur;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Organisateur : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<IEnumerable<Organisateur?>> GetAllOrganisateurs(bool includeInactive = false)
        {
            try
            {
                string sql = includeInactive ? "SELECT * FROM Organisateur" : "SELECT *FROM Organisateur WHERE Active = 1";

                var organisateurs = await _connection.QueryAsync<Organisateur?>(sql);
                return organisateurs;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving Organisators: {ex.Message}");
                return Enumerable.Empty<Organisateur>();
            }
            
        }

        public async Task<Organisateur?> GetByIdOrganisateur(int organisateur_Id)
        {
            try
            {
                string sql = "SELECT * FROM Organisateur WHERE Organisateur_Id = @organisateur_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@organisateur_Id", organisateur_Id);

                var organisateur = await _connection.QueryFirstAsync<Organisateur?>(sql, parameters);

                return organisateur ?? null;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Organisator : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<Organisateur?> UpdateOrganisateur(Organisateur organisateur)
        {
            try
            {
                string sql = @"
                    UPDATE Organisateur 
                    SET 
                        CompanyName = @companyName, 
                        BusinessNumber = @businessNumber, 
                        NUser_Id = @nUser_Id, 
                        Point = @point 
                    WHERE 
                        Organisateur_Id = @organisateur_Id";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@companyName", organisateur.CompanyName);
                parameters.Add("@businessNumber", organisateur.BusinessNumber);
                parameters.Add("@nUser_Id", organisateur.NUser_Id);
                parameters.Add("@point", organisateur.Point);
                parameters.Add("@organisateur_Id", organisateur.Organisateur_Id);

                await _connection.QueryFirstAsync<Organisateur?>(sql, parameters);

                return organisateur;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Organisator : {ex}");
                return null;
            }
            
        }
    }
}
