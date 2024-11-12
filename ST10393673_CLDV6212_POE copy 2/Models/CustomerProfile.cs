
// EF Core Version - For Relational Database (e.g., SQL Server)
// This version is designed to work with Entity Framework Core for interacting with a relational database.
// It uses the [Key] attribute to mark the primary key, and it maps to a typical relational database table schema.


using System;
using System.ComponentModel.DataAnnotations;

namespace ST10393673_CLDV6212_POE.Models
{
    public class CustomerProfile
    {
        [Key]
        public string CustomerId { get; set; } // Primary Key for EF Core
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}





// Azure Table Storage Version - For NoSQL Storage (Azure Table Storage)
// This version implements ITableEntity and is used for storing data in Azure Table Storage.
// It leverages PartitionKey and RowKey to store and query entities efficiently in Azure's distributed NoSQL storage.



//using Azure;
//using Azure.Data.Tables;
//using System;

//namespace ST10393673_CLDV6212_POE.Models
//{
//    public class CustomerProfile : ITableEntity
//    {
//        public string PartitionKey { get; set; }
//        public string RowKey { get; set; }
//        public DateTimeOffset? Timestamp { get; set; }
//        public ETag ETag { get; set; }

//        // Custom properties
//        public string FirstName { get; set; }
//        public string LastName { get; set; }
//        public string Email { get; set; }
//        public string PhoneNumber { get; set; }

//        public CustomerProfile()
//        {
//            PartitionKey = "CustomerProfiles";
//            RowKey = Guid.NewGuid().ToString();
//        }
//    }
//}
