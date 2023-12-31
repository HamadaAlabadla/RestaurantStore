﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using NToastNotify;
using RestaurantStore.Core.Dtos;
using RestaurantStore.Core.ModelViewModels;
using RestaurantStore.EF.Data;
using RestaurantStore.EF.Models;
using RestaurantStore.Infrastructure.Services.FileService;
using System.Linq.Dynamic.Core;
using static RestaurantStore.Core.Enums.Enums;

namespace RestaurantStore.Infrastructure.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IFileService fileService;
        private readonly IMapper mapper;
        private readonly IToastNotification toastNotification;
        public ProductService(ApplicationDbContext dbContext,
            IMapper mapper,
            IFileService fileService,
            IToastNotification toastNotification)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
            this.fileService = fileService;
            this.toastNotification = toastNotification;
        }

        public async Task<int> CreateProduct(ProductDto productDto)
        {
            if (productDto == null)
            {
                toastNotification.AddWarningToastMessage("Error entering product data");
                return -1;
            }
            var product = GetProduct(productDto.Name);
            if (product != null)
            {
                toastNotification.AddWarningToastMessage($"The product already exists.</br> ID: #{product.ProductNumber} , Name: ${product.Name}");
                return -1;
            }
            product = mapper.Map<Product>(productDto);
            if (productDto.Image != null)
            {
                var imagePath = await fileService.UploadFile(productDto.Image, "products", Guid.NewGuid().ToString());
                if (string.IsNullOrEmpty(imagePath))
                {
                    return -1;
                }
                product.Image = imagePath;
            }
            if (product == null)
            {
                toastNotification.AddWarningToastMessage($"Internal error, we could not add the product");
                return -1;
            }
            product.DateCreate = DateTime.UtcNow;
            dbContext.Products.Add(product);
            dbContext.SaveChanges();
            toastNotification.AddSuccessToastMessage($"The product has been added successfully. </br> ID:${product.ProductNumber} , Name: ${product.Name}");
            return product.ProductNumber;
        }

        public Product? DeleteProduct(int id)
        {
            var product = GetProduct(id);
            if (product == null)
            {
                toastNotification.AddErrorToastMessage($"Product #{id} not found");
                return null;
            }
            product.isDelete = true;
            UpdateProduct(product);
            toastNotification.AddSuccessToastMessage($"The product #{product.ProductNumber} has been successfully deleted");
            return product;
        }
        private IQueryable<Product> GetAllProducts(string search, string filter, string supplierId)
        {
            search = search.ToLower();
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
            var statusToSearch = Enum.Parse<StatusProduct>(Enum.GetValues(typeof(StatusProduct))
                         .Cast<StatusProduct>()
                         .FirstOrDefault(enumValue => enumValue.ToString().ToLower().Contains(search!)).ToString(), ignoreCase: true);

            var users = dbContext.Products.Include(x => x.User)
                          .Where(x => !x.isDelete
                                && (string.IsNullOrEmpty(supplierId) ? x.Status != StatusProduct.Draft : true)
                                && (supplierId.Equals("admin") || x.UserId.Equals(supplierId))
                                && (filterEnum == null || x.Status == filterEnum)
                                && (string.IsNullOrEmpty(search!)
                                    || x.Name.ToLower().Contains(search!)
                                    || statusToSearch == x.Status
                                    || (x.Category.Name ?? "").ToLower().Contains(search!)
                                    || (x.Description ?? "").ToLower().Contains(search!)
                                    || x.Price.ToString().ToLower().Contains(search!)
                                    || (x.UnitPrice.Name ?? "").ToLower().Contains(search!)

                                    ));
            return users;


        }

        public object? GetAllProducts(HttpRequest request, string supplierId)
        {
            var pageLength = int.Parse(request.Form["length"].ToString() ?? "");
            var skiped = int.Parse(request.Form["start"].ToString() ?? "");
            var searchData = request.Form["search"];
            var sortColumn = request.Form[string.Concat("columns[", request.Form["order[0][column]"], "][name]")];
            var sortDir = request.Form["order[0][dir]"];
            var filter = request.Form["filter"];
            if (string.IsNullOrEmpty(filter))
                filter = new StringValues("") { };
            if (string.IsNullOrEmpty(searchData))
                searchData = new StringValues("") { };
            var products = GetAllProducts(searchData[0] ?? "", filter[0] ?? "", supplierId);
            var recordsTotal = products.Count();

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDir))
                products = products.OrderBy(string.Concat(sortColumn, " ", sortDir));


            var data = products.Include(x => x.User).Include(x => x.Category).Include(x => x.QuantityUnit).Include(x => x.UnitPrice)
                .Skip(skiped).Take(pageLength).ToList();
            var productsViewModel = mapper.Map<IEnumerable<ProductViewModel>>(data);
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = productsViewModel };

            return jsonData;
        }


        public object? GetAllProductsItemDto(HttpRequest request, string supplierId, string userId)
        {
            var pageLength = int.Parse(request.Form["length"].ToString() ?? "");
            var skiped = int.Parse(request.Form["start"].ToString() ?? "");
            var searchData = request.Form["search"];
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
                .Where(x => x.Status == StatusProduct.Published)
                .Skip(skiped).Take(pageLength).ToList();
            var productsViewModel = mapper.Map<IEnumerable<OrderItemDto>>(data);
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = productsViewModel };

            return jsonData;
        }

        public Product? GetProduct(int id)
        {
            return dbContext.Products.Where(x => !x.isDelete)
                .Include(x => x.User).Include(x => x.Category).Include(x => x.QuantityUnit).Include(x => x.UnitPrice)
                .FirstOrDefault(x => x.ProductNumber == id);
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
            if (productDto == null)
            {
                toastNotification.AddWarningToastMessage("Error entering product data");
                return -1;
            }
            var product = mapper.Map<Product>(productDto);
            if (product == null)
            {
                toastNotification.AddErrorToastMessage($"Product #{productDto.ProductNumber} not found");
                return -1;
            }
            if (productDto.Image != null)
            {
                var imagePath = await fileService.UploadFile(productDto.Image, "products", product.ProductNumber + "");
                if (string.IsNullOrEmpty(imagePath))
                    return -1;
                product.Image = imagePath ?? "";
            }
            var id = UpdateProduct(product);
            return id;
        }

        public int UpdateProduct(Product? product)
        {
            if (product == null)
            {
                toastNotification.AddErrorToastMessage($"Product not found");
                return -1;
            }
            var productNew = GetProduct(product.ProductNumber);
            if (productNew == null)
            {
                toastNotification.AddErrorToastMessage($"Product #{product.ProductNumber} not found");
                return -1;
            }
            productNew.UnitPriceId = product.UnitPriceId;
            productNew.QuantityUnitId = product.QuantityUnitId;
            productNew.CategoryId = product.CategoryId;
            productNew.Description = product.Description;
            productNew.isDelete = product.isDelete;
            productNew.Image = product.Image ?? productNew.Image;
            productNew.Name = product.Name;
            productNew.Status = product.Status;

            dbContext.Products.Update(productNew);
            dbContext.SaveChanges();
            toastNotification.AddSuccessToastMessage($"The product #{product.ProductNumber} has been updated successfully");
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
