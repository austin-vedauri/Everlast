using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.Managers
{
    public class PeriodManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Period Create(Period model)
        {
            Guid periodGuid = Guid.NewGuid();

            string textCommand = "INSERT INTO tbl_Periods " +
                "(PeriodGuid, AccountGuid, Start, Stop)" +
                " OUTPUT INSERTED.PeriodId VALUES " +
                "(@PeriodGuid, @AccountGuid, @Start, @Stop)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PeriodGuid", periodGuid);
                command.Parameters.AddWithValue("@AccountGuid", model.AccountGuid);
                command.Parameters.AddWithValue("@Start", model.Start);
                command.Parameters.AddWithValue("@Stop", model.Stop);

                connection.Open();

                command.ExecuteNonQuery();

                connection.Close();

            }
            model.PeriodGuid = periodGuid;

            return model;
        }

        public Period Read(Guid periodGuid, Guid accountGuid)
        {
            Period model = new Period();

            string textCommand = "SELECT * FROM tbl_Periods WHERE PeriodGuid = @PeriodGuid AND AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PeriodGuid", periodGuid);
                command.Parameters.AddWithValue("@AccountGuid", accountGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.PeriodGuid = Guid.Parse(reader["PeriodGuid"].ToString());
                        model.AccountGuid = Guid.Parse(reader["AccountGuid"].ToString());
                        model.Start = Convert.ToDateTime(reader["Start"].ToString());
                        model.Stop = Convert.ToDateTime(reader["Stop"].ToString());
                    }
                    connection.Close();
                }
            }
            return model;
        }

        public int Update(Period model)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Periods SET Start = @Start, Stop = @Stop WHERE PeriodGuid = @PeriodGuid AND AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PeriodGuid", model.PeriodGuid);
                command.Parameters.AddWithValue("@AccountGuid", model.AccountGuid);
                command.Parameters.AddWithValue("@Start", model.Start);
                command.Parameters.AddWithValue("@Stop", model.Stop);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }

            return result;
        }

        public int Delete(Guid periodGuid, Guid accountGuid)
        {
            int result = 0;

            string textCommand = "DELETE FROM tbl_Periods WHERE PeriodGuid = @PeriodGuid AND AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PeriodGuid", periodGuid);
                command.Parameters.AddWithValue("@AccountGuid", accountGuid);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }

        public List<Period> GetPeriods()
        {
            List<Period> models = new List<Period>();

            string textCommand = "SELECT * FROM tbl_Periods";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Period model = new Period
                        {
                            PeriodGuid = Guid.Parse(reader["PeriodGuid"].ToString()),
                            AccountGuid = Guid.Parse(reader["AccountGuid"].ToString()),
                            Start = Convert.ToDateTime(reader["Start"].ToString()),
                            Stop = Convert.ToDateTime(reader["Stop"].ToString()),
                        };
                        models.Add(model);
                    }
                }
            }
            return models;
        }

        public List<Period> GetPeriodsByAccount(Guid accountGuid)
        {
            List<Period> models = new List<Period>();

            string textCommand = "SELECT * FROM tbl_Periods WHERE AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AccountGuid", accountGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Period model = new Period
                        {
                            PeriodGuid = Guid.Parse(reader["PeriodGuid"].ToString()),
                            AccountGuid = Guid.Parse(reader["AccountGuid"].ToString()),
                            Start = Convert.ToDateTime(reader["Start"].ToString()),
                            Stop = Convert.ToDateTime(reader["Stop"].ToString()),
                        };
                        models.Add(model);
                    }
                }
            }
            return models;
        }

        public List<Period> GetPeriodsByAccountForDate(DateTime date, Guid accountGuid)
        {
            List<Period> models = GetPeriodsByAccount(accountGuid);

            if (models.Count > 0)
            {
                models = models.Where(model =>
                model.Start.Year == date.Year &&
                model.Start.Month == date.Month &&
                model.Start.Day == date.Day
                ).ToList();
            }

            return models;
        }

        public List<Period> GetPeriodsByAccountForCurrentWeek(Guid accountGuid)
        {
            List<Period> models = GetPeriodsByAccount(accountGuid);

            if (models.Count > 0)
            {
                models = models.Where(model => model.Start > ManagerAssistant.GetFirstDayOfCurrentWeek()).ToList();
                models = models.Where(model => model.Start < ManagerAssistant.GetLastDayOfCurrentWeek()).ToList();
            }

            return models;
        }

        public List<Slot> GetAvailableAppointmentTimesForInjectorOnDate(DateTime date, Guid accountGuid, Guid serviceGuid)
        {
            List<Period> periods = new List<Period>();
            Service service = new Service();
            List<Slot> timeslots = new List<Slot>();
            List<Appointment> appointments = new List<Appointment>();
            List<TimeSpan> potentials = new List<TimeSpan>();
            Dictionary<DateTime, TimeSpan> unavailables = new Dictionary<DateTime, TimeSpan>();

            //periods = GetPeriodsByAccountForDate(date, accountGuid);
            periods = GetPeriodsByAccountForCurrentWeek(accountGuid);
            appointments = new AppointmentManager().GetAppointmentsByInjectorForDate(accountGuid, date);
            service = new ServiceManager().Read(serviceGuid);
            TimeSpan requestedDuration = new TimeSpan(service.Hours, service.Minutes, 0);

            foreach (Period period in periods)
            {
                List<Appointment> bookedApps = appointments.Where(app => app.PeriodGuid == period.PeriodGuid).OrderBy(model => model.AppointmentStart.Hour).ToList();

                if (bookedApps.Count > 0)
                {
                    foreach (Appointment app in bookedApps)
                    {
                        // what is the duration of the appointment?
                        TimeSpan bookedAppointmentDuration = app.AppointmentEnd - app.AppointmentStart;

                        // this appointment creates an unavailable moment
                        // add whenever the moment starts and how long it is
                        unavailables.Add(app.AppointmentStart, bookedAppointmentDuration);
                    }

                    // now, we see when we ARE available
                    DateTime currentStartTime = period.Start;
                    // go thru each unavailable moment
                    foreach (KeyValuePair<DateTime, TimeSpan> unavailableMoment in unavailables)
                    {
                        Slot slot = new Slot();
                        // think about a timeline
                        // think about measuring the first unavailable timespan
                        // get the before time..

                        // this is my available moment
                        TimeSpan availableSpan = unavailableMoment.Key - currentStartTime;
                        slot.StartTime = currentStartTime;
                        // add that available moment to a list of moments
                        slot.Duration = availableSpan;

                        // this is how i get my next start time
                        DateTime endOfUnavailableMoment = currentStartTime.Add(unavailableMoment.Value);
                        currentStartTime = endOfUnavailableMoment;

                        slot.PeriodGuid = period.PeriodGuid;
                        potentials.Add(availableSpan);

                        timeslots.Add(slot);
                    }

                }
                else
                {
                    DateTime currentStartTime = period.Start;
                    TimeSpan totalPeriod = period.Stop - period.Start;
                    do
                    {
                        Slot slot = new Slot
                        {
                            PeriodGuid = period.PeriodGuid,
                            Duration = requestedDuration,
                            StartTime = currentStartTime,
                            EndTime = currentStartTime.Add(requestedDuration)
                        };

                        if (slot.StartTime > DateTime.Now.AddHours(requestedDuration.Hours).AddMinutes(requestedDuration.Minutes))
                        {
                            timeslots.Add(slot);
                        }

                        currentStartTime = currentStartTime.Add(requestedDuration);

                        totalPeriod -= requestedDuration;

                    } while (totalPeriod > requestedDuration);
                }
            }
            return timeslots;
        }

    }
}