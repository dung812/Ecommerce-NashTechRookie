using ShoesShop.Data;
using ShoesShop.Domain;
using ShoesShop.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Service
{
    public interface IPaymentService
    {
        public List<PaymentViewModel> GetAllPayment();

    }
    public class PaymentService : IPaymentService
    {
        private readonly ApplicationDbContext _context;
        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<PaymentViewModel> GetAllPayment()
        {
            List<PaymentViewModel> list = new List<PaymentViewModel>();
            list = _context.Payments.Where(m => m.Status).Select(m => new PaymentViewModel
            {
                PaymentId = m.PaymentId,
                PaymentName = m.PaymentName,
            }).ToList();
            return list;
        }
    }
}
