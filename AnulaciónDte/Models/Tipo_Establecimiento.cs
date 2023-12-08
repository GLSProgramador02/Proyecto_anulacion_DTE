using System.ComponentModel.DataAnnotations;

namespace AnulaciónDte.Models
{
    public class Tipo_Establecimiento
    {
        [Key]
        public string CODIGO { get; set; }

        public string VALORES { get; set; }
    }
}