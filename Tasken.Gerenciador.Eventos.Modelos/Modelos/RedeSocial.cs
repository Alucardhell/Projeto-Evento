using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasken.Gerenciador.Eventos.Modelos.Modelos
{
    public class RedeSocial
    {
        public int RedeSocialId { get; set; }

        public string Nome { get; set; }

        public string Url { get; set; }

        public int EventoId { get; set; }

        public int PalestranteId { get; set; }

        public RedeSocial()
        {

        }
        public RedeSocial(int redeSocialId, string nome, string url, int eventoId)
        {
            this.RedeSocialId = redeSocialId;
            this.Nome = nome;
            this.Url = url;
            this.EventoId = eventoId;
        }

        public RedeSocial(int redeSocialId, int palestranteId, string nome, string url)
        {
            this.RedeSocialId = redeSocialId;
            this.Nome = nome;
            this.Url = url;
            this.PalestranteId = palestranteId;
        }

        public override string ToString() => $"Rede Social ID: {RedeSocialId}, Nome: {Nome}, Url: {Url}, EventoId: {EventoId}, PalestranteId: {PalestranteId}";

    }
}