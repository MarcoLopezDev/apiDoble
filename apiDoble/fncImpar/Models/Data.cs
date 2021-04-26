using System;
using System.ComponentModel.DataAnnotations;

namespace fncImpar.Models
{
    public class Data
    {
        [Key]
        [Required]
        public string random { get; set; }

        [DataType(DataType.DateTime)]
        [Required]
        public DateTime DateTime { get; set; }

    }
}
