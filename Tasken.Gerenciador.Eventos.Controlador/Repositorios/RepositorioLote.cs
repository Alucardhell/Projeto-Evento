using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasken.Gerenciador.Eventos.Modelos.Modelos;

namespace Tasken.Gerenciador.Eventos.Controlador.Repositorios
{
    public class RepositorioLote : RepositorioBase
    {
        private readonly string _connectionString;

        public RepositorioLote(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;

        }

        public void Inserir(Lote lote)
        {
            string _query = $"INSERT INTO LOTE VALUES('{lote.Nome}', {lote.Preco}, '{lote.DataInicio.ToString("dd/MM/yyyy")}', '{lote.DataFim.ToString("dd/MM/yyyy")}', {lote.Quantidade}, (select Eventoid from evento (nolock) where tema = '{lote.NomeEvento}'))";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

        public void Alterar(Lote lote)
        {
            string _query = $"update lote set Nome = '{lote.Nome}', Preco = {lote.Preco}, DataInicio = '{lote.DataInicio.ToString("dd/MM/yyyy")}', DataFim = '{lote.DataFim.ToString("dd/MM/yyyy")}', Quantidade = {lote.Quantidade} ,EventoId = (select Eventoid from evento(nolock) where tema = '{lote.NomeEvento}') where Loteid = {lote.Loteid}";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

        public List<Lote> ConsultarTodos()
        {
            string _query = "SELECT E.Tema, L.Loteid, L.Nome, L.Preco, L.DataInicio, L.DataFim, L.Quantidade FROM LOTE as L INNER JOIN EVENTO as E on L.EventoId = E.EventoId order by L.EventoId";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            List<Lote> listaConsulta = new List<Lote>();
            while (dr.Read())
            {
                Lote criarLote = new Lote(dr.GetString(0), dr.GetInt32(1), dr.GetString(2), dr.GetDouble(3), dr.GetDateTime(4), dr.GetDateTime(5), dr.GetInt32(6));
                listaConsulta.Add(criarLote);
            }
            return listaConsulta;
        }

        public List<Lote> ConsultarPorEvento(string eventoTema)
        {
            string _query = $"SELECT E.Tema, L.Loteid, L.Nome, L.Preco, L.DataInicio, L.DataFim, L.Quantidade FROM LOTE as L INNER JOIN EVENTO as E on L.EventoId = E.EventoId where E.Tema = '{eventoTema}' order by L.EventoId";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            List<Lote> listaConsulta = new List<Lote>();
            while (dr.Read())
            {
                Lote criarLote = new Lote(dr.GetString(0), dr.GetInt32(1), dr.GetString(2), dr.GetDouble(3), dr.GetDateTime(4), dr.GetDateTime(5), dr.GetInt32(6));
                listaConsulta.Add(criarLote);
            }
            return listaConsulta;
        }

        public void Excluir(int id)
        {
            string _query = $"DELETE FROM Lote WHERE Loteid = {id}";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }
    }
}
