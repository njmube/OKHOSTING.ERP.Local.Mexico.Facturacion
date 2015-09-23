using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using OKHOSTING.ERP.Local.Mexico.Facturacion.UI.Console;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion.UI.Web
{
	public partial class Home : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				cldDesde.SelectedDate = DateTime.Today.AddMonths(-1);
				cldHasta.SelectedDate = DateTime.Today;
			}
		}

		protected void cmdSiguiente_Click(object sender, EventArgs e)
		{
			SolicitudDescarga solicitud = new SolicitudDescarga();

			solicitud.RFC = txtRFC.Text;
			solicitud.Contrasena = txtContrasena.Text;
			solicitud.Email = txtEmail.Text;
			solicitud.FechaDesde = cldDesde.SelectedDate;
			solicitud.FechaHasta = cldHasta.SelectedDate; ;
			solicitud.Busqueda = (Descargador.TipoBusqueda)Enum.Parse(typeof(Descargador.TipoBusqueda), ddlBusqueda.SelectedValue);

			string json = Newtonsoft.Json.JsonConvert.SerializeObject(solicitud);
			json = OKHOSTING.Core.Cryptography.SimpleEncryption.Encrypt(json);

			//guardar solicitud
			string carpetaSolicitud = Path.Combine(OKHOSTING.Core.DefaultPaths.Custom, "Solicitudes");

			if (!Directory.Exists(carpetaSolicitud))
			{
				Directory.CreateDirectory(carpetaSolicitud);
			}

			string rutaSolicitud = Path.Combine(carpetaSolicitud, txtRFC.Text + "-" + new Random().Next());

			File.WriteAllText(rutaSolicitud, json);

			Response.Redirect("Confirmacion.aspx", false);
		}
	}
}