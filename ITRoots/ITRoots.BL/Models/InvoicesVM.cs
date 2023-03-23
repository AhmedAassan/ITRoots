using ITRoots.DAL.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Models
{
    public class InvoicesVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "total is Required")]
        public string? totalAmount { get; set; }
        [Required(ErrorMessage = "DateOfInvoice is Required")]
        public DateTime DateOfInvoice { get; set; }

        public IEnumerable<int>? ProductId { get; set; }
        public ICollection<ProductInvoices>? ProductInvoices { get; set; } 
    }
}
