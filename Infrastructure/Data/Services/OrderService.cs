using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specification.Orders;

namespace Infrastructure.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork,IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _basketRepository = basketRepository;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {

            //getting customer basket
            var basket = await  _basketRepository.GetBasketAsync(basketId);

            var itemsList = new List<OrderItem>();

            foreach(var item in basket.Items)
            {
                var productItem = await _unitOfWork.Repository<Product>().getByIdAsync(item.Id);

                var itemOrdered = new ProductItemOrdered(productItem.id, productItem.name, productItem.pictureUrl);

                var orderItem = new OrderItem(itemOrdered, productItem.price, item.Quantity);

                itemsList.Add(orderItem);

            }

            //get delivery method from repository
            var deliveryMethod= await _unitOfWork.Repository<DeliveryMethod>().getByIdAsync(deliveryMethodId);


            //calculate subtotal
            var subtotal=itemsList.Sum(item => item.Price * item.Quantity);


            //create a order
            var order = new Order(itemsList, buyerEmail, shippingAddress, deliveryMethod, subtotal);
            _unitOfWork.Repository<Order>().Add(order);

            //save to database
            var result = await _unitOfWork.Complete();

            if(result<=0) return null;

            //delete basket
            await _basketRepository.DeleteBasketAsync(basketId);

            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().listAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            var specification = new OrdersWithItemsAndOrderingSpecification(id, buyerEmail);

            return await _unitOfWork.Repository<Order>().getEntityWithSpecification(specification);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var specification= new OrdersWithItemsAndOrderingSpecification(buyerEmail);

            return await _unitOfWork.Repository<Order>().listAsync(specification);
        }
    }
}
