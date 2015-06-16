using CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OKHOSTING.ERP.Local.Mexico.Facturacion.UI.Console
{
	public class Arguments
	{
		[Option(Required = true)]
		public string RFC { get; set; }
		
		[Option(Required = true)]
		public string Contrasena { get; set; }

		[Option(Required = true)]
		public string Carpeta { get; set; }

		[Option(Required = true)]
		public string FechaDesde { get; set; }

		[Option(Required = true)]
		public string FechaHasta { get; set; }

		[Option(Required = true)]
		public Descargador.TipoBusqueda TipoBusqueda { get; set; }
	}
}