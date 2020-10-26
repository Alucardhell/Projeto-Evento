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
    public partial class FrmInformacoesEvento : Form
    {
        private Evento _evento;
        private Palestrante _palestrante;
        private Lote _lote;

        public FrmInformacoesEvento()
        {
            InitializeComponent();
        }

        public FrmInformacoesEvento(Evento evento)
        {
            InitializeComponent();
            this._evento = evento;
        }

        private void BuscarTodosPalestrantes()
        {
            FabricaRepositorio fabricarEvento = new FabricaRepositorio(ConnectionSQL.connectionString);

            List<Palestrante> palestrantes = fabricarEvento.RepositorioPalestrante.ConsultarPorIdEvento(_evento.EventoID);

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

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void FrmInformacoesEvento_Load(object sender, EventArgs e)
        {
            textBoxEventoId.Text = _evento.EventoID.ToString();
            textBoxEventoLocal.Text = _evento.Local;
            textBoxEventoData.Text = _evento.DataEvento.ToString("dd/MM/yyyy");
            textBoxEventoTema.Text = _evento.Tema;
            textBoxEventoQtd.Text = _evento.Qtd.ToString();
            textBoxEventoUrl.Text = _evento.ImagemUrl;
            textBoxEventoTelefone.Text = _evento.Telefone;

            try
            {

                pictureBoxUrl.LoadAsync(_evento.ImagemUrl);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            BuscarTodosPalestrantes();

            BuscaTodosLote();

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
