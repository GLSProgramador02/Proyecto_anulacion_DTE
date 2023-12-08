

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AnulaciónDte.Models
{
    public class Responsable_Anulacion
    {
        [Key]
        [JsonIgnore]
        public Int64? Id { get; set; }
        [JsonIgnore]
        public Int64? id_identificacion { get; set; }
        [ForeignKey("id_identificacion")]
        [JsonIgnore]
        public virtual identificacion? identificacion { get; set; }
        public int? tipoAnulacion { get; set; }
        public string? motivoAnulacion { get; set; }
        public string? nombreResponsable { get; set; }
        public string? tipDocResponsable { get; set; }
        public string? numDocResponsable { get; set; }
        [NotMapped]
        public string? nombreSolicita { get; set; }
        [NotMapped]
        public string? tipDocSolicita { get; set; }
        [NotMapped]
        public string? numDocSolicita { get; set; }
    }
}
