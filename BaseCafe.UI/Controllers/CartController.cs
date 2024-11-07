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

        private readonly IGenericManager<OrderDTO,Order> _orderManager;

        private readonly IGenericManager<OrderDetailDTO,OrderDetail> _orderDetailManager;

        public CartController(IGenericManager<ProductDTO, Product> productManager, IGenericManager<CategoryDTO, Category> categoryManager, IGenericManager<OrderDTO, Order> orderManager = null, IGenericManager<OrderDetailDTO, OrderDetail> orderDetailManager = null)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _orderManager = orderManager;
            _orderDetailManager = orderDetailManager;
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

        public IActionResult Cart()
        {
            return View();
        }

        [HttpPost]

        public IActionResult CompleteOrder([FromBody]List<CartDTO> cart)
        {
            if (cart == null || !cart.Any())
            { 
                return BadRequest("Cart is null");
            }

            //sepetteki toplam tutar
            var totalAmount = cart.Sum(item => item.Quantity* _productManager.Find(item.ProductId).Price);

            //yeni sipariş oluştur
            var newOrder = new OrderDTO(0,DateTime.Now,totalAmount,"Created");

            //sipariş ekle
            var createdOrder = _orderManager.Add(newOrder);


            //cookieden OrderIDyi tutma
            Response.Cookies.Append("OrderID", createdOrder.Id.ToString());

            foreach (var item in cart)
            {
                //sipariş detayları oluştur
                var orderDetail = new OrderDetailDTO(0,createdOrder.Id,item.ProductId,item.Quantity,_productManager.Find(item.ProductId).Price);

                //sipariş detaylarını ekle
                _orderDetailManager.Add(orderDetail);
            }
            return Ok("Order Success");

        }
         
    }
}
