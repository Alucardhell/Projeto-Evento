using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tasken.Gerenciador.Eventos.Controlador.Repositorios;
using Tasken.Gerenciador.Eventos.Controlador.Utils;
using Tasken.Gerenciador.Eventos.Enums;
using Tasken.Gerenciador.Eventos.Modelos.Modelos;

namespace Tasken.Gerenciador.Eventos
{
    public partial class FrmEventoCRUD : Form
    {
        private Evento _evento;
        private EnumAcaoCrud _acao;
        private RedeSocial _redeSocial = new RedeSocial();
        private Lote _lote = new Lote();

        public FrmEventoCRUD()
        {
            InitializeComponent();
        }

        public FrmEventoCRUD(Evento evento, EnumAcaoCrud acao)
        {
            InitializeComponent();
            _evento = evento;
            _acao = acao;
        }

        public Evento criarEvento()
        {
            Evento novoEvento = new Evento(textBoxLocal.Text, DateTime.Parse(textBoxData.Text), textBoxTema.Text, int.Parse(textBoxQtd.Text), textBoxUrl.Text, textBoxTelefone.Text);
            return novoEvento;
        }


        private void BuscarTodosRedeSocial()
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
            List<RedeSocial> redesocial = fabricarEvento.RepositorioRedeSocial.ConsultarTodosIdEvento(_evento.EventoID);

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();


            for (int i = 0; i < redesocial.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = redesocial[i].RedeSocialId;
                dataGridView1.Rows[i].Cells[1].Value = redesocial[i].Nome;
                dataGridView1.Rows[i].Cells[2].Value = redesocial[i].Url;
                dataGridView1.Rows[i].Cells[3].Value = redesocial[i].EventoId;

            }

        }

        private void BuscaTodosLote()
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
            List<Lote> lote = fabricarEvento.RepositorioLote.ConsultarPorEvento(_evento.Tema);

            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();


            for (int i = 0; i < lote.Count; i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[i].Cells[0].Value = lote[i].Loteid;
                dataGridView2.Rows[i].Cells[1].Value = lote[i].Nome;
                dataGridView2.Rows[i].Cells[2].Value = lote[i].Preco;
                dataGridView2.Rows[i].Cells[3].Value = lote[i].DataInicio;
                dataGridView2.Rows[i].Cells[4].Value = lote[i].DataFim;
                dataGridView2.Rows[i].Cells[5].Value = lote[i].Quantidade;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void button2_Click_1(object sender, EventArgs e)
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);

            switch (_acao)
            {
                case EnumAcaoCrud.Alterar:
                    try
                    {
                        Evento eventoAlterar = criarEvento();
                        fabricarEvento.RepositorioEvento.Alterar(eventoAlterar, _evento.EventoID);
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
                        Evento eventoCadastro = criarEvento();
                        fabricarEvento.RepositorioEvento.Inserir(eventoCadastro);                       
                        Console.WriteLine(_evento.ToString());
                        MessageBox.Show("Cadastrado com sucesso.");

                        textBoxLocal.Enabled = false;
                        textBoxData.Enabled = false;
                        textBoxTema.Enabled = false;
                        textBoxQtd.Enabled = false;
                        textBoxUrl.Enabled = false;
                        textBoxTelefone.Enabled = false;
                        button2.Enabled = false;
                        dataGridView1.Enabled = true;
                        dataGridView2.Enabled = true;


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
                        List<RedeSocial> redeDeletar = fabricarEvento.RepositorioRedeSocial.ConsultarTodosIdEvento(_evento.EventoID);

                        if (redeDeletar.Count > 0)
                        {
                            redeDeletar.ForEach(redeSocial =>
                            {
                                fabricarEvento.RepositorioRedeSocial.Excluir(redeSocial.RedeSocialId);
                            });
                        }

                        List<Lote> loteDeletar = fabricarEvento.RepositorioLote.ConsultarPorEvento(_evento.Tema);

                        if (loteDeletar.Count > 0)
                        {
                            loteDeletar.ForEach(lote =>
                            {
                                fabricarEvento.RepositorioLote.Excluir(lote.Loteid);
                            });
                        }

                        fabricarEvento.RepositorioEvento.ExcluirPalestranteEvento(_evento.EventoID);
                        fabricarEvento.RepositorioEvento.Excluir(_evento.EventoID);
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {

                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    string gridRedeSocialId = Convert.ToString(selectedRow.Cells[0].Value);
                    string gridNome = Convert.ToString(selectedRow.Cells[1].Value);
                    string gridUrl = Convert.ToString(selectedRow.Cells[2].Value);
                    string gridEventoId = Convert.ToString(selectedRow.Cells[3].Value);

                    _redeSocial.RedeSocialId = int.Parse(gridRedeSocialId);
                    _redeSocial.Nome = gridNome;
                    _redeSocial.Url = gridUrl;
                    _redeSocial.EventoId = int.Parse(gridEventoId.ToString());
                    Console.WriteLine(_redeSocial.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Selecione uma Linha valida !!!");
                }
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(dataGridView1, new Point(e.X, e.Y));

          
        }

        private void Menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            EnumAcaoCrud acaoMenu;

            bool conseguiuConverter = Enum.TryParse(e.ClickedItem.Tag.ToString(), out acaoMenu);

            if (!conseguiuConverter)
                MessageBox.Show("Ocorreu um erro ao selecionar o menu");


            switch (acaoMenu)
            {
                case EnumAcaoCrud.Incluir:
                    FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
                    _evento = fabricarEvento.RepositorioEvento.ConsultarPorNome(textBoxTema.Text);
                    FrmRedeSocialCRUD frmAdicionar = new FrmRedeSocialCRUD(_redeSocial, _evento, EnumAcaoCrud.Incluir);
                    frmAdicionar.ShowDialog();
                    BuscarTodosRedeSocial();
                    break;
                case EnumAcaoCrud.Alterar:
                    if (_redeSocial.EventoId != 0)
                    {
                         FrmRedeSocialCRUD frmEditar = new FrmRedeSocialCRUD(_redeSocial, _evento, EnumAcaoCrud.Alterar);
                         frmEditar.ShowDialog();
                        RedeSocial reseteRede = new RedeSocial();
                        _redeSocial = reseteRede;
                         BuscarTodosRedeSocial();
                    }
                    else
                    {
                        MessageBox.Show("Selecione um item para Alterar");
                    }
                    break;
                case EnumAcaoCrud.Deletar:
                    if (_redeSocial.EventoId != 0)
                    {
                        FrmRedeSocialCRUD frmExcluir = new FrmRedeSocialCRUD(_redeSocial, _evento, EnumAcaoCrud.Deletar);
                        frmExcluir.ShowDialog();
                        RedeSocial reseteRede = new RedeSocial();
                        _redeSocial = reseteRede;
                        BuscarTodosRedeSocial();
                    }
                    else
                    {
                        MessageBox.Show("Selecione um item para Deletar");
                    }
                    break;
            }

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FrmEventoCRUD_Load(object sender, EventArgs e)
        {
            textBoxId.Text = _evento.EventoID.ToString();
            textBoxLocal.Text = _evento.Local;
            textBoxData.Text = _evento.DataEvento.ToString("dd/MM/yyyy");
            textBoxTema.Text = _evento.Tema;
            textBoxQtd.Text = _evento.Qtd.ToString();
            textBoxUrl.Text = _evento.ImagemUrl;
            textBoxTelefone.Text = _evento.Telefone;
            try
            {

                pictureBoxUrl.LoadAsync(_evento.ImagemUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            switch (_acao)
            {
                case EnumAcaoCrud.Alterar:
                    BuscarTodosRedeSocial();
                    BuscaTodosLote();
                    Console.WriteLine("ALTERAR");
                    this.Text = "ALTERAR";
                    break;
                case EnumAcaoCrud.Incluir:
                    this.Text = "CADASTRAR";
                    textBoxId.Text = "";
                    textBoxLocal.Text = "";
                    textBoxData.Text = "";
                    textBoxTema.Text = "";
                    textBoxQtd.Text = "";
                    textBoxUrl.Text = "";
                    textBoxTelefone.Text = "";
                    pictureBoxUrl.LoadAsync("www.erro.com");
                    labelId.Visible = false;
                    textBoxId.Visible = false;
                    dataGridView1.Enabled = false;
                    dataGridView2.Enabled = false;
                    break;
                case EnumAcaoCrud.Deletar:
                    BuscarTodosRedeSocial();
                    BuscaTodosLote();
                    textBoxLocal.Enabled = false;
                    textBoxData.Enabled = false;
                    textBoxTema.Enabled = false;
                    textBoxQtd.Enabled = false;
                    textBoxUrl.Enabled = false;
                    textBoxTelefone.Enabled = false;
                    this.Text = "DELETAR";
                    break;
            }
        }

        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip2.Show(dataGridView2, new Point(e.X, e.Y));
        }

        private void contextMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            EnumAcaoCrud acaoMenu;

            bool conseguiuConverter = Enum.TryParse(e.ClickedItem.Tag.ToString(), out acaoMenu);

            if (!conseguiuConverter)
                MessageBox.Show("Ocorreu um erro ao selecionar o menu");

            switch (acaoMenu)
            {
                case EnumAcaoCrud.Incluir:
                    _lote.NomeEvento = textBoxTema.Text;
                    FrmLoteCRUD frmAdicionar = new FrmLoteCRUD(_lote, EnumAcaoCrud.Incluir);
                    frmAdicionar.ShowDialog();
                    _evento.Tema = textBoxTema.Text;
                    BuscaTodosLote();
                    break;
                case EnumAcaoCrud.Alterar:
                    if (_lote.Loteid != 0)
                    {

                    FrmLoteCRUD frmEditar = new FrmLoteCRUD(_lote, EnumAcaoCrud.Alterar);
                    frmEditar.ShowDialog();
                        _evento.Tema = textBoxTema.Text;
                        BuscaTodosLote();
                    }
                    else
                    {
                        MessageBox.Show("Selecione a linha para Alterar!");
                    }
                    break;
                case EnumAcaoCrud.Deletar:
                    if (_lote.Loteid != 0)
                    {

                    FrmLoteCRUD frmExcluir = new FrmLoteCRUD(_lote, EnumAcaoCrud.Deletar);
                    frmExcluir.ShowDialog();
                        _evento.Tema = textBoxTema.Text;
                        BuscaTodosLote();
                    }
                    else
                    {
                        MessageBox.Show("Selecione a linha para Deletar!");
                    }
                    break;
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                try
                {
                    //FAZENDO
                    int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                    string gridLoteId = Convert.ToString(selectedRow.Cells[0].Value);
                    string gridNome = Convert.ToString(selectedRow.Cells[1].Value);
                    string gridPreco = Convert.ToString(selectedRow.Cells[2].Value);
                    string gridDataInicio = Convert.ToString(selectedRow.Cells[3].Value);
                    string gridDataFim = Convert.ToString(selectedRow.Cells[4].Value);
                    string gridQuantidade = Convert.ToString(selectedRow.Cells[5].Value);
                    string gridTemaEvento = textBoxTema.Text;

                    _lote.Loteid = int.Parse(gridLoteId);
                    _lote.Nome = gridNome;
                    _lote.Preco = double.Parse(gridPreco);
                    _lote.DataInicio = DateTime.Parse(gridDataInicio);
                    _lote.DataFim = DateTime.Parse(gridDataFim);
                    _lote.Quantidade = int.Parse(gridQuantidade);
                    _lote.NomeEvento = gridTemaEvento;
                    Console.WriteLine(_lote.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Selecione uma Linha valida !!!");
                }
            }
        }
    }
}
