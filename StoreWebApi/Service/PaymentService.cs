using Microsoft.EntityFrameworkCore;
using StoreWebApi.Interfaces;
using StoreWebApi.Models;

namespace StoreWebApi.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly StoreDbContext _dbContext;

        public PaymentService(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Payment>> GetAll()
        {
            return await _dbContext.Payments.AsNoTracking().ToListAsync();
        }

        public async Task<Payment?> GetById(int id)
        {
            return await _dbContext.Payments.FindAsync(id);
        }

        public async Task<PaymentDto> Create(CreatePaymentRq paymentRq)
        {
            var payment = new Payment()
            {
                Amount = paymentRq.Amount,
                Type = paymentRq.Type,
            };

            _dbContext.Payments.Add(payment);
            await _dbContext.SaveChangesAsync();

            var pay = new PaymentDto()
            {
                Id = payment.Id,
                Amount = payment.Amount,
                Type = payment.Type
            };

            return pay;
        }
        
        public async Task Update(int id, PaymentDto paymentRq)
        {
            var payment = new Payment()
            {
                Id = paymentRq.Id,
                Amount = paymentRq.Amount,
                Type = paymentRq.Type,
            };

            _dbContext.Entry(payment).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var item = await _dbContext.Payments.FindAsync(id);
            _dbContext.Payments.Remove(item);
            await _dbContext.SaveChangesAsync(); ;
        }
    }
}
