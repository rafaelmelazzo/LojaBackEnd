using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Newtonsoft.Json;

namespace LojaApi.Controllers
{
    /// <summary>
    /// ProdutosController controller de gerenciamento de produtos
    /// </summary>
    public class ProdutosController : ApiController
    {

        /// <summary>
        /// Retorna todos os produtos
        /// </summary>
        public IHttpActionResult GetAllProducts()
        {
            object response = this.ExecFindCommand(0);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Retorna produto pelo Id
        /// </summary>
        public IHttpActionResult GetProduct(int id)
        {               
            object response = this.ExecFindCommand(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        /// <summary>
        /// Cria produto
        /// </summary>
        public IHttpActionResult PostProduct(LojaApi.Models.Produto produto)
        {
            this.ExecCreateUpdateProcedureCommand(produto);

            return Ok(produto);
        }

        /// <summary>
        /// Altera produto
        /// </summary>
        public IHttpActionResult PutProduct(LojaApi.Models.Produto produto)
        {
            this.ExecCreateUpdateProcedureCommand(produto);

            return Ok(produto);
        }

        /// <summary>
        /// Exclui produto
        /// </summary>
        public IHttpActionResult DeleteProduct(int id)
        {
            this.ExecDeleteProcedureCommand(id);

            return Ok(id);
        }

        /// <summary>
        /// Executa busca no banco de dados via SQL
        /// </summary>
        private Object ExecFindSqlCommand(string sql)
        {
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            SqlConnection DbConnection = new SqlConnection(strcon);        
            DbConnection.Open();
            SqlCommand command = new SqlCommand(sql, DbConnection);
            SqlDataReader reader = command.ExecuteReader();

            ArrayList objs = new ArrayList();

            while (reader.Read())
            {
                objs.Add(new
                {
                    id = reader["id"],
                    categoria = reader["categoria"],
                    nome = reader["nome"],
                    preco = reader["preco"]
                });
            }

            return objs;
        }

        /// <summary>
        /// Executa busca no banco de dados via STORED PROCEDURE
        /// </summary>
        private Object ExecFindCommand(int id)
        {
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            SqlConnection DbConnection = new SqlConnection(strcon);
            DbConnection.Open();
            SqlCommand command = new SqlCommand("[dbo].[usp_find_produtos]", DbConnection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@id", SqlDbType.Int, 10).Value = id;
            SqlDataReader reader = command.ExecuteReader();

            ArrayList objs = new ArrayList();

            while (reader.Read())
            {
                objs.Add(new
                {
                    id = reader["id"],
                    categoria = reader["categoria"],
                    nome = reader["nome"],
                    preco = reader["preco"]
                });
            }

            return objs;
        }

        /// <summary>
        /// Realiza persistência no banco de dados (insert/update) via STORED PROCEDURE
        /// </summary>
        private void ExecCreateUpdateProcedureCommand(LojaApi.Models.Produto produto)
        {
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            SqlConnection DbConnection = new SqlConnection(strcon);
            DbConnection.Open();

            SqlCommand command = new SqlCommand("[dbo].[usp_ins_alt_produto]", DbConnection);

            command.Parameters.Add("@id", SqlDbType.Int, 10).Value = produto.id;
            command.Parameters.Add("@nome", SqlDbType.VarChar, 50).Value = produto.nome;
            command.Parameters.Add("@categoria", SqlDbType.VarChar, 20).Value = produto.categoria;
            command.Parameters.Add("@preco", SqlDbType.Float, 10).Value = produto.preco;

            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
        }

        /// <summary>
        /// Realiza exclusão no banco de dados (delete) via STORED PROCEDURE
        /// </summary>
        private void ExecDeleteProcedureCommand(int id)
        {
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            SqlConnection DbConnection = new SqlConnection(strcon);
            DbConnection.Open();

            SqlCommand command = new SqlCommand("[dbo].[usp_del_produto]", DbConnection);

            command.Parameters.Add("@id", SqlDbType.Int, 10).Value = id;

            command.CommandType = CommandType.StoredProcedure;
            command.ExecuteNonQuery();
        }
    }
}
