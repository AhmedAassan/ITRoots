using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.DAL.Entities
{
    public class ProductInvoices
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        [ForeignKey("invoices")]
        public int invoicesId { get; set; }
        public invoices? invoices { get; set; }
    }
}
