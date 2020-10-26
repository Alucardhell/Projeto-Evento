using System;
using System.Collections.Generic;
using System.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasken.Gerenciador.Eventos.Controlador.Repositorios;
using Tasken.Gerenciador.Eventos.Controlador.Utils;
using Tasken.Gerenciador.Eventos.Modelos.Modelos;


namespace Tasken.Gerenciador.Eventos.Testes
{
    [TestClass]
    public class UnitTestRepositorios
    {
        [TestMethod]
        public void TestMethodInserirEvento()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConexaoBancoTeste"].ToString();

            Evento evento = new Evento()
            {
                DataEvento = DateTime.Now,
                Local = "Rua Uruguaiana 788"
            };

            FabricaRepositorio fabricaRepositorio = new FabricaRepositorio(connectionString);
            fabricaRepositorio.RepositorioEvento.Inserir(evento);
        }

        [TestMethod]
        public void TestMethodConsultarTodosEvento()
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
            List<Evento> eventos = fabricarEvento.RepositorioEvento.ConsultarTodos();
        }

        [TestMethod]
        public void TestMethodConsultarPalestrantePorIDEvento()
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
            List<Palestrante> eventos = fabricarEvento.RepositorioPalestrante.ConsultarPorIdEvento(1);
        }
    }
}
