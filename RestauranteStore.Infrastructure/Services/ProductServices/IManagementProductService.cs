using AutoMapper;
using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using NToastNotify;
using RestaurantStore.Core.ModelViewModels;
using RestaurantStore.Core.Results;
using RestaurantStore.EF.Data;
using RestaurantStore.EF.Models.TestExcel;
using RestaurantStore.Infrastructure.Hubs;
using System.Data;
using System.Diagnostics;
using System.Linq.Dynamic.Core;
using System.Text;
using static RestaurantStore.Core.Enums.Enums;

namespace RestaurantStore.Infrastructure.Services.ProductServices
{
    public interface IManagementProductService
    {
        Task<ResultProduct> AddProductAsync(List<ProductExcel> dataList, string connectionId, string userId);
        ProductExcel? GetProductExcel(string? SKU);
        Task<ResultProduct> UpdateProductExcelAsync(DataRow row);
        Task<bool> AddFromFile(IFormFile file, string connectionId, string userId);
        object? GetAllProducts(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir);
        byte[] ExportExcel(string search = "");

    }

    public class ManagementProductService : IManagementProductService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IToastNotification toastNotification;
        private readonly IHubContext<FileUploadHub> hubContext;
        private readonly IMapper mapper;
        private int _chunkRowLimit = 1000;
        private List<Task<ResultProduct>> tasks = new List<Task<ResultProduct>>();
        private int countRows = 0;
        private readonly object _progressLock = new object();
        private readonly object _taskLock = new object();
        private List<string> existingData = new List<string>();
        private int progress { get; set; } = 0;

        public ManagementProductService(ApplicationDbContext dbContext
            , IToastNotification toastNotification,
            IHubContext<FileUploadHub> hubContext,
            IMapper mapper)
        {
            this.dbContext = dbContext;
            this.toastNotification = toastNotification;
            this.hubContext = hubContext;
            this.mapper = mapper;

        }

        private IQueryable<ProductExcel> GetAllProducts(string search, string filter)
        {
            Manufacturer filterEnum;
            search = search.ToLower();

            var users = dbContext.ProductExcels
                .Where(x => (string.IsNullOrEmpty(search) ? true :
                                (x.Description ?? "").ToLower().Contains(search!)
                             || (x.SKU ?? "").ToLower().Contains(search!)
                           )
                    &&
                    (!string.IsNullOrEmpty(filter) ? Enum.TryParse(filter, out filterEnum) ? x.Manufacturer == filterEnum : false : true)
                );
            return users;

        }

        public object? GetAllProducts(int pageLength, int skiped, StringValues searchData, StringValues sortColumn, StringValues sortDir)
        {

            var products = GetAllProducts(searchData[0] ?? "", "");
            var recordsTotal = products.Count();

            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortDir))
                products = products.OrderBy(string.Concat(sortColumn, " ", sortDir));


            var data = products.Skip(skiped).Take(pageLength).Include(x => x.CategoryExcel)
                .ToList();
            var productsExcelViewModel = mapper.Map<IEnumerable<ProductsExcelViewModel>>(data);
            var jsonData = new { recordsFiltered = recordsTotal, recordsTotal, data = productsExcelViewModel };

            return jsonData;

        }


        public byte[] ExportExcel(string search = "")
        {
            try
            {
                var data = GetAllProducts(search, "").Include(x => x.CategoryExcel).ToList();
                var excelFilePath = Guid.NewGuid().ToString() + ".xlsx";
                var excelContent = new StringBuilder();

                //using (StreamWriter sw = new StreamWriter(excelFilePath))
                //            {
                var entityType = dbContext.Model.FindEntityType(typeof(ProductExcel));
                var propertyNames = entityType!.GetProperties().Select(p => p.Name);

                foreach (var propertyName in propertyNames)
                {
                    excelContent.Append(propertyName);
                    excelContent.Append("\t");
                }
                excelContent.AppendLine();
                foreach (var item in data)
                {
                    excelContent.Append($"{item.SKU}\t{item.Band}\t{item.Manufacturer}\t{item.CategoryExcel.Code}" +
                        $"\t{item.Description}\t{item.ListPrice}\t{item.DiscountedPrice}\t{item.MinimumDiscount}");
                    //excelContent.Append(item.SKU);
                    //excelContent.Append("\t");
                    //excelContent.Append(item.Band);
                    //excelContent.Append("\t");
                    //excelContent.Append(item.Manufacturer);
                    //excelContent.Append("\t");
                    //excelContent.Append(item.CategoryExcel.Code);
                    //excelContent.Append("\t");
                    //excelContent.Append(item.Description);
                    //excelContent.Append("\t");
                    //excelContent.Append(item.ListPrice);
                    //excelContent.Append("\t");
                    //excelContent.Append(item.DiscountedPrice);
                    //excelContent.Append("\t");
                    //excelContent.Append(item.MinimumDiscount);
                    //excelContent.Append("\t");
                    excelContent.AppendLine();
                }
                // }
                var excelBytes = Encoding.UTF8.GetBytes(excelContent.ToString());

                // Provide the Excel file for download
                return excelBytes;

            }
            catch (Exception ex)
            {
                return new byte[0];
            }
        }

        public async Task<bool> AddFromFile(IFormFile file, string connectionId, string userId)
        {
            try
            {
                string tempFilePath = Path.Combine(Path.GetTempPath(), file.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), $@"wwwroot\", file.FileName);
                using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                    fileStream.Close();
                }
                using (var stream = File.Open(tempFilePath, FileMode.Open, FileAccess.Read))
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    existingData = dbContext.ProductExcels.Select(e => e.SKU).ToList();
                    //List<IExcelDataReader> data = new List<IExcelDataReader>();
                    //do
                    //{
                    //    countRows += reader.RowCount;
                    //    while (reader.Read())
                    //    {
                    //        if (reader.IsDBNull(1)
                    //          || reader.IsDBNull(2)
                    //          || reader.IsDBNull(3)
                    //          || reader.IsDBNull(4)
                    //          || reader.IsDBNull(5)
                    //          || reader.IsDBNull(6)
                    //          || reader.IsDBNull(7)
                    //          || reader.IsDBNull(8)
                    //          || (reader.GetValue(1) is string) )
                    //        {
                    //            countRows--;
                    //        }
                    //        else
                    //        {
                    //            data.Add(reader);
                    //        }

                    //    }
                    //} while (reader.NextResult());
                    //reader.Reset();
                    var dataList = new List<ProductExcel>();
                    do
                    {
                        var nameSheet = reader.Name;
                        var block = 0;
                        var val = 0;
                        while (reader.Read())
                        {
                            if (reader.IsDBNull(1)
                              || reader.IsDBNull(4)
                              || reader.IsDBNull(7)
                              || reader.IsDBNull(5)
                              || reader.IsDBNull(6)
                              || reader.IsDBNull(2)
                              || reader.IsDBNull(3)
                              || reader.IsDBNull(8)
                              || !(reader.GetValue(1) is double)
                              || !(reader.GetValue(2) is string)
                              || !(reader.GetValue(3) is string)
                              || !(reader.GetValue(4) is string)
                              || !(reader.GetValue(5) is string)
                              || !(reader.GetValue(6) is double)
                              || !(reader.GetValue(7) is double)
                              || !(reader.GetValue(8) is double)
                              )
                            {
                                block++;
                                if (block >= 30)
                                {
                                    reader.NextResult();
                                    nameSheet = reader.Name;
                                    block = 0;
                                    val = 0;
                                    continue;
                                }
                                continue;
                            }
                            countRows++;
                            if (block != 0)
                                block = 0;
                            if (val == 0)
                            {
                                val++;
                                continue;
                            }
                            ProductExcel ProductModel = new ProductExcel();
                            ProductModel = new ProductExcel();
                            ProductModel.Band = int.Parse(reader.GetDouble(1) + "");
                            ProductModel.CategoryExcelCode = reader.GetString(2);
                            Manufacturer manufacturer;
                            Enum.TryParse(reader.GetString(3), out manufacturer);
                            ProductModel.Manufacturer = manufacturer;
                            ProductModel.SKU = reader.GetString(4);
                            ProductModel.Description = reader.GetString(5);
                            ProductModel.ListPrice = reader.GetDouble(6);
                            ProductModel.DiscountedPrice = reader.GetDouble(7);
                            ProductModel.MinimumDiscount = reader.GetDouble(8);
                            dataList.Add(ProductModel);
                            if (dataList.Count() >= _chunkRowLimit)
                            {
                                tasks.Add(AddProductAsync(dataList, connectionId, userId));
                                dataList.Clear();
                            }
                        }
                    } while (reader.NextResult()); // Move to the next sheet if available
                    if (dataList.Count() > 0)
                    {
                        tasks.Add(AddProductAsync(dataList, connectionId, userId));
                    }
                    stream.Close();
                    stream.Dispose();
                }
                await Task.WhenAll(tasks);
                lock (_progressLock)
                {
                    progress = countRows;
                }
                double prog = GetProgress();

                await hubContext.Clients.Group(userId).SendAsync("ReceiveProgress", prog);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }


        public async Task<ResultProduct> AddProductAsync(List<ProductExcel> dataList, string connectionId, string userId)
        {
            var resultProduct = new ResultProduct()
            {
                Process = Core.Enums.Enums.Process.add,
                SKUs = new List<string>(),
                SKUsFailed = new List<string>()
            };
            List<ProductExcel> commonRows = new List<ProductExcel>();

            var newRecordes = dataList
            .GroupBy(m => m.SKU)
            .Select(group => group.Last()) // Keep only the last element from each group
            .ToList();



            var nonDuplicateNewRecords = newRecordes
                .Where(newRecord => !existingData.Contains(newRecord.SKU))
                .ToList();

            var DuplicateNewRecords = newRecordes
                .Where(newRecord => existingData.Contains(newRecord.SKU))
                .ToList();

            UpdateRangeAsync(DuplicateNewRecords);
            lock (dbContext)
            {
                dbContext.ProductExcels.AddRangeAsync(nonDuplicateNewRecords);
                dbContext.SaveChangesAsync();
            }
            lock (existingData)
            {
                existingData.AddRange(nonDuplicateNewRecords.Select(x => x.SKU).ToList());
            }
            lock (_progressLock)
            {
                progress += _chunkRowLimit;
            }
            double prog = GetProgress();

            await hubContext.Clients.Group(userId).SendAsync("ReceiveProgress", prog);

            return resultProduct;
        }

        private void UpdateRangeAsync(List<ProductExcel> products)
        {
            var productsToUpdate = new List<ProductExcel>();
            foreach (var product in products)
            {
                var dbProduct = GetProductExcel(product.SKU);
                if (dbProduct != null)
                    productsToUpdate.Add(dbProduct);
            }
            lock (dbContext)
            {
                dbContext.ProductExcels.UpdateRange(productsToUpdate);
                dbContext.SaveChangesAsync();
            }
        }
        private double GetProgress()
        {
            double prog;
            lock (_progressLock)
            {
                prog = progress;
            }
            return prog / countRows * 100;
        }




        public ProductExcel? GetProductExcel(string? SKU)
        {
            return dbContext.ProductExcels.FirstOrDefault(x => x.SKU.Equals(SKU));
        }

        public async Task<ResultProduct> UpdateProductExcelAsync(DataRow row)
        {
            var productExcel = new ProductExcel()
            {
                Band = (int)row[1],
                CategoryExcelCode = (string)row[3],
                Manufacturer = (Manufacturer)row[2],
                SKU = (string)row[0],
                Description = (string)row[4],
                ListPrice = (double)row[5],
                MinimumDiscount = (double)row[7],
                DiscountedPrice = (double)row[6],
            };

            var resultProduct = new ResultProduct()
            {
                Process = Core.Enums.Enums.Process.update,
            };
            if (productExcel == null || string.IsNullOrEmpty(productExcel.SKU))
            {
                resultProduct.Status = Status.Failed;
                return resultProduct;
            }
            resultProduct.SKU = productExcel.SKU;
            var productdb = GetProductExcel(productExcel.SKU);
            if (productdb == null)
            {
                resultProduct.Status = Status.Failed;
                return resultProduct;
            }
            productdb.Manufacturer = productExcel.Manufacturer;
            productdb.ListPrice = productExcel.ListPrice;
            productdb.DiscountedPrice = productExcel.DiscountedPrice;
            productdb.CategoryExcelCode = productExcel.CategoryExcelCode;
            productdb.MinimumDiscount = productExcel.MinimumDiscount;
            productdb.Band = productExcel.Band;
            productdb.Description = productExcel.Description;

            dbContext.ProductExcels.Update(productdb);
            await dbContext.SaveChangesAsync();
            return resultProduct;
        }
        static int GetAvailableFreeMemory()
        {
            using (PerformanceCounter counter = new PerformanceCounter("Memory", "Available Bytes"))
            {
                return (int)counter.NextValue();
            }
        }

    }
}


//foreach (DataRow row in dataTable.Rows)
//{
//    var productExcel = dbContext.ProductExcels.FirstOrDefault(x => x.SKU.Equals((string)row[0])); // Implement this method to query the database
//    if (productExcel != null)
//    {
//        productExcel.Band = (int)row[1];
//        productExcel.Manufacturer = (Manufacturer)row[2];
//        productExcel.CategoryExcelCode = (string)row[3];
//        productExcel.Description = (string)row[4];
//        productExcel.ListPrice = (double)row[5];
//        productExcel.MinimumDiscount = (double)row[6];
//        productExcel.DiscountedPrice = (double)row[7];
//        commonRows.Add(productExcel);
//    }
//    else
//    {
//        DataRow newRow = dataTableInsert.NewRow();
//        newRow.ItemArray = row.ItemArray;
//        dataTableInsert.Rows.Add(newRow);
//    }
//}
//if (commonRows.Count() > 0)
//{
//    dbContext.ProductExcels.UpdateRange(commonRows);
//    await dbContext.SaveChangesAsync();
//}
//if (dataTableInsert.Rows.Count > 0)
//{
//    connectionString = dbContext.Database.GetConnectionString() ?? "";
//    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
//    {
//        sqlConnection.Open();
//        using var bulkCopy = new SqlBulkCopy(sqlConnection)
//        {
//            DestinationTableName = "ProductExcels",
//            BulkCopyTimeout = 300
//        };
//        foreach (DataColumn column in dataTableInsert.Columns)
//        {
//            bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
//        }
//        await bulkCopy.WriteToServerAsync(dataTableInsert);
//    }
//}