using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
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
    public partial class LoteFrm : UserControl
    {
        private Lote _lote = new Lote();
        public LoteFrm()
        {
            InitializeComponent();
        }

        private void BuscarTodos()
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
            List<Lote> lotes = fabricarEvento.RepositorioLote.ConsultarTodos();

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();


            for (int i = 0; i < lotes.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = lotes[i].NomeEvento;
                dataGridView1.Rows[i].Cells[1].Value = lotes[i].Loteid;
                dataGridView1.Rows[i].Cells[2].Value = lotes[i].Nome;
                dataGridView1.Rows[i].Cells[3].Value = lotes[i].Preco;
                dataGridView1.Rows[i].Cells[4].Value = lotes[i].DataInicio;
                dataGridView1.Rows[i].Cells[5].Value = lotes[i].DataFim;
                dataGridView1.Rows[i].Cells[6].Value = lotes[i].Quantidade;
            }
        }

        private void btnCadastrarLote_Click(object sender, EventArgs e)
        {
            FrmLoteCRUD frmLote = new FrmLoteCRUD(_lote, Enums.EnumAcaoCrud.Incluir);
            frmLote.ShowDialog();
            BuscarTodos();
            _lote = new Lote();
        }

        private void btnAlterarLote_Click(object sender, EventArgs e)
        {
            if (_lote.Loteid != 0)
            {

                FrmLoteCRUD frmLote = new FrmLoteCRUD(_lote, EnumAcaoCrud.Alterar);
                frmLote.ShowDialog();
                BuscarTodos();
                _lote = new Lote();

            }
            else
            {
                MessageBox.Show("AVISO !!! Selecione uma linha valida !!!");
            }
        }

        private void btnExcluirLote_Click(object sender, EventArgs e)
        {
            if (_lote.Loteid != 0)
            {

                FrmLoteCRUD frmLote = new FrmLoteCRUD(_lote, Enums.EnumAcaoCrud.Deletar);
                frmLote.ShowDialog();
                BuscarTodos();
                _lote = new Lote();
            }
            else
            {
                MessageBox.Show("AVISO !!! Selecione uma linha valida !!!");
            }
        }

        private void btnPesquisarEventoNome_Click(object sender, EventArgs e)
        {
            try
            {

                FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
                List<Lote> lotes = fabricarEvento.RepositorioLote.ConsultarPorEvento(textBoxPesquisaEventoTema.Text);

                if (lotes.Count != 0)
                {

                    dataGridView1.Rows.Clear();
                    dataGridView1.Refresh();


                    for (int i = 0; i < lotes.Count; i++)
                    {
                        dataGridView1.Rows.Add();
                        dataGridView1.Rows[i].Cells[0].Value = lotes[i].NomeEvento;
                        dataGridView1.Rows[i].Cells[1].Value = lotes[i].Loteid;
                        dataGridView1.Rows[i].Cells[2].Value = lotes[i].Nome;
                        dataGridView1.Rows[i].Cells[3].Value = lotes[i].Preco;
                        dataGridView1.Rows[i].Cells[4].Value = lotes[i].DataInicio;
                        dataGridView1.Rows[i].Cells[5].Value = lotes[i].DataFim;
                        dataGridView1.Rows[i].Cells[6].Value = lotes[i].Quantidade;
                    }
                }
                else
                {
                    MessageBox.Show("Nenhum Lote encontrado.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Informe um nome valido !!!", "Aviso !!!");
            }
        }

        private void btnBuscaTodosLote_Click(object sender, EventArgs e)
        {
            BuscarTodos();
        }

        private void dataGridViewLote_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                try
                {

                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    string gridNomeEvento = Convert.ToString(selectedRow.Cells[0].Value);
                    string gridLoteId = Convert.ToString(selectedRow.Cells[1].Value);
                    string gridNome = Convert.ToString(selectedRow.Cells[2].Value);
                    string gridPreco = Convert.ToString(selectedRow.Cells[3].Value);
                    string gridDataInicio = Convert.ToString(selectedRow.Cells[4].Value);
                    string gridDataFim = Convert.ToString(selectedRow.Cells[5].Value);
                    string gridQuantidade = Convert.ToString(selectedRow.Cells[6].Value);

                    _lote.NomeEvento = gridNomeEvento.ToString();
                    _lote.Loteid = int.Parse(gridLoteId.ToString());
                    _lote.Nome = gridNome.ToString();
                    _lote.Preco = double.Parse(gridPreco.ToString());
                    _lote.DataInicio = DateTime.Parse(gridDataInicio.ToString());
                    _lote.DataFim = DateTime.Parse(gridDataFim.ToString());
                    _lote.Quantidade = int.Parse(gridQuantidade.ToString());
                    Console.WriteLine(_lote.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Selecione uma Linha valida !!!");
                }
            }
        }

        private void LoteFrm_Load(object sender, EventArgs e)
        {
            BuscarTodos();
        }
    }
}
