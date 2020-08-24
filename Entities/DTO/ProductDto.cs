using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
   public class ProductDto
    {
        public int Id { get; set; }
        [Required]
        public CompanyDto Company { get; set; }
        public string CodeInCompany { get; set; }
        [Required]
        public int R { get; set; }
        [Required]
        public int G { get; set; }
        [Required]
        public int B { get; set; }
        [Required]
        public double Price { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }

    }
}
