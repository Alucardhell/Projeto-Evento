using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Tasken.Gerenciador.Eventos.Modelos.Modelos
{
    class PalestranteEvento
    {
        public int _id { get; set; }
        
        public int _palestranteId { get; set; }

        public int _eventoId { get; set; }

        public PalestranteEvento(int _id, int _palestranteId, int _eventoId)
        {
            this._id = _id;
            this._palestranteId = _palestranteId;
            this._eventoId = _eventoId;
        }
    }
}
