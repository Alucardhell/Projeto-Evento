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
    public partial class FrmLoteCRUD : Form
    {

        private EnumAcaoCrud _acao;
        private EnumControleLoteNome _controleCadastro;
        private Lote _lote = new Lote();
        private int _caminhoCadastro {get; set;}
        public FrmLoteCRUD()
        {
            InitializeComponent();
        }

        public FrmLoteCRUD(Lote lote, EnumAcaoCrud acao)
        {
            this._lote = lote;
            this._acao = acao;
            InitializeComponent();
        }

        public FrmLoteCRUD(Lote lote, EnumAcaoCrud acao, EnumControleLoteNome controleCadastro)
        {
            this._lote = lote;
            this._acao = acao;
            this._controleCadastro = controleCadastro;
            InitializeComponent();
        }

        public Lote CriarLote()
        {
            Lote novoLote = new Lote(comboBoxNomeEvento.Text, int.Parse(textBoxId.Text) , textBoxNome.Text, double.Parse(textBoxPreco.Text), DateTime.Parse(dateTimeInicio.Value.ToString("dd/MM/yyyy")), DateTime.Parse(dateTimeFim.Value.ToString("dd/MM/yyyy")), int.Parse(textBoxQuantidade.Text));
            return novoLote;
        }


        private void FrmLoteCRUD_Load(object sender, EventArgs e)
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
            List<Evento> comboBoxEventos = fabricarEvento.RepositorioEvento.ConsultarTodos();

            comboBoxEventos.ForEach(ex => comboBoxNomeEvento.Items.Add(ex.Tema));
            comboBoxNomeEvento.SelectedIndex = 0;


            textBoxId.Text = _lote.Loteid.ToString();
            textBoxNome.Text = _lote.Nome;
            textBoxPreco.Text = _lote.Preco.ToString();
            textBoxQuantidade.Text = _lote.Quantidade.ToString();

            if (_controleCadastro.ToString().Equals("Desabilitar"))
                this.comboBoxNomeEvento.Enabled = false;


            switch (_acao)
            {
                case EnumAcaoCrud.Alterar:
                    Console.WriteLine("ALTERAR");
                    this.Text = "ALTERAR LOTE";
                    comboBoxNomeEvento.Text = _lote.NomeEvento;
                    this.textBoxId.Enabled = false;
                    break;
                case EnumAcaoCrud.Incluir:
                    this.Text = "CADASTRAR LOTE";
                    textBoxId.Text = "0";
                    textBoxNome.Text = "";
                    textBoxPreco.Text = "";
                    textBoxQuantidade.Text = "";
                    comboBoxNomeEvento.Text = _lote.NomeEvento;
                    labelLoteId.Visible = false;
                    textBoxId.Visible = false;

                    break;
                case EnumAcaoCrud.Deletar:
                    textBoxId.Enabled = false;
                    textBoxNome.Enabled = false;
                    textBoxPreco.Enabled = false;
                    dateTimeInicio.Enabled = false;
                    dateTimeFim.Enabled = false;
                    textBoxQuantidade.Enabled = false;
                    comboBoxNomeEvento.Enabled = false;
                    comboBoxNomeEvento.Text = _lote.NomeEvento;
                    this.Text = "DELETAR LOTE";
                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);

            switch (_acao)
            {
                case EnumAcaoCrud.Alterar:
                    try
                    {
                        Lote loteAlterar = CriarLote();
                        fabricarEvento.RepositorioLote.Alterar(loteAlterar);
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
                        Lote loteCadastro = CriarLote();
                        fabricarEvento.RepositorioLote.Inserir(loteCadastro);
                        MessageBox.Show("Cadastrado com sucesso.");
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
                        fabricarEvento.RepositorioLote.Excluir(_lote.Loteid);
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
    }
    }
