using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AnulaciónDte.Models
{
    public class Emisor
    {
        [Key]
        [JsonIgnore]
        public Int64 Id { get; set; }

        [JsonIgnore]
        public Int64 id_identificacion { get; set; }

        [ForeignKey("id_identificacion")]
        [JsonIgnore]
        public virtual identificacion Identificacion { get; set; }

        public string nit { get; set; }
        [JsonIgnore]

        public string? nrc { get; set; }
        public string? nombre { get; set; }
        [Column("nombreComercial")]
        public string? nomEstablecimiento  { get; set; }
        public string? tipoEstablecimiento { get; set; }

        [ForeignKey("tipoEstablecimiento")]
        [JsonIgnore]
        public virtual Tipo_Establecimiento Tipo_Establecimiento { get; set; }

        public string? telefono { get; set; }
        public string? correo { get; set; }
        public string? codEstableMH { get; set; }
        public string? codEstable { get; set; }
        public string? codPuntoVentaMH { get; set; }
        public string? codPuntoVenta { get; set; }
    }
}