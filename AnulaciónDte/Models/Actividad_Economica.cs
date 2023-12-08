using System.ComponentModel.DataAnnotations;

namespace AnulaciónDte.Models
{
    public class Actividad_Economica
    {
        [Key]
        public string CODIGO { get; set; }

        public string VALORES { get; set; }
    }
}