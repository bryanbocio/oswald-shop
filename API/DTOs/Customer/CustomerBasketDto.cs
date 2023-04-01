
using System.ComponentModel.DataAnnotations;

namespace API.DTOs.Customer
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        
        public List<BasketItemsDto> Items { get; set; }
    }
}
