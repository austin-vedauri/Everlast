using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.CRUD
{
    public class PortalCRUD
    {

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Member Register(Member model)
        {
            int result = 0;
            Guid memberGuid = Guid.NewGuid();

            string textCommand = "INSERT INTO tbl_Members " +
                "(MemberGuid, FirstName, LastName, Username, Password, Email, Phone)" +
                " OUTPUT INSERTED.MemberId VALUES " +
                "(@MemberGuid, @FirstName, @LastName, @Username, @Password, @Email, @Phone)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@MemberGuid", memberGuid);
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@Username", model.Username);
                command.Parameters.AddWithValue("@Password", model.Password);
                command.Parameters.AddWithValue("@Email", model.Email);
                command.Parameters.AddWithValue("@Phone", model.Phone);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["MemberId"].ToString());
                    }
                    connection.Close();
                }
            }

            if (result > 0)
            {
                model.MemberGuid = memberGuid;
                model.MemberId = result;
                return model;
            }
            else
            {
                return new Member();
            }
        }
        public Member Login(Member model)
        {
            Member member = new Member();
            string textCommand = "SELECT * FROM tbl_Members WHERE Username = @Username AND Password = @Password";

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
                        member.MemberId = Convert.ToInt32(reader["MemberId"].ToString());
                        member.MemberGuid = Guid.Parse(reader["MemberGuid"].ToString());
                        member.FirstName = reader["FirstName"].ToString();
                        member.LastName = reader["LastName"].ToString();
                        member.Username = reader["Username"].ToString();
                        member.Password = reader["Password"].ToString();
                        member.Email = reader["Email"].ToString();
                        member.Phone = reader["Phone"].ToString();
                    }
                    connection.Close();
                }
            }

            if (member.MemberId > 0)
            {
                return member;
            }
            else
            {
                return new Member();
            }
        }

        public int Logout(int userId)
        {
            // log the user out
            return 0;
        }

        public bool UsernameExists(string userName)
        {
            Member member = new Member();
            string textCommand = "SELECT * FROM tbl_Members WHERE Username = @Username";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@Username", userName);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        member.MemberId = Convert.ToInt32(reader["MemberId"].ToString());
                    }
                    connection.Close();
                }
            }
            return member.MemberId > 0 ? true : false;
        }
    }
}