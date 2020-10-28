using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasken.Gerenciador.Eventos.Controlador.Repositorios
{
    public class FabricaRepositorio : RepositorioBase
    {
        private readonly string _connectionString;

        public FabricaRepositorio(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
        }


        private RepositorioEvento _repositorioEvento;
        public RepositorioEvento RepositorioEvento { get { return _repositorioEvento ?? (_repositorioEvento = new RepositorioEvento(_connectionString)); } }

        private RepositorioPalestrante _repositorioPalestrante;
        public RepositorioPalestrante RepositorioPalestrante { get { return _repositorioPalestrante ?? (_repositorioPalestrante = new RepositorioPalestrante(_connectionString)); } }

        private RepositorioRedeSocial _repositorioRedeSocial;

        public RepositorioRedeSocial RepositorioRedeSocial { get { return _repositorioRedeSocial ?? (_repositorioRedeSocial = new RepositorioRedeSocial(_connectionString)); } }

        private RepositorioLote _repositorioLote;

        public RepositorioLote RepositorioLote { get { return _repositorioLote ?? (_repositorioLote = new RepositorioLote(_connectionString)); } }

        private RepositorioLogin _repositorioLogin;

        public RepositorioLogin RepositorioLogin { get { return _repositorioLogin ?? (_repositorioLogin = new RepositorioLogin(_connectionString)); } }
    }
}
