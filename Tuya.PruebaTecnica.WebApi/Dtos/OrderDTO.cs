namespace Tuya.PruebaTecnica.WebApi.Dtos
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public decimal TotalAmount { get; set; }
        
        public List<OrderItemDTO> OrderItems { get; set; }
        
        public CustomerDTO Customer { get; set; }
    }
}
