using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategories.Models;

namespace ProductCategories.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;//add this whole block
        public HomeController(MyContext context){
            dbContext = context;
        }
        [HttpGet("/")]
        public IActionResult Index()
        {
            List<Product> allProducs = dbContext.products.ToList();
            ViewBag.products = allProducs;
            return View("Index");
        }
        [HttpGet("/categories")]
        public IActionResult Categories (){
            List<Categoria> AllCategories = dbContext.categories.ToList();
            ViewBag.categories = AllCategories;
            return View("Categories");
        }
        [HttpGet("products/{id}")]
        public IActionResult DisplayProduct(int id){
            DisplayProductView viewModel = new DisplayProductView();
            
            viewModel.product = dbContext.products
                .Include(p=>p.Associations)
                .ThenInclude(ass=>ass.Categoria)
                .FirstOrDefault(p=>p.ProductId == id);
            viewModel.categorias = dbContext.categories
                .Include(cat=>cat.Associations)
                .ThenInclude(ass=>ass.Product)
                .Where(catt=>!catt.Associations.Select(a=>a.ProductId).Contains(id))
                .ToList();
            
            return View("Displayproduct", viewModel);
        }

        [HttpGet("categories/{id}")]
        public IActionResult DisplayCategories(int id){

            return View("DisplayCategories");
        }  

        [HttpPost("newproduct")]
        public IActionResult AddNewProduct(Association newProduct){
            dbContext.products.Add(newProduct.Product);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost("newcatrgories")]
        public IActionResult NewCategories(Association newCategoria){
            dbContext.categories.Add(newCategoria.Categoria);
            dbContext.SaveChanges();
            return RedirectToAction ("Categories");
        }
        [HttpPost("addassosiation/{id}")]
        public IActionResult addAssociation(DisplayProductView newAss, int id){
            Association newass = new Association();
            newass.ProductId = id;
            newass.CategoriaId = newAss.CategoriaId;
            dbContext.associations.Add(newass);
            dbContext.SaveChanges();
            return RedirectToAction("DisplayProduct", new {id=id});
        }
    }
}
