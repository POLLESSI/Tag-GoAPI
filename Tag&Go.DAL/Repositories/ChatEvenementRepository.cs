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

        public bool Create(ChatEvenement chat)
        {
            try
            {
                string sql = "INSERT INTO ChatEvenement (NewMessage, Author, SendingDate, Evenement_Id) VALUES " +
                    "(@NewMessage, @Author, @SendingDate, @Evenement_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NewMessage", chat.NewMessage);
                parameters.Add("@Author", chat.Author);
                parameters.Add("@SendingDate", chat.SendingDate);
                parameters.Add("@Evenement_Id", chat.Evenement_Id);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding chat : {ex.ToString}");
            }
            return false;
        }

        public void CreateChat(ChatEvenement chat)
        {
            try
            {
                string sql = "INSERT INTO ChatEvenement (NewMessage, Author, SendingDate, Evenement_Id) " +
                    "VALUES (@newMessage, @author, @sendingDate, @evenement_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@newMessage", chat.NewMessage);
                parameters.Add("@author", chat.Author);
                parameters.Add("@sendingDate", chat.SendingDate);
                parameters.Add("@evenement_Id", chat.Evenement_Id);
 
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateChat : {ex.ToString}");
            }
        }

        public Task<ChatEvenement?> DeleteMessage(int chat_Id)
        {
            try
            {
                string sql = "DELETE FROM ChatEvenement WHERE Chat_Id = @chat_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@chat_Id", chat_Id);
                return _connection.QueryFirstAsync<ChatEvenement?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Chat : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<ChatEvenement?>> GetAllMessages()
        {
            string sql = "SELECT * FROM ChatEvenement";
            return _connection.QueryAsync<ChatEvenement?>(sql);
        }

        public async Task<ChatEvenement?> GetByIdChat(int chat_Id)
        {
            try
            {
                string sql = "SELECT * FROM ChatEvenement WHERE Chat_Id = @chat_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@chat_Id", chat_Id);

                var chatEvenement = await _connection.QueryFirstAsync<ChatEvenement?>(sql, parameters);
                return chatEvenement;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting ChatEvenement : {ex.ToString}");
                return null;
            }
            
        }
    }
}
