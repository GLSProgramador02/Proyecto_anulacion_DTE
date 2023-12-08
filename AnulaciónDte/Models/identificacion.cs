
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace AnulaciónDte.Models
{
    public class identificacion
    {
        [Key]
        [JsonIgnore]
        public Int64 Id { get; set; }
        [JsonIgnore]
        public int? idempresa { get; set; }
        [ForeignKey("idempresa")]
        [JsonIgnore]
        public virtual Empresa? Empresa { get; set; }
        public int? version { get; set; }
        public string? ambiente { get; set; }
        [ForeignKey("ambiente")]
        [JsonIgnore]

        public virtual Ambiente_Destino? ambiente_fk { get; set; }
        [JsonIgnore]

        public string? tipoDte { get; set; }
        [ForeignKey("tipoDte")]
        [JsonIgnore]

        public virtual Tipo_Documento? tipoDte_fk { get; set; }
        [JsonIgnore]
        public string? numeroControl { get; set; }
        public string? codigoGeneracion { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [JsonIgnore]
        public DateTime? fechaEmi { get; set; }
        [JsonIgnore]

        [NotMapped]
        public string? fecEmi { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Column("fecAnula")]
        [JsonIgnore]

        public DateTime? fechaAnula { get; set; }
        [NotMapped]
        public string? fecAnula { get; set; }
        [DataType(DataType.Time)]
        [Column(TypeName = "Time")]
        public TimeSpan? horAnula { get; set; }
        [JsonIgnore]

        public string? usuario { get; set; }
        [JsonIgnore]

        public string? equipo { get; set; }
        [JsonIgnore]

        public string? ipusuario { get; set; }
        [Column(TypeName = "DateTime2")]
        [JsonIgnore]
        public DateTime fechasistema { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        [JsonIgnore]
        public string? respuestaHacienda { get; set; }
        [JsonIgnore]
        public string? selloRecepcion { get; set; }
        [JsonIgnore]

        public int? tipoContingencia { get; set; }
        [ForeignKey("tipoContingencia")]
        [JsonIgnore]

        public virtual Tipo_Contingencia? tipoContingencia_fk { get; set; }
        [JsonIgnore]

        public string? motivoContin { get; set; }
    }
}
