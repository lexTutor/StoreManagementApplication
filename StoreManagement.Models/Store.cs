using System;

namespace StoreManagement.Models
{
    public class Store
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string Name { get; set; }
        public int TotalNumbeOfProducts { get; set; }
    }
}