using AutoMapper;
using ITRoots.BL.Interface;
using ITRoots.BL.Models;
using ITRoots.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITRoots.Controllers
{
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IInvoicesRep invoices;
        private readonly IProductInvoicesRep productInvoices;

        public InvoicesController(IMapper mapper, IInvoicesRep invoices, IProductInvoicesRep productInvoices)
        {
            this.mapper = mapper;
            this.invoices = invoices;
            this.productInvoices = productInvoices;
        }
        public IActionResult Index()
        {
            var data = invoices.Get();
            var model = mapper.Map<IEnumerable<InvoicesVM>>(data);
            return View(model);
        }


        public IActionResult Details(int id)
        {
            var data = invoices.GetById(id);
            var model = mapper.Map<InvoicesVM>(data);
            return View(model);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(InvoicesVM model)
        {
            try
            {
                

                    var data = mapper.Map<invoices>(model);
                    invoices.Create(data);
                    foreach (var item in model.ProductId)
                {
                    //var orderProduct = new OrderProduct() { OrderId = data, ProductId = item };
                    //await _orderProduct.Create(orderProduct);

                     productInvoices.Create(data.Id, item);
                }
                    return RedirectToAction("Index");
                

                

            }
            catch (Exception ex)
            {

                return View(model);
            }
        }


        public IActionResult Update(int id)
        {
            var data = invoices.GetById(id);
            var model = mapper.Map<InvoicesVM>(data);
            return View(model);
        }


        [HttpPost]
        public IActionResult Update(InvoicesVM model)
        {
            try
            {
                
                    var data = mapper.Map<invoices>(model);
                    invoices.Update(data);
                    return RedirectToAction("Index");
                

            }
            catch (Exception ex)
            {

                return View(model);
            }
        }



        public IActionResult Delete(int? id)
        {

            var data = invoices.GetById(id);
            invoices.Delete(data);
            return RedirectToAction("Index");


        }
    }
}
