using StoreWebApi.Models;

namespace StoreWebApi.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetAll();
        Task<Customer?> GetById(int id);
        Task<CustomerDto> Create(CreateCustomerRq request);
        Task Update(int id, CustomerDto request);
        Task Delete(int id);
    }
}
