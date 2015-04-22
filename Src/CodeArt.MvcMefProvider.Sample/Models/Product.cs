using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CodeArt.MvcMefProvider.Sample.Models
{
    public class Product
    {
        public int Id { get; set; }

        [StringLength(5)]
        public string Sku { get; set; }

        [StringLength(50, MinimumLength = 2)]
        public string Name { get; set; }

        [Range(0.01, 999999)]
        public double Price { get; set; }

        [Range(0, 1000000)]
        public int Stock { get; set; }
    }
}