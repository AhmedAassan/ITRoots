using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.DAL.Entities
{
    [Table("Product")]
    public class Product
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double Price { get; set; }
        public ICollection<ProductInvoices> ProductInvoices { get; set; } = new HashSet<ProductInvoices>();
    }
}
