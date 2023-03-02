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
        public DataTable selecionarUsuario(string email)
        {
            SqlConnection conn = new SqlConnection(Conectar());
            SqlCommand cmdo = new SqlCommand();
            DataTable dt = new DataTable();

            try
            {
                conn.Open();
                cmdo.Connection = conn;
                cmdo.CommandType = CommandType.Text;
                cmdo.CommandText = "select *from Usuario where Email = @Email";
                cmdo.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = usuario.Email;
                SqlDataReader dr = cmdo.ExecuteReader();
                dt.Load(dr);

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
        public bool inserirUsuario(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(Conectar());
            bool result = false;
            int cadastrar;

            try
            {
                conn.Open();
                SqlCommand cmdo = new SqlCommand("inserirUsuario", conn);
                cmdo.CommandType = CommandType.StoredProcedure;
                cmdo.Parameters.Add("@Email", SqlDbType.VarChar, 50).Value = usuario.Email;
                cmdo.Parameters.Add("@Nome", SqlDbType.VarChar, 100).Value = usuario.Nome;
                cmdo.Parameters.Add("@Senha", SqlDbType.VarChar, 50).Value = usuario.Senha;
                cadastrar = cmdo.ExecuteNonQuery();

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
                conn.Close();
            }

            return result;

        }
        public bool alterarSenha(Usuario usuario)
        {
            SqlConnection conn = new SqlConnection(Conectar());
            bool result = false;
            int alterar;

            try
            {
                conn.Open();
                SqlCommand cmdo = new SqlCommand("alterarSenha",conn);
                cmdo.CommandType = CommandType.StoredProcedure;
                cmdo.Parameters.Add("@idUsuario", SqlDbType.Int).Value = usuario.idUsuario;
                cmdo.Parameters.Add("@Senha", SqlDbType.VarChar, 50).Value = usuario.Senha;
                alterar = cmdo.ExecuteNonQuery();

                if (alterar >= 1)
                {
                    result = true;
                }
                if(alterar < 1)
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                conn.Close();
            }

            return result;

        }


    }
}
