using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Front_End.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        // Query / paging properties
        [BindProperty(SupportsGet = true)]
        public string? Keyword { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? OrderDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Page { get; set; } = 1;

        // Strict 5 per page
        public int PageSize { get; set; } = 5;

        // Results for view
        public List<SalesOrderViewModel> Items { get; set; } = new();
        public int PageIndex { get; set; } = 1;
        public int TotalPages { get; set; } = 1;

        public void OnGet()
        {
            // Static sample data
            var all = GetStaticData();

            // filter by keyword
            if (!string.IsNullOrWhiteSpace(Keyword))
            {
                var q = Keyword.Trim().ToLowerInvariant();
                all = all.Where(x => (x.SoNumber ?? "").ToLowerInvariant().Contains(q)
                    || (x.CustomerName ?? "").ToLowerInvariant().Contains(q)
                    || (x.Address ?? "").ToLowerInvariant().Contains(q)).ToList();
            }

            // filter by order date (yyyy-MM-dd expected)
            if (!string.IsNullOrWhiteSpace(OrderDate) && DateTime.TryParse(OrderDate, out var od))
            {
                all = all.Where(x => x.OrderDate.Date == od.Date).ToList();
            }

            var total = all.Count;
            TotalPages = (int)Math.Ceiling(total / (double)PageSize);
            if (TotalPages < 1) TotalPages = 1;

            PageIndex = Page < 1 ? 1 : Page > TotalPages ? TotalPages : Page;

            Items = all.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        public IActionResult OnPostDelete(int id, string soNumber)
        {
            // For static demo, just redirect back with a message (could add TempData later)
            // In real app, delete from database here.
            return RedirectToPage();
        }

        private List<SalesOrderViewModel> GetStaticData()
        {
            return new List<SalesOrderViewModel>
            {
                new SalesOrderViewModel{ Id=1, SoNumber="SO-001", OrderDate=DateTime.Today.AddDays(-1), CustomerName="Alpha", Address="Jl. Merdeka 1"},
                new SalesOrderViewModel{ Id=2, SoNumber="SO-002", OrderDate=DateTime.Today.AddDays(-2), CustomerName="Beta", Address="Jl. Sudirman 2"},
                new SalesOrderViewModel{ Id=3, SoNumber="SO-003", OrderDate=DateTime.Today, CustomerName="Gamma", Address="Jl. Thamrin 3"},
                new SalesOrderViewModel{ Id=4, SoNumber="SO-004", OrderDate=DateTime.Today, CustomerName="Delta", Address="Jl. Merpati 4"},
                new SalesOrderViewModel{ Id=5, SoNumber="SO-005", OrderDate=DateTime.Today.AddDays(-5), CustomerName="Epsilon", Address="Jl. Kenanga 5"},
                new SalesOrderViewModel{ Id=6, SoNumber="SO-006", OrderDate=DateTime.Today.AddDays(-3), CustomerName="Zeta", Address="Jl. Mawar 6"},
                new SalesOrderViewModel{ Id=7, SoNumber="SO-007", OrderDate=DateTime.Today.AddDays(-10), CustomerName="Eta", Address="Jl. Melati 7"},
                new SalesOrderViewModel{ Id=8, SoNumber="SO-008", OrderDate=DateTime.Today.AddDays(-1), CustomerName="Theta", Address="Jl. Anggrek 8"},
                new SalesOrderViewModel{ Id=9, SoNumber="SO-009", OrderDate=DateTime.Today.AddDays(-4), CustomerName="Iota", Address="Jl. Kamboja 9"},
                new SalesOrderViewModel{ Id=10, SoNumber="SO-010", OrderDate=DateTime.Today.AddDays(-2), CustomerName="Kappa", Address="Jl. Akasia 10"},
                new SalesOrderViewModel{ Id=11, SoNumber="SO-011", OrderDate=DateTime.Today.AddDays(-2), CustomerName="Lambda", Address="Jl. Kenari 11"},
                new SalesOrderViewModel{ Id=12, SoNumber="SO-012", OrderDate=DateTime.Today.AddDays(-7), CustomerName="Mu", Address="Jl. Cempaka 12"}
            };
        }

        public class SalesOrderViewModel
        {
            public int Id { get; set; }
            public string? SoNumber { get; set; }
            public DateTime OrderDate { get; set; }
            public string? CustomerName { get; set; }
            public string? Address { get; set; }
        }
    }
}
