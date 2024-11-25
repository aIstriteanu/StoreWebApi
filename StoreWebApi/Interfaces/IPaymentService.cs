using StoreWebApi.Models;

namespace StoreWebApi.Interfaces
{
    public interface IPaymentService
    {
        Task<List<Payment>> GetAll();
        Task<Payment?> GetById(int id);
        Task<PaymentDto> Create(CreatePaymentRq request);
        Task Update(int id, PaymentDto request);
        Task Delete(int id);
    }
}
