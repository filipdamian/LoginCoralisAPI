using LoginCoralisAPI.Data;
using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Repositories.OrderRepository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(LoginContext context) : base(context) { }
        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.Include(ord => ord.Address).ToListAsync();
        }

        public async Task<Order> GetbyId(int id)
        {
            return await _context.Orders.Where(ord => ord.id.Equals(id)).FirstOrDefaultAsync();
        }

      
    }
}
