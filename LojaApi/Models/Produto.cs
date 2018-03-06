using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LojaApi.Models
{
    public class Produto
    {
        public int id { get; set; }
        public string nome { get; set; }
        public string categoria { get; set; }
        public decimal preco { get; set; }   
    }
}