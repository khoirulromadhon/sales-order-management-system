using SalesOrderService.Models;

namespace SalesOrderService.ViewModel
{
    public class SalesSoVm
    {
        public int SalesSoId { get; set; }

        public string SoNo { get; set; } = null!;

        public DateTime OrderDate { get; set; }

        public int ComCustomerId { get; set; }

        public string? Address { get; set; }

        public virtual ICollection<SalesSoLitemVm> SalesSoLitems { get; set; } = new List<SalesSoLitemVm>();
    }
}
