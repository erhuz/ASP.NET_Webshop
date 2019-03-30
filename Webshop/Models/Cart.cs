using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models;

namespace Webshop.Models
{
    public class Cart
    {
        public int id { get; set; }
        public List<Product> items { get; set; }
    }
}
