using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tasken.Gerenciador.Eventos.Controlador.Repositorios;
using Tasken.Gerenciador.Eventos.Modelos.Modelos;
using System.Configuration;

namespace Tasken.Gerenciador.Eventos.Testes
{
    [TestClass]
    public class UnitTestControlador
    {
    
        [TestMethod]
        public void TestMethodConexaoComBanco()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConexaoBancoTeste"].ToString();

            Console.WriteLine("Teste");
            FabricaRepositorio fabricaRepositorio = new FabricaRepositorio(connectionString);
        }
    }
}

