using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasken.Gerenciador.Eventos.Modelos.Modelos;

namespace Tasken.Gerenciador.Eventos.Controlador.Repositorios
{
    public class RepositorioLogin : RepositorioBase
    {
        private readonly string _connectionString;

        public RepositorioLogin(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;

        }

        public int VerificarLogin(string login, string senha)
        {

            string _query = $"select * from CONTROLE_USUARIO where usuario = '{login}' and senha = '{senha}'";
            return ExecutarComandoExecuteScalar(new SqlCommand(_query));
        }

    }
}
