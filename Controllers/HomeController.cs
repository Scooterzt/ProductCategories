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
            DisplayCategoriaView viewModel = new DisplayCategoriaView();
            viewModel.categoria = dbContext.categories
            .Include(c=>c.Associations)
            .ThenInclude(ass=>ass.Product)
            .FirstOrDefault(p=>p.CategoriaId == id);
            viewModel.products = dbContext.products
            .Include(p=>p.Associations)
            .ThenInclude(ass=>ass.Categoria)
            .Where(prod=>!prod.Associations.Select(a=>a.CategoriaId).Contains(id))
            .ToList();
            return View("DisplayCategories", viewModel);
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
        public IActionResult addAssociation(DisplayProductView newAssosuation, int id){
            Association tempass = new Association();
            tempass.ProductId = id;
            tempass.CategoriaId = newAssosuation.CategoriaId;
            dbContext.associations.Add(tempass);
            dbContext.SaveChanges();
            return RedirectToAction("DisplayProduct", new {id=id});
        }

        [HttpPost("addassociationcategoria/{id}")]
        public IActionResult addAssociationCategoria (DisplayCategoriaView newAssosuation,int id){
            Association tempcat = new Association();
            tempcat.CategoriaId = id;
            tempcat.ProductId = newAssosuation.ProductId;
            dbContext.associations.Add(tempcat);
            dbContext.SaveChanges();
            return RedirectToAction("DisplayCategories", new{id=id});
        }
    }
}
