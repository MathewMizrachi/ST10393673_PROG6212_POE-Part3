using Azure;
using Azure.Data.Tables;
using System;

namespace ST10393673_CLDV6212_POE.Models
{
    public class RegisterViewModel : ITableEntity
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        // Registration properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }

        public RegisterViewModel()
        {
            PartitionKey = "UserProfiles";
            RowKey = Guid.NewGuid().ToString();
        }
    }
}
