using System;

namespace Tasken.Gerenciador.Eventos.Modelos.Modelos
{
    public class Evento
    {

        public int EventoID { get; set; }

        public string Local { get; set; }

        public DateTime DataEvento { get; set; }

        public string Tema { get; set; }

        public int Qtd { get; set; }

        public string ImagemUrl { get; set; }

        public string Telefone { get; set; }

        public Evento()
        {

        }
        public Evento(string local, DateTime dataEvento, string tema, int qtd, string imagemUrl, string telefone)
        {
            this.Local = local;
            this.DataEvento = dataEvento;
            this.Tema = tema;
            this.Qtd = qtd;
            this.ImagemUrl = imagemUrl;
            this.Telefone = telefone;
        }

        public Evento(int eventoId, string local, DateTime dataEvento, string tema, int qtd, string imagemUrl, string telefone)
        {
            this.EventoID = eventoId;
            this.Local = local;
            this.DataEvento = dataEvento;
            this.Tema = tema;
            this.Qtd = qtd;
            this.ImagemUrl = imagemUrl;
            this.Telefone = telefone;
        }


        public override string ToString() => $"ID: {this.EventoID}, Local: {this.Local}, Data: {this.DataEvento.ToString("dd/MM/yyyy")}, Tema: {this.Tema}, " +
            $"Quantidade de Pessoas: {this.Qtd}, ImagemURL: {this.ImagemUrl}, Telefone: {this.Telefone}";

    }
}
