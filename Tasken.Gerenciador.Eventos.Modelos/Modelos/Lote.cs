using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Tasken.Gerenciador.Eventos.Modelos.Modelos
{
    public class Lote
    {
        public int Loteid { get; set; }

        public string Nome { get; set; }
        
        public double Preco { get; set; }

        public DateTime DataInicio { get; set; }

        public DateTime DataFim { get; set; }

        public int Quantidade { get; set; }

        public int Eventoid { get; set; }

        public string NomeEvento { get; set; }

        public Lote()
        {

        }


        public Lote(int loteid, string nome, float preco, DateTime dataInicio, DateTime dataFim, int quantidade, int eventoid)
        {
            this.Loteid = loteid;
            this.Nome = nome;
            this.Preco = preco;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
            this.Quantidade = quantidade;
            this.Eventoid = eventoid;
        }

        public Lote(string nomeEvento, int loteid, string nome, double preco, DateTime dataInicio, DateTime dataFim, int quantidade)
        {
            this.Loteid = loteid;
            this.Nome = nome;
            this.Preco = preco;
            this.DataInicio = dataInicio;
            this.DataFim = dataFim;
            this.Quantidade = quantidade;
            this.NomeEvento = nomeEvento;
        }

        public override string ToString() => $"Nome Evento: {NomeEvento}, LoteId: {Loteid}, Nome: {Nome}, Preço: {Preco}, Data Inicio: {DataInicio}" +
            $", Data Fim {DataFim}, Quantidade: {Quantidade}.";
    }
}
