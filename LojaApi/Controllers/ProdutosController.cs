using System;
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
    public class ProdutosController : ApiController
    {

        public IHttpActionResult GetAllProducts()
        {
            object response = this.execFindSqlComand("select * from produtos FOR JSON AUTO");

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        public IHttpActionResult GetProduct(int id)
        {               
            object response = this.execFindSqlComand("select * from produtos where id = " + id + " FOR JSON AUTO");

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        public IHttpActionResult PostProduct(LojaApi.Models.Produto produto)
        {
            this.execCreateUpdateProcedureComand(produto);

            return Ok(produto);
        }

        public IHttpActionResult PutProduct(LojaApi.Models.Produto produto)
        {
            this.execCreateUpdateProcedureComand(produto);

            return Ok(produto);
        }

        public IHttpActionResult DeleteProduct(int id)
        {
            this.execDeleteProcedureComand(id);

            return Ok(id);
        }

        private Object execFindSqlComand(string sql)
        {
            string strcon = System.Configuration.ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            SqlConnection DbConnection = new SqlConnection(strcon);        
            DbConnection.Open();
            SqlCommand command = new SqlCommand(sql, DbConnection);
            SqlDataReader reader = command.ExecuteReader();

            string retorno = "";
            while (reader.Read())
            {
                retorno = retorno + String.Format("{0}", reader[0]);
            }

            JavaScriptSerializer j = new System.Web.Script.Serialization.JavaScriptSerializer();
            object response = j.Deserialize(retorno, typeof(object));

            DbConnection.Close();

            return response;
        }

        private void execCreateUpdateProcedureComand(LojaApi.Models.Produto produto)
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

        private void execDeleteProcedureComand(int id)
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
