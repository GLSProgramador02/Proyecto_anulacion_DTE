using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AnulaciónDte.Models
{
    public class Receptor
    {
        [Key]
        [JsonIgnore]

        public Int64 Id { get; set; }
        [JsonIgnore]

        public Int64 id_identificacion { get; set; }
        [ForeignKey("id_identificacion")]
        [JsonIgnore]
        public virtual identificacion Identificacion { get; set; }
        public string? nit { get; set; }
        public string? nrc { get; set; }
        public string? nombre { get; set; }
        public string? codActividad { get; set; }
        [ForeignKey("codActividad")]
        [JsonIgnore]
        public virtual Actividad_Economica Actividad_Economica { get; set; }
        public string? descActividad { get; set; }
        public string? nombreComercial { get; set; }
        public string? telefono { get; set; }
        public string? correo { get; set; }
        public string? tipoDocumento { set; get; }
        [ForeignKey("tipoDocumento")]
        [JsonIgnore]
        public virtual Tipo_Docemento_Identificacion_Receptor? Tipo_Documento_fc { get; set; }
        public string? numDocumento { set; get; }
    }
}
