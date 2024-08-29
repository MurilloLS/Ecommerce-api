using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models;

namespace ECommerceApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}