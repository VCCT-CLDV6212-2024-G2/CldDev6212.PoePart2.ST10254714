using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CldDev6212.St10254714.Poe.S2.Models
{
    public class Logincntrl
    {
        public static string con_string = "Server=tcp:clouddevone.database.windows.net,1433;Initial Catalog=ST10254714CldDev;Persist Security Info=False;User ID=Caleb;Password=Liverpoolfour100;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        


        public int SelectUser(string userEmail, string userName)
        {
            int userId = -1; 
            using (SqlConnection con = new SqlConnection(con_string))
            {
                string sql = "SELECT userID FROM dbo.userTable WHERE userEmail = @userEmail AND userName = @userName";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@userEmail", userEmail);
                cmd.Parameters.AddWithValue("@userName", userName);
                try
                {
                    con.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        userId = Convert.ToInt32(result);
                    }
                }
                catch (Exception ex)
                {
                    
                    throw ex;
                }
            }
            return userId;
        }

    }
}
