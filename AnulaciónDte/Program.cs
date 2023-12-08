using AnulaciónDte.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Globalization;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace AnulaciónDte
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            string? idDTE = args[0];

            Dte_estructura dte_Estructura = new Dte_estructura();
            documento dte_documento = new documento();
            using (var anulacion = new Dte_MinisterioHacienda())
            {
                var identificacion = anulacion.identificacions.Where(x => x.Id == Convert.ToInt64(idDTE)).SingleOrDefault();
                var emisor = anulacion.emisors.Where(x => x.id_identificacion == identificacion.Id).SingleOrDefault();
                var receptor = anulacion.receptor.Where(x => x.id_identificacion == identificacion.Id).SingleOrDefault();
                if (identificacion == null)
                {
                    Console.Write("No se encontraron datos de identificación");
                    return;
                }
                identificacion.version = 2;
                identificacion.fecAnula = identificacion.fechaAnula.Value.ToString("yyyy-MM-dd");
                identificacion.fecEmi = identificacion.fechaEmi.Value.ToString("yyyy-MM-dd");
                if (emisor == null)
                {
                    Console.Write("No se encontraron datos de emisor");
                    return;
                }
                if (receptor == null)
                {
                    Console.Write("No se encontraron datos de receptor");
                    return;
                }
                dte_documento.tipoDte = identificacion.tipoDte;
                dte_documento.codigoGeneracion = identificacion.codigoGeneracion;
                dte_documento.numeroControl = identificacion.numeroControl;
                dte_documento.selloRecibido = identificacion.selloRecepcion;
                dte_documento.fecEmi = identificacion.fecEmi;
                dte_documento.montoIva = 0;
                dte_documento.codigoGeneracionR = Guid.NewGuid().ToString().ToUpper();
                dte_documento.tipoDocumento = receptor.tipoDocumento == null ? "36": receptor.tipoDocumento;
                dte_documento.numDocumento = receptor.numDocumento == null ? receptor.nit : receptor.numDocumento;
                dte_documento.nombre = receptor.nombre;
                dte_documento.telefono = receptor.telefono;
                dte_documento.correo = receptor.correo;
                var motivo = anulacion.responsable_Anulacions.Where(x => x.id_identificacion == identificacion.Id).SingleOrDefault();
                if (motivo == null)
                {
                    Console.Write("No se encontraron datos de motivo");
                    return;
                }
                motivo.nombreSolicita = emisor.nombre;
                motivo.tipDocSolicita = identificacion.tipoDte;
                motivo.numDocSolicita = emisor.nit;

                dte_Estructura.identificacion = identificacion;
                dte_Estructura.emisor = emisor;
                dte_Estructura.documento = dte_documento;
                dte_Estructura.motivo = motivo;
                //var dte = JsonConvert.SerializeObject(dte_Estructura);
                //Console.WriteLine(dte);
                //return;
                try
                {
                    respuestaFirmador respFirmador = new respuestaFirmador();
                respFirmador = await firmarDocumentoAsync(dte_Estructura);
                if (respFirmador.status == "OK")
                {
                    respuestaAutenticador respFirmadorAuth = new respuestaAutenticador();
                    respFirmadorAuth = await autenticadorMinisterioHacienda();
                    if (respFirmadorAuth.status != "ERROR")
                    {
                        await envioDteMinisterioHacienda(idDTE, dte_Estructura, respFirmador, respFirmadorAuth);
                    }
                }
                
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during serialization: {ex.Message}");
                }
            }
         }

        public static async Task<respuestaFirmador?> firmarDocumentoAsync(object obj)
        {
            string apiUrl = "http://172.16.101.166:8113/firmardocumento/";
            object jsonBody = null;
            jsonBody = new
            {
                nit = "11231710761012",
                activo = true,
                passwordPri = "Tridente253215.9*P",
                dteJson = obj
            };
            respuestaFirmador respFirmador = new respuestaFirmador();

            // Desactivar la validación del certificado SSL
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // Configurar el encabezado Content-Type correctamente como "application/json"
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // Serializar el objeto jsonBody a JSON y crear un contenido StringContent
                    string jsonBodyString = JsonConvert.SerializeObject(jsonBody);
                    var content = new StringContent(jsonBodyString, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await httpClient.PostAsync(apiUrl, content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseContent = await response.Content.ReadAsStringAsync();
                        respFirmador = JsonConvert.DeserializeObject<respuestaFirmador>(responseContent);
                        //Console.WriteLine("Respuesta del servidor: " + responseContent);
                        return respFirmador;
                    }
                    else
                    {
                        Console.WriteLine("Error en la solicitud. Código de estado: " + (int)response.StatusCode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    if (ex.InnerException != null)
                    {
                        Console.WriteLine("Excepción interna: " + ex.InnerException.Message);
                        return null;
                    }
                    return null;
                }
                return null;
            }
        }
        public static async Task<respuestaAutenticador?> autenticadorMinisterioHacienda()
        {
            try
            {
                string apiUrlAuth = "https://apitest.dtes.mh.gob.sv/seguridad/auth";
                List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("user", "11231710761012"),
                new KeyValuePair<string, string>("pwd", "Silofono5896*.tr")
            };

                respuestaAutenticador respFirmadorAuth = new respuestaAutenticador();
                using (HttpClient httpClient = new HttpClient())
                {
                    try
                    {
                        httpClient.DefaultRequestHeaders.Add("User-Agent", "GrupoLD SA de CV");
                        HttpResponseMessage response = await httpClient.PostAsync(apiUrlAuth, new FormUrlEncodedContent(formData));

                        if (response.IsSuccessStatusCode)
                        {
                            string responseContentAuth = await response.Content.ReadAsStringAsync();
                            respFirmadorAuth = JsonConvert.DeserializeObject<respuestaAutenticador>(responseContentAuth);
                            return respFirmadorAuth;
                            //Console.WriteLine("Respuesta del servidor: " + responseContentAuth);
                        }
                        else
                        {
                            Console.WriteLine("Error en la solicitud. Código de estado: " + (int)response.StatusCode);
                            Console.WriteLine("Error en la solicitud. Código de estado: " + response.Content);
                            return null;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error: " + ex.Message);
                        return null;
                    }
                }
            }
            catch (Exception e)
            {
                return null;
                Console.WriteLine(e.Message);
            }
        }
        public static async Task envioDteMinisterioHacienda(string idDTE, object obj, respuestaFirmador respFirmador, respuestaAutenticador respFirmadorAuth)
        {
            try
            {

                string apiUrlRecepcion = "https://apitest.dtes.mh.gob.sv/fesv/anulardte";
                object jsonBodyRecepcion = null;

                string ambiente = "";
                int version = 0;
                string tipoDte = "";
                string idEnvio = "";
                string codigoGeneracion = "";
                if (obj is Dte_estructura factura)
                {
                    ambiente = factura.identificacion.ambiente;
                    version = factura.identificacion.version.Value;
                    tipoDte = factura.identificacion.tipoDte;
                    idEnvio = factura.identificacion.codigoGeneracion;
                    codigoGeneracion = factura.identificacion.codigoGeneracion;
                }
                
                jsonBodyRecepcion = new { ambiente = ambiente, idEnvio = 1, version = version, documento = respFirmador.body };
                var json = JsonConvert.SerializeObject(jsonBodyRecepcion);
                HttpContent httpContent = new StringContent(json, Encoding.UTF8, "application/json");

                respuestaHacienda respFirmadorRecepcion = new respuestaHacienda();
                HttpClient httpClient = new HttpClient();
                try
                {
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    httpClient.DefaultRequestHeaders.Add("Authorization", respFirmadorAuth.body.token);
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "GrupoLD SA de CV");

                    HttpResponseMessage response = await httpClient.PostAsync(apiUrlRecepcion, httpContent);

                    string responseContentRecepcion = await response.Content.ReadAsStringAsync();
                    respFirmadorRecepcion = JsonConvert.DeserializeObject<respuestaHacienda>(responseContentRecepcion);

                    if (response.IsSuccessStatusCode)
                    {

                        if (obj is Dte_estructura dte_factura)
                        {
                            ActualizarDTE(idDTE, responseContentRecepcion, respFirmadorRecepcion);
                        }
                    }
                    else
                    {
                        if (obj is Dte_estructura dte_factura)
                        {
                            metodoContingenciaDTE(Convert.ToInt64(idDTE), 3, "No disponibilidad de sistema del MH"+respFirmadorRecepcion.descripcionMsg);
                            ActualizarDTE(idDTE, responseContentRecepcion, respFirmadorRecepcion);
                        }
                    }
                    Console.WriteLine("\nMensaje de estado: " + responseContentRecepcion);
                }
                catch (Exception ex)
                {
                    metodoContingenciaDTE(Convert.ToInt64(idDTE), 3, "No disponibilidad de sistema del MH");
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    httpClient.Dispose();
                }

            }
            catch (Exception e)
            {
                metodoContingenciaDTE(Convert.ToInt64(idDTE), 3, "No disponibilidad de sistema del MH");
                Console.WriteLine(e.Message);
            }
        }
        public static string retornarFecha(string fechaEntrada)
        {
            if (fechaEntrada != null)
            {
                string[] partes = fechaEntrada.Split(' ');
                if (partes.Length > 0)
                {
                    return partes[0];
                }
            }
            return string.Empty;
        }
        public static string retornarFechaHora(string fechaEntrada)
        {
            var fechaMo = fechaEntrada.Replace("/", "-");
            DateTime fechaDateTime = DateTime.ParseExact(fechaMo, "dd-MM-yyyy HH:mm:ss", new CultureInfo("es-ES"));
            return fechaDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
        public static void ActualizarDTE(string idDte = "", string respuesta = "", respuestaHacienda respuestaHacienda = null)
    {
        try
        {


            using (var facturaDTE = new Dte_MinisterioHacienda())
            {
                var respSerializado = JsonConvert.DeserializeObject<respuestaHacienda>(respuesta);

                respSerializado.fhProcesamiento = retornarFechaHora(respSerializado.fhProcesamiento);

                string jsonRespuesta = JsonConvert.SerializeObject(respSerializado);

                var respHaciendaSerializadoR = JsonConvert.DeserializeObject<Respuesta_hacienda>(jsonRespuesta);

                var bfact = facturaDTE.identificacions.FirstOrDefault(e => e.Id == Convert.ToInt32(idDte));


                bfact.respuestaHacienda = respuesta;
                bfact.selloRecepcion = respuestaHacienda.selloRecibido;

                Respuesta_hacienda respuestaHaciendafc = new Respuesta_hacienda();
                respuestaHaciendafc = JsonConvert.DeserializeObject<Respuesta_hacienda>(jsonRespuesta);
                respuestaHaciendafc.id_identificacion = Convert.ToInt32(idDte);
                respuestaHaciendafc.id = Convert.ToInt32(idDte);

                facturaDTE.respuesta_Haciendas.Add(respuestaHaciendafc);

                facturaDTE.SaveChanges();
            }
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("Error al guardar cambios en la base de datos: " + ex.Message);
            Exception innerException = ex.InnerException;
            if (innerException != null)
            {
                Console.WriteLine("Excepción interna: " + innerException.Message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
        public static void metodoContingenciaDTE(Int64 id, int idContingencia, string mensaje)
        {
            using (var CredtiDTE = new Dte_MinisterioHacienda())
            {

                var identigicacion = CredtiDTE.identificacions.Find(id);
                identigicacion.tipoContingencia = Convert.ToInt32(idContingencia);
                identigicacion.motivoContin = mensaje;
                CredtiDTE.SaveChanges();

            }
        }

    }
}