using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Everlast.Models;
using Everlast.ViewModels;

namespace Everlast.Managers
{
    public class AppointmentManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Appointment Create(Appointment model)
        {
            Guid appointmentGuid = Guid.NewGuid();

            string textCommand = "INSERT INTO tbl_Appointments " +
                "(AppointmentGuid, ClientGuid, InjectorGuid, ServiceGuid, PeriodGuid, AppointmentStart, AppointmentEnd)" +
                " OUTPUT INSERTED.AppointmentId VALUES " +
                "(@AppointmentGuid, @ClientGuid, @InjectorGuid, @ServiceGuid, @PeriodGuid, @AppointmentStart, @AppointmentEnd)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AppointmentGuid", appointmentGuid);
                command.Parameters.AddWithValue("@ClientGuid", model.ClientGuid);
                command.Parameters.AddWithValue("@InjectorGuid", model.InjectorGuid);
                command.Parameters.AddWithValue("@ServiceGuid", model.ServiceGuid);
                command.Parameters.AddWithValue("@PeriodGuid", model.PeriodGuid);

                command.Parameters.AddWithValue("@AppointmentStart", model.AppointmentStart);
                command.Parameters.AddWithValue("@AppointmentEnd", model.AppointmentEnd);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

            }
            model.AppointmentGuid = appointmentGuid;
            return model;
        }

        public Appointment Read(Guid appointmentGuid)
        {
            Appointment appointment = new Appointment();

            string textCommand = "SELECT * FROM tbl_Appointments WHERE AppointmentGuid = @AppointmentGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AppointmentGuid", appointmentGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        appointment.AppointmentGuid = Guid.Parse(reader["AppointmentGuid"].ToString());
                        appointment.ClientGuid = Guid.Parse(reader["ClientGuid"].ToString());
                        appointment.InjectorGuid = Guid.Parse(reader["InjectorGuid"].ToString());
                        appointment.ServiceGuid = Guid.Parse(reader["ServiceGuid"].ToString());
                        appointment.PeriodGuid = Guid.Parse(reader["PeriodGuid"].ToString());

                        appointment.AppointmentStart = Convert.ToDateTime(reader["AppointmentStart"].ToString());
                        appointment.AppointmentEnd = Convert.ToDateTime(reader["AppointmentEnd"].ToString());
                    }
                    connection.Close();
                }
            }
            return appointment;
        }

        public int Update(Appointment appointment)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Appointments SET ClientGuid = @ClientGuid, ServiceGuid = @ServiceGuid, InjectorGuid = @InjectorGuid, PeriodGuid = @PeriodGuid, AppointmentStart = @AppointmentStart, AppointmentEnd = @AppointmentEnd WHERE AppointmentGuid = @AppointmentGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AppointmentGuid", appointment.AppointmentGuid);
                command.Parameters.AddWithValue("@ClientGuid", appointment.ClientGuid);
                command.Parameters.AddWithValue("@ServiceGuid", appointment.ServiceGuid);
                command.Parameters.AddWithValue("@InjectorGuid", appointment.InjectorGuid);
                command.Parameters.AddWithValue("@PeriodGuid", appointment.PeriodGuid);
                command.Parameters.AddWithValue("@AppointmentStart", appointment.AppointmentStart);
                command.Parameters.AddWithValue("@AppointmentEnd", appointment.AppointmentEnd);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }

            return result;
        }

        public int Delete(Guid appointmentGuid)
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

        public List<AppointmentViewModel> GetScheduledAppointmentsForView()
        {
            List<AppointmentViewModel> models = new List<AppointmentViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("proc_GetScheduledAppointments", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AppointmentViewModel model = new AppointmentViewModel
                        {
                            AppointmentGuid = Guid.Parse(reader["AppointmentGuid"].ToString()),
                            ClientName = reader["ClientName"].ToString(),
                            InjectorName = reader["InjectorName"].ToString(),
                            Title = reader["Title"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            AppointmentStart = Convert.ToDateTime(reader["AppointmentStart"].ToString()),
                            AppointmentEnd = Convert.ToDateTime(reader["AppointmentEnd"].ToString()),
                        };
                        models.Add(model);
                    }
                }
            }
            return models;
        }

        public AppointmentViewModel GetScheduledAppointmentByAppointmentGuidForView(Guid appointmentGuid)
        {
            AppointmentViewModel model = new AppointmentViewModel();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("proc_GetScheduledAppointmentByAppointmentGuid", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@AppointmentGuid", appointmentGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        model.AppointmentGuid = Guid.Parse(reader["AppointmentGuid"].ToString());
                        model.ClientName = reader["ClientName"].ToString();
                        model.InjectorName = reader["InjectorName"].ToString();
                        model.Title = reader["Title"].ToString();
                        model.Price = Convert.ToDecimal(reader["Price"].ToString());
                        model.AppointmentStart = Convert.ToDateTime(reader["AppointmentStart"].ToString());
                        model.AppointmentEnd = Convert.ToDateTime(reader["AppointmentEnd"].ToString());

                    }
                }
            }
            return model;
        }

        public List<Appointment> GetAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();

            string textCommand = "SELECT * FROM tbl_Appointments";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Appointment appointment = new Appointment
                        {
                            AppointmentGuid = Guid.Parse(reader["AppointmentGuid"].ToString()),
                            ClientGuid = Guid.Parse(reader["ClientGuid"].ToString()),
                            InjectorGuid = Guid.Parse(reader["InjectorGuid"].ToString()),
                            ServiceGuid = Guid.Parse(reader["ServiceGuid"].ToString()),
                            PeriodGuid = Guid.Parse(reader["PeriodGuid"].ToString()),

                            AppointmentStart = Convert.ToDateTime(reader["AppointmentStart"].ToString()),
                            AppointmentEnd = Convert.ToDateTime(reader["AppointmentEnd"].ToString()),
                        };
                        appointments.Add(appointment);
                    }
                }
            }
            return appointments;
        }

        public List<Appointment> GetAppointmentsByYear(int year)
        {
            List<Appointment> appointments = GetAppointments().Where(m => m.AppointmentStart.Year == year).ToList();
            return appointments;
        }

        public List<Appointment> GetAppointmentsByMonth(int year, int month)
        {
            List<Appointment> appointments = GetAppointments().Where(m => m.AppointmentStart.Year == year && m.AppointmentStart.Month == month).ToList();
            return appointments;
        }

        public List<Appointment> GetAppointmentsByDay(int year, int month, int day)
        {
            List<Appointment> appointments = GetAppointments().Where(m => m.AppointmentStart.Year == year && m.AppointmentStart.Month == month && m.AppointmentStart.Day == day).ToList();
            return appointments;
        }

        public List<Appointment> GetAppointmentsByServiceGuid(Guid serviceGuid)
        {
            List<Appointment> appointments = GetAppointments().Where(m => m.ServiceGuid == serviceGuid).ToList();
            return appointments;
        }

        public List<Appointment> GetAppointmentsByInjectorGuid(Guid injectorGuid)
        {
            List<Appointment> appointments = GetAppointments().Where(m => m.InjectorGuid == injectorGuid).ToList();
            return appointments;
        }

        public List<Appointment> GetAppointmentsByClientGuid(Guid clientGuid)
        {
            List<Appointment> appointments = GetAppointments().Where(m => m.ClientGuid == clientGuid).ToList();
            return appointments;
        }

        public List<Appointment> GetAppointmentsByPeriodGuid(Guid periodGuid)
        {
            List<Appointment> appointments = GetAppointments().Where(m => m.PeriodGuid == periodGuid).ToList();
            return appointments;
        }

        public List<Appointment> GetAppointmentsByPeriodGuidForToday(Guid periodGuid)
        {
            List<Appointment> appointments = GetAppointments().Where(m =>
            m.PeriodGuid == periodGuid &&
            m.AppointmentStart.Year == DateTime.Now.Year &&
            m.AppointmentStart.DayOfYear == DateTime.Now.DayOfYear
            ).ToList();

            return appointments;
        }

        public List<Appointment> GetAppointmentsByPeriodGuidForCurrentWeek(Guid periodGuid)
        {
            List<Appointment> models = GetAppointments();

            if (models.Count > 0)
            {
                models = models.Where(model => model.PeriodGuid == periodGuid).ToList();
                models = models.Where(model => model.AppointmentStart > ManagerAssistant.GetFirstDayOfCurrentWeek()).ToList();
                models = models.Where(model => model.AppointmentEnd < ManagerAssistant.GetLastDayOfCurrentWeek()).ToList();
            }

            return models;
        }

        public List<Appointment> GetAppointmentsByInjectorForDate(Guid injectorGuid, DateTime date)
        {
            // get appointments for the injector
            List<Appointment> models = GetAppointmentsByInjectorGuid(injectorGuid);
            // get the appointments specifically for today
            models = models.Where(model =>
            model.AppointmentStart.Year == date.Year &&
            model.AppointmentStart.DayOfYear == date.DayOfYear
            ).ToList();

            return models;
        }
    }
}