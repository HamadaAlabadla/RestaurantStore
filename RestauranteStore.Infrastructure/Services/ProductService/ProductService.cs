using AutoMapper;
using Microsoft.AspNetCore.Http;
using RestauranteStore.Core.Dtos;
using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Infrastructure.Services.ProductService
{
	public class ProductService : IProductService
	{
		private readonly ApplicationDbContext dbContext;
		private readonly IMapper mapper;
		public ProductService(ApplicationDbContext dbContext,
			IMapper mapper)
		{
			this.dbContext = dbContext;
			this.mapper = mapper;
		}

		public int CreateProduct(ProductDto productDto)
		{
			var product = mapper.Map<Product>(productDto);
			if (product == null) return -1;
			dbContext.Products.Add(product);
			dbContext.SaveChanges();
			return product.ProductNumber;
		}

		public Product? DeleteProduct(int id)
		{
			var product = GetProduct(id);
			if (product == null) return null;
			product.isDelete = true;
			UpdateProduct(product);
			return product;
		}


		public Task<object?> GetAllProducts(HttpRequest request)
		{
			throw new NotImplementedException();
		}

		public Product? GetProduct(int id)
		{
			return dbContext.Products.Where(x => !x.isDelete).FirstOrDefault(x => x.ProductNumber == id);
		}

		public List<Product>? GetProduct(string name)
		{
			return dbContext.Products.Where(x => !x.isDelete && (x.Name ?? "").Equals(name)).ToList();
		}

		public List<Product>? GetProducts()
		{
			throw new NotImplementedException();
		}

		public int UpdateProduct(ProductDto? productDto)
		{
			if(productDto == null) return -1;
			var product = mapper.Map<Product>(productDto);
			var id = UpdateProduct(product);
			return id;
		}

		public int UpdateProduct(Product? product)
		{
			if (product == null) return -1;
			var productNew = GetProduct(product.ProductNumber);
			if (productNew == null) return -1;
			productNew.UnitPriceId = product.UnitPriceId;
			productNew.QuantityUnitId = product.QuantityUnitId;
			productNew.CategoryId = product.CategoryId;
			productNew.Description = product.Description;
			productNew.isDelete = product.isDelete;
			productNew.Image = product.Image;
			productNew.Name = product.Name;
			dbContext.Products.Update(productNew);
			dbContext.SaveChanges();
			return productNew.ProductNumber;
		}
	}
}
