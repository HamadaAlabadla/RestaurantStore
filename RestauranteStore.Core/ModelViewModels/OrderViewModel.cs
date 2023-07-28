using RestauranteStore.Core.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RestauranteStore.Core.Enums.Enums;

namespace RestaurantStore.Core.ModelViewModels
{
	public class OrderViewModel
	{
		public int Id { get; set; }
		public string SupplierId { get; set; }
		public string SupplierName { get; set; }
		public string SupplierImage { get; set; }
		public string SupplierEmail { get; set; }
		public string SupplierPhoneNumber { get; set; }
		public string RestaurantId { get; set; }
		public string RestaurantName { get; set; }
		public string RestaurantImage { get; set; }
		public string RestaurantEmail { get; set; }
		public string RestaurantPhoneNumber { get; set; }
		public double TotalPrice { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public string DateCreate { get; set; }
		[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
		public string OrderDate { get; set; }
		public string DateModified { get; set; }
		public string ShippingAddress { get; set; }
		public string ShippingCity { get; set; }
		public StatusOrder StatusOrder { get; set; }
		public ICollection<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
		public bool isDelete { get; set; } = false;
	}
}
