using ITRoots.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Models
{
    public class ProductVM
    {
        
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Quantity is Required")]
        public int Quantity { get; set; }
        [Required(ErrorMessage = "Price is Required")]
        public double Price { get; set; }
        public ICollection<ProductInvoices>? ProductInvoices { get; set; }
    }
}
