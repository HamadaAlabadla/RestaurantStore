﻿using System.ComponentModel.DataAnnotations;
using static RestauranteStore.Core.Enums.Enums;

namespace RestauranteStore.Core.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        [Required]
        public string? SupplierId { get; set; }
        [Required]
        public string RestaurantId { get; set; }
        [Required]
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime DateCreate { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        public string ShippingAddress { get; set; }
        [Required]
        public string ShippingCity { get; set; }
        [Required]
        public StatusOrder StatusOrder { get; set; }
        public bool IsDraft { get; set; }
        //[Required]
        //public List<OrderItemDto> OrderItems { get; set; } = new List<OrderItemDto>();
    }
}
