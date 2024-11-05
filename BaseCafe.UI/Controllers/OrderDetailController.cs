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

		public OrderDetailController(IGenericManager<ProductDTO, Product> productManager, IGenericManager<OrderDTO, Order> orderManager, IGenericManager<OrderDetailDTO, OrderDetail> orderDetailManager)
		{
			_productManager = productManager;
			_orderManager = orderManager;
			_orderDetailManager = orderDetailManager;
		}
		public IActionResult Index()
		{

			return View();
		}
	}
}
