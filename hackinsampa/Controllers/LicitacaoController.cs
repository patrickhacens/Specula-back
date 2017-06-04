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
using System.Web.Http.Cors;

namespace hackinsampa.Controllers
{
	public class LicitacaoController : ApiController
	{
		private Db db = new Db();


		[ResponseType(typeof(IEnumerable<Licitacao>))]
		[EnableCors(origins: "*", headers: "*", methods: "*")]
		public async Task<IHttpActionResult> GetLicitacoes()
		{
			Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pt-BR");
			Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pt-BR");
			var extratos = (await db.Extrato.OrderByDescending(d => d.DataAssinatura).Take(100).ToListAsync()).Select(d => new VMExtrato()
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
			Dictionary<DateTime, decimal> lastFiveMonthsQuantity = new Dictionary<DateTime, decimal>();
			Dictionary<DateTime, decimal> lastFiveMonthsSum = new Dictionary<DateTime, decimal>();
			var lastMonth = new DateTime(2015, 12, 1);
			for (int i = 0; i < 5; i++)
			{
				var firstDay = lastMonth.AddMonths(-i);
				var lastDay = firstDay.AddMonths(1).AddDays(-1);
				var data = (await db.Extrato.ToArrayAsync()).Where(d => !string.IsNullOrEmpty(d.DataAssinatura) && DateTime.Parse(d.DataAssinatura) >= firstDay && DateTime.Parse(d.DataAssinatura) <= lastDay);
				lastFiveMonthsQuantity.Add(lastMonth.AddMonths(-i), data.Count());
				lastFiveMonthsSum.Add(lastMonth.AddMonths(-i), data.Sum(d => d.Valor ?? 0.0m));
			}
			return Ok(new Licitacao() { Extratos = extratos, Quantity = lastFiveMonthsQuantity, Sum = lastFiveMonthsSum });
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

	public class Licitacao
	{
		public IEnumerable<VMExtrato> Extratos { get; set; }
		public Dictionary<DateTime, decimal> Quantity { get; set; }
		public Dictionary<DateTime, decimal> Sum { get; set; }
	}
}