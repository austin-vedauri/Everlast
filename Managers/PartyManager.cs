using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.Managers
{
    public class PartyManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Party Create(Party party)
        {
            Guid partyGuid = Guid.NewGuid();

            int result = 0;
            string textCommand = "INSERT INTO tbl_Parties " +
                " (PartyGuid, Title, Description, Price, PartyStart, PartyEnd, Address, City, Postal, State) " +
                "OUTPUT INSERTED.PartyId VALUES " +
                "(@PartyGuid, @Title, @Description, @Price, @PartyStart, @PartyEnd, @Address, @City, @Postal, @State)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PartyGuid", partyGuid);
                command.Parameters.AddWithValue("@Title", party.Title);
                command.Parameters.AddWithValue("@Description", party.Description);
                command.Parameters.AddWithValue("@Price", party.Price);
                command.Parameters.AddWithValue("@PartyStart", party.PartyStart);
                command.Parameters.AddWithValue("@PartyEnd", party.PartyEnd);
                command.Parameters.AddWithValue("@Address", party.Address);
                command.Parameters.AddWithValue("@City", party.City);
                command.Parameters.AddWithValue("@Postal", party.Postal);
                command.Parameters.AddWithValue("@State", party.State);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["PartyId"].ToString());
                    }
                    connection.Close();
                }
            }
            if (result > 0) party.PartyGuid = partyGuid; return party;

        }
        public Party Read(Guid partyGuid)
        {
            Party model = new Party();

            string textCommand = "SELECT * FROM tbl_Parties WHERE PartyGuid = @PartyGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PartyGuid", partyGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.PartyGuid = Guid.Parse(reader["PartyGuid"].ToString());
                        model.Title = reader["Title"].ToString();
                        model.Description = reader["Description"].ToString();
                        model.Price = Convert.ToDecimal(reader["Price"].ToString());
                        model.PartyStart = Convert.ToDateTime(reader["PartyStart"].ToString());
                        model.PartyEnd = Convert.ToDateTime(reader["PartyEnd"].ToString());
                        model.Address = reader["Address"].ToString();
                        model.City = reader["City"].ToString();
                        model.Postal = reader["Postal"].ToString();
                        model.State = reader["State"].ToString();

                    }
                    connection.Close();
                }
            }
            return model;
        }
        public Party Update(Party model)
        {
            int result = 0;

            string textCommand = "UPDATE tbl_Parties SET Title = @Title, Description = @Description, Price = @Price, PartyStart = @PartyStart, PartyEnd = @PartyEnd, Address = @Address, City = @City, Postal = @Postal, State = @State WHERE PartyGuid = @PartyGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PartyGuid", model.PartyGuid);
                command.Parameters.AddWithValue("@Title", model.Title);
                command.Parameters.AddWithValue("@Description", model.Description);
                command.Parameters.AddWithValue("@Price", model.Price);
                command.Parameters.AddWithValue("@PartyStart", model.PartyStart);
                command.Parameters.AddWithValue("@PartyEnd", model.PartyEnd);
                command.Parameters.AddWithValue("@Address", model.Address);
                command.Parameters.AddWithValue("@City", model.City);
                command.Parameters.AddWithValue("@Postal", model.Postal);
                command.Parameters.AddWithValue("@State", model.State);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return model;
        }
        public int Destroy(Guid partyGuid)
        {
            int result = 0;

            string textCommand = "DELETE FROM tbl_Parties WHERE PartyGuid = @PartyGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PartyGuid", partyGuid);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<Party> GetParties()
        {
            List<Party> models = new List<Party>();

            string textCommand = "SELECT * FROM tbl_Parties";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Party model = new Party
                        {
                            PartyGuid = Guid.Parse(reader["PartyGuid"].ToString()),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            PartyStart = Convert.ToDateTime(reader["PartyStart"].ToString()),
                            PartyEnd = Convert.ToDateTime(reader["PartyEnd"].ToString()),
                            Address = reader["Address"].ToString(),
                            City = reader["City"].ToString(),
                            Postal = reader["Postal"].ToString(),
                            State = reader["State"].ToString(),
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