using LoginCoralisAPI.Data;
using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Repositories.GenericRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Repositories.AddressRepository
{
    public class AddressRepository : GenericRepository<Address>, IAddressRepository
    {
        public AddressRepository(LoginContext context) : base(context) { }
        public async Task<List<Address>> GetAddressesByTown(string town)
        {
            return await _context.Addresses.Where(adr => adr.town.Equals(town)).ToListAsync();
        }
        public async Task<List<Address>> GetAllAddresses()
        {
            return await _context.Addresses.Include(adr => adr.User).ToListAsync();
        }
        public async Task<Address> GetAddressByEmail(string email)
        {
            return await _context.Addresses.Where(adr => adr.UserId.Equals(email)).FirstOrDefaultAsync();
        }
        public async Task<Address>GetAddressById(int id)
        {
            return await _context.Addresses.Where(adr => adr.id.Equals(id)).FirstOrDefaultAsync();
        }
    }
}
