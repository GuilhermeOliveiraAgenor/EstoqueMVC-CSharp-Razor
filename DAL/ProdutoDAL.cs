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
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");//arquivo da string de conexao

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
       
       public bool inserirProduto(Produto produto)
       {
            SqlConnection conn = new SqlConnection(conectar());
            int cadastrar;
            bool result = false;

            try
            {
                conn.Open();//abrir conexao
                SqlCommand cmdo = new SqlCommand("inserirProduto", conn);//defini procedure
                cmdo.CommandType = CommandType.StoredProcedure;
                cmdo.Parameters.Add("@Nome", SqlDbType.VarChar,50).Value = produto.Nome;//parametros
                cmdo.Parameters.Add("@Preco", SqlDbType.Decimal).Value = produto.Preco;
                cmdo.Parameters.Add("@Quantidade", SqlDbType.Int).Value = produto.Quantidade;
                cmdo.Parameters.Add("@Observacoes", SqlDbType.VarChar).Value = produto.Observacoes;

                cadastrar = cmdo.ExecuteNonQuery();//recebe o resultado

                if (cadastrar >= 1)
                {
                    result = true;
                }
                if (cadastrar < 1)
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();//fechar conexao
            }

            return result;

       }

       public bool alterarProduto(Produto produto)
       {
            SqlConnection conn = new SqlConnection(conectar());
            int cadastrar;
            bool result = false;

            try
            {
                conn.Open();
                SqlCommand cmdo = new SqlCommand("alterarProduto", conn);//defini procedure 
                cmdo.CommandType = CommandType.StoredProcedure;
                cmdo.Parameters.Add("@idProduto", SqlDbType.Int).Value = produto.idProduto;//parametros
                cmdo.Parameters.Add("@Nome", SqlDbType.VarChar, 50).Value = produto.Nome;
                cmdo.Parameters.Add("@Preco", SqlDbType.Decimal).Value = produto.Preco;
                cmdo.Parameters.Add("@Quantidade", SqlDbType.Int).Value = produto.Quantidade;
                cmdo.Parameters.Add("@Observacoes", SqlDbType.VarChar, 100).Value = produto.Observacoes;

                cadastrar = cmdo.ExecuteNonQuery();//recebe o resultado

                if (cadastrar >= 1)
                {
                    result = true;
                }
                if (cadastrar < 1)
                {
                    result = false;
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();//fechar conexao
            }

            return result;

        }
       public List<Produto> pesqProdutoCodigo(int id)
       {
            List<Produto> produtos = new List<Produto>();
            SqlConnection conn = new SqlConnection(conectar());
            SqlCommand cmdo = new SqlCommand();

            try
            {
                conn.Open();
                cmdo.Connection = conn;
                cmdo.CommandType = CommandType.Text;
                cmdo.CommandText = "select *from Produto where idProduto = @idProduto";
                cmdo.Parameters.Add("@idProduto", SqlDbType.Int).Value = id;
                SqlDataReader dr = cmdo.ExecuteReader();

                while(dr.Read())
                {
                    produtos.Add(new Produto()
                    {
                        idProduto = Convert.ToInt32(dr["idProduto"]),
                        Nome = dr["Nome"].ToString(),
                        Preco = Convert.ToDecimal(dr["Preco"]),
                        Quantidade = Convert.ToInt32(dr["Quantidade"]),
                        Observacoes = dr["Observacoes"].ToString()
                    });
                }

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return produtos;


       }

    }
}
