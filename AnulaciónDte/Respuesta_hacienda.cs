using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using AnulaciónDte.Models;

namespace AnulaciónDte
{
    public class Respuesta_hacienda
    {
        [Key]
        [JsonIgnore]
        public Int64 id { get; set; }

        [Column("id_identficicacion")]
        [JsonIgnore]
        public Int64? id_identificacion { get; set; }
        [ForeignKey("id_identificacion")]
        [JsonIgnore]
        public virtual identificacion? Identificacion { get; set; }

        public int? version { get; set; }

        [MaxLength(2)]
        public string? ambiente { get; set; }

        [Column("versionApp")]
        [MaxLength(5)]
        public string? versionApp { get; set; }

        [MaxLength(20)]
        public string? estado { get; set; }

        [Column("codigogeneracion")]
        [MaxLength(36)]
        public string? codigogeneracion { get; set; }

        [Column("selloRecibido")]
        [MaxLength(40)]
        public string? selloRecibido { get; set; }
        [NotMapped]
        public DateTime? fhProcesamiento { get; set; }

        [MaxLength(2)]
        public string? clasificaMsg { get; set; }

        [MaxLength(3)]
        public string? codigoMsg { get; set; }

        [MaxLength(20)]
        public string? descripcionMsg { get; set; }

        [MaxLength(3000)]
        public string? observacion { get; set; }
    }
}