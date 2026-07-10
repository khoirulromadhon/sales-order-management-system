using System;
using System.Collections.Generic;

namespace SalesOrderService.Models;

public partial class SalesSo
{
    public int SalesSoId { get; set; }

    public string SoNo { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public int ComCustomerId { get; set; }

    public string? Address { get; set; }

    public virtual ICollection<SalesSoLitem> SalesSoLitems { get; set; } = new List<SalesSoLitem>();
}
