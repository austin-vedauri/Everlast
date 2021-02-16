using Everlast.Models;
using Everlast.ViewModels;
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
                "(PeriodGuid, AccountGuid, StartDate, StopDate)" +
                " OUTPUT INSERTED.PeriodId VALUES " +
                "(@PeriodGuid, @AccountGuid, @StartDate, @StopDate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PeriodGuid", periodGuid);
                command.Parameters.AddWithValue("@AccountGuid", model.AccountGuid);
                command.Parameters.AddWithValue("@StartDate", model.StartDate);
                command.Parameters.AddWithValue("@StopDate", model.StopDate);

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
                        model.StartDate = Convert.ToDateTime(reader["StartDate"].ToString());
                        model.StopDate = Convert.ToDateTime(reader["StopDate"].ToString());
                    }
                    connection.Close();
                }
            }
            return model;
        }

        public int Update(Period model)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Periods SET StartDate = @StartDate, StopDate = @StopDate WHERE PeriodGuid = @PeriodGuid AND AccountGuid = @AccountGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PeriodGuid", model.PeriodGuid);
                command.Parameters.AddWithValue("@AccountGuid", model.AccountGuid);
                command.Parameters.AddWithValue("@StartDate", model.StartDate);
                command.Parameters.AddWithValue("@StopDate", model.StopDate);

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
                            StartDate = Convert.ToDateTime(reader["StartDate"].ToString()),
                            StopDate = Convert.ToDateTime(reader["StopDate"].ToString()),
                        };
                        models.Add(model);
                    }
                }
            }
            return models;
        }

        public List<PeriodViewModel> GetWorkPeriodsForView()
        {
            List<PeriodViewModel> models = new List<PeriodViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("proc_GetWorkPeriods", connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PeriodViewModel model = new PeriodViewModel
                        {
                            PeriodGuid = Guid.Parse(reader["PeriodGuid"].ToString()),
                            AccountGuid = Guid.Parse(reader["AccountGuid"].ToString()),
                            FullName = reader["FullName"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"].ToString()),
                            StopDate = Convert.ToDateTime(reader["StopDate"].ToString()),
                        };
                        models.Add(model);
                    }
                }
            }
            return models;
        }

        public List<PeriodViewModel> SearchPeriods(DateTime? startDate = null, DateTime? endDate = null, Guid? accountGuid = null)
        {
            List<PeriodViewModel> models = new List<PeriodViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("proc_GetWorkPeriodsBySearch", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;

                if (startDate != null && endDate != null)
                {
                    command.Parameters.AddWithValue("@StartDate", (DateTime)startDate);
                    command.Parameters.AddWithValue("@EndDate", (DateTime)endDate);
                }

                if (accountGuid != null && accountGuid != Guid.Empty)
                {
                    command.Parameters.AddWithValue("@AccountGuid", (Guid)accountGuid);
                }

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PeriodViewModel model = new PeriodViewModel
                        {
                            PeriodGuid = Guid.Parse(reader["PeriodGuid"].ToString()),
                            AccountGuid = Guid.Parse(reader["AccountGuid"].ToString()),
                            FullName = reader["FullName"].ToString(),
                            StartDate = Convert.ToDateTime(reader["StartDate"].ToString()),
                            StopDate = Convert.ToDateTime(reader["StopDate"].ToString()),
                        };
                        models.Add(model);
                    }
                }
            }
            return models;
        }

        public Period GetPeriodByPeriodGuid(Guid periodGuid)
        {
            Period model = new Period();

            string textCommand = "SELECT * FROM tbl_Periods WHERE PeriodGuid = @PeriodGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@PeriodGuid", periodGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        model.PeriodGuid = Guid.Parse(reader["PeriodGuid"].ToString());
                        model.AccountGuid = Guid.Parse(reader["AccountGuid"].ToString());
                        model.StartDate = Convert.ToDateTime(reader["StartDate"].ToString());
                        model.StopDate = Convert.ToDateTime(reader["StopDate"].ToString());

                    }
                }
            }
            return model;
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
                            StartDate = Convert.ToDateTime(reader["StartDate"].ToString()),
                            StopDate = Convert.ToDateTime(reader["StopDate"].ToString()),
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
                model.StartDate.Year == date.Year &&
                model.StartDate.Month == date.Month &&
                model.StartDate.Day == date.Day
                ).ToList();
            }

            return models;
        }

        public List<Period> GetPeriodsByAccountForCurrentWeek(Guid accountGuid)
        {
            List<Period> models = GetPeriodsByAccount(accountGuid);

            if (models.Count > 0)
            {
                models = models.Where(model => model.StartDate > ManagerAssistant.GetFirstDayOfCurrentWeek()).ToList();
                models = models.Where(model => model.StartDate < ManagerAssistant.GetLastDayOfCurrentWeek()).ToList();
            }

            return models;
        }

        public List<Slot> GetAvailableAppointmentTimesForInjectorByPeriod(Guid periodGuid, Guid accountGuid, Guid serviceGuid)
        {
            List<Slot> timeslots = new List<Slot>();
            Dictionary<DateTime, TimeSpan> unavailables = new Dictionary<DateTime, TimeSpan>();
            Dictionary<DateTime, TimeSpan> availables = new Dictionary<DateTime, TimeSpan>();

            Period period = GetPeriodByPeriodGuid(periodGuid);
            Service service = new ServiceManager().Read(serviceGuid);
            TimeSpan requestedDuration = new TimeSpan(service.Hours, service.Minutes, 0);

            List<Appointment> appointments = new AppointmentManager().GetAppointmentsByPeriodGuid(periodGuid).OrderBy(model => model.AppointmentStart.Hour).ToList();

            if (appointments.Count > 0)
            {
                // build unavailable slots
                foreach (Appointment app in appointments)
                {
                    TimeSpan appDuration = app.AppointmentEnd - app.AppointmentStart;
                    unavailables.Add(app.AppointmentStart, appDuration);
                }

                if (unavailables.Count > 0)
                {
                    // build available slots

                    // calculate the time in between unavailable slots
                    // to get available slots

                    DateTime currentStartTime = period.StartDate;

                    foreach (KeyValuePair<DateTime, TimeSpan> bookedSlot in unavailables)
                    {
                        // get the difference slot based on the unavailable slot and start time
                        TimeSpan potentialSlotTime = bookedSlot.Key - currentStartTime;

                        // check if that available slot is long enough for the duration
                        if (potentialSlotTime >= requestedDuration)
                        {
                            // it's long enough, we should add it to the availables
                            availables.Add(currentStartTime, potentialSlotTime);
                        }
                        else
                        {
                            // it's not long enough, discard it's use
                        }
                    }

                    // now we can check the timespan after all of the appointments
                    // based on the last appointments end time

                    // get last appointments end time
                    DateTime lastAppointmentEndTime = unavailables.Last().Key.Add(unavailables.Last().Value);

                    // subtract it from the end of the work period to get the remaining time
                    TimeSpan remainingWorkPeriodTime = period.StopDate - lastAppointmentEndTime;

                    // we can add the remaining work period time to the availables because it's not booked
                    availables.Add(lastAppointmentEndTime, remainingWorkPeriodTime);

                    // now we can loop through the availables and build some slots

                    // set a start time to keep up with each available
                    DateTime availableStartTime = new DateTime();

                    foreach (KeyValuePair<DateTime, TimeSpan> emptyWorkPeriodSpan in availables)
                    {
                        // set the start time
                        availableStartTime = emptyWorkPeriodSpan.Key;

                        TimeSpan allowedTime = emptyWorkPeriodSpan.Value;

                        if (allowedTime >= requestedDuration)
                        {
                            // we have an empty work period span that is longer than the requested duration
                            do
                            {
                                Slot slot = new Slot
                                {
                                    PeriodGuid = period.PeriodGuid,
                                    Duration = requestedDuration,
                                    StartTime = availableStartTime,
                                    EndTime = availableStartTime.Add(requestedDuration)
                                };

                                allowedTime -= requestedDuration;

                                if (slot.StartTime >= DateTime.Now.AddMinutes(15))
                                {
                                    timeslots.Add(slot);
                                }

                            } while (allowedTime >= requestedDuration);
                        }
                    }
                }
            }
            else
            {
                // set the current start time
                DateTime currentStartTime = period.StartDate;
                // set the total time for the current work period
                TimeSpan totalPeriod = period.StopDate - period.StartDate;

                do
                {
                    // create a new slot 
                    Slot slot = new Slot
                    {
                        PeriodGuid = period.PeriodGuid,
                        Duration = requestedDuration,
                        StartTime = currentStartTime,
                        EndTime = currentStartTime.Add(requestedDuration)
                    };

                    // check if the slot's start time is later than 15 minutes from now
                    if (slot.StartTime > DateTime.Now.AddMinutes(15))
                    {
                        timeslots.Add(slot);
                    }

                    // set the start time to the next available time based on duration
                    currentStartTime = currentStartTime.Add(requestedDuration);

                    // subtract the duration from the work period
                    totalPeriod -= requestedDuration;

                } while (totalPeriod > requestedDuration);
            }
            return timeslots;
        }

    }
}