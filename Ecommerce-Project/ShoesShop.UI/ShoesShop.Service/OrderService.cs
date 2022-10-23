using ShoesShop.Data;
using ShoesShop.DTO;
using ShoesShop.Domain;
using ShoesShop.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoesShop.Domain.Enum;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace ShoesShop.Service
{
    public interface IOrderService
    {
        public bool CreateNewOrder(OrderViewModel orderViewModel, int customerId, int paymentId);
        public bool CreateOrderDetail(string orderId, List<CartViewModel> listCart);
        public List<OrderViewModel> GetListOrderByCustomerId(int customerId);
        public OrderViewModel GetOrderDetailById(string orderId);
        public List<OrderDetailViewModel> GetItemOfOderById(string orderId);
        public bool DeleteOrder(int customerId, string orderId);
    }
    public class OrderService : IOrderService
    {
        public bool CreateNewOrder(OrderViewModel orderViewModel, int customerId, int paymentId)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    Order order = new Order()
                    {
                        OrderId = orderViewModel.OrderId,
                        OrderDate = orderViewModel.OrderDate,
                        OrderStatus = OrderStatus.UnChecked,
                        OrderName = orderViewModel.OrderName,
                        Address = orderViewModel.Address,
                        Phone = orderViewModel.Phone,
                        Note = orderViewModel.Note,
                        CustomerId = customerId,
                        PaymentId = paymentId
                    };
                    context.Orders.Add(order);
                    context.SaveChanges();
                    return true;
                }
            } catch (Exception)
            {
                return false;
            }

        }
        public bool CreateOrderDetail(string orderId, List<CartViewModel> listCart)
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    //foreach (var item in listCart)
                    //{
                    //    OrderDetail orderDetail = new OrderDetail();
                    //    orderDetail.OrderId = order.OrderId;
                    //    orderDetail.ProductId = item.ProductId;
                    //    orderDetail.AttributeValueId = item.AttributeId;
                    //    orderDetail.Quantity = item.Quantity;
                    //    orderDetail.UnitPrice = item.UnitPrice;
                    //    orderDetail.PromotionPercent = item.PromotionPercent;
                    //    db.OrderDetails.Add(orderDetail);
                    //}
                    foreach (var item in listCart)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderId = orderId;
                        orderDetail.ProductId = item.ProductId;
                        orderDetail.AttributeValueId = item.AttributeId;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.UnitPrice = item.UnitPrice;
                        orderDetail.PromotionPercent = item.PromotionPercent;
                        context.OrderDetails.Add(orderDetail);
                    }
                    context.SaveChanges();
                    return true;
                }
            } catch (Exception)
            {
                return false;
            }
        }

        public List<OrderViewModel> GetListOrderByCustomerId(int customerId)
        {
            List<OrderViewModel> orders = new List<OrderViewModel>();
            using (var context = new ApplicationDbContext())
            {
                orders = context.Orders
                    .Where(m => m.CustomerId == customerId)
                    .Include(m => m.Payment)
                    .OrderByDescending(m => m.OrderDate)
                    .Select(m => new OrderViewModel
                    {
                        OrderId = m.OrderId,
                        OrderDate = m.OrderDate,
                        OrderStatus = m.OrderStatus,
                        DeliveryDate = m.DeliveryDate,
                        OrderName = m.OrderName,
                        Address = m.Address,
                        Phone = m.Phone,    
                        Note = m.Note,
                        PaymentName = m.Payment.PaymentName
                    }).ToList();
            }
            return orders;
        }

        public OrderViewModel GetOrderDetailById(string orderId)
        {
            var orderDetail = new OrderViewModel();
            using (var context = new ApplicationDbContext())
            {
                orderDetail = context.Orders
                    .Where(m => m.OrderId == orderId)
                    .Include(m => m.Payment)
                    .OrderByDescending(m => m.OrderDate)
                    .Select(m => new OrderViewModel
                    {
                        OrderId = m.OrderId,
                        OrderDate = m.OrderDate,
                        OrderStatus = m.OrderStatus,
                        DeliveryDate = m.DeliveryDate,
                        OrderName = m.OrderName,
                        Address = m.Address,
                        Phone = m.Phone,
                        Note = m.Note,
                        PaymentName = m.Payment.PaymentName
                    }).FirstOrDefault();
            }
            return orderDetail;
        }

        public List<OrderDetailViewModel> GetItemOfOderById(string orderId)
        {
            List<OrderDetailViewModel> orderDetail = new List<OrderDetailViewModel>();
            using (var context = new ApplicationDbContext())
            {
                orderDetail = context.OrderDetails
                    .Where(m => m.OrderId == orderId)
                    .Include(m => m.Product)
                    .Include(m => m.AttributeValue)
                    .Select(m => new OrderDetailViewModel
                    {
                        OrderId = m.OrderId,
                        ProductName = m.Product.ProductName,
                        ProductImage = m.Product.Image,
                        AttributeName = m.AttributeValue.Name,
                        Quantity = m.Quantity,
                        UnitPrice = m.UnitPrice,
                        PromotionPercent = m.PromotionPercent
                    }).ToList();
            }
            return orderDetail;
        }

        public bool DeleteOrder(int customerId, string orderId)
        {
            using (var context = new ApplicationDbContext())
            {
                var order = context.Orders.FirstOrDefault(m => m.CustomerId == customerId && m.OrderId == orderId);
                if (order != null)
                {
                    // Delete order detail
                    context.Database.ExecuteSqlRaw("DELETE FROM OrderDetailS WHERE OrderId = '" + orderId + "'");

                    // Delete order
                    context.Orders.Remove(order);
                    context.SaveChanges();
                    return true;
                }
            }
            return false;
        }
    }
}
