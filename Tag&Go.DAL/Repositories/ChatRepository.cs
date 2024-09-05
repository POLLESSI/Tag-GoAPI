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
    public class ChatRepository : IChatRepository
    {
    #nullable disable
        private readonly SqlConnection _connection;

        public ChatRepository(SqlConnection connection)
        {
            _connection = connection;
        }

        public bool Create(Chat chat)
        {
            try
            {
                string sql = "INSERT INTO Chat (NewMessage, Author, SendingDate Evenement_Id, Activity_Id) VALUES " +
                    "(@NewMessage, @Author, @SendingDate, @Evenement_Id, @Activity_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@NewMessage", chat.NewMessage);
                parameters.Add("@Author", chat.Author);
                parameters.Add("@SendingDate", chat.SendingDate);
                parameters.Add("@Evenement_Id", chat.Evenement_Id);
                parameters.Add("@Activity_Id", chat.Activity_Id);
                return _connection.Execute(sql, parameters) > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error encoding chat : {ex.ToString}");
            }
            return false;
        }

        public void CreateChat(Chat chat)
        {
            try
            {
                string sql = "INSERT INTO Chat (NewMessage, Author, SendingDate, Evenement_Id, Activity_Id) " +
                    "VALUES (@newMessage, @author, @sendingDate, @evenement_Id, @activity_Id)";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@newMessage", chat.NewMessage);
                parameters.Add("@author", chat.Author);
                parameters.Add("@sendingDate", chat.SendingDate);
                parameters.Add("@evenement_Id", chat.Evenement_Id);
                parameters.Add("@activity_Id", chat.Activity_Id);
                _connection.Execute(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error CreateChat : {ex.ToString}");
            }
        }

        public Task<Chat?> DeleteMessage(int chat_Id)
        {
            try
            {
                string sql = "DELETE FROM Chat WHERE Chat_Id = @chat_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@chat_Id", chat_Id);
                return _connection.QueryFirstAsync<Chat?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error deleting Chat : {ex.ToString}");
            }
            return null;
        }

        public Task<IEnumerable<Chat?>> GetAllMessages()
        {
            string sql = "SELECT * FROM Chat";
            return _connection.QueryAsync<Chat?>(sql);
        }

        public Task<Chat?> GetByIdChat(int chat_Id)
        {
            try
            {
                string sql = "SELECT * FROM Chat WHERE Chat_Id = @chat_Id";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@chat_Id", chat_Id);
                return _connection.QueryFirstAsync<Chat?>(sql, parameters);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error geting Chat : {ex.ToString}");
            }
            return null;
        }
    }
}
