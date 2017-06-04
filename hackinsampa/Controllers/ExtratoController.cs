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

namespace hackinsampa.Controllers
{
	public class ExtratoController : ApiController
	{
		private Db db = new Db();

		// GET: api/Extrato
		[ResponseType(typeof(IEnumerable<VMExtrato>))]
		public async Task<IHttpActionResult> GetExtrato()
		{
			var extratos = (await db.Extrato.Take(50).ToListAsync()).Select(d => new VMExtrato()
			{
				Id = d.Id,
				Orgão = d.Orgão,
				Retranca = d.Retranca,
				Modalidade = d.Modalidade,
				Licitacao = d.Licitacao,
				Processo = d.Processo,
				Evento = d.Evento,
				Objeto = d.Objeto,
				DataPublicacao = d.DataPublicacao,
				Fornecedor = d.Fornecedor,
				DataAssinatura = d.DataAssinatura,
				DocumentoFornecedor = d.DocumentoFornecedor,
				NumeroContrato = d.NumeroContrato,
				TipoFornecedor = d.TipoFornecedor,
				TipoValidade = d.TipoValidade,
				Validade = d.Validade,
				Valor = d.Valor
			});
			return Ok(extratos);
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

	public class VMExtrato
	{
		public int Id { get; set; }

		public string Orgão { get; set; }

		public string Retranca { get; set; }

		public string Modalidade { get; set; }

		public string Licitacao { get; set; }

		public string Processo { get; set; }

		public string Evento { get; set; }

		public string Objeto { get; set; }

		public string DataPublicacao { get; set; }

		public string Fornecedor { get; set; }

		public string TipoFornecedor { get; set; }

		public string DocumentoFornecedor { get; set; }

		public string DataAssinatura { get; set; }

		public double? Validade { get; set; }

		public string TipoValidade { get; set; }

		public decimal? Valor { get; set; }

		public string NumeroContrato { get; set; }
	}
}