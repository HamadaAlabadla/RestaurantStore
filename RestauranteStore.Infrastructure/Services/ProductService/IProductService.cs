using Microsoft.AspNetCore.Http;
using RestaurantStore.Core.Dtos;
using RestaurantStore.EF.Models;

namespace RestaurantStore.Infrastructure.Services.ProductService
{
    public interface IProductService
    {
        Product? GetProduct(int id);
        List<Product>? GetProducts(string name);
        Product? GetProduct(string name);
        public object? GetAllProducts(HttpRequest request, string supplierId);
        public object? GetAllProductsItemDto(HttpRequest request, string supplierId, string userId);
        Task<int> CreateProduct(ProductDto productDto);
        Task<int> UpdateProduct(ProductDto? productDto);
        int UpdateProduct(Product? product);
        int UpdateProductPrice(int productId, double price);
        int UpdateProductQTY(int productId, double QTY);
        Product? DeleteProduct(int id);
    }
}
