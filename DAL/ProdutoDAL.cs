using System.Data.SqlClient;
using System.Data;
using EstoqueMVC.Models;

namespace EstoqueMVC.DAL
{
    public class ProdutoDAL
    {
       public static IConfiguration Configuration { get; set; }

       private string conectar()
       {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsetting.json");//arquivo da string de conexao

            Configuration = builder.Build();
            return Configuration.GetConnectionString("conexao");//recebe a string de conexao no arquivo

       }

       public List<Produto> listarProduto()
       {
            List<Produto> produtos = new List<Produto>();  
            SqlConnection conn = new SqlConnection(conectar());
            SqlCommand cmdo = new SqlCommand();

            try
            {
                conn.Open();
                cmdo.Connection = conn;
                cmdo.CommandType = CommandType.Text;
                cmdo.CommandText = "select *from Produto";
                SqlDataReader dr = cmdo.ExecuteReader();
                while (dr.Read())
                {
                    produtos.Add(new Produto()
                    {
                        idProduto = Convert.ToInt32(dr["idProduto"].ToString()),
                        Nome = dr["Nome"].ToString(),
                        Preco = Convert.ToDecimal(dr["Preco"]),
                        Quantidade = Convert.ToInt32(dr["Quantidade"].ToString()),
                        Observacoes = dr["Observacoes"].ToString(),
                    });
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return produtos;

       }

    }
}
