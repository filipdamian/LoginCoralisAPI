using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Repositories.OrderRepository
{
    public interface IOrderRepository:IGenericRepository<Order>
    {
        Task<Order> GetbyId(int id);

        Task<List<Order>> GetAllOrders();
    }
}
