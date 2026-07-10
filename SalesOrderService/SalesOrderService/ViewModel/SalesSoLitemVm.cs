namespace SalesOrderService.ViewModel
{
    public class SalesSoLitemVm
    {
        public int SalesSoLitemId { get; set; }
        public int SalesSoId { get; set; }

        public string ItemName { get; set; } = null!;

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}
