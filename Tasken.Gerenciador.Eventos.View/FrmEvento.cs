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
    public partial class FrmEvento : Form
    {

        private Evento eventoFrm = new Evento();

        public FrmEvento()
        {
            InitializeComponent();
        }

        private void BuscarTodos()
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);
            List<Evento> eventos = fabricarEvento.RepositorioEvento.ConsultarTodos();

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

        private void button1_Click(object sender, EventArgs e)
        {
            FrmEventoCRUD alt = new FrmEventoCRUD(eventoFrm, Enums.EnumAcaoCrud.Incluir);
            alt.ShowDialog();
            BuscarTodos();
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
                Evento eventoId = fabricarEvento.RepositorioEvento.ConsultarPorId(int.Parse(textBoxPesquisarId.Text));
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                dataGridView1.Rows[0].Cells[0].Value = eventoId.EventoID;
                dataGridView1.Rows[0].Cells[1].Value = eventoId.Local;
                dataGridView1.Rows[0].Cells[2].Value = eventoId.DataEvento;
                dataGridView1.Rows[0].Cells[3].Value = eventoId.Tema;
                dataGridView1.Rows[0].Cells[4].Value = eventoId.Qtd;
                dataGridView1.Rows[0].Cells[5].Value = eventoId.ImagemUrl;
                dataGridView1.Rows[0].Cells[6].Value = eventoId.Telefone;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Informe um numero valido !!!", "Aviso !!!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (eventoFrm.EventoID != 0)
            {
            FrmEventoCRUD alt = new FrmEventoCRUD(eventoFrm, Enums.EnumAcaoCrud.Deletar);
            alt.ShowDialog();
            BuscarTodos();
            }
            else
            {
                MessageBox.Show("Selecione uma Linha Valida.");
            }
            eventoFrm = new Evento();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (eventoFrm.EventoID != 0)
            {
                FrmEventoCRUD alt = new FrmEventoCRUD(eventoFrm, Enums.EnumAcaoCrud.Alterar);
                alt.ShowDialog();
                BuscarTodos();
            }
            else
            {
                MessageBox.Show("AVISO !!! Selecione uma linha valida !!!");
            }

            eventoFrm = new Evento();

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
                }catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show("AVISO !!! Selecione uma linha valida !!!");
                }
            }
        }
    }
}
