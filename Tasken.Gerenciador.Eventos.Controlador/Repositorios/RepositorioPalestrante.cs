using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tasken.Gerenciador.Eventos.Modelos.Modelos;

namespace Tasken.Gerenciador.Eventos.Controlador.Repositorios
{
    public class RepositorioPalestrante : RepositorioBase
    {
        private readonly string _connectionString;

        public RepositorioPalestrante(string connectionString) : base(connectionString)
        {
            _connectionString = connectionString;

        }

        public void Inserir(Palestrante palestrante)
        {
            string _query = $"INSERT INTO PALESTRANTE VALUES('{palestrante.Nome}', '{palestrante.ImagemUrl}', '{palestrante.Telefone}', '{palestrante.Minicurriculo}', '{palestrante.Email}')";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

        public List<Palestrante> ConsultarTodos()
        {
            string _query = "SELECT * FROM PALESTRANTE";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            List<Palestrante> listaConsulta = new List<Palestrante>();
            while (dr.Read())
            {
                Palestrante criarEvento = new Palestrante(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));
                listaConsulta.Add(criarEvento);
            }
            return listaConsulta;
        }

        public Palestrante ConsultarPorId(int id)
        {
            string _query = $"SELECT * FROM PALESTRANTE WHERE PalestranteID = {id}";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            Palestrante buscaPalestrante = new Palestrante();

            while (dr.Read())
            {
                buscaPalestrante.PalestranteId = dr.GetInt32(0);
                buscaPalestrante.Nome = dr.GetString(1);
                buscaPalestrante.ImagemUrl = dr.GetString(2);
                buscaPalestrante.Telefone = dr.GetString(3);
                buscaPalestrante.Minicurriculo = dr.GetString(4);
                buscaPalestrante.Email = dr.GetString(5);

            }
            return buscaPalestrante;
        }

        public List<Palestrante> ConsultarPorIdEvento(int id)
        {
            string _query = $"Select * from Palestrante where PalestranteID in (select PalestranteId from PalestranteEvento where EventoId = {id})";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            List<Palestrante> listaConsulta = new List<Palestrante>();
            while (dr.Read())
            {
                Palestrante criarPalestrante = new Palestrante(dr.GetInt32(0), dr.GetString(1), dr.GetString(2), dr.GetString(3), dr.GetString(4), dr.GetString(5));
                listaConsulta.Add(criarPalestrante);
            }
            return listaConsulta;
        }

        public Palestrante ConsultarPorNome(string nome)
        {
            string _query = $"SELECT * FROM Palestrante WHERE Nome = '{nome}'";
            SqlDataReader dr = ExecutarComandoReader(new SqlCommand(_query));
            Palestrante buscaPalestrante = new Palestrante();

            while (dr.Read())
            {
                buscaPalestrante.PalestranteId = dr.GetInt32(0);
                buscaPalestrante.Nome = dr.GetString(1);
                buscaPalestrante.ImagemUrl = dr.GetString(2); ;
                buscaPalestrante.Telefone = dr.GetString(3);
                buscaPalestrante.Minicurriculo = dr.GetString(4);
                buscaPalestrante.Email = dr.GetString(5);

            }
            return buscaPalestrante;
        }

        public void Excluir(int id)
        {
            string _query = $"DELETE FROM Palestrante WHERE PalestranteID = {id}";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

        public void ExcluirPalestranteEvento(int id)
        {
            string _query = $"DELETE FROM PalestranteEvento WHERE PalestranteID = {id}";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

        public void Alterar(Palestrante palestrante, int id)
        {
            string _query = $"UPDATE PALESTRANTE SET Nome = '{palestrante.Nome}', ImagemUrl = '{palestrante.ImagemUrl}', Telefone = '{palestrante.Telefone}', Minicurriculo = '{palestrante.Minicurriculo}', Email = '{palestrante.Email}' WHERE PalestranteID = {id}";
            ExecutarComandoNoQuery(new SqlCommand(_query));
        }

    }
}
