using BaseCafe.BLL.DTOs;
using BaseCafe.BLL.Managers.Abstract;
using BaseCafe.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BaseCafe.UI.Controllers
{
	public class OrderDetailController : Controller
	{
		private readonly IGenericManager<ProductDTO, Product> _productManager;
		private readonly IGenericManager<OrderDTO, Order> _orderManager;
		private readonly IGenericManager<OrderDetailDTO, OrderDetail> _orderDetailManager;
		private readonly IGenericManager<CategoryDTO, Category> _categoryManager;

		public OrderDetailController(IGenericManager<ProductDTO, Product> productManager, IGenericManager<OrderDTO, Order> orderManager, IGenericManager<OrderDetailDTO, OrderDetail> orderDetailManager, IGenericManager<CategoryDTO, Category> categoryManager)
		{
			_productManager = productManager;
			_orderManager = orderManager;
			_orderDetailManager = orderDetailManager;
			_categoryManager = categoryManager;
		}
		public IActionResult Index()
		{
            var orders = _orderManager.GetAll();

            var orderDetails = _orderDetailManager.GetAll();

            var orderDtos = orders.Select(o => new

            {

                o.Id,

                o.OrderDate,

                o.TotalAmount,

                o.Status,

                OrderDetails = orderDetails.Where(od => od.OrderId == o.Id).Select(od => new

                {

                    od.ProductId,

                    od.Quantity,

                    od.UnitPrice,

                    ProductName = _productManager.Find(od.ProductId)?.Name,

                    CategoryName = _categoryManager.Find(_productManager.Find(od.ProductId)?.CategoryID ?? 0)?.Name

                }).ToList()


            }).ToList();

            return View(orderDtos);

        }
    }
}
