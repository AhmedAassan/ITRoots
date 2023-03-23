using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.DAL.Entities
{
    public class invoices
    {
        public int Id { get; set; }
        public string? totalAmount { get; set; }
        public DateTime DateOfInvoice { get; set; }
        public ICollection<ProductInvoices> ProductInvoices { get; set; } = new HashSet<ProductInvoices>();
    }
}
