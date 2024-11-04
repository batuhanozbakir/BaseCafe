using BaseCafe.BLL.DTOs;
using BaseCafe.BLL.Managers.Abstract;
using BaseCafe.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BaseCafe.UI.Controllers
{
  
    public class CategoryController : Controller
    {
        private readonly IGenericManager<CategoryDTO, Category> _categoryManager;

        public CategoryController(IGenericManager<CategoryDTO, Category> categoryManager)
        {
            _categoryManager = categoryManager;
        }

        public IActionResult Index()
        {
            var categories = _categoryManager.GetAll();
            return View(categories);
        }
    }
}
