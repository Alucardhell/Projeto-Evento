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
using Tasken.Gerenciador.Eventos.Enums;
using Tasken.Gerenciador.Eventos.Modelos.Modelos;

namespace Tasken.Gerenciador.Eventos
{
    public partial class FrmRedeSocialCRUD : Form
    {

        private RedeSocial _redeSocial { get; set; }
        private Evento _evento = new Evento();

        private Palestrante _palestrante = new Palestrante();

        private EnumAcaoCrud _acao { get; set; }

        public FrmRedeSocialCRUD()
        {
            InitializeComponent();
        }


        public FrmRedeSocialCRUD(RedeSocial redeSocial, Evento evento, EnumAcaoCrud acao )
        {
            InitializeComponent();
            this._redeSocial = redeSocial;
            this._evento = evento;
            this._acao = acao;
        }

        public FrmRedeSocialCRUD(RedeSocial redeSocial, Palestrante palestrante, EnumAcaoCrud acao)
        {
            InitializeComponent();
            this._redeSocial = redeSocial;
            this._palestrante = palestrante;
            this._acao = acao;
        }

        public RedeSocial criarRedeSocial()
        {
            RedeSocial novaRedeSocial = new RedeSocial(_redeSocial.RedeSocialId, textBoxNome.Text,textBoxUrl.Text, _redeSocial.EventoId);
            return novaRedeSocial;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);

            switch (_acao)
            {
                case EnumAcaoCrud.Alterar:
                    try
                    {
                        RedeSocial redeSocialAlterar = criarRedeSocial();
                        fabricarEvento.RepositorioRedeSocial.AlterarRedeSocial(redeSocialAlterar);
                        MessageBox.Show("Alterado com sucesso");
                        this.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show("Formulario invalido !!!");
                    }
                    break;
                case EnumAcaoCrud.Incluir:
                    try
                    {
                        //_redeSocial.EventoId = _evento.EventoID;
                        if (_evento.EventoID != 0)
                        {
                            RedeSocial redeSocialAlterar = criarRedeSocial();
                            fabricarEvento.RepositorioRedeSocial.InserirRedeSocialEvento(redeSocialAlterar, _evento);
                            MessageBox.Show("Cadastrado com sucesso.");
                        } else if (_palestrante.PalestranteId != 0)
                        {
                            RedeSocial redeSocialAlterar = criarRedeSocial();
                            fabricarEvento.RepositorioRedeSocial.InserirRedeSocialPalestrante(redeSocialAlterar, _palestrante);
                            MessageBox.Show("Cadastrado com sucesso.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show("Formulario invalido !!!");
                    }
                    break;
                case EnumAcaoCrud.Deletar:
                    try
                    {
                            fabricarEvento.RepositorioRedeSocial.Excluir(_redeSocial.RedeSocialId);
                            MessageBox.Show("Deletado com sucesso !!!");
                            this.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        MessageBox.Show("Ocorreu um erro inesperado !!!");
                    }
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmRedeSocialCRUD_Load(object sender, EventArgs e)
        {
            textBoxNome.Text = _redeSocial?.Nome;
            textBoxUrl.Text = _redeSocial?.Url;

            switch (_acao)
            {
                case EnumAcaoCrud.Alterar:
                    Console.WriteLine("ALTERAR");
                    this.Text = "Alterar Rede Social";
                    textBoxNome.Text = _redeSocial?.Nome;
                    textBoxUrl.Text = _redeSocial?.Url;
                    break;
                case EnumAcaoCrud.Incluir:
                    this.Text = "Cadastrar Rede Social";
                    textBoxNome.Text = "";
                    textBoxUrl.Text = "";
                    break;
                case EnumAcaoCrud.Deletar:
                    textBoxNome.Enabled = false;
                    textBoxUrl.Enabled = false;
                    this.Text = "Deletar Rede Social";
                    break;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                System.Diagnostics.Process.Start(textBoxUrl.Text);
            }catch(Exception ex)
            {
                MessageBox.Show("Link Invalido");
            }
        }
    }
}
