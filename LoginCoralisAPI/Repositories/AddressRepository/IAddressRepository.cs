using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Repositories.GenericRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Repositories.AddressRepository
{
    public interface IAddressRepository:IGenericRepository<Address>
    {
        Task<List<Address>> GetAddressesByTown(string town);
        Task<List<Address>> GetAllAddresses();
        Task<Address> GetAddressByEmail(string email);
        Task<Address> GetAddressById(int id);
    }
}
