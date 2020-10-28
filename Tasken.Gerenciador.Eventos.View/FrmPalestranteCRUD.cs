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
    public partial class FrmPalestranteCRUD : Form
    {
        private Palestrante _palestrante;

        private EnumAcaoCrud _acao;

        private RedeSocial _redeSocial = new RedeSocial();
        public FrmPalestranteCRUD()
        {
            InitializeComponent();
        }

        public FrmPalestranteCRUD(Palestrante _palestrante, EnumAcaoCrud _acao)
        {
            InitializeComponent();
            this._palestrante = _palestrante;
            this._acao = _acao;
        }

        public Palestrante CriarPalestrante()
        {
            Palestrante palestrante = new Palestrante(textBoxNome.Text, textBoxUrl.Text, textBoxTelefone.Text, textBoxMinicurriculo.Text, textBoxEmail.Text);
            return palestrante;
        }


        private void BuscarTodosRedeSocial()
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
            List<RedeSocial> palestrante = fabricarEvento.RepositorioRedeSocial.ConsultarTodosIdPalestrante(_palestrante.PalestranteId);

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();


            for (int i = 0; i < palestrante.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = palestrante[i].RedeSocialId;
                dataGridView1.Rows[i].Cells[1].Value = palestrante[i].Nome;
                dataGridView1.Rows[i].Cells[2].Value = palestrante[i].Url;
                dataGridView1.Rows[i].Cells[3].Value = palestrante[i].PalestranteId;

            }

        }

        private void btnConfirma_Click(object sender, EventArgs e)
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);

            switch (_acao)
            {
                case EnumAcaoCrud.Alterar:
                    try
                    {
                        Palestrante palestranteAlterar = CriarPalestrante();
                        fabricarEvento.RepositorioPalestrante.Alterar(palestranteAlterar, _palestrante.PalestranteId);
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
                        Palestrante palestranteCadastro = CriarPalestrante();
                        Console.WriteLine(palestranteCadastro.ToString());
                        fabricarEvento.RepositorioPalestrante.Inserir(palestranteCadastro);
                        MessageBox.Show("Cadastrado com sucesso.");

                        textBoxNome.Enabled = false;
                        textBoxEmail.Enabled = false;
                        textBoxMinicurriculo.Enabled = false;
                        textBoxTelefone.Enabled = false;
                        textBoxUrl.Enabled = false;
                        dataGridView1.Enabled = true;
                        btnConfirma.Enabled = false;
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
                        List<RedeSocial> deleteRede = fabricarEvento.RepositorioRedeSocial.ConsultarTodosIdPalestrante(_palestrante.PalestranteId);

                        if (deleteRede.Count > 0)
                        {
                            deleteRede.ForEach(rede => {
                                fabricarEvento.RepositorioRedeSocial.Excluir(rede.RedeSocialId);
                            });
                        }

                        fabricarEvento.RepositorioPalestrante.Excluir(_palestrante.PalestranteId);
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridViewRedeSocial_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                contextMenuStrip1.Show(dataGridView1, new Point(e.X, e.Y));

        }


        private void MenuRedeSocial_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            EnumAcaoCrud acaoMenu;

            bool conseguiuConverter = Enum.TryParse(e.ClickedItem.Tag.ToString(), out acaoMenu);

            if (!conseguiuConverter)
                MessageBox.Show("Ocorreu um erro ao selecionar o menu");

            switch (acaoMenu)
            {
                case EnumAcaoCrud.Incluir:

                    FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
                    _palestrante = fabricarEvento.RepositorioPalestrante.ConsultarPorNome(textBoxNome.Text);
                    FrmRedeSocialCRUD frmAdicionar = new FrmRedeSocialCRUD(_redeSocial, _palestrante, EnumAcaoCrud.Incluir);
                    frmAdicionar.ShowDialog();
                    BuscarTodosRedeSocial();

                    break;
                case EnumAcaoCrud.Alterar:
                    if (_redeSocial.RedeSocialId != 0)
                    {
                        FrmRedeSocialCRUD frmEditar = new FrmRedeSocialCRUD(_redeSocial, _palestrante, EnumAcaoCrud.Alterar);
                        frmEditar.ShowDialog();
                        BuscarTodosRedeSocial();
                    }
                    else
                    {
                        MessageBox.Show("Selecione a linha que deseja alterar.");
                    }
                    break;
                    case EnumAcaoCrud.Deletar:
                    if (_redeSocial.RedeSocialId != 0)
                    {
                        FrmRedeSocialCRUD frmExcluir = new FrmRedeSocialCRUD(_redeSocial, _palestrante, EnumAcaoCrud.Deletar);
                    frmExcluir.ShowDialog();
                    BuscarTodosRedeSocial();
                    }
                    else
                    {
                        MessageBox.Show("Selecione a linha que deseja Deletar.");
                    }
                    break;
            }
        }

        private void FrmPalestranteCRUD_Load(object sender, EventArgs e)
        {

            textBoxId.Text = _palestrante.PalestranteId.ToString();
            textBoxNome.Text = _palestrante.Nome;
            textBoxUrl.Text = _palestrante.ImagemUrl;
            textBoxTelefone.Text = _palestrante.Telefone;
            textBoxMinicurriculo.Text = _palestrante.Minicurriculo;
            textBoxEmail.Text = _palestrante.Email;
            textBoxTelefone.Text = _palestrante.Telefone;
            
            try
            {
                pictureBoxUrl.LoadAsync(_palestrante.ImagemUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }


            switch (_acao)
            {
                case EnumAcaoCrud.Alterar:
                    this.Text = "ALTERAR PALESTRANTE";
                    BuscarTodosRedeSocial();
                    break;
                case EnumAcaoCrud.Incluir:
                    this.Text = "CADASTRAR PALESTRANTE";
                    dataGridView1.Enabled = false;
                    textBoxId.Text = "";
                    textBoxNome.Text = "";
                    textBoxUrl.Text = "";
                    textBoxTelefone.Text = "";
                    textBoxMinicurriculo.Text = "";
                    textBoxEmail.Text = "";
                    pictureBoxUrl.LoadAsync("www.erro.com");
                    break;
                case EnumAcaoCrud.Deletar:
                    BuscarTodosRedeSocial();
                    textBoxNome.Enabled = false;
                    textBoxUrl.Enabled = false;
                    textBoxTelefone.Enabled = false;
                    textBoxMinicurriculo.Enabled = false;
                    textBoxEmail.Enabled = false;
                    textBoxTelefone.Enabled = false;
                    dataGridView1.Enabled = false;
                    this.Text = "DELETAR PALESTRANTE";
                    break;
            }
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
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
    }
}
