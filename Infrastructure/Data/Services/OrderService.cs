using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;

namespace Infrastructure.Data.Services
{
    public class OrderService : IOrderService
    {

        private readonly IGenericRepository<Order> _orderRepository;
        private readonly IGenericRepository<DeliveryMethod> _deliveryMethodRepository;
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IBasketRepository _basketRepository;

        public OrderService(IGenericRepository<Order> orderRepository, 
                            IGenericRepository<DeliveryMethod> deliveryRepository,
                            IGenericRepository<Product> productRepository,
                            IBasketRepository basketRepository)
        {

            _orderRepository = orderRepository;
            _deliveryMethodRepository = deliveryRepository;
            _productRepository = productRepository;
            _basketRepository = basketRepository;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {

            //getting customer basket
            var basket = await  _basketRepository.GetBasketAsync(basketId);

            var itemsList = new List<OrderItem>();

            foreach(var item in basket.Items)
            {
                var productItem = await _productRepository.getByIdAsync(item.Id);

                var itemOrdered = new ProductItemOrdered(productItem.id, productItem.name, productItem.pictureUrl);

                var orderItem = new OrderItem(itemOrdered, productItem.price, item.Quantity);

                itemsList.Add(orderItem);

            }

            //get delivery method from repository
            var deliveryMethod= await _deliveryMethodRepository.getByIdAsync(deliveryMethodId);


            //calculate subtotal
            var subtotal=itemsList.Sum(item => item.Price * item.Quantity);


            //create a order
            var order = new Order(itemsList, buyerEmail, shippingAddress, deliveryMethod, subtotal);

            //save to database


            return order;
        }

        public Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetOrderByIdAsync(int id, string buyerEmail)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
