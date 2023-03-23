using ITRoots.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Interface
{
    public interface IInvoicesRep
    {
        IEnumerable<invoices> Get();
        invoices? GetById(int? id);
        int Create(invoices obj);
        int Update(invoices obj);
        void Delete(invoices obj);
    }
}

