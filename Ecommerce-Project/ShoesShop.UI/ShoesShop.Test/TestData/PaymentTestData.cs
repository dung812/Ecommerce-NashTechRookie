using ShoesShop.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesShop.Test.TestData
{
    public class PaymentTestData
    {
        public static List<Payment> GetPayments()
        {
            return new List<Payment>() {
                new Payment() {
                    PaymentId = 1,
                    PaymentName = "Cash on delivery",
                    Status = true
                },                
                new Payment() {
                    PaymentId = 2,
                    PaymentName = "Payment",
                    Status = true
                },
            };
        }
    }
}

