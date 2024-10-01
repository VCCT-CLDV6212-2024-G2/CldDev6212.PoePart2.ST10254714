using Azure.Data.Tables;
using Azure;

namespace CldDev6212.Poe.Models
{
   
        public class CustomerProfile : ITableEntity
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public DateTimeOffset? Timestamp { get; set; }
            public ETag ETag { get; set; }

            // Custom properties
            public string Name { get; set; }
            public string Email { get; set; }

            public CustomerProfile()
            {
                PartitionKey = "CustomerProfile";
                RowKey = Guid.NewGuid().ToString();
            }
        }

        public class Product : ITableEntity
        {
            public string PartitionKey { get; set; }
            public string RowKey { get; set; }
            public DateTimeOffset? Timestamp { get; set; }
            public ETag ETag { get; set; }
            public string name { get; set; }
            public string price { get; set; }
            public int stockQuantity { get; set; }




        }
    
}
