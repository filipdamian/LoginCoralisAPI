using LoginCoralisAPI.Data;
using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Entities.DTOs;
using LoginCoralisAPI.Repositories.OrderRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _repository;
        private readonly LoginContext _context;
        public OrderController(IOrderRepository repository,LoginContext context)
        {
            _repository = repository;
            _context = context;
        }

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AllOrders()
        {
            var orders = await _repository.GetAllOrders();
            var ordersToReturn = new List<OrderDTO>();
            foreach(var order in orders)
            {
                ordersToReturn.Add(new OrderDTO(order));
            }
            return Ok(ordersToReturn);


        }
          [HttpGet("byid/{id}")]
        public async Task<IActionResult> OrderById(int id)
        {

            // var order = await _repository.GetbyId(id);
            var result =await (from ord in _context.Orders
                         from adr in _context.Addresses
                         where ord.AddressId == adr.id
                         select new
                         {
                             id = ord.id,
                             totalprice=ord.totalprice,
                             
                             town=adr.town,
                             street=adr.street

                         }).ToListAsync();
            foreach(var r in result)
            {
                if(r.id==id)
                    return Ok(r);

            }
            return BadRequest();
            
        }
        [HttpPost]
        public async Task<IActionResult>CreateOrder(CreateOrderDTO dto)
        {
            Order newOrder = new Order();
           // newOrder.id = dto.id;
            newOrder.totalprice = dto.totalprice;
            newOrder.AddressId = dto.AddressId;
            newOrder.Address = dto.Address;
            newOrder.ProductOrders = dto.ProductOrders;
            _repository.create(newOrder);
            await _repository.saveAsync();
            return Ok(new OrderDTO(newOrder));

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _repository.GetbyId(id);
            if (order == null)
                return NotFound("Order doesn't exist");
            _repository.delete(order);
            await _repository.saveAsync();
            return NoContent();

        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTotalPrice(int id, CreateOrderDTO dto)
        {
            var order = await _repository.GetbyId(id);
            if (order == null)
                return NotFound("Order doesn't exist");
            order.totalprice = dto.totalprice;
            await _repository.saveAsync();
            return Ok(new OrderDTO(order));
        }
    }
}
