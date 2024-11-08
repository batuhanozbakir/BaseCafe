using BaseCafe.BLL.DTOs;
using BaseCafe.BLL.Managers.Abstract;
using BaseCafe.DAL.Entities.Concrete;
using BaseCafe.DAL.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BaseCafe.UI.Controllers
{
    //[Authorize("Customer")]

    public class CartController : Controller

    {

        //Card alınarak ürnün bilgileir kategoris olucak sonrasında eltında SepeteEkle buttonu olucak bunu indexe yapacağız menu tasarlayalım 

        private readonly IGenericManager<ProductDTO, Product> _productManager;

        private readonly IGenericManager<CategoryDTO, Category> _categoryManager;

        private readonly IGenericManager<OrderDTO, Order> _orderManager;

        private readonly IGenericManager<OrderDetailDTO, OrderDetail> _orderDetailManager;

        private readonly IMailService _mailService;

        public CartController(IGenericManager<ProductDTO, Product> productManager, IGenericManager<CategoryDTO, Category> categoryManager, IGenericManager<OrderDTO, Order> orderManager, IGenericManager<OrderDetailDTO, OrderDetail> orderDetailManager, IMailService mailService)

        {

            _categoryManager = categoryManager;

            _productManager = productManager;

            _orderManager = orderManager;

            _orderDetailManager = orderDetailManager;

            _mailService = mailService;

        }

        public async Task<IActionResult> Index()

        {

            var products = _productManager.GetAll();

            await _mailService.SendMailAsync("bozbakir06@gmail.com", "Slm bıyığını yediğim", "çıkışta gel pisuvarda yan yana <3", true);

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

        public IActionResult CompleteOrder([FromBody] List<CartDTO> cart)

        {

            if (cart == null || !cart.Any())

            {

                return BadRequest("Cart is null");

            }

            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //sepetteki toplam tutar

            var totalAmount = cart.Sum(item => item.Quantity * _productManager.Find(item.ProductId).Price);

            //yeni sipariş oluştur

            var newOrder = new OrderDTO(0, DateTime.Now, totalAmount, "Created", userId);

            //sipariş ekle

            var createdOrder = _orderManager.Add(newOrder);

            //cookieden OrderId yi tutma

            Response.Cookies.Append("OrderId", createdOrder.Id.ToString()); //Transection eklenebilirmiş. Buna bakarız belki.

            foreach (var item in cart)

            {

                //sipariş detayları oluşacak

                var orderDetail = new OrderDetailDTO(0, createdOrder.Id, item.ProductId, item.Quantity, _productManager.Find(item.ProductId).Price);

                //sipariş detaylarını ekle

                _orderDetailManager.Add(orderDetail);

            }

            return Ok("Order Success");

        }

    }

}
