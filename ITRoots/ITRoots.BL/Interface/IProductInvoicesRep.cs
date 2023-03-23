using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Interface
{
    public interface IProductInvoicesRep
    {
        public void Create(int InvoicesId, int ProductId);
    }
}
