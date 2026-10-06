using Microsoft.AspNetCore.Mvc;
using WebAppProductsandCateories.Data;
using WebAppProductsandCateories.Models;

namespace WebAppProductsandCateories.Controllers
{
    public class ProductController : Controller
    {
        private readonly ProductData _dataAccess;
        private readonly CategoryData _dataAccessCat;

        public ProductController(ProductData dataAccess, CategoryData dataAccessCategory)
        {
            _dataAccess = dataAccess;
            _dataAccessCat = dataAccessCategory;
        }

        public IActionResult Index()
        {
            var products = _dataAccess.GetProducts();
            ViewBag.Categories = GetCategories();
            return View(products);
        }

        // Create - Mostrar formulario para crear un nuevo producto
        public IActionResult Create()
        {
            ViewBag.Categories = GetCategories(); // para lista dropdown
            return View();
        }

        // Create - Formulario de proceso
        [HttpPost]
    }
}
