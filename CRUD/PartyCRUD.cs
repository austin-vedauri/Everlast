using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.CRUD
{
    public class PartyCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Party Create(Party party)
        {
            Guid partyGuid = Guid.NewGuid();

            int result = 0;
            string textCommand = "INSERT INTO tbl_Parties " +
                " (PartyGuid, Title, Description, EntryFee, Active, PartyDate, PartyStart, PartyEnd, AddressLineOne, AddressLineTwo, City, Postal, State) " +
                "OUTPUT INSERTED.UserId VALUES " +
                "(@PartyGuid, @Title, @Description, @EntryFee, @Active, @PartyDate, @PartyStart, @PartyEnd, @AddressLineOne, @AddressLineTwo, @City, @Postal, @State)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PartyGuid", partyGuid);
                command.Parameters.AddWithValue("@Title", party.Title);
                command.Parameters.AddWithValue("@Description", party.Description);
                command.Parameters.AddWithValue("@EntryFee", party.EntryFee);
                command.Parameters.AddWithValue("@Active", party.Active);
                command.Parameters.AddWithValue("@PartyDate", party.PartyDate);
                command.Parameters.AddWithValue("@PartyStart", party.PartyStart);
                command.Parameters.AddWithValue("@PartyEnd", party.PartyEnd);
                command.Parameters.AddWithValue("@AddressLineOne", party.Active);
                command.Parameters.AddWithValue("@AddressLineTwo", party.Active);
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
            if (result > 0)
            {
                party.PartyGuid = partyGuid;
                party.PartyId = result;
                return party;
            }
            else
            {
                return new Party();
            }
        }
        //public Party Read(Guid partyGuid)
        //{
        //    Party party = new Party();
        //    string textCommand = "SELECT * FROM tbl_Users WHERE UserId = @UserId";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(textCommand, connection);

        //        command.Parameters.AddWithValue("@UserId", userId);

        //        connection.Open();

        //        using (SqlDataReader reader = command.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                user.UserId = Convert.ToInt32(reader["UserId"].ToString());
        //                user.Name = reader["Name"].ToString();
        //                user.Pin = reader["Pin"].ToString();
        //                user.Working = Convert.ToBoolean(reader["Working"].ToString());
        //                user.WorkPeriodId = Convert.ToInt32(reader["WorkPeriodId"].ToString());
        //                user.WorkRate = Convert.ToDecimal(reader["WorkRate"].ToString());

        //            }
        //            connection.Close();
        //        }
        //    }
        //    return user;
        //}
        //public Party Update(Party party)
        //{
        //    int result = 0;
        //    string textCommand = "UPDATE tbl_Users SET Name = @Name, WorkPeriodId = @WorkPeriodId, Pin = @Pin, Working = @Working, WorkRate = @WorkRate WHERE UserId = @UserId";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(textCommand, connection);

        //        command.Parameters.AddWithValue("@UserId", user.UserId);
        //        command.Parameters.AddWithValue("@Name", user.Name);
        //        command.Parameters.AddWithValue("@WorkPeriodId", user.WorkPeriodId);
        //        command.Parameters.AddWithValue("@Pin", user.Pin);
        //        command.Parameters.AddWithValue("@Working", user.Working);
        //        command.Parameters.AddWithValue("@WorkRate", user.WorkRate);

        //        connection.Open();

        //        result = command.ExecuteNonQuery();

        //        connection.Close();
        //    }
        //    return result;
        //}
        //public Party Destroy(Guid partyGuid)
        //{
        //    int result = 0;
        //    string textCommand = "DELETE FROM tbl_Users WHERE UserId = @UserId";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        SqlCommand command = new SqlCommand(textCommand, connection);

        //        command.Parameters.AddWithValue("@UserId", userId);

        //        connection.Open();

        //        result = command.ExecuteNonQuery();

        //        connection.Close();
        //    }
        //    return result;
        //}
        public List<Party> GetParties()
        {
            List<Party> parties = new List<Party>();
            string textCommand = "SELECT * FROM tbl_Parties";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Party party = new Party
                        {
                            PartyGuid = Guid.Parse(reader["PartyGuid"].ToString()),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            EntryFee = Convert.ToDecimal(reader["EntryFee"].ToString()),
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                            PartyDate = Convert.ToDateTime(reader["PartyDate"].ToString()),
                            PartyStart = Convert.ToDateTime(reader["PartyStart"].ToString()),
                            PartyEnd = Convert.ToDateTime(reader["PartyEnd"].ToString()),
                            AddressLineOne = reader["AddressLineOne"].ToString(),
                            AddressLineTwo = reader["AddressLineTwo"].ToString(),
                            City = reader["City"].ToString(),
                            Postal = reader["Postal"].ToString(),
                            State = reader["State"].ToString(),
                        };
                        parties.Add(party);
                    }
                    connection.Close();
                }
            }
            return parties;
        }
    }
}