using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnulaciónDte.Models
{
    public class Tipo_Documento
    {
        [Key]
        public string CODIGO { get; set; }
        public string VALORES { get; set; }
    }
}
