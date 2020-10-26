using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasken.Gerenciador.Eventos.Modelos.Modelos;

namespace Tasken.Gerenciador.Eventos.Controlador.Repositorios
{
    public class RepositorioRedeSocial : RepositorioBase
    {
        private readonly string _connectionString;

        public RepositorioRedeSocial(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;

        }



        public void InserirRedeSocialEvento(RedeSocial redeSocial, Evento eventoId)
        {
            string _query = $"INSERT INTO Redesocial VALUES('{redeSocial.Nome}', '{redeSocial.Url}', '{eventoId.EventoID}', NULL)";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

        public void InserirRedeSocialPalestrante(RedeSocial redeSocial, Palestrante palestranteId)
        {
            string _query = $"INSERT INTO Redesocial VALUES('{redeSocial.Nome}', '{redeSocial.Url}', NULL , {palestranteId.PalestranteId})";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

        public List<RedeSocial> ConsultarTodosIdEvento(int id)
        {
            string _query = $"SELECT * FROM REDESOCIAL Where EventoId = {id}";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            List<RedeSocial> listaConsulta = new List<RedeSocial>();
            while (dr.Read())
            {
                RedeSocial criarRedeSocial = new RedeSocial(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetInt32(3));
                listaConsulta.Add(criarRedeSocial);
            }
            return listaConsulta;
        }

        public List<RedeSocial> ConsultarTodosIdPalestrante(int id)
        {
            string _query = $"SELECT * FROM REDESOCIAL Where PalestranteID = {id}";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            List<RedeSocial> listaConsulta = new List<RedeSocial>();
            while (dr.Read())
            {
                RedeSocial criarRedeSocial = new RedeSocial(dr.GetInt32(0), dr.GetInt32(4), dr.GetString(1), dr.GetString(2));
                listaConsulta.Add(criarRedeSocial);
            }
            return listaConsulta;
        }

        public void AlterarRedeSocial(RedeSocial redesocial)
        {
            string _query = $"UPDATE REDESOCIAL SET Nome = '{redesocial.Nome}', Url = '{redesocial.Url}' WHERE RedeSocialId = {redesocial.RedeSocialId}";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

        public void Excluir(int id)
        {
            string _query = $"DELETE FROM REDESOCIAL WHERE RedeSocialId = {id}";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

    }
}
