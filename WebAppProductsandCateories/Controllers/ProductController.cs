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
        public IActionResult Create(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                _dataAccess.CreateProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = GetCategories(); // para lista dropdown
            return View(product);
        }

        // Edit - Mostrar formulario para editar un producto existente
        public IActionResult Edit (int id)
        {
            var product = _dataAccess.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.Categories = GetCategories(); // para lista dropdown
            return View(product);
        }

        // Edit - Formulario de proceso
        [HttpPost]
        public IActionResult Edit(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                _dataAccess.UpdateProduct(product);
                return RedirectToAction("Index");
            }
            ViewBag.Categories = GetCategories(); // para lista dropdown
            return View(product);
        }

        // Delete - Mostrar formulario para eliminar un producto existente
        public IActionResult Delete(int id)
        {
            var product = _dataAccess.GetProductById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        })
        // Delete - Formulario de proceso
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int ProductId)
        {
            _dataAccess.DeleteProduct(ProductId);
            return RedirectToAction("Index");
        }

        private List<CategoriesModel> GetCategories()
        {
            return _dataAccessCat.GetCategories();
        }
    }
}
