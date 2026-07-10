using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesOrderService.Models;
using SalesOrderService.Service;
using SalesOrderService.ViewModel;

namespace SalesOrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesRestController : ControllerBase
    {
        private readonly SalesService _salesService;

        public SalesRestController(SalesService salesService)
        {
            _salesService = salesService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SalesSoVm order)
        {
            var createdSalesSo = await _salesService.Create(order);
            return Ok(createdSalesSo);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var salesSos = await _salesService.GetAll();
            return Ok(salesSos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var salesSo = await _salesService.GetById(id);

            if (salesSo == null)
            {
                return NotFound();
            }
            
            return Ok(salesSo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, SalesSoVm order)
        {
            if (id != order.SalesSoId)
            {
                return BadRequest();
            }

            await _salesService.Update(id, order);
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            return await _salesService.Delete(id) ? Ok() : NotFound();
        }
    }
}
