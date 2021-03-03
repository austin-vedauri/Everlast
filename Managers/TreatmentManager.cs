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
    public class TreatmentManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Treatment Create(Treatment model)
        {
            Guid treatmentGuid = Guid.NewGuid();

            int result = 0;
            string textCommand = "INSERT INTO tbl_Treatments " +
                " (TreatmentGuid, ServiceGuid, TreatmentName, TreatmentDescription) " +
                "OUTPUT INSERTED.TreatmentId VALUES " +
                "(@TreatmentGuid, @ServiceGuid, @TreatmentName, @TreatmentDescription)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@TreatmentGuid", treatmentGuid);
                command.Parameters.AddWithValue("@ServiceGuid", model.ServiceGuid);
                command.Parameters.AddWithValue("@TreatmentName", model.TreatmentName);
                command.Parameters.AddWithValue("@TreatmentDescription", model.TreatmentDescription);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        result = Convert.ToInt32(reader["TreatmentId"].ToString());
                    }
                    connection.Close();
                }
            }

            return model;
        }
        public Treatment Read(Guid treatmentGuid)
        {
            Treatment model = new Treatment();

            string textCommand = "SELECT * FROM tbl_Treatments WHERE TreatmentGuid = @TreatmentGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@TreatmentGuid", treatmentGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.TreatmentGuid = Guid.Parse(reader["TreatmentGuid"].ToString());
                        model.ServiceGuid = Guid.Parse(reader["ServiceGuid"].ToString());
                        model.TreatmentName = reader["TreatmentName"].ToString();
                        model.TreatmentDescription = reader["TreatmentDescription"].ToString();

                    }
                    connection.Close();
                }
            }
            return model;
        }
        public Treatment Update(Treatment model)
        {
            int result = 0;

            string textCommand = "UPDATE tbl_Treatments SET " +
                "TreatmentGuid = @TreatmentGuid, ServiceGuid = @ServiceGuid, TreatmentName = @TreatmentName, TreatmentDescription = @TreatmentDescription" +
                " WHERE TreatmentGuid = @TreatmentGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@TreatmentGuid", model.TreatmentGuid);
                command.Parameters.AddWithValue("@ServiceGuid", model.ServiceGuid);
                command.Parameters.AddWithValue("@TreatmentName", model.TreatmentName);
                command.Parameters.AddWithValue("@TreatmentDescription", model.TreatmentDescription);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return model;
        }
        public int Destroy(Guid treatmentGuid)
        {
            int result = 0;

            string textCommand = "DELETE FROM tbl_Treatments WHERE TreatmentGuid = @TreatmentGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@TreatmentGuid", treatmentGuid);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }
        public List<Treatment> GetTreatments()
        {
            List<Treatment> models = new List<Treatment>();

            string textCommand = "SELECT * FROM tbl_Treatments";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Treatment model = new Treatment
                        {
                            TreatmentGuid = Guid.Parse(reader["TreatmentGuid"].ToString()),
                            ServiceGuid = Guid.Parse(reader["ServiceGuid"].ToString()),
                            TreatmentName = reader["TreatmentName"].ToString(),
                            TreatmentDescription = reader["TreatmentDescription"].ToString()
                        };
                        models.Add(model);
                    }
                    connection.Close();
                }
            }
            return models;
        }
        public Treatment GetTreatmentByServiceGuid(Guid serviceGuid)
        {
            Treatment model = new Treatment();

            string textCommand = "SELECT * FROM tbl_Treatments WHERE ServiceGuid = @ServiceGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);
                command.Parameters.AddWithValue("@ServiceGuid", serviceGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.TreatmentGuid = Guid.Parse(reader["TreatmentGuid"].ToString());
                        model.ServiceGuid = Guid.Parse(reader["ServiceGuid"].ToString());
                        model.TreatmentName = reader["TreatmentName"].ToString();
                        model.TreatmentDescription = reader["TreatmentDescription"].ToString();
                    }
                    connection.Close();
                }
            }
            return model;
        }
        public List<TreatmentViewModel> GetAllTreatmentsWithServiceName()
        {
            List<TreatmentViewModel> models = new List<TreatmentViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("proc_GetAllTreatmentsWithServiceName", connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TreatmentViewModel model = new TreatmentViewModel
                        {
                            TreatmentGuid = Guid.Parse(reader["TreatmentGuid"].ToString()),
                            ServiceGuid = Guid.Parse(reader["ServiceGuid"].ToString()),
                            ServiceName = reader["ServiceName"].ToString(),
                            TreatmentName = reader["TreatmentName"].ToString(),
                            TreatmentDescription = reader["TreatmentDescription"].ToString()
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