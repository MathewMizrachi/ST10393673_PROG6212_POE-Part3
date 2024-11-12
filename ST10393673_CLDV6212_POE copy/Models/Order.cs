
using System;

namespace ST10393673_CLDV6212_POE.Models
{
    public class Order
    {
        public string OrderId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }

        // Add these two properties to the model
        public string Status { get; set; }  // Order status (e.g., "Processing")
        public DateTime DateProcessed { get; set; }  // Date when the order was processed
    }
}
