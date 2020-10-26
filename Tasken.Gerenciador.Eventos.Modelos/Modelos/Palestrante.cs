using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasken.Gerenciador.Eventos.Modelos.Modelos
{
    public class Palestrante
    {
        public int PalestranteId { get; set; }
        public string Nome { get; set; }

        public string ImagemUrl { get; set; }

        public string Telefone { get; set; }

        public string Minicurriculo { get; set; }

        public string Email { get; set; }

        public Palestrante()
        {

        }

        public Palestrante(int palestranteId)
        {
            this.PalestranteId = palestranteId;
        }

        public Palestrante(string nome, string imagemUrl, string telefone, string minicurriculo, string email)
        {
            this.Nome = nome;
            this.ImagemUrl = imagemUrl;
            this.Telefone = telefone;
            this.Minicurriculo = minicurriculo;
            this.Email = email;
        }
        public Palestrante(int palestranteId, string nome, string imagemUrl, string telefone, string minicurriculo, string email)
        {
            this.PalestranteId = palestranteId;
            this.Nome = nome;
            this.ImagemUrl = imagemUrl;
            this.Telefone = telefone;
            this.Minicurriculo = minicurriculo;
            this.Email = email;
        }


        public override string ToString() => $"ID: {this.PalestranteId}, Nome: {this.Nome}, Imagem URL: {this.ImagemUrl}, Telefone: {this.Telefone}, " +
            $"Minicurriculo: {this.Minicurriculo}, Email: {this.Email}";
    }
}
