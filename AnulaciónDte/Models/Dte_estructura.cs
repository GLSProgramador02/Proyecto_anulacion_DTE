using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnulaciónDte.Models
{
    public class Dte_estructura
    {
        public identificacion identificacion {  get; set; }
        public Emisor emisor { get; set; }
        public documento documento { get; set; }
        public Responsable_Anulacion motivo { get; set; }
    }
}
