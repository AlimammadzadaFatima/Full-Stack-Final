using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanBack.Models
{
    public class Portfolio:BaseEntity
    {
        public string Image { get; set; }
        [NotMapped]
        public IFormFile FileImage { get; set; }
    }
}
