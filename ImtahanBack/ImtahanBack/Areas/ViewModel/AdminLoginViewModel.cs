using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImtahanBack.Areas.ViewModel
{
    public class AdminLoginViewModel
    {
        [StringLength(maximumLength:50)]
        [Required]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 25)]
        [Required]
        public string Password { get; set; }
    }
}
