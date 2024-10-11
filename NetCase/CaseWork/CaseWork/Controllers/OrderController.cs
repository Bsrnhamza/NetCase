using CaseWork.DataAccess.Repositories;
using CaseWork.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CaseWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly OrderRepository _orderRepository;

        public OrderController(OrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Tüm siparişleri getir
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            return Ok(orders);
        }

        // Yeni sipariş ekle
        [HttpPost]
        public async Task<IActionResult> AddOrder(Order order)
        {
            if (order == null || order.OrderDesi <= 0)
            {
                return BadRequest("Geçersiz sipariş verileri.");
            }

            try
            {
                await _orderRepository.AddOrderAsync(order);
                return Ok("Sipariş başarıyla eklendi!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sipariş eklenirken hata oluştu: {ex.Message}");
            }
        }

        // Sipariş güncelle
        [HttpPut]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            if (order == null || order.OrderId <= 0)
            {
                return BadRequest("Geçersiz sipariş verileri.");
            }

            try
            {
                await _orderRepository.UpdateOrderAsync(order);
                return Ok("Sipariş başarıyla güncellendi!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sipariş güncellenirken hata oluştu: {ex.Message}");
            }
        }

        // Sipariş sil
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(int orderId)
        {
            if (orderId <= 0)
            {
                return BadRequest("Geçersiz sipariş ID'si.");
            }

            try
            {
                await _orderRepository.DeleteOrderAsync(orderId);
                return Ok("Sipariş başarıyla silindi!");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sipariş silinirken hata oluştu: {ex.Message}");
            }
        }

        // Siparişi hesaplayıp ekle
        [HttpPost("calculate-and-add")]
        public async Task<IActionResult> CalculateAndAddOrder(Order order)
        {
            if (order == null || order.OrderDesi <= 0)
            {
                return BadRequest("Geçersiz sipariş verileri.");
            }

            try
            {
                await _orderRepository.AddOrderAsync(order);
                return Ok($"Sipariş başarıyla eklendi! Kargo ücreti: {order.OrderCarrierCost}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Sipariş eklenirken hata oluştu: {ex.Message}");
            }
        }
    }
}
