using AutoMapper;
using ITRoots.BL.Models;
using ITRoots.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITRoots.BL.Mapper
{
    public class DomainProfile :Profile
    {
        public DomainProfile()
        {
            CreateMap<Product, ProductVM>();
            CreateMap<ProductVM, Product>();


            CreateMap<invoices, InvoicesVM>();
            CreateMap<InvoicesVM, invoices>();
        }
    }
}
