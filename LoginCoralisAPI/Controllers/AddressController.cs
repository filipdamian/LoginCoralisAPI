using LoginCoralisAPI.Data;
using LoginCoralisAPI.Entities;
using LoginCoralisAPI.Entities.DTOs;
using LoginCoralisAPI.Repositories.AddressRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginCoralisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepository _repository;
        public AddressController(IAddressRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> AllAddresses() 
        {
            var addresses = await _repository.GetAllAddresses();
            var addressesToReturn = new List<AddressDTO>();
            foreach(var address in addresses)
            {
                addressesToReturn.Add(new AddressDTO(address));
            }
            return Ok(addressesToReturn);
        }

        [HttpGet("{town}")]
        public async Task<IActionResult> GetAllAdrByTown(string town)
        {
            var addresses = await _repository.GetAddressesByTown(town);
            var addressesToReturn = new List<AddressDTO>();
            foreach (var address in addresses)
            {
                addressesToReturn.Add(new AddressDTO(address));
            }
            return Ok(addressesToReturn);
        }
        [HttpDelete("{email}")]
        public async Task<IActionResult> DeleteAddress(string email)
        {
            var address = await _repository.GetAddressByEmail(email);
            if (address == null)
                return NotFound("No address found. Check if the email or the address was introduced correctly");
            _repository.delete(address);
            await _repository.saveAsync();
            return NoContent();

        }
        [HttpPost]
        public async Task<IActionResult> CreateAddress(CreateAddressDTO dto)
        {
            Address newaddress = new Address();
            //newaddress.id = dto.id;
            newaddress.town = dto.town;
            newaddress.street = dto.street;
            newaddress.UserId = dto.UserId;
            _repository.create(newaddress);
      
            await _repository.saveAsync();
            return Ok(new AddressDTO(newaddress));

        }
        [HttpDelete("{id}/deletebyid")]
        public async Task<IActionResult> DeleteAddress(int id)
        {
            var address = await _repository.GetAddressById(id);
            if (address == null)
                return NotFound("Adress doesn't exist");
            _repository.delete(address);
            await _repository.saveAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAddressDetails(int id, CreateAddressDTO dto)
        {
            var address = await _repository.GetAddressById(id);
            if (address == null)
                return NotFound("Address doesn't exist");
            address.street = dto.street;
            address.town = dto.town;
            await _repository.saveAsync();
            return Ok(new AddressDTO(address));

        }

    }
}
