using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Everlast.Managers
{
    public class ErrorManager
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ToString();

        public void RegisterError(string errorMessage, string methodName, Guid accountGuid)
        {
            string textCommand = "INSERT INTO tbl_Errors " +
                "(AccountGuid, ErrorMessage, MethodName, ErrorDate)" +
                " OUTPUT INSERTED.ErrorId VALUES " +
                "(@AccountGuid, @ErrorMessage, @MethodName, @ErrorDate)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(textCommand, connection);

                command.Parameters.AddWithValue("@AccountGuid", accountGuid);
                command.Parameters.AddWithValue("@ErrorMessage", errorMessage);
                command.Parameters.AddWithValue("@MethodName", methodName);
                command.Parameters.AddWithValue("@ErrorDate", DateTime.Now);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}