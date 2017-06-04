using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using hackinsampa.Models;
using System.Threading;

namespace hackinsampa.Controllers
{
	public class VarianciaController : ApiController
	{
		private Db db = new Db();


		[ResponseType(typeof(IEnumerable<FornecedoresPorVariancia>))]
		public async Task<IHttpActionResult> GetLit()
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");

			var data = await db.FornecedoresPorVariancia.OrderByDescending(d => d.variancia).ToListAsync();
			return Ok(data);
		}


		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

	}

}