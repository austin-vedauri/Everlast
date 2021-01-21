using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.CRUD
{
    public class AppointmentCRUD
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Appointment Create(Appointment appointment)
        {

            string textCommand = "INSERT INTO tbl_Appointments (AppointmentGuid, MemberId, MemberGuid, ServiceId, ServiceGuid, PartyId, PartyGuid, AppointmentStart, AppomentEnd, AddressLineOne, AddressLineTwo, AddressCity, AddressState, AddressPostal) OUTPUT INSERTED.AppointmentId VALUES (@AppointmentGuid, @MemberId, @MemberGuid, @ServiceId, @ServiceGuid, @PartyId, @PartyGuid, @AppointmentStart, @AppomentEnd, @AddressLineOne, @AddressLineTwo, @AddressCity, @AddressState, @AddressPostal)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AppointmentGuid", Guid.NewGuid());

                command.Parameters.AddWithValue("@MemberId", appointment.MemberId);
                command.Parameters.AddWithValue("@MemberGuid", Guid.NewGuid());

                command.Parameters.AddWithValue("@ServiceId", appointment.ServiceId);
                command.Parameters.AddWithValue("@ServiceGuid", Guid.NewGuid());

                command.Parameters.AddWithValue("@PartyId", appointment.PartyId);
                command.Parameters.AddWithValue("@PartyGuid", Guid.NewGuid());

                command.Parameters.AddWithValue("@AppointmentStart", appointment.AppointmentStart);
                command.Parameters.AddWithValue("@AppointmentEnd", appointment.AppointmentEnd);
                command.Parameters.AddWithValue("@AddressLineOne", appointment.AddressLineOne);
                command.Parameters.AddWithValue("@AddressLineTwo", appointment.AddressLineTwo);
                command.Parameters.AddWithValue("@AddressCity", appointment.AddressCity);
                command.Parameters.AddWithValue("@AddressState", appointment.AddressState);
                command.Parameters.AddWithValue("@AddressPostal", appointment.AddressPostal);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointment.AppointmentId = Convert.ToInt32(reader["AppointmentId"].ToString());
                    }
                    connection.Close();
                }
            }

            return appointment;
        }

        public Appointment Read(Guid appointmentGuid)
        {
            Appointment appointment = new Appointment();

            string textCommand = "SELECT * FROM tbl_Appointments WHERE AppointmentGuid = AppointmentGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AppointmentGuid", appointmentGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointment.AppointmentId = Convert.ToInt32(reader["AppointmentId"].ToString());
                        appointment.AppointmentGuid = Guid.Parse(reader["AppointmentGuid"].ToString());

                        appointment.MemberId = Convert.ToInt32(reader["MemberId"].ToString());
                        appointment.MemberGuid = Guid.Parse(reader["MemberGuid"].ToString());

                        appointment.ServiceId = Convert.ToInt32(reader["ServiceId"].ToString());
                        appointment.ServiceGuid = Guid.Parse(reader["ServiceGuid"].ToString());

                        appointment.PartyId = Convert.ToInt32(reader["PartyId"].ToString());
                        appointment.PartyGuid = Guid.Parse(reader["PartyGuid"].ToString());

                        appointment.AppointmentStart = Convert.ToDateTime(reader["AppointmentStart"].ToString());
                        appointment.AppointmentEnd = Convert.ToDateTime(reader["AppointmentEnd"].ToString());
                        appointment.AddressLineOne = reader["AddressLineOne"].ToString();
                        appointment.AddressLineTwo = reader["AddressLineTwo"].ToString();
                        appointment.AddressCity = reader["AddressCity"].ToString();
                        appointment.AddressState = reader["AddressState"].ToString();
                        appointment.AddressPostal = reader["AddressPostal"].ToString();
                    }
                    connection.Close();
                }
            }
            return appointment;
        }

        public int Update(Appointment appointment)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Appointments SET AppointmentGuid = @AppointmentGuid, MemberId = @MemberId, MemberGuid = @MemberGuid, ServiceId = @ServiceId, ServiceGuid = @ServiceGuid, PartyId = @PartyId, PartyGuid = @PartyGuid, AppointmentStart = @AppointmentStart, AppomentEnd = @AppomentEnd, AddressLineOne = @AddressLineOne, AddressLineTwo = @AddressLineTwo, AddressCity = @AddressCity, AddressState = @AddressState, AddressPostal = @AddressPostal WHERE AppointmentId = @AppointmentId";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AppointmentId", appointment.AppointmentId);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }

            return result;
        }

        public int Destroy(Guid appointmentGuid)
        {
            int result = 0;

            string textCommand = "DELETE FROM tbl_Appointments WHERE AppointmentGuid = @AppointmentGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AppointmentGuid", appointmentGuid);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }

        public List<Appointment> GetAppointments()
        {
            return new List<Appointment>();
        }

        public List<Appointment> GetAppointmentsByYear(int year)
        {
            return new List<Appointment>();
        }

        public List<Appointment> GetAppointmentsByMonth(int year, int month)
        {
            return new List<Appointment>();
        }

        public List<Appointment> GetAppointmentsByDay(int year, int month, int day)
        {
            return new List<Appointment>();
        }

        public List<Appointment> GetAppointmentsByService(int serviceTypeId)
        {
            return new List<Appointment>();
        }

        public List<Appointment> GetAppointmentsByMemberId(int memberId)
        {
            return new List<Appointment>();
        }
    }
}