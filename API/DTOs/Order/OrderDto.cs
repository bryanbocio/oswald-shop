using API.DTOs.Account;

namespace API.DTOs.Order
{
    public class OrderDto
    {
        public string BasketId { get; set; }

        public int DeliveryMethodId { get; set; }
        public AddressDto ShipToAddress{ get; set; }
    }
}
