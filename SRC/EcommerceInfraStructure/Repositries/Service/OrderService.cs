using AutoMapper;
using Ecommerce.Core.DTOS;
using Ecommerce.Core.Intefaces;
using Ecommerce.Core.Models.Order;
using Ecommerce.Core.Services;
using EcommerceInfraStructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceInfraStructure.Repositries.Service
{
    public class OrderService : IOrderService
    {
        private readonly IUniOfwork _unitOfWork;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IUniOfwork unitOfWork, AppDbContext context, IMapper mapper, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _mapper = mapper;
            _paymentService = paymentService;
        }


        public async Task<Orders> CreateOrdersAsync(OrderDTO orderDTO, string BuyerEmail)
        {
            var basket = await _unitOfWork.customerBasketRepository.GetBasketAsync(orderDTO.basketId);

            List<OrderItem> orderItems = new List<OrderItem>();

            foreach (var item in basket.basketItems)
            {
                var Product = await _unitOfWork.productRepository.GetByIdAsync(item.Id);
                var orderItem = new OrderItem
                    (Product.Id, item.Image, Product.Name, item.Price, item.Qunatity);
                orderItems.Add(orderItem);

            }
            var deliverMethod = await _context.DeliveryMethods.FirstOrDefaultAsync(m => m.Id == orderDTO.deliveryMethodId);

            var subTotal = orderItems.Sum(m => m.Price * m.Quntity);

            var ship = _mapper.Map<ShippingAddress>(orderDTO.shipAddress);

            var ExisitOrder = await _context.Orders.Where(m => m.PaymentIntentId == basket.PaymentIntentId).FirstOrDefaultAsync(); 
            var order = new
                Orders(BuyerEmail, subTotal, ship, deliverMethod, orderItems, basket.PaymentIntentId);

            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            await _unitOfWork.customerBasketRepository.DeleteBasketAsync(orderDTO.basketId);
            return order;

        }

        public async Task<IReadOnlyList<OrderToReturnDTO>> GetAllOrdersForUserAsync(string BuyerEmail)
        {
            var orders = await _context.Orders.Where(m => m.BuyerEmail == BuyerEmail)
                .Include(inc => inc.orderItems).Include(inc => inc.deliveryMethod)
                .ToListAsync();
            var result = _mapper.Map<IReadOnlyList<OrderToReturnDTO>>(orders);
            result = result.OrderByDescending(m => m.Id).ToList();
            return result;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodAsync()=> await _context.DeliveryMethods.AsNoTracking().ToListAsync();

        public async Task<OrderToReturnDTO> GetOrderByIdAsync(int Id, string BuyerEmail)
        {
            var order = await _context.Orders.Where(m => m.Id == Id && m.BuyerEmail == BuyerEmail)
                  .Include(inc => inc.orderItems).Include(inc => inc.deliveryMethod)
                  .FirstOrDefaultAsync();
            var result = _mapper.Map<OrderToReturnDTO>(order);
            return result;
        }
    }
}
