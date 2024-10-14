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
    public class NPersonRepository : INPersonRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public NPersonRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task <NPerson> Create(NPerson nPerson)
        {
            try
            {
                string sql = "INSERT INTO NPerson (Lastname, Firstname, Email, Address_Street, Address_Nbr, PostalCode, Address_City, Address_Country, Telephone, Gsm) VALUES " +
                    "(@Lastname, @Firstname, @Email, @Address_Street, @Address_Nbr, @PostalCode, @Address_City, @Address_Country, @Telephone, @Gsm)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Lastname", nPerson.Lastname);
                parameters.Add("@Firstname", nPerson.Firstname);
                parameters.Add("@Email", nPerson.Email);
                parameters.Add("@Address_Street", nPerson.Address_Street);
                parameters.Add("@Address_Nbr", nPerson.Address_Nbr);
                parameters.Add("@PostalCode", nPerson.PostalCode);
                parameters.Add("@Address_City", nPerson.Address_City);
                parameters.Add("@Address_Country", nPerson.Address_Country);
                parameters.Add("@Telephone", nPerson.Telephone);
                parameters.Add("@Gsm", nPerson.Gsm);
                //return _connection.Execute(sql, parameters) > 0;
                var newId = _connection.QuerySingle<int>(sql, parameters);

                nPerson.NPerson_Id = newId;

                return nPerson;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding New Person : {ex.ToString}");
                return null;
            }
            
        }

        public void CreatePerson(NPerson nPerson)
        {
            try
            {
                string sql = "INSERT INTO NPerson (Lastname, Firstname, Email, Address_Street, Address_Nbr, PostalCode, Address_City, Address_Country, Telephone, Gsm) " +
                    "VALUES (@lastname, @firstname, @email, @address_Street, @address_Nbr, @postalCode, @address_City, @address_Country, @telephone, @gsm)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@lastname", nPerson.Lastname);
                parameters.Add("@firstname", nPerson.Firstname);
                parameters.Add("@email", nPerson.Email);
                parameters.Add("@address_Street", nPerson.Address_Street);
                parameters.Add("@address_Nbr", nPerson.Address_Nbr);
                parameters.Add("@postalCode", nPerson.PostalCode);
                parameters.Add("@address_City", nPerson.Address_City);
                parameters.Add("@address_Country", nPerson.Address_Country);
                parameters.Add("@telephone", nPerson.Telephone);
                parameters.Add("@gsm", nPerson.Gsm);
                _connection.Query<NPerson?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error creating New Person : {ex.ToString}");
            }
        }

        public async Task<NPerson?> DeleteNPerson(int nPerson_Id)
        {
            try
            {
                string sql = "SELECT * FROM NPerson WHERE NPerson_Id = @nPerson_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nPerson_Id", nPerson_Id);

                var nPerson = await _connection.QueryFirstOrDefaultAsync<NPerson?>(sql, new { nPerson_Id });

                if (nPerson == null)
                {
                    return null;
                }

                string deleteSql = "DELETE FROM NPerson WHERE NPerson_Id = @nPerson_Id";

                await _connection.ExecuteAsync(deleteSql, parameters);

                return nPerson;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Person : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<IEnumerable<NPerson?>> GetAllNPersons(bool includeInactive = false)
        {
            try
            {
                string sql = includeInactive ? "SELECT * FROM NPerson": "SELECT * FROM NPerson WHERE Active = 1";
                var npersons = await _connection.QueryAsync<NPerson?>(sql);
                return npersons;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving persons: {ex.Message}");
                return Enumerable.Empty<NPerson>();
            }
            
        }

        public async Task<NPerson?> GetByIdNPerson(int nPerson_Id)
        {
            try
            {
                string sql = "SELECT * FROM NPerson WHERE NPerson_Id = @nPerson_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@nPerson_Id", nPerson_Id);

                var nPerson = await _connection.QueryFirstAsync<NPerson?>(sql, parameters);
                return nPerson;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Person : {ex.ToString}");
                return null;
            }
            
        }

        public async Task<NPerson> UpdateNPerson(NPerson nPerson)
        {
            try
            {
                string sql = @"
                    UPDATE NPerson 
                    SET 
                        Lastname = @lastname, 
                        Firstname = @firstname, 
                        Email = @email, 
                        Address_Street = @address_Street, 
                        Address_Nbr = @address_Nbr, 
                        PostalCode = @postalCode, 
                        Address_City = @address_City, 
                        Address_Country = @address_Country, 
                        Telephone = @telephone, 
                        Gsm = @gsm 
                    WHERE 
                        NPerson_Id = @nPerson_Id";

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("@lastname", nPerson.Lastname);
                parameters.Add("@firstname", nPerson.Firstname);
                parameters.Add("@email", nPerson.Email);
                parameters.Add("@address_Street", nPerson.Address_Street);
                parameters.Add("@address_Nbr", nPerson.Address_Nbr);
                parameters.Add("@postalCode", nPerson.PostalCode);
                parameters.Add("@address_City", nPerson.Address_City);
                parameters.Add("@address_Country", nPerson.Address_Country);
                parameters.Add("@telephone", nPerson.Telephone);
                parameters.Add("@gsm", nPerson.Gsm);
                parameters.Add("@nPerson_Id", nPerson.NPerson_Id);

                await _connection.QueryFirstAsync<NPerson?>(sql, parameters);

                return nPerson;
            }
            catch (System.ComponentModel.DataAnnotations.ValidationException ex)
            {

                Console.WriteLine($"Validation error : {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating Person : {ex}");
                return null;
            }
            
        }
    }
}
