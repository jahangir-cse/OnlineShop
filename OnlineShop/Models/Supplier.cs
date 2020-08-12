using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineShop.Models
{
    public class Supplier
    {
        public int Id { get; set; }
        [Required]
        public string Code { get; set; }
        [Required]
        [Display(Name ="Supplier Name")]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Mobile Number")]
        public string MobileNo { get; set; }   
        public string Company { get; set; }
        [Required]
        public string Address { get; set; }
        public string Image { get; set; }        
    }
}
