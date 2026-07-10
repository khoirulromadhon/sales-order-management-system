using System;
using System.Collections.Generic;

namespace Front_End.Pages.Models
{
    public class SalesOrder
    {
        public int Id { get; set; }
        public string? SoNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public List<SalesOrderDetail> Details { get; set; } = new();
    }

    public class SalesOrderDetail
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
