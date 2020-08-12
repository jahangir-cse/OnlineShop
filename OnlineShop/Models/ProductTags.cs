using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class ProductTags
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Tag")]
        public string ProductTag { get; set; }
    }
}
