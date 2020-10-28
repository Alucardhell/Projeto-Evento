using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tasken.Gerenciador.Eventos.Controlador.Repositorios
{
    public class RepositorioBase
    {
        private readonly string _connectionString;

        public RepositorioBase(string connectionString)
        {
            _connectionString = connectionString;
        }
        public SqlConnection AbrirConexao()
        {

            SqlConnection _sqlCon = new SqlConnection(_connectionString);
            _sqlCon.Open();
            Console.WriteLine("ServerVersion: {0}", _sqlCon.ServerVersion);
            Console.WriteLine("State: {0}", _sqlCon.State);
            return _sqlCon;
        }

        public void FecharConexao()
        {
            SqlConnection _sqlCon = new SqlConnection(_connectionString);
            _sqlCon.Close();
            Console.WriteLine("Fechando Conexão.");
        }

        protected void ExecutarComandoNoQuery(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = AbrirConexao();
                cmd.ExecuteNonQuery();

            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao executar um comando {ex.Message}.", "Erro.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                FecharConexao();
            }
        }


        protected int ExecutarComandoExecuteScalar(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = AbrirConexao();
                var retornoSQL = cmd.ExecuteScalar();
                int resultado;
                if (retornoSQL == null)
                {
                    resultado = 0;
                }
                else
                {
                    resultado = 1;
                }

                return resultado;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao executar um comando {ex.Message}.", "Erro.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
                FecharConexao();
            }
        }

        protected SqlDataReader ExecutarComandoReader(SqlCommand cmd)
        {
            try
            {
                cmd.Connection = AbrirConexao();
                SqlDataReader dr = cmd.ExecuteReader();
                return dr;
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro ao executar um comando {ex.Message}.", "Erro.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                FecharConexao();
            }
        }
    }
}
