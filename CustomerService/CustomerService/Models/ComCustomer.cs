using System;
using System.Collections.Generic;

namespace CustomerService.Models;

public partial class ComCustomer
{
    public int ComCustomerId { get; set; }

    public string CustomerName { get; set; } = null!;
}
