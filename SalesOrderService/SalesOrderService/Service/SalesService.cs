using Microsoft.EntityFrameworkCore;
using SalesOrderService.Models;
using SalesOrderService.ViewModel;

namespace SalesOrderService.Service
{
    public class SalesService
    {
        private readonly SalesDbContext _context;

        public SalesService(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<SalesSoVm> Create(SalesSoVm order)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (!Validation(order))
                {
                    throw new ArgumentException("Invalid sales order data.");
                }

                SalesSo header = new SalesSo();
                header.SoNo = order.SoNo;
                header.OrderDate = order.OrderDate;
                header.ComCustomerId = order.ComCustomerId;
                header.Address = order.Address;

                _context.SalesSos.Add(header);

                await _context.SaveChangesAsync();

                var headerId = header.SalesSoId;

                if (order.SalesSoLitems != null && order.SalesSoLitems.Any())
                {
                    foreach (var item in order.SalesSoLitems)
                    {
                        SalesSoLitem detail = new SalesSoLitem();
                        detail.SalesSoId = headerId;
                        detail.ItemName = item.ItemName;
                        detail.Quantity = item.Quantity;
                        detail.Price = item.Price;

                        _context.Add(detail);
                    }

                    await _context.SaveChangesAsync();
                }

                await transaction.CommitAsync();

                return order;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<SalesSoVm>> GetAll()
        {
            return await _context.SalesSos
                .Include(x => x.SalesSoLitems)
                .Select(x => new SalesSoVm
                {
                    SalesSoId = x.SalesSoId,
                    SoNo = x.SoNo,
                    OrderDate = x.OrderDate,
                    ComCustomerId = x.ComCustomerId,
                    Address = x.Address,

                    SalesSoLitems = x.SalesSoLitems.Select(d => new SalesSoLitemVm
                    {
                        SalesSoLitemId = d.SalesSoLitemId,
                        SalesSoId = d.SalesSoId,
                        ItemName = d.ItemName,
                        Quantity = d.Quantity,
                        Price = d.Price
                    }).ToList()
                })
                .ToListAsync();
        }

        public async Task<SalesSoVm?> GetById(int id)
        {
            return await _context.SalesSos
                .Where(x => x.SalesSoId == id)
                .Select(x => new SalesSoVm
                {
                    SalesSoId = x.SalesSoId,
                    SoNo = x.SoNo,
                    OrderDate = x.OrderDate,
                    ComCustomerId = x.ComCustomerId,
                    Address = x.Address,

                    SalesSoLitems = x.SalesSoLitems
                        .Select(item => new SalesSoLitemVm
                        {
                            SalesSoLitemId = item.SalesSoLitemId,
                            SalesSoId = item.SalesSoId,
                            ItemName = item.ItemName,
                            Quantity = item.Quantity,
                            Price = item.Price
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync();
        }

        public async Task<SalesSoVm> Update(int id, SalesSoVm order)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (!Validation(order))
                {
                    throw new ArgumentException("Invalid sales order data.");
                }

                var header = await _context.SalesSos
                    .Include(x => x.SalesSoLitems)
                    .FirstOrDefaultAsync(x => x.SalesSoId == id);

                if (header == null) 
                    throw new KeyNotFoundException($"Sales order with ID {id} not found.");
                
                
                header.SoNo = order.SoNo;
                header.OrderDate = order.OrderDate;
                header.ComCustomerId = order.ComCustomerId;
                header.Address = order.Address;

                var existingDetails = header.SalesSoLitems.ToList();

                foreach (var item in order.SalesSoLitems ?? [])
                {
                    var detail = existingDetails
                        .FirstOrDefault(x => x.SalesSoLitemId == item.SalesSoLitemId);

                    if (detail == null)
                    {
                        header.SalesSoLitems.Add(new SalesSoLitem
                        {
                            ItemName = item.ItemName,
                            Quantity = item.Quantity,
                            Price = item.Price
                        });
                    }
                    else
                    {
                        detail.ItemName = item.ItemName;
                        detail.Quantity = item.Quantity;
                        detail.Price = item.Price;
                    }
                }

                var requestIds = order.SalesSoLitems?
                    .Where(x => x.SalesSoLitemId > 0)
                    .Select(x => x.SalesSoLitemId)
                    .ToList() ?? [];

                var deletedItems = existingDetails
                    .Where(x => !requestIds.Contains(x.SalesSoLitemId))
                    .ToList();

                _context.SalesSoLitems.RemoveRange(deletedItems);

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return await GetById(id);
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public async Task<bool> Delete(int id)
        {
            var data = await _context.SalesSos
                .Include(x => x.SalesSoLitems)
                .FirstOrDefaultAsync(x => x.SalesSoId == id);

            if (data == null)
            {
                return false;
            }

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                if (data.SalesSoLitems != null)
                {
                    _context.SalesSoLitems.RemoveRange(data.SalesSoLitems);
                }

                _context.SalesSos.Remove(data);

                await _context.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }


        public bool Validation(SalesSoVm salesSo)
        {
            var isSONumberExist = _context.SalesSos.Any(s => s.SoNo == salesSo.SoNo && s.SalesSoId != salesSo.SalesSoId);

            if (string.IsNullOrEmpty(salesSo.SoNo) || isSONumberExist)
            {
                return false;
            }
            
            return true;
        }
    }
}
