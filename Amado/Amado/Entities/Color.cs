﻿using System.ComponentModel.DataAnnotations;

namespace Amado.Entities
{
    public class Color
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string HexCode { get; set; }

        public List<Product> Products { get; set; }
    }
}
