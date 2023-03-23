using ITRoots.BL.Interface;
using ITRoots.DAL.Database;
using ITRoots.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Repository
{
    public class ProductInvoicesRep :IProductInvoicesRep
    {
        private readonly ITRootsContext db;

        public ProductInvoicesRep(ITRootsContext db)
        {
            this.db = db;
        }
        public void Create(int InvoicesId, int ProductId)
        {
            var data = new ProductInvoices() { invoicesId= InvoicesId ,ProductId = ProductId };
            db.ProductInvoices.Add(data);
            db.SaveChangesAsync();
        }

    }
}
