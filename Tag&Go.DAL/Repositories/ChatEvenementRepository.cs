using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using System.Data.SqlClient;

namespace Tag_Go.DAL.Repositories
{
    public class ChatEvenementRepository : IChatEvenementRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public ChatEvenementRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<ChatEvenement> CreateChatEvenement(ChatEvenement chat)
        {
            try
            {
                string sql = "INSERT INTO ChatEvenement (NewMessage, Author, SendingDate, NEvenement_Id) VALUES " +
                    "(@NewMessage, @Author, @SendingDate, @NEvenement_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NewMessage", chat.NewMessage);
                parameters.Add("@Author", chat.Author);
                parameters.Add("@SendingDate", chat.SendingDate);
                parameters.Add("@NEvenement_Id", chat.NEvenement_Id);
                //return _connection.Execute(sql, parameters) > 0;

                var newId = _connection.QuerySingle<int>(sql, parameters);

                chat.ChatEvenement_Id = newId;

                return chat;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding chat Event: {ex.ToString}");
                return null;
            }
            
        }

        public void CreateChat(ChatEvenement chat)
        {
            try
            {
                string sql = "INSERT INTO ChatEvenement (NewMessage, Author, SendingDate, NEvenement_Id) " +
                    "VALUES (@newMessage, @author, @sendingDate, @nEvenement_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@newMessage", chat.NewMessage);
                parameters.Add("@author", chat.Author);
                parameters.Add("@sendingDate", chat.SendingDate);
                parameters.Add("@nEvenement_Id", chat.NEvenement_Id);
 
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateChat Event : {ex.ToString}");
            }
        }

        public Task<ChatEvenement?> DeleteMessageEvenement(int chatEvenement_Id)
        {
            try
            {
                string sql = "DELETE FROM ChatEvenement WHERE ChatEvenement_Id = @chatEvenement_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@chatEvenement_Id", chatEvenement_Id);
                return _connection.QueryFirstAsync<ChatEvenement?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Chat Event : {ex.ToString}");
            }
            return null;
        }

        public async Task<IEnumerable<ChatEvenement?>> GetAllMessagesEvenements(bool includeInactive = false)
        {
            try
            {
                string sql = includeInactive ? "SELECT * FROM ChatEvenement" : "SELECT * FROM ChatEvenement WHERE Active = 1";

                var chatEvenements = await _connection.QueryAsync<ChatEvenement?>(sql);
                return chatEvenements;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error retrieving chats events: {ex.Message}");
                return Enumerable.Empty<ChatEvenement>();
            }
            
        }

        public async Task<ChatEvenement?> GetByIdChatEvenement(int chatEvenement_Id)
        {
            try
            {
                string sql = "SELECT * FROM ChatEvenement WHERE ChatEvenement_Id = @chatEvenement_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@chat_Id", chatEvenement_Id);

                var chatEvenement = await _connection.QueryFirstAsync<ChatEvenement?>(sql, parameters);
                return chatEvenement;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Chat Event : {ex.ToString}");
                return null;
            }
            
        }
    }
}
