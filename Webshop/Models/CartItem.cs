using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Webshop.Models;

namespace Webshop.Models
{
    public class CartItem
    {
        public int? CartId { get; set; }
        public int ProductId{ get; set; }
    }
}
