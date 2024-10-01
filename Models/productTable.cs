using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CldDev6212.Poe.Models
{

    public class productTable
    {
        public static string con_string = "Server=tcp:clouddevone.database.windows.net,1433;Initial Catalog=ST10254714CldDev;Persist Security Info=False;User ID=Caleb;Password=Liverpoolfour100;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        

        public static SqlConnection con = new SqlConnection(con_string);

        public string name { get; set; }

        public string price { get; set; }

        public string category { get; set; }

        //public string avaliability { get; set; }



        public int insert_product(productTable p)
        {
/*
            DECLARE @Name NVARCHAR(MAX) = @pName;
            DECLARE @Price DECIMAL(18, 2) = @pPrice;
            DECLARE @Category NVARCHAR(MAX) = @pCategory;
            DECLARE @Availability BIT = @pAvailability;
*/


        /*
            try
            {
                string sql = "INSERT INTO productTable (name, price, category) VALUES (@Name, @Price, @Category);";
                    

                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@Name", p.name);
                cmd.Parameters.AddWithValue("@Price", p.price);
                cmd.Parameters.AddWithValue("@Category", p.category);
                //cmd.Parameters.AddWithValue("@Avaliability", p.avaliability);

                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                con.Close();

                return rowsAffected;
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                Console.WriteLine("Error: " + ex.Message);
                return -1; // or any other appropriate value to indicate failure
            } */
            
            string sql = "INSERT INTO dbo.productTable (name, price, category, availability) VALUES (@Name, @Price, @Category, @Availability)";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.Parameters.AddWithValue("@Name", p.name);
            cmd.Parameters.AddWithValue("@Price", p.price);
            cmd.Parameters.AddWithValue("@Category", p.category);
           // cmd.Parameters.AddWithValue("@Availability", p.availability);
            con.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            con.Close();
            return rowsAffected; 
        }
            
   
        }

        



    public class ProductService
    {
        public List<productTable> GetAllProducts()
        {
            var products = new List<productTable>();
            using (SqlConnection con = new SqlConnection(productTable.con_string))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM productTable", con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new productTable
                            {
                                name = reader.GetString(reader.GetOrdinal("Name")),
                                price = reader.GetString(reader.GetOrdinal("Price")),
                                category = reader.GetString(reader.GetOrdinal("Category")),
                                //avaliability = reader.GetString(reader.GetOrdinal("Availability"))
                            });
                        }
                    }
                }
            }
            return products;
        }
    }
}
