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
        public List<PaymentViewModel> GetAllPayment()
        {
            List<PaymentViewModel> list = new List<PaymentViewModel>();
            using (var context = new ApplicationDbContext())
            {
                list = context.Payments.Where(m => m.Status).Select(m => new PaymentViewModel
                {
                    PaymentId = m.PaymentId,
                    PaymentName = m.PaymentName,
                }).ToList();
            }
            return list;
        }
    }
}
