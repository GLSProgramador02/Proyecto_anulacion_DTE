namespace AnulaciónDte
{
    public class respuestaHacienda
    {
        public int version { get; set; }
        public string ambiente { get; set; }
        public int versionApp { get; set; }
        public string estado { get; set; }
        public string codigoGeneracion { get; set; }
        public string selloRecibido { get; set; }
        public string fhProcesamiento { get; set; }
        public string clasificaMsg { get; set; }
        public string codigoMsg { get; set; }
        public string descripcionMsg { get; set; }
        public List<string> observaciones { get; set; }
    }
}