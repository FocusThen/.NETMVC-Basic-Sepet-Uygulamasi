using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Uygulama1.Models
{
    public class Urun
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Ad { get; set; }
        public int Adet { get; set; }
        public decimal Fiyat { get; set; }
    }
}