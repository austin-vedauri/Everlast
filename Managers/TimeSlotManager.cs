using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.Managers
{
    public class TimeSlotManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public TimeSlot Create(TimeSlot model)
        {
            Guid timeSlotGuid = Guid.NewGuid();

            string textCommand = "INSERT INTO tbl_TimeSlots " +
                "(TimeSlotGuid, AccountGuid, TimeSlotStart, TimeSlotEnd, Available)" +
                " OUTPUT INSERTED.TimeSlotId VALUES " +
                "(@sTimeSlotGuid, @AccountGuid, @TimeSlotStart, @TimeSlotEnd, @Available)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@TimeSlotGuid", timeSlotGuid);
                command.Parameters.AddWithValue("@AccountGuid", model.AccountGuid);
                command.Parameters.AddWithValue("@TimeSlotStart", model.TimeSlotStart);
                command.Parameters.AddWithValue("@TimeSlotEnd", model.TimeSlotEnd);
                command.Parameters.AddWithValue("@Available", model.Available);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

            }
            model.TimeSlotGuid = timeSlotGuid;
            
            return model;
        }

        public TimeSlot Read(Guid timeSlotGuid, Guid accountGuid)
        {
            TimeSlot model = new TimeSlot();

            string textCommand = "SELECT * FROM tbl_TimeSlots WHERE TimeSlotGuid = @TimeSlotGuid AND AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@TimeSlotGuid", timeSlotGuid);
                command.Parameters.AddWithValue("@AccountGuid", accountGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.TimeSlotGuid = Guid.Parse(reader["TimeSlotGuid"].ToString());
                        model.AccountGuid = Guid.Parse(reader["AccountGuid"].ToString());
                        model.TimeSlotStart = Convert.ToDateTime(reader["TimeSlotStart"].ToString());
                        model.TimeSlotEnd = Convert.ToDateTime(reader["TimeSlotEnd"].ToString());
                        model.Available = Convert.ToBoolean(reader["Available"].ToString());
                    }
                    connection.Close();
                }
            }
            return model;
        }

        public int Update(TimeSlot model)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_TimeSlots SET AccountGuid = @AccountGuid, TimeSlotStart = @TimeSlotStart, TimeSlotEnd = @TimeSlotEnd, Available = @Available WHERE TimeSlotGuid = @TimeSlotGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@TimeSlotGuid", model.TimeSlotGuid);
                command.Parameters.AddWithValue("@AccountGuid", model.AccountGuid);
                command.Parameters.AddWithValue("@TimeSlotStart", model.TimeSlotStart);
                command.Parameters.AddWithValue("@TimeSlotEnd", model.TimeSlotEnd);
                command.Parameters.AddWithValue("@Available", model.Available);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }

            return result;
        }

        public int Delete(Guid timeSlotGuid, Guid accountGuid)
        {
            int result = 0;

            string textCommand = "DELETE FROM tbl_TimeSlots WHERE TimeSlotGuid = @TimeSlotGuid AND AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@TimeSlotGuid", timeSlotGuid);
                command.Parameters.AddWithValue("@AccountGuid", accountGuid);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }

        public List<TimeSlot> GetTimeSlots()
        {
            List<TimeSlot> models = new List<TimeSlot>();

            string textCommand = "SELECT * FROM tbl_TimeSlots";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TimeSlot model = new TimeSlot
                        {
                            TimeSlotGuid = Guid.Parse(reader["TimeSlotGuid"].ToString()),
                            AccountGuid = Guid.Parse(reader["AccountGuid"].ToString()),
                            TimeSlotStart = Convert.ToDateTime(reader["TimeSlotStart"].ToString()),
                            TimeSlotEnd = Convert.ToDateTime(reader["TimeSlotEnd"].ToString()),
                            Available = Convert.ToBoolean(reader["Available"].ToString()),
                        };
                        models.Add(model);
                    }
                }
            }
            return models;
        }

        public List<TimeSlot> GetTimeSlotsByAccount(Guid accountGuid)
        {
            List<TimeSlot> models = new List<TimeSlot>();

            string textCommand = "SELECT * FROM tbl_TimeSlots WHERE AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AccountGuid", accountGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TimeSlot model = new TimeSlot
                        {
                            TimeSlotGuid = Guid.Parse(reader["TimeSlotGuid"].ToString()),
                            AccountGuid = Guid.Parse(reader["AccountGuid"].ToString()),
                            TimeSlotStart = Convert.ToDateTime(reader["TimeSlotStart"].ToString()),
                            TimeSlotEnd = Convert.ToDateTime(reader["TimeSlotEnd"].ToString()),
                            Available = Convert.ToBoolean(reader["Available"].ToString()),
                        };
                        models.Add(model);
                    }
                }
            }
            return models;
        }

        public List<TimeSlot> GetAvailabilityForAccountDaily(Guid accountGuid)
        {
            List<TimeSlot> models = GetTimeSlotsByAccount(accountGuid);
            // get availables
            models = models.Where(model => model.Available == true).ToList();
            // get this years
            models = models.Where(model => model.TimeSlotStart.Year == DateTime.Now.Year).ToList();
            // get this months
            models = models.Where(model => model.TimeSlotStart.Month == DateTime.Now.Month).ToList();
            // get todays
            models = models.Where(model => model.TimeSlotStart.Day == DateTime.Now.Day).ToList();
            

            return models;
        }

        public List<TimeSlot> GetAvailabilityForAccountWeekly(Guid accountGuid)
        {
            List<TimeSlot> models = GetTimeSlotsByAccount(accountGuid);
            // get availables
            models = models.Where(model => model.Available == true).ToList();
            // get this years
            models = models.Where(model => model.TimeSlotStart.Year == DateTime.Now.Year).ToList();
            // get this months
            models = models.Where(model => model.TimeSlotStart.Month == DateTime.Now.Month).ToList();
            // get todays
            models = models.Where(model => model.TimeSlotStart.Day == DateTime.Now.Day).ToList();

            return models;
        }
    }
}

/*
 * 
 * 1. Have dropdowns for the user to select an appointment time
 * 2. Have the appointment times in 15 minute increments
 * 3. Want to automatically deduct appointment times from the injectors work period
 * 4. 
 * 
 * 
 * Work Period - 1pm to 5pm
 * Appointment - 1pm to 2pm
 * 
 * Make the Work Period display from 2pm to 5pm
 * 
 * THIS IS TO SET THE WORK PERIOD
 * 
 * have 1 work period record
 * tie the appointments to the work period record
 * pull all appointments related to the work period record
 * subtract the appointment times from the work period
 * 
 * tie appointment into period and remove the appointment duration from the period where the 
 * start time matches on the period time
 * 
 * THIS IS TO GET THE WORK PERIOD
 * 
 * let the user select an available day that the injector has set
 * 
 * next, show them the time the injector is available
 * 
 * to get the available injector times, pull that work period
 * and then pull all appointments related to that work period
 * 
 * then, subtract the current work periods from their time line
 * 
 * have a dropdown for which day
 * 
 * after selecting a day,
 * 
 * have a dropdown for the appointment times in 15 minute increments
 * 
 * BE SURE to not include available times that will overlap in duration
 * 
 * 
 * 
 */