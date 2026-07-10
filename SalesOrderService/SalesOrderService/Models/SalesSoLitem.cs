using System;
using System.Collections.Generic;

namespace SalesOrderService.Models;

public partial class SalesSoLitem
{
    public int SalesSoLitemId { get; set; }

    public int SalesSoId { get; set; }

    public string ItemName { get; set; } = null!;

    public int Quantity { get; set; }

    public double Price { get; set; }

    public virtual SalesSo SalesSo { get; set; } = null!;
}
