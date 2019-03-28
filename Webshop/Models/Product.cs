using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Webshop.Models
{
    public class Product
    {
        public int id { get; set; }
        public int price{ get; set; }
        public int category_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public Product()
        {
        }
    }
}
