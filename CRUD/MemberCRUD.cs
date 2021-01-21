using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.CRUD
{
    public class MemberCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Member Read(Guid memberGuid)
        {
            Member member = new Member();

            string textCommand = "SELECT * FROM tbl_Members WHERE MemberGuid = MemberGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@MemberGuid", memberGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        member.MemberId = Convert.ToInt32(reader["MemberId"].ToString());
                        member.MemberGuid = Guid.Parse(reader["MemberGuid"].ToString());
                        member.Username = reader["Username"].ToString();
                        member.Password = reader["Password"].ToString();
                        member.FirstName = reader["FirstName"].ToString();
                        member.LastName = reader["LastName"].ToString();
                        member.Email = reader["Email"].ToString();
                        member.Verified = Convert.ToBoolean(reader["Verified"].ToString());
                        member.Phone = reader["Phone"].ToString();
                    }
                    connection.Close();
                }
            }
            return member;
        }
    }
}