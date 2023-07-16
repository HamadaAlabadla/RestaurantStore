using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using RestauranteStore.Core.Dtos;
using RestauranteStore.EF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestauranteStore.Infrastructure.Services.ProductService
{
	public interface IProductService
	{
		Product? GetProduct(int id);
		List<Product>? GetProduct(string name);
		List<Product>? GetProducts();
		Task<object?> GetAllProducts(HttpRequest request);
		int CreateProduct(ProductDto productDto);
		int UpdateProduct(ProductDto? productDto);
		int UpdateProduct(Product? product);
		Product? DeleteProduct(int id);
	}
}
