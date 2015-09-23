using OKHOSTING.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion.UI.Console
{
	class Program
	{
		public static string Ruta;

		[STAThread]
		static void Main(string[] args)
		{
			Ruta = args[0];
			FileSystemWatcher watcher = new FileSystemWatcher(Path.Combine(Ruta, "Solicitudes"));
			
			watcher.Created += watcher_Created;
			watcher.EnableRaisingEvents = true;

			while (true)
			{
				foreach (string s in Directory.GetFiles(Path.Combine(Ruta, "Solicitudes")))
				{
					ProcesarDescarga(s);
				}
	
				Thread.Sleep(10000);
			}
		}

		static void watcher_Created(object sender, FileSystemEventArgs e)
		{
			Thread.Sleep(1000);
			ProcesarDescarga(e.FullPath);
		}

		static void ProcesarDescarga(string rutaSolicitud)
		{
			string json = File.ReadAllText(rutaSolicitud);
			json = OKHOSTING.Core.Cryptography.SimpleEncryption.Decrypt(json);
			System.Net.Mail.MailMessage mail;

			var solicitud = Newtonsoft.Json.JsonConvert.DeserializeObject<SolicitudDescarga>(json);
			//solicitud.Busqueda = Descargador.TipoBusqueda.Recibidas; //testing

			Log.Write("ProcesarDescarga", "---------------------NUEVA SOLICITUD : " + rutaSolicitud, Log.Information);
			Log.Write("ProcesarDescarga", "RFC: " + solicitud.RFC, Log.Information);
			Log.Write("ProcesarDescarga", "FechaDesde: " + solicitud.FechaDesde, Log.Information);
			Log.Write("ProcesarDescarga", "FechaHasta: " + solicitud.FechaHasta, Log.Information);
			Log.Write("ProcesarDescarga", "Busqueda: " + solicitud.Busqueda, Log.Information);
			Log.Write("ProcesarDescarga", "Email: " + solicitud.Email, Log.Information);

			string carpeta = Path.Combine(Ruta, "Facturas", Path.GetFileName(rutaSolicitud), solicitud.Busqueda.ToString());
			
			//Descargador.Descargar(solicitud.RFC, solicitud.Contrasena, carpeta, solicitud.FechaDesde, solicitud.FechaHasta, solicitud.Busqueda);

			try
			{
				Descargador.Descargar(solicitud.RFC, solicitud.Contrasena, carpeta, solicitud.FechaDesde, solicitud.FechaHasta, solicitud.Busqueda);
			}
			catch (Exception e)
			{
				OKHOSTING.Core.Log.Write("ProcesarDescarga", "Error on " + rutaSolicitud + ": " + e, Log.Information);

				if (e.InnerException != null &&  e.InnerException.Message.StartsWith("Los datos de acceso son incorrectos"))
				{
					//mandar correo
					mail = new System.Net.Mail.MailMessage();
					mail.To.Add(solicitud.Email);
					mail.Subject = "Tus facturas no pudieron descargarse por contraseña incorrecta";
					mail.Body = string.Format("El RFC {0} y contraseña {1} proporcionados no funcionaron, por favor intentalo de nuevo en http://factura.me", solicitud.RFC, solicitud.Contrasena);

					OKHOSTING.Core.Net.Mail.MailManager.Send(mail);

					File.Delete(rutaSolicitud);
				}

				return;
			}

			//exportar a excel
			List<XmlDocument> facturas = new List<XmlDocument>();

			foreach (var f in Directory.GetFiles(carpeta))
			{
				if (!f.EndsWith(".xml"))
				{
					continue;
				}

				XmlDocument xml = new XmlDocument();
				xml.Load(f);

				facturas.Add(xml);
			}

			Exportador.ExportarExcel(facturas, Path.Combine(carpeta, "Facturas.xlsx"));
			OKHOSTING.Core.Log.Write("ProcesarDescarga", "Excel generado", Log.Information);
			
			OKHOSTING.Files.ZipTools.CompressDirectory(carpeta, carpeta + ".zip");
			OKHOSTING.Core.Log.Write("ProcesarDescarga", "Zip generado", Log.Information);

			//mandar correo
			using (mail = new System.Net.Mail.MailMessage())
			using (var att = new System.Net.Mail.Attachment(carpeta + ".zip"))
			{
				mail.To.Add(solicitud.Email);
				mail.Attachments.Add(att);
				mail.Subject = "Tus facturas estan listas";
				mail.Body = string.Format(
					@"Gracias por usar nuestro servicio, patrocinado por OK HOSTING.
					RFC:
					Desde:
					Hasta:
					Tipo: ", solicitud.RFC, solicitud.FechaDesde, solicitud.FechaHasta, solicitud.Busqueda);

				OKHOSTING.Core.Net.Mail.MailManager.Send(mail);
				OKHOSTING.Core.Log.Write("ProcesarDescarga", "Correo enviado", Log.Information);
			}

			File.Delete(rutaSolicitud);
			
			//borrar datos
			if (Directory.Exists(carpeta))
			{
				foreach (string file in Directory.GetFiles(carpeta, "*.*", SearchOption.AllDirectories))
				{
					File.Delete(file);
				}

				//Directory.Delete(Path.Combine(Ruta, "Facturas", s.RFC), true);
			}

			OKHOSTING.Core.Log.Write("ProcesarDescarga", "Archivos eliminados", Log.Information);
		}
	}
}