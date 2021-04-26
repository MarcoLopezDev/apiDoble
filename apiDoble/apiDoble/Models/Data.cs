using System;
using System.ComponentModel.DataAnnotations;

namespace apiProductorParcial.Models
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