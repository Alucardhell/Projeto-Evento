﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
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

        public void testeSenha()
        {
            Console.WriteLine(GerarHashMd5("123"));
        }



        [TestMethod]
        public static string GerarHashMd5(string input)
        {
            MD5 md5Hash = MD5.Create();
            // Converter a String para array de bytes, que é como a biblioteca trabalha.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Cria-se um StringBuilder para recompôr a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop para formatar cada byte como uma String em hexadecimal
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            return sBuilder.ToString();
        }
    }
}
