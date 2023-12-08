using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AnulaciónDte.Models
{
    public class Empresa
    {
        [Key]
        public int Id { get; set; }
        public string CODIGOINTERNO { get; set; }
        public string NOMBRE { get; set; }
        public string NIT {  get; set; }
        public string NRC { get; set; }
        public string CODIGOACTIVIDAD { get; set; }
        [ForeignKey("CODIGOACTIVIDAD")]
        [JsonIgnore]
        public virtual Actividad_Economica Actividad_Economica { get; set; }
        public string TIPO_ESTABLECIMIENTO { get; set; }

        [ForeignKey("TIPO_ESTABLECIMIENTO")]
        [JsonIgnore]
        public virtual Tipo_Establecimiento Tipo_Establecimiento { get; set; }
        public string DEPARTAMENTO { get; set; }
        public string MUNICIPIO { get; set; }
        public string COMPLEMENTO { get; set; }
        public string TELEFONO { get; set; }
        public string CORREO { get; set; }
        public string icono { get; set; }
    }
}
