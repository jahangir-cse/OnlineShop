using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Models
{
    public class ProductTypes
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Product Type")]
        //[Unique(ErrorMessage = "This already exist !!")]
        public string ProductType { get; set; }
    }
}
