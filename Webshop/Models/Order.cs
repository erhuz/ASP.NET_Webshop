using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models;

namespace Webshop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Items { get; set; }
    }
}
