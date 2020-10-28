using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tasken.Gerenciador.Eventos.Controlador.Repositorios;
using Tasken.Gerenciador.Eventos.Controlador.Utils;

namespace Tasken.Gerenciador.Eventos
{
    public partial class FrmLogin : Form
    {
        private FabricaRepositorio _fabrica = new FabricaRepositorio(ConnectionSQL.connectionString);

        private bool acesso = false;
        public FrmLogin()
        {
            InitializeComponent();
        }



        private void btnEfetuarLogin(object sender, EventArgs e)
        {



            int a = _fabrica.RepositorioLogin.VerificarLogin("diogo", Utilidades.GerarSenha("123"));
            Console.WriteLine(a);

            if (_fabrica.RepositorioLogin.VerificarLogin(textBoxLogin.Text, Utilidades.GerarSenha(textBoxSenha.Text)) > 0)
            {
                MessageBox.Show("Login aceito");
                acesso = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Login ou Senha Invalido!");
            }
        }

        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!acesso)
                Application.Exit();
        }
    }
}
