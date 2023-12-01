using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Tuya.PruebaTecnica.Domain.Models
{
    public class OrderItem
    {
        public int Id { get; set; } 
        public int OrderId { get; set; }
        public int ProductId { get; set; } = 0;
        public int Quantity { get; set; }        
        public decimal Price { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }


    }
}
