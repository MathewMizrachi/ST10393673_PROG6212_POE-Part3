﻿using System.ComponentModel.DataAnnotations;

namespace ST10393673_CLDV6212_POE.Models
{
    public class Product
    {
        [Key]
        public string ProductId { get; set; } // Primary Key for EF Core
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductImageUrl { get; set; }
    }
}





// Below is How to Use this Product Class with A Micosoft Storage Container Instead of a Database 

//using Azure;
//using Azure.Data.Tables;
//using System;

//namespace ST10393673_CLDV6212_POE.Models
//{
//    public class Product : ITableEntity
//    {
//        // ITableEntity properties
//        public string PartitionKey { get; set; }  // Typically used to organize entities in partitions
//        public string RowKey { get; set; }  // Unique identifier for the product (used by Azure Table Storage)
//        public DateTimeOffset? Timestamp { get; set; }  // Automatically managed by Azure Table Storage
//        public ETag ETag { get; set; }  // Automatically managed by Azure Table Storage

//        // Product-specific properties
//        public string ProductId { get; set; } // Logical product ID for your application
//        public string ProductName { get; set; } // Product name
//        public string ProductDescription { get; set; } // Product description
//        public decimal ProductPrice { get; set; } // Product price
//        public string ProductImageUrl { get; set; } // Product image URL

//        // Parameterless constructor for TableEntity
//        public Product() { }

//        // Constructor with PartitionKey and RowKey
//        public Product(string partitionKey, string rowKey, string productId, string name, decimal price, string description, string imageUrl)
//        {
//            PartitionKey = partitionKey;
//            RowKey = rowKey;  // RowKey used as the unique identifier for Table Storage
//            ProductId = productId;  // Keep the logical ProductId
//            ProductName = name;
//            ProductPrice = price;
//            ProductDescription = description;
//            ProductImageUrl = imageUrl;
//        }

//        // Constructor without PartitionKey and RowKey, allows generating them dynamically
//        public Product(string productId, string name, decimal price, string description, string imageUrl, string category = "defaultCategory")
//        {
//            ProductId = productId;  // Set the logical ProductId
//            ProductName = name;
//            ProductPrice = price;
//            ProductDescription = description;
//            ProductImageUrl = imageUrl;
//            PartitionKey = category ?? "defaultCategory";  // Use category if available, default to "defaultCategory"
//            RowKey = GenerateRowKey();  // Generate a unique RowKey for each product
//        }

//        // Optional: Method to help create a unique RowKey if needed
//        public static string GenerateRowKey()
//        {
//            return Guid.NewGuid().ToString();  // Generates a unique RowKey for each product
//        }
//    }
//}
