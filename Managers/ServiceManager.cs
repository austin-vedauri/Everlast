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
    public class ServiceManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public Service Create(Service model)
        {
            int result = 0;

            Guid serviceGuid = Guid.NewGuid();

            string textCommand = "INSERT INTO tbl_Services " +
                "(ServiceGuid, ServiceTypeGuid, Title, Description, Price, Hours, Minutes, Active)" +
                " OUTPUT INSERTED.ServiceId VALUES " +
                "(@ServiceGuid, @ServiceTypeGuid, @Title, @Description, @Price, @Hours, @Minutes, @Active)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ServiceGuid", serviceGuid);
                command.Parameters.AddWithValue("@ServiceTypeGuid", model.ServiceTypeGuid);
                command.Parameters.AddWithValue("@Title", model.Title);
                command.Parameters.AddWithValue("@Description", model.Description);
                command.Parameters.AddWithValue("@Price", model.Price);

                command.Parameters.AddWithValue("@Hours", model.Hours);
                command.Parameters.AddWithValue("@Minutes", model.Minutes);
                command.Parameters.AddWithValue("@Active", model.Active);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();

            }

            model.ServiceGuid = serviceGuid;
            return model;
        }

        public Service Read(Guid serviceGuid)
        {
            Service model = new Service();

            string textCommand = "SELECT * FROM tbl_Services WHERE "+
                "ServiceGuid = @ServiceGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ServiceGuid", serviceGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.ServiceGuid = Guid.Parse(reader["ServiceGuid"].ToString());
                        model.ServiceTypeGuid = Guid.Parse(reader["ServiceTypeGuid"].ToString());
                        model.Title = reader["Title"].ToString();
                        model.Description = reader["Description"].ToString();
                        model.Price = Convert.ToDecimal(reader["Price"].ToString());
                        model.Hours = Convert.ToInt32(reader["Hours"].ToString());
                        model.Minutes = Convert.ToInt32(reader["Minutes"].ToString());
                        model.Active = Convert.ToBoolean(reader["Active"].ToString());
                    }
                    connection.Close();
                }
            }
            return model;
        }

        public int Update(Service model)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_Services SET ServiceTypeGuid = @ServiceTypeGuid, Title = @Title, Description = @Description, Price = @Price, Hours = @Hours, Minutes = @Minutes, Active = @Active WHERE ServiceGuid = @ServiceGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ServiceGuid", model.ServiceGuid);
                command.Parameters.AddWithValue("@ServiceTypeGuid", model.ServiceTypeGuid);
                command.Parameters.AddWithValue("@Title", model.Title);
                command.Parameters.AddWithValue("@Description", model.Description);
                command.Parameters.AddWithValue("@Price", model.Price);
                command.Parameters.AddWithValue("@Hours", model.Hours);
                command.Parameters.AddWithValue("@Minutes", model.Minutes);
                command.Parameters.AddWithValue("@Active", model.Active);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }

            return result;
        }

        public int Delete(Guid serviceGuid)
        {
            int result = 0;

            string textCommand = "DELETE FROM tbl_Services WHERE ServiceGuid = @ServiceGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ServiceGuid", serviceGuid);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }

        public List<Service> GetServices()
        {
            List<Service> models = new List<Service>();

            string textCommand = "SELECT * FROM tbl_Services";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Service model = new Service
                        {
                            ServiceGuid = Guid.Parse(reader["ServiceGuid"].ToString()),
                            ServiceTypeGuid = Guid.Parse(reader["ServiceTypeGuid"].ToString()),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            Hours = Convert.ToInt32(reader["Hours"].ToString()),
                            Minutes = Convert.ToInt32(reader["Minutes"].ToString()),
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                    };
                        models.Add(model);
                    }
                }
            }
            return models;
        }

        public List<ServiceViewModel> GetServicesWithServiceTypeName()
        {
            List<ServiceViewModel> models = new List<ServiceViewModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("proc_GetAllServicesWithServiceTypeName", connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServiceViewModel model = new ServiceViewModel
                        {
                            ServiceGuid = Guid.Parse(reader["ServiceGuid"].ToString()),
                            ServiceTypeGuid = Guid.Parse(reader["ServiceTypeGuid"].ToString()),
                            ServiceTypeName = reader["ServiceTypeName"].ToString(),
                            Title = reader["Title"].ToString(),
                            Description = reader["Description"].ToString(),
                            Price = Convert.ToDecimal(reader["Price"].ToString()),
                            Hours = Convert.ToInt32(reader["Hours"].ToString()),
                            Minutes = Convert.ToInt32(reader["Minutes"].ToString()),
                            Active = Convert.ToBoolean(reader["Active"].ToString()),
                        };
                        models.Add(model);
                    }
                }
            }
            return models;
        }
    }
}