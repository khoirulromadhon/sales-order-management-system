using CustomerService.Models;

namespace CustomerService.Service
{
    public class GetCustomerService
    {
        private readonly SalesOrderManagementContext _context;

        public GetCustomerService(SalesOrderManagementContext context)
        {
            _context = context;
        }

        public List<ComCustomer> GetAllCustomers()
        {
            List<ComCustomer> customers = _context.ComCustomers.ToList();
            return customers;
        }
    }
}
