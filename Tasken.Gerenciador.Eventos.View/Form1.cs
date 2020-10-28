using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Tasken.Gerenciador.Eventos.Controlador.Repositorios;
using Tasken.Gerenciador.Eventos.Modelos.Modelos;
using Tasken.Gerenciador.Eventos.Controlador.Utils;

namespace Tasken.Gerenciador.Eventos
{
    public partial class HomeFrm : Form
    {

        private Evento eventoFrm = new Evento();
        public HomeFrm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Console.WriteLine("Botão funcionando");

            FabricaRepositorio fr = new FabricaRepositorio(ConnectionSQL.connectionString);
            Evento ev = new Evento();
            fr.RepositorioEvento.Inserir(ev);

        }

        private void _dataEvento_Click(object sender, EventArgs e)
        {

        }

        private void btnEvento_Click(object sender, EventArgs e)
        {
            palestranteFrm1.Hide();
            crudEvento1.Show();
            crudEvento1.BringToFront();
            this.Text = "Evento";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
            List<Evento> eventos = fabricarEvento.RepositorioEvento.ConsultarEntreDatas(dateTimePickerInicio.Value.ToString("dd/MM/yyyy"), dateTimePickerFim.Value.ToString("dd/MM/yyyy"));

            if (eventos.Count != 0)
            {

            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();


            for (int i = 0; i < eventos.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[i].Cells[0].Value = eventos[i].EventoID;
                dataGridView1.Rows[i].Cells[1].Value = eventos[i].Local;
                dataGridView1.Rows[i].Cells[2].Value = eventos[i].DataEvento;
                dataGridView1.Rows[i].Cells[3].Value = eventos[i].Tema;
                dataGridView1.Rows[i].Cells[4].Value = eventos[i].Qtd;
                dataGridView1.Rows[i].Cells[5].Value = eventos[i].ImagemUrl;
                dataGridView1.Rows[i].Cells[6].Value = eventos[i].Telefone;
            }
            }
            else
            {
                MessageBox.Show("Nenhum Evento encontrado.");
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Console.WriteLine(dateTimePickerInicio.Value.ToString("dd/MM/yyyy"));
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
                    string gridLocal = Convert.ToString(selectedRow.Cells[1].Value);
                    string gridDataEvento = Convert.ToString(selectedRow.Cells[2].Value);
                    string gridTema = Convert.ToString(selectedRow.Cells[3].Value);
                    string gridQtd = Convert.ToString(selectedRow.Cells[4].Value);
                    string gridImagemUrl = Convert.ToString(selectedRow.Cells[5].Value);
                    string gridTelefone = Convert.ToString(selectedRow.Cells[6].Value);

                    eventoFrm.EventoID = int.Parse(gridId.ToString());
                    eventoFrm.Local = gridLocal.ToString();
                    eventoFrm.DataEvento = DateTime.Parse(gridDataEvento.ToString());
                    eventoFrm.Tema = gridTema.ToString();
                    eventoFrm.Qtd = int.Parse(gridQtd.ToString());
                    eventoFrm.ImagemUrl = gridImagemUrl.ToString();
                    eventoFrm.Telefone = gridTelefone.ToString();
                    Console.WriteLine(eventoFrm.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("AVISO !!! Selecione uma linha valida !!!");
                }
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            Console.WriteLine(eventoFrm.ToString());
            FrmInformacoesEvento formInformacao = new FrmInformacoesEvento(eventoFrm);
            formInformacao.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            palestranteFrm1.Hide();
            loteFrm1.Hide();
            crudEvento1.Hide();
        }

        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            crudEvento1.Hide();
            palestranteFrm1.Hide();
            loteFrm1.Hide();
            this.Text = "Home";
        }

        private void crudEvento1_Load(object sender, EventArgs e)
        {
            crudEvento1.Hide();
            palestranteFrm1.Hide();
        }

        private void btnPalestrante_Click(object sender, EventArgs e)
        {
            crudEvento1.Hide();
            loteFrm1.Hide();
            palestranteFrm1.Show();
            palestranteFrm1.BringToFront();
            this.Text = "Palestrante";
        }

        private void btnLote_Click(object sender, EventArgs e)
        {
            crudEvento1.Hide();
            palestranteFrm1.Hide();
            loteFrm1.Show();
            loteFrm1.BringToFront();
            this.Text = "Lote";
        }
    }
}
