using System.ComponentModel.DataAnnotations;

namespace AnulaciónDte.Models
{
    public class Tipo_Transmision
    {
        [Key]
        public int CODIGO { get; set; }

        public string VALORES { get; set; }
    }
}