using System.ComponentModel.DataAnnotations;

namespace AnulaciónDte.Models
{
    public class Tipo_Docemento_Identificacion_Receptor
    {
        [Key]
        public string CODIGO { get; set; }
        public string VALORES { get; set; }
    }
}