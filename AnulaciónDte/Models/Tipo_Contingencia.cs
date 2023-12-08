using System.ComponentModel.DataAnnotations;

namespace AnulaciónDte.Models
{
    public class Tipo_Contingencia
    {
        [Key]
        public int CODIGO { get; set; }

        public string VALORES { get; set; }
    }
}