using System.ComponentModel.DataAnnotations;

namespace AnulaciónDte.Models
{
    public class Modelo_Faturacion
    {
        [Key]
        public int CODIGO { get; set; }
        public string VALORES { get; set; }
    }
}