namespace AnulaciónDte
{
    public class respuestaAutenticador
    {
        public string status { get; set; }
        public Body body { get; set; }
    }
    public class Body
    {
        public string user { get; set; }
        public string token { get; set; }
        public Rol rol { get; set; }
        public List<string> roles { get; set; }
        public string tokenType { get; set; }
    }

    public class Rol
    {
        public string nombre { get; set; }
        public string codigo { get; set; }
        public object descripcion { get; set; }
        public object rolSuperior { get; set; }
        public object nivel { get; set; }
        public object activo { get; set; }
        public object permisos { get; set; }
    }
}