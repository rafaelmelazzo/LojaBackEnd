using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LojaApi.Controllers
{
    public class ProdutosController : ApiController
    {
        Models.Produto[] produtos = new Models.Produto[]
        {
            new Models.Produto { Id = 1, Nome = "Tomato1 Soup", Categoria = "Groceries", Preco = 1 },
            new Models.Produto { Id = 2, Nome = "Yo-yo", Categoria = "Toys", Preco = 3.75M },
            new Models.Produto { Id = 3, Nome = "Hammer", Categoria = "Hardware", Preco = 16.99M }
        };

        public IEnumerable<Models.Produto> GetAllProducts()
        {
            return produtos;
        }

        public IHttpActionResult GetProduct(int id)
        {
            var product = produtos.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
    }
}
