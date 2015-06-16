using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion.UI.Console
{
	public class SolicitudDescarga
	{
		/// <summary>
		/// RFC para entrar al SAT
		/// </summary>
		public string RFC;

		/// <summary>
		/// Contraseña para entrar al SAT
		/// </summary>
		public string Contrasena;

		/// <summary>
		/// Email donde se mandarán las facturas
		/// </summary>
		public string Email;

		/// <summary>
		/// Desde que fecha descargar (solo se usan mes y año, el dia se ignora)
		/// </summary>
		public DateTime FechaDesde;

		/// <summary>
		/// Hasta que fecha descargar (solo se usan mes y año, el dia se ignora)
		/// </summary>
		public DateTime FechaHasta;

		/// <summary>
		/// Define si descargar facturas emitidas o recibidas
		/// </summary>
		public OKHOSTING.ERP.Local.Mexico.Facturacion.Descargador.TipoBusqueda Busqueda;
	}
}