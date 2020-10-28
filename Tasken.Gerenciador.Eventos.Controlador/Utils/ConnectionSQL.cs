using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Tasken.Gerenciador.Eventos.Controlador.Utils
{
    public static class ConnectionSQL
    {
        public static string connectionString = ConfigurationManager.ConnectionStrings["ConexaoBancoTeste"]?.ToString() ?? string.Empty;

    }
}
