using BaseCafe.BLL.DTOs;
using BaseCafe.BLL.Managers.Abstract;
using BaseCafe.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BaseCafe.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly IGenericManager<ProductDTO, Product> _productManager;

        private readonly IGenericManager<CategoryDTO,Category> _categoryManager;

        public CartController(IGenericManager<ProductDTO, Product> productManager, IGenericManager<CategoryDTO, Category> categoryManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
        }

        public IActionResult Index()
        {
            var products = _productManager.GetAll();
            var productDtos = products.Select(
                p => new
                {
                    p.Id,
                    p.CategoryID,
                    p.Name,
                    p.Price,
                    p.Description,
                    p.StockQuantity,
                    categoryName = _categoryManager.Find(p.CategoryID)?.Name
                }
                ).ToList();
            return View(productDtos);
        }
    }
}
