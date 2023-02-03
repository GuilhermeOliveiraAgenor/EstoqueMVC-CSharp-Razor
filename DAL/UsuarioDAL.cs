using EstoqueMVC.Models;
using System.Data;
using System.Data.SqlClient;

namespace EstoqueMVC.DAL
{
    public class UsuarioDAL
    {
        public static IConfiguration Configuration { get; set; }
        private string Conectar()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");//arquivo da string de conexao

            Configuration = builder.Build();
            return Configuration.GetConnectionString("conexao");//recebe a string de conexao no arquivo

        }

        public DataTable Login(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(Conectar());
            SqlCommand cmdo = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                conn.Open();//abrir conexao
                cmdo.Connection = conn;
                cmdo.CommandType = CommandType.Text;//defini text
                cmdo.CommandText = "select Email, Senha from Usuario where Email = @Email and Senha = @Senha";
                cmdo.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = usuario.Email;//parametros
                cmdo.Parameters.Add("@Senha", SqlDbType.VarChar, 50).Value = usuario.Senha;
              
                SqlDataReader dr = cmdo.ExecuteReader();
                dt.Load(dr);//carrega o dt

            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }
            
            return dt;

        }

    }
}
