﻿namespace Tuya.PruebaTecnica.WebApi.Dtos
{
    public class OrderItemDTO
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }        
        public ProductDTO Product { get; set; }
    }
}
