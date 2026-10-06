using Microsoft.AspNetCore.Mvc;
using WebAppProductsandCateories.Data;
using WebAppProductsandCateories.Models;

namespace WebAppProductsandCateories.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryData _dataAccess;
        public CategoryController(CategoryData dataAccess)
        {
            _dataAccess = dataAccess;
        }

        // Index - Mostrar categorías
        public IActionResult Index()
        {
            var categories = _dataAccess.GetCategories();
            return View(categories);
        }

        // Create - Mostrar formulario para crear una nueva categoría
        public IActionResult Create()
        {
            return View();
        }

        // Create - Formulario de proceso
        [HttpPost]
        public IActionResult Create(CategoriesModel category)
        {
            if (ModelState.IsValid)
            {
                _dataAccess.CreateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // Edit - Mostrar formulario para editar una categoría existente
        public IActionResult Edit(int id)
        {
            var category = _dataAccess.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // Edit - Formulario de proceso
        [HttpPost]
        public IActionResult Edit(CategoriesModel category)
        {
            if (ModelState.IsValid)
            {
                _dataAccess.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // Delete - Mostrar formulario para eliminar una categoría existente
        public IActionResult Delete(int id)
        {
            var category = _dataAccess.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // Delete - Formulario de proceso
        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int CategoryId)
        {
            var ok = _dataAccess.DeleteCategory(CategoryId);
            if (!ok)
            {
                TempData["Error"] = "No se puede eliminar la categoría: existen productos asignados.";
                return RedirectToAction("Delete", new { id = CategoryId });
            }
            return RedirectToAction("Index");
        }
    }
}
