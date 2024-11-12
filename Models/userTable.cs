using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata.Ecma335;
using System.Collections.Generic;
using System.Xml.Linq;
using Azure.Data.Tables;

namespace CldDev6212.St10254714.Poe.S2.Models
{
    public class userTable
    {

        private readonly string _conString;
        /*public static string con_string = "Server=tcp:clouddevone.database.windows.net,1433;Initial Catalog=ST10254714CldDev;Persist Security Info=False;User ID=Caleb;Password=Liverpoolfour100;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        
        public static SqlConnection con = new SqlConnection(con_string);
        
        */
        public userTable(IConfiguration configuration)
        {
            _conString = configuration.GetConnectionString("ST10254714CldDev");
        }

        


        public string userName { get; set; }

        public string userEmail { get; set; }





        public int insert_User(userTable m)
        {
            

            string sql = "INSERT INTO userTable (userName, userEmail) VALUES (@UserName, @Email)";
            using (SqlConnection con = new SqlConnection(_conString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, con);

                    cmd.Parameters.AddWithValue("@UserName", m.userName);
                    cmd.Parameters.AddWithValue("@Email", m.userEmail);

                    con.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    con.Close();

                    return rowsAffected;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

          
        }
        /*private string GetAzureConnectionString()
        {
           
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Retrieve the connection string from configuration
            string connectionString = config.GetConnectionString("ST10254714CldDev");

            return connectionString;
        }*/
        
    }
}
