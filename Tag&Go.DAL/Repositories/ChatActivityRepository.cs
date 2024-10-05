﻿using Tag_Go.DAL.Entities;
using Tag_Go.DAL.Interfaces;
using System;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Tag_Go.DAL.Repositories
{
    public class ChatActivityRepository : IChatActivityRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public ChatActivityRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool CreateChatActivity(ChatActivity chat)
        {
            try
            {
                string sql = "INSERT INTO ChatActivity (NewMessage, Author, SendingDate, Activity_Id) VALUES " +
                    "(@NewMessage, @Author, @SendingDate, @Activity_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NewMessage", chat.NewMessage);
                parameters.Add("@Author", chat.Author);
                parameters.Add("@SendingDate", chat.SendingDate);
                parameters.Add("@Activity_Id", chat.Activity_Id);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding chat Activity : {ex.ToString}");
            }
            return false;
        }

        public void CreateChat(ChatActivity chat)
        {
            try
            {
                string sql = "INSERT INTO ChatActivity (NewMessage, Author, SendingDate, Activity_Id) " +
                    "VALUES (@newMessage, @author, @sendingDate, @activity_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@newMessage", chat.NewMessage);
                parameters.Add("@author", chat.Author);
                parameters.Add("@sendingDate", chat.SendingDate);
                parameters.Add("@activity_Id", chat.Activity_Id);

                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateChat Activity : {ex.ToString}");
            }
        }

        public Task<ChatActivity?> DeleteMessageActivity(int chatActivity_Id)
        {
            try
            {
                string sql = "DELETE FROM ChatActivity WHERE ChatActivity_Id = @chatActivity_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@chatActivity_Id", chatActivity_Id);
                return _connection.QueryFirstAsync<ChatActivity?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Chat Activity : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<ChatActivity?>> GetAllMessagesActivities()
        {
            string sql = "SELECT * FROM ChatActivity";
            return _connection.QueryAsync<ChatActivity?>(sql);
        }

        public async Task<ChatActivity?> GetByIdChatActivity(int chatActivity_Id)
        {
            try
            {
                string sql = "SELECT * FROM ChatActivity WHERE ChatActivity_Id = @chatActivity_Id";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@chatActivity_Id", chatActivity_Id);

                var chatActivity = await _connection.QueryFirstAsync<ChatActivity?>(sql, parameters);
                return chatActivity;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Chat Activity : {ex.ToString}");
                return null;
            }
            
        }
    }
}
