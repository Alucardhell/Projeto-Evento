using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasken.Gerenciador.Eventos.Modelos.Modelos;


namespace Tasken.Gerenciador.Eventos.Controlador.Repositorios
{
    public class RepositorioEvento : RepositorioBase
    {
        private readonly string _connectionString;

        public RepositorioEvento(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;
            

        }

        public void Inserir(Evento evento)
        {

            string _query = $"INSERT INTO EVENTO VALUES('{evento.Local}','{evento.DataEvento.ToString("dd/MM/yyyy")}','{evento.Tema}',{evento.Qtd},'{evento.ImagemUrl}','{evento.Telefone}')";
            ExecutarComandoNoQuery(new SqlCommand(_query));

        }

        public List<Evento> ConsultarTodos()
        {
            string _query = "SELECT * FROM EVENTO";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            List<Evento> listaConsulta = new List<Evento>();
            while (dr.Read())
            {
                Evento criarEvento = new Evento(dr.GetInt32(0), dr.GetString(1), dr.GetDateTime(2), dr.GetString(3), dr.GetInt32(4), dr.GetString(5), dr.GetString(6));
                listaConsulta.Add(criarEvento);
            }
            return listaConsulta;
        }

        public Evento ConsultarPorId(int id)
        {
            string _query = $"SELECT * FROM EVENTO WHERE EventoId = {id}";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            Evento buscaEvento = new Evento();

            while (dr.Read())
            {
                buscaEvento.EventoID = dr.GetInt32(0);
                buscaEvento.Local = dr.GetString(1);
                buscaEvento.DataEvento = dr.GetDateTime(2);
                buscaEvento.Tema = dr.GetString(3);
                buscaEvento.Qtd = dr.GetInt32(4);
                buscaEvento.ImagemUrl = dr.GetString(5);
                buscaEvento.Telefone = dr.GetString(6);

            }
            return buscaEvento;
        }

        public Evento ConsultarPorNome(string nome)
        {
            string _query = $"SELECT * FROM EVENTO WHERE Tema = '{nome}'";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            Evento buscaEvento = new Evento();

            while (dr.Read())
            {
                buscaEvento.EventoID = dr.GetInt32(0);
                buscaEvento.Local = dr.GetString(1);
                buscaEvento.DataEvento = dr.GetDateTime(2);
                buscaEvento.Tema = dr.GetString(3);
                buscaEvento.Qtd = dr.GetInt32(4);
                buscaEvento.ImagemUrl = dr.GetString(5);
                buscaEvento.Telefone = dr.GetString(6);

            }
            return buscaEvento;
        }

        public List<Evento> ConsultarEntreDatas(string dataInicio, string dataFim)
        {
            string _query = $"Select * from Evento where DataEvento between '{dataInicio}' and '{dataFim}'";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            List<Evento> listaConsulta = new List<Evento>();
            while (dr.Read())
            {
                Evento criarEvento = new Evento(dr.GetInt32(0), dr.GetString(1), dr.GetDateTime(2), dr.GetString(3), dr.GetInt32(4), dr.GetString(5), dr.GetString(6));
                listaConsulta.Add(criarEvento);
            }
            return listaConsulta;
        }

            public void Excluir (int id)
        {
            string _query = $"DELETE FROM EVENTO WHERE EventoId = {id}";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }


        public void ExcluirPalestranteEvento(int id)
        {
            string _query = $"DELETE FROM PalestranteEvento WHERE EventoId = {id}";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

        public void Alterar (Evento evento, int id)
        {
            string _query = $"UPDATE EVENTO SET Local = '{evento.Local}', DataEvento = '{evento.DataEvento.ToString("dd/MM/yyyy")}', Tema = '{evento.Tema}', QtdPessoas = {evento.Qtd}, ImagemUrl = '{evento.ImagemUrl}', Telefone = '{evento.Telefone}' WHERE eventoid = {id}";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }
    }
}
