using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Webshop.Models
{
    public class Category
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
