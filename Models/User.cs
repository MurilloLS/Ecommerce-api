using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ECommerceApi.Models;

namespace ECommerceApi.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public ShoppingCart? ShoppingCart { get; set; }
    }
}