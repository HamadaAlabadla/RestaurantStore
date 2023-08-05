using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using RestauranteStore.Core.Dtos;
using RestauranteStore.Core.ModelViewModels;
using RestauranteStore.EF.Data;
using RestauranteStore.EF.Models;
using RestauranteStore.Infrastructure.Services.FileService;
using System.Linq.Dynamic.Core;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.Infrastructure.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        public ProductService(ApplicationDbContext dbContext,
            IMapper mapper,
            IFileService fileService)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.fileService = fileService;
        }

        public async Task<int> CreateProduct(ProductDto productDto)
        {
            if (productDto == null) return -1;
            var product = GetProduct(productDto.Name);
            if (product != null) return -1;
            product = mapper.Map<Product>(productDto);
            if (productDto.Image != null)
            {
                var imagePath = await fileService.UploadFile(productDto.Image, "products", Guid.NewGuid().ToString());
                product.Image = imagePath ?? "";
            }
            if (product == null) return -1;
            product.DateCreate = DateTime.UtcNow;
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
        private IQueryable<Product> GetAllProducts(string search, string filter, string supplierId)
        {
            StatusProduct? filterEnum = null;
            if (!string.IsNullOrEmpty(filter))
            {
                try
                {
                    filterEnum = (StatusProduct)Enum.Parse(typeof(StatusProduct), filter, true);
                }
                catch
                {
                    filterEnum = null;
                }
            }
            var users = dbContext.Products.Include(x => x.Category)
                .Where(x => !x.isDelete
                        && (
                            (filterEnum == null) ? true : x.Status == filterEnum
                        )
                        && (string.IsNullOrEmpty(supplierId) || x.UserId.Equals(supplierId))
                      );
            return users;


        }

        public object? GetAllProducts(HttpRequest request, string supplierId)
        {
            var pageLength = int.Parse((request.Form["length"].ToString()) ?? "");
            var skiped = int.Parse((request.Form["start"].ToString()) ?? "");
            var searchData = request.Form["search[value]"];
            var sortColumn = request.Form[string.Concat("columns[", request.Form["order[0][column]"], "][name]")];
            var sortDir = request.Form["order[0][dir]"];
            var filter = request.Form["filter"];
            if (string.IsNullOrEmpty(filter))
                filter = new StringValues("") { };
            var products = GetAllProducts(searchData[0] ?? "", filter[0] ?? "", "");
            var recordsTotal = products.Count();

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDir))
                products = products.OrderBy(string.Concat(sortColumn, " ", sortDir));


            var data = products.Include(x => x.User).Include(x => x.Category).Include(x => x.QuantityUnit).Include(x => x.UnitPrice)
                .Where(x => supplierId.Equals("admin") ? true : (x.User.UserType == UserType.supplier) && x.UserId.Equals(supplierId))
                .Skip(skiped).Take(pageLength).ToList();
            var productsViewModel = mapper.Map<IEnumerable<ProductViewModel>>(data);
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = productsViewModel };

            return jsonData;
        }

        public object? GetAllProductsItemDto(HttpRequest request, string supplierId, string userId)
        {
            var pageLength = int.Parse((request.Form["length"].ToString()) ?? "");
            var skiped = int.Parse((request.Form["start"].ToString()) ?? "");
            var searchData = request.Form["search[value]"];
            var sortColumn = request.Form[string.Concat("columns[", request.Form["order[0][column]"], "][name]")];
            var sortDir = request.Form["order[0][dir]"];
            var filter = request.Form["filter"];
            if (string.IsNullOrEmpty(filter))
                filter = new StringValues("") { };

            var products = GetAllProducts(searchData[0] ?? "", filter[0] ?? "", supplierId);
            var recordsTotal = products.Count();

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDir))
                products = products.OrderBy(string.Concat(sortColumn, " ", sortDir));


            var data = products.Include(x => x.User).Include(x => x.Category).Include(x => x.QuantityUnit).Include(x => x.UnitPrice)
                .Skip(skiped).Take(pageLength).ToList();
            var productsViewModel = mapper.Map<IEnumerable<OrderItemDto>>(data);
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = productsViewModel };

            return jsonData;
        }

        public Product? GetProduct(int id)
        {
            return dbContext.Products.Include(x => x.User).Include(x => x.Category).Include(x => x.QuantityUnit).Include(x => x.UnitPrice)
                .Where(x => !x.isDelete).FirstOrDefault(x => x.ProductNumber == id);
        }

        public Product? GetProduct(string name)
        {
            return dbContext.Products.Include(x => x.User).Include(x => x.Category).Include(x => x.QuantityUnit).Include(x => x.UnitPrice)
                .Where(x => !x.isDelete).FirstOrDefault(x => (x.Name ?? "").Equals(name));
        }

        public List<Product>? GetProducts(string name)
        {
            return dbContext.Products.Include(x => x.User).Include(x => x.Category).Include(x => x.QuantityUnit).Include(x => x.UnitPrice)
                .Where(x => !x.isDelete && (x.Name ?? "").Equals(name)).ToList();
        }

        public async Task<int> UpdateProduct(ProductDto? productDto)
        {
            if (productDto == null) return -1;
            var product = mapper.Map<Product>(productDto);
            if (product == null) return -1;
            if (productDto.Image != null)
            {
                var imagePath = await fileService.UploadFile(productDto.Image, "products", product.ProductNumber + "");
                product.Image = imagePath ?? "";
            }
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
            productNew.Image = product.Image ?? productNew.Image;
            productNew.Name = product.Name;
            try
            {
                dbContext.Products.Update(productNew);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

            }
            return productNew.ProductNumber;
        }

        public int UpdateProductPrice(int productId, double price)
        {
            var product = GetProduct(productId);
            if (product == null) return -1;
            product.Price = price;
            UpdateProduct(product);
            return product.ProductNumber;
        }

        public int UpdateProductQTY(int productId, double QTY)
        {
            var product = GetProduct(productId);
            if (product == null) return -1;
            product.QTY = QTY;
            UpdateProduct(product);
            return product.ProductNumber;
        }
    }
}
