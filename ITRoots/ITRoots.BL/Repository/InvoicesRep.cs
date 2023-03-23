using ITRoots.BL.Interface;
using ITRoots.DAL.Database;
using ITRoots.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Repository
{
    public class InvoicesRep : IInvoicesRep
    {
        private readonly ITRootsContext db;

        public InvoicesRep(ITRootsContext db)
        {
            this.db = db;
        }



        public IEnumerable<invoices> Get()
        {
            var data = db.invoices.Include(e => e.ProductInvoices).ThenInclude(e => e.Product).Distinct().ToList();
            return data;
        }

        public invoices? GetById(int? id)
        {
            var data = db.invoices.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
        public int Create(invoices obj)
        {
            db.Add(obj);
            
            return db.SaveChanges();
        }


        public int Update(invoices obj)
        {
            db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            return db.SaveChanges();
        }


        public void Delete(invoices obj)
        {
            db.Remove(obj);
            db.SaveChanges();
        }
    }
}
