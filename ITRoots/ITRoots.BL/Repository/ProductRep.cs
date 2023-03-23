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
    public class ProductRep : IProductRep
    {
        private readonly ITRootsContext db;

        public ProductRep(ITRootsContext db)
        {
            this.db = db;
        }



        public IEnumerable<Product> Get()
        {
            var data = db.Product.Select(a => a);
            return data;
        }

        public Product? GetById(int id)
        {
            var data = db.Product.Where(a => a.Id == id).FirstOrDefault();
            return data;
        }
        public void Create(Product obj)
        {
            db.Add(obj);
            db.SaveChanges();
        }


        public void Update(Product obj)
        {
            db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
        }


        public void Delete(Product obj)
        {
            db.Remove(obj);
            db.SaveChanges();
        }
    }
}
