﻿using System.ComponentModel.DataAnnotations;

namespace RestauranteStore.EF.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required, StringLength(50), DataType(dataType: DataType.Text)]
        public string? Name { get; set; }
        [Required, StringLength(15), DataType(dataType: DataType.Text)]
        public string? UserName { get; set; }
        [Required, DataType(dataType: DataType.EmailAddress)]
        public string? Email { get; set; }
        [Required, StringLength(9), DataType(dataType: DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        [Required]
        public string? Logo { get; set; }
        [Required]
        public string? MainBranchName { get; set; }
        [Required]
        public string? MainBranchAddress { get; set; }
        [Required]
        public string? UserId { get; set; }
        public User? User { get; set; }

    }
}
