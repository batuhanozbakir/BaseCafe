﻿using BaseCafe.BLL.DTOs;
using BaseCafe.BLL.Managers.Abstract;
using BaseCafe.DAL.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace BaseCafe.UI.Controllers
{
	public class ProductController : Controller
	{
		private readonly IGenericManager<ProductDTO, Product> _productManager;
		private readonly IGenericManager<CategoryDTO, Category> _categoryManager;

		public ProductController(IGenericManager<ProductDTO, Product> productManager,IGenericManager<CategoryDTO, Category> categoryManager)
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

		public IActionResult Create()
		{
			ViewBag.Categories = _productManager.GetAll();
			return View();
		}

		[HttpPost]
		public IActionResult Create(ProductDTO productDTO)
		{
			if (ModelState.IsValid)
			{
				_productManager.Add(productDTO);
				return RedirectToAction(nameof(Index));
			}
			ViewBag.Categories = _categoryManager.GetAll();

			return View(productDTO);
		}

		public IActionResult Edit(int id)
		{
			var product = _productManager.Find(id);
			ViewBag.Categories = _categoryManager.GetAll();
			return View(product);
		}

		[HttpPost]
		public IActionResult Edit(ProductDTO productDTO)
		{
			if (ModelState.IsValid)
			{
				_productManager.Update(productDTO);
				return RedirectToAction(nameof(Index));
			}
			ViewBag.Categories = _categoryManager.GetAll();
			return View(productDTO);

		}

		public IActionResult Delete(ProductDTO productDTO)
		{
			_productManager.Remove(productDTO);
			return RedirectToAction(nameof(Index));
		}
	}
}
