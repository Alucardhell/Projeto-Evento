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
using Tasken.Gerenciador.Eventos.Modelos.Modelos;

namespace Tasken.Gerenciador.Eventos
{
    public partial class FrmPalestrante : Form
    {

        private Palestrante palestranteFrm = new Palestrante();
        public FrmPalestrante()
        {
            InitializeComponent();
        }


        private void BuscarTodos()
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
            List<Palestrante> palestrantes = fabricarEvento.RepositorioPalestrante.ConsultarTodos();

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();


            for (int i = 0; i < palestrantes.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = palestrantes[i].PalestranteId;
                dataGridView1.Rows[i].Cells[1].Value = palestrantes[i].Nome;
                dataGridView1.Rows[i].Cells[2].Value = palestrantes[i].ImagemUrl;
                dataGridView1.Rows[i].Cells[3].Value = palestrantes[i].Telefone;
                dataGridView1.Rows[i].Cells[4].Value = palestrantes[i].Minicurriculo;
                dataGridView1.Rows[i].Cells[5].Value = palestrantes[i].Email;
                Console.WriteLine("Nome: {0}, Imagem Url: {1}, Telefone: {2}, Minicurrilo: {3}, Email: {4}.",palestrantes[1], palestrantes[2], palestrantes[3], palestrantes[4], palestrantes[5], palestrantes[6]);

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BuscarTodos();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
                Palestrante PalestranteId = fabricarEvento.RepositorioPalestrante.ConsultarPorId(int.Parse(textBoxPesquisarId.Text));

                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dataGridView1.Rows[0].Cells[0].Value = PalestranteId.PalestranteId;
                dataGridView1.Rows[0].Cells[1].Value = PalestranteId.Nome;
                dataGridView1.Rows[0].Cells[2].Value = PalestranteId.ImagemUrl;
                dataGridView1.Rows[0].Cells[3].Value = PalestranteId.Telefone;
                dataGridView1.Rows[0].Cells[4].Value = PalestranteId.Minicurriculo;
                dataGridView1.Rows[0].Cells[5].Value = PalestranteId.Email;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Informe um numero valido !!!", "Aviso !!!");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmPalestranteCRUD alt = new FrmPalestranteCRUD(palestranteFrm, Enums.EnumAcaoCrud.Incluir);
            alt.ShowDialog();
            BuscarTodos();
            palestranteFrm = new Palestrante();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (palestranteFrm.PalestranteId != 0)
            {
            FrmPalestranteCRUD alt = new FrmPalestranteCRUD(palestranteFrm, Enums.EnumAcaoCrud.Alterar);
            alt.ShowDialog();
                BuscarTodos();
                palestranteFrm = new Palestrante();
            }
            else
            {
                MessageBox.Show("AVISO !!! Selecione uma linha valida !!!");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells.Count > 0)
            {
                try
                {

                    int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
                    string gridId = Convert.ToString(selectedRow.Cells[0].Value);
                    string gridNome = Convert.ToString(selectedRow.Cells[1].Value);
                    string gridImagemUrl = Convert.ToString(selectedRow.Cells[2].Value);
                    string gridTelefone = Convert.ToString(selectedRow.Cells[3].Value);
                    string gridMinucurriculo = Convert.ToString(selectedRow.Cells[4].Value);
                    string gridEmail = Convert.ToString(selectedRow.Cells[5].Value);

                    palestranteFrm.PalestranteId = int.Parse(gridId.ToString());
                    palestranteFrm.Nome = gridNome;
                    palestranteFrm.ImagemUrl = gridImagemUrl;
                    palestranteFrm.Telefone = gridTelefone;
                    palestranteFrm.Minicurriculo = gridMinucurriculo;
                    palestranteFrm.Email = gridEmail;
                    Console.WriteLine(palestranteFrm.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("Selecione uma Linha valida !!!");
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (palestranteFrm.PalestranteId != 0)
            {
                FrmPalestranteCRUD alt = new FrmPalestranteCRUD(palestranteFrm, Enums.EnumAcaoCrud.Deletar);
                alt.ShowDialog();
                BuscarTodos();
                palestranteFrm = new Palestrante();
            }
            else
            {
                MessageBox.Show("AVISO !!! Selecione uma linha valida !!!");
            }
        }
    }
}