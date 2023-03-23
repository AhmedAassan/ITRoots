using AutoMapper;
using ITRoots.BL.Interface;
using ITRoots.BL.Models;
using ITRoots.DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ITRoots.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductRep product;

        public ProductController(IMapper mapper, IProductRep product)
        {
            this.mapper = mapper;
            this.product = product;
        }
        public IActionResult Index()
        {
            var data = product.Get();
            var model = mapper.Map<IEnumerable<ProductVM>>(data);
            return View(model);
        }


        public IActionResult Details(int id)
        {
            var data = product.GetById(id);
            var model = mapper.Map<ProductVM>(data);
            return View(model);
        }


        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ProductVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var data = mapper.Map<Product>(model);
                    product.Create(data);
                    return RedirectToAction("Index");
                }

                return View(model);

            }
            catch (Exception ex)
            {

                return View(model);
            }
        }


        public IActionResult Update(int id)
        {
            var data = product.GetById(id);
            var model = mapper.Map<ProductVM>(data);
            return View(model);
        }


        [HttpPost]
        public IActionResult Update(ProductVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var data = mapper.Map<Product>(model);
                    product.Update(data);
                    return RedirectToAction("Index");
                }
                return View(model);

            }
            catch (Exception ex)
            {

                return View(model);
            }
        }



        public IActionResult Delete(int id)
        {

            var data = product.GetById(id);
            product.Delete(data);
            return RedirectToAction("Index");


        }
    }
}
