using Everlast.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.Managers
{
    public class ServiceTypeManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public ServiceType Create(ServiceType model)
        {
            int result = 0;

            Guid serviceTypeGuid = Guid.NewGuid();

            string textCommand = "INSERT INTO tbl_ServiceTypes " +
                "(ServiceTypeGuid, ServiceTypeName)" +
                " OUTPUT INSERTED.ServiceTypeId VALUES " +
                "(@ServiceTypeGuid, @ServiceTypeName)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ServiceTypeGuid", serviceTypeGuid);
                command.Parameters.AddWithValue("@ServiceTypeName", model.ServiceTypeName);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();

            }

            model.ServiceTypeGuid = serviceTypeGuid;
            return model;
        }

        public ServiceType Read(Guid serviceTypeGuid)
        {
            ServiceType model = new ServiceType();

            string textCommand = "SELECT * FROM tbl_ServiceTypes WHERE " +
                "ServiceTypeGuid = @ServiceTypeGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ServiceTypeGuid", serviceTypeGuid);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        model.ServiceTypeId = Convert.ToInt32(reader["ServiceTypeId"].ToString());
                        model.ServiceTypeGuid = Guid.Parse(reader["ServiceTypeGuid"].ToString());
                        model.ServiceTypeName = reader["ServiceTypeName"].ToString();
                    }
                    connection.Close();
                }
            }
            return model;
        }

        public int Update(ServiceType model)
        {
            int result = 0;
            string textCommand = "UPDATE tbl_ServiceTypes SET ServiceTypeName = @ServiceTypeName WHERE ServiceTypeGuid = @ServiceTypeGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ServiceTypeGuid", model.ServiceTypeGuid);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }

            return result;
        }

        public int Delete(Guid serviceTypeGuid)
        {
            int result = 0;

            string textCommand = "DELETE FROM tbl_ServiceTypes WHERE ServiceTypeGuid = @ServiceTypeGuid";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@ServiceTypeGuid", serviceTypeGuid);

                connection.Open();

                result = command.ExecuteNonQuery();

                connection.Close();
            }
            return result;
        }

        public List<ServiceType> GetServiceTypes()
        {
            List<ServiceType> models = new List<ServiceType>();

            string textCommand = "SELECT * FROM tbl_ServiceTypes";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ServiceType model = new ServiceType
                        {
                            ServiceTypeId = Convert.ToInt32(reader["ServiceTypeId"].ToString()),
                            ServiceTypeGuid = Guid.Parse(reader["ServiceTypeGuid"].ToString()),
                            ServiceTypeName = reader["ServiceTypeName"].ToString()
                        };
                        models.Add(model);
                    }
                }
            }
            return models;
        }
    }
}