using ITRoots.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Interface
{
    public interface IProductRep
    {
        IEnumerable<Product> Get();
        Product? GetById(int id);
        void Create(Product obj);
        void Update(Product obj);
        void Delete(Product obj);
    }
}
