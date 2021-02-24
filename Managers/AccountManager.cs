using Everlast.enums;
using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.Managers
{
    public class AccountManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Account Register(Account model)
        {
            int result = 0;
            Guid accountGuid = Guid.NewGuid();

            string textCommand = "INSERT INTO tbl_Accounts " +
                "(AccountGuid, AccountType, FirstName, LastName, Username, Password, Email, Phone)" +
                " OUTPUT INSERTED.AccountId VALUES " +
                "(@AccountGuid, @AccountType, @FirstName, @LastName, @Username, @Password, @Email, @Phone)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AccountGuid", accountGuid);
                command.Parameters.AddWithValue("@AccountType", model.AccountType);
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Phone", model.Phone);

                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();
            }

            if (result > 0) model.AccountGuid = accountGuid;

            return model;
        }
        public Account Login(Account model)
        {
            Account newModel = new Account();
            string textCommand = "SELECT * FROM tbl_Accounts WHERE Username = @Username AND Password = @Password";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        newModel.AccountGuid = Guid.Parse(reader["AccountGuid"].ToString());
                        newModel.AccountType = Convert.ToInt32(reader["AccountType"].ToString());
                        newModel.FirstName = reader["FirstName"].ToString();
                        newModel.LastName = reader["LastName"].ToString();
                        newModel.Username = reader["Username"].ToString();
                        newModel.Password = reader["Password"].ToString();
                        newModel.Email = reader["Email"].ToString();
                        newModel.Phone = reader["Phone"].ToString();
                    }
                    connection.Close();
                }
            }

            return newModel;
        }
        public bool UsernameExists(string userName)
        {
            Account model = new Account();
            string textCommand = "SELECT * FROM tbl_Accounts WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@Username", userName);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.AccountGuid = Guid.Parse(reader["AccountGuid"].ToString());
                    }
                    connection.Close();
                }
            }

            if (model.AccountGuid == null || model.AccountGuid == Guid.Empty)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public Account Read(Guid accountGuid)
        {
            Account model = new Account();

            string textCommand = "SELECT * FROM tbl_Accounts WHERE AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AccountGuid", accountGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.AccountGuid = Guid.Parse(reader["AccountGuid"].ToString());
                        model.AccountType = Convert.ToInt32(reader["AccountType"].ToString());
                        model.Username = reader["Username"].ToString();
                        model.Password = reader["Password"].ToString();
                        model.FirstName = reader["FirstName"].ToString();
                        model.LastName = reader["LastName"].ToString();
                        model.Email = reader["Email"].ToString();
                        model.Phone = reader["Phone"].ToString();
                    }
                    connection.Close();
                }
            }
            return model;
        }
        public Account Update(Account model)
        {
            int result = 0;

            string textCommand = "UPDATE tbl_Accounts SET FirstName = @FirstName, " +
                "LastName = @LastName, Username = @Username, Password = @Password, " +
                "Email = @Email, Phone = @Phone WHERE AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AccountGuid", model.AccountGuid);
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Phone", model.Phone);

                connection.Open();
                result = command.ExecuteNonQuery();
                connection.Close();
            }
            return model;
        }
        public int Delete(Guid accountGuid)
        {
            int result = 0;

            string textCommand = "DELETE FROM tbl_Accounts WHERE AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AccountGuid", accountGuid);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<Account> GetAccounts()
        {
            List<Account> models = new List<Account>();

            string textCommand = "SELECT * FROM tbl_Accounts";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Account model = new Account
                        {
                            AccountGuid = Guid.Parse(reader["AccountGuid"].ToString()),
                            AccountType = Convert.ToInt32(reader["AccountType"].ToString()),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString()
                        };

                        models.Add(model);
                    }
                    connection.Close();
                }
            }
            return models;
        }
        public List<Account> GetAccountsByType(AccountTypes accountType)
        {
            List<Account> models = new List<Account>();

            string textCommand = "SELECT * FROM tbl_Accounts WHERE AccountType = @AccountType";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AccountType", (int)accountType);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Account model = new Account
                        {
                            AccountGuid = Guid.Parse(reader["AccountGuid"].ToString()),
                            AccountType = Convert.ToInt32(reader["AccountType"].ToString()),
                            Username = reader["Username"].ToString(),
                            Password = reader["Password"].ToString(),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Email = reader["Email"].ToString(),
                            Phone = reader["Phone"].ToString()
                        };

                        models.Add(model);
                    }
                    connection.Close();
                }
            }
            return models;
        }
    }
}