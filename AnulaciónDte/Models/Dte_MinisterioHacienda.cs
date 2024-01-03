using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
namespace AnulaciónDte.Models
{
    public class Dte_MinisterioHacienda : DbContext
    {
        public Dte_MinisterioHacienda() { }

        public Dte_MinisterioHacienda(DbContextOptions<Dte_MinisterioHacienda> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ambiente_Destino>().ToTable("CAT001_AMBIENTE_DESTINO");
            modelBuilder.Entity<Tipo_Documento>().ToTable("CAT002_TIPO_DOCUMENTO");
            modelBuilder.Entity<Modelo_Faturacion>().ToTable("CAT003_MODELO_FACTURACION");
            modelBuilder.Entity<Tipo_Transmision>().ToTable("CAT004_TIPO_TRANSMISION");
            modelBuilder.Entity<Tipo_Contingencia>().ToTable("CAT005_TIPO_CONTINGENCIA");
            modelBuilder.Entity<identificacion>().ToTable("DTE_IDENTIFICACION");
            modelBuilder.Entity<Tipo_Establecimiento>().ToTable("CAT009_TIPO_ESTABLECIMIENTO");
            modelBuilder.Entity<Actividad_Economica>().ToTable("CAT019_ACTIVIDAD_ECONOMICA");
            modelBuilder.Entity<Receptor>().ToTable("DTE_RECEPTOR");
            modelBuilder.Entity<Emisor>().ToTable("DTE_EMISOR");
            modelBuilder.Entity<Responsable_Anulacion>().ToTable("DTE_MOTIVO_ANULACION");
            modelBuilder.Entity<Tipo_Docemento_Identificacion_Receptor>().ToTable("CAT022_TIPO_DOCU_IDENTIFICACION_RECEPTOR");

            modelBuilder.Entity<Respuesta_hacienda>().ToTable("DTE_RESPUESTA_HACIENDA");
            modelBuilder.Entity<Empresa>().ToTable("EMP_EMPRESA");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            ConnectionStringSettings value = ConfigurationManager.ConnectionStrings["MyDbContextName"];
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.UseSqlServer(value.ConnectionString);
        }
        public DbSet<Ambiente_Destino> ambiente_Destinos { get; set; }
        public DbSet<Tipo_Transmision> Tipo_Transmisions { get; set; }
        public DbSet<Modelo_Faturacion> Modelo_Faturacions { get; set; }
        public DbSet<Tipo_Contingencia> tipo_Contingencias { get; set; }
        public DbSet<Tipo_Documento> tipo_Documentos { get; set; }
        public DbSet<identificacion> identificacions { get; set; }
        public DbSet<Emisor> emisors { get; set; } 
        public DbSet<Receptor> receptor { get; set; }
        public DbSet<Tipo_Establecimiento> tipo_Establecimientos { get; set; }
        public DbSet<Actividad_Economica> actividad_Economicas { get; set; }
        public DbSet<Respuesta_hacienda>? respuesta_Haciendas { get; set; }
        public DbSet<Empresa>? empresas { get; set; }
        public DbSet<Responsable_Anulacion>? responsable_Anulacions { get; set; }
        public DbSet<Tipo_Docemento_Identificacion_Receptor>? tipo_Docemento_Identificacion_Receptors { get; set; }

    }
}
