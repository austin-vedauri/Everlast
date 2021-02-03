using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.Managers
{
    public class AttendeeManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Attendee Create(Attendee model)
        {
            Guid attendeeGuid = Guid.NewGuid();

            string textCommand = "INSERT INTO tbl_Attendees " +
                "(AttendeeGuid, PartyGuid, FirstName, LastName, Phone, Email)" +
                " OUTPUT INSERTED.AttendeeId VALUES " +
                "(@AttendeeGuid, @PartyGuid, @FirstName, @LastName, @Phone, @Email)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AttendeeGuid", attendeeGuid);
                command.Parameters.AddWithValue("@PartyGuid", model.PartyGuid);
                command.Parameters.AddWithValue("@FirstName", model.FirstName);
                command.Parameters.AddWithValue("@LastName", model.LastName);
                command.Parameters.AddWithValue("@Phone", model.Phone);
                command.Parameters.AddWithValue("@Email", "");

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

            }
            model.AttendeeGuid = attendeeGuid;

            return model;
        }

        public Attendee Read(Guid attendeeGuid, Guid partyGuid)
        {
            Attendee model = new Attendee();

            string textCommand = "SELECT * FROM tbl_Attendees WHERE AttendeeGuid = @AttendeeGuid AND PartyGuid = @PartyGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AttendeeGuid", attendeeGuid);
                command.Parameters.AddWithValue("@PartyGuid", partyGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.AttendeeGuid = Guid.Parse(reader["AttendeeGuid"].ToString());
                        model.PartyGuid = Guid.Parse(reader["PartyGuid"].ToString());
                        model.FirstName = reader["FirstName"].ToString();
                        model.LastName = reader["LastName"].ToString();
                        model.Phone = reader["Phone"].ToString();

                    }
                    connection.Close();
                }
            }
            return model;
        }
    }
}