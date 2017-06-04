using hackinsampa.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace hackinsampa.Controllers
{
	public class FornecedorController : ApiController
	{ 
		private Db db = new Db();

		[ResponseType(typeof(IEnumerable<Fornecedores>))]
		public async Task<IHttpActionResult> GetFornecedor()
		{
			var extratos = await db.Extrato.GroupBy(d => d.DocumentoFornecedor).Select(d => new Fornecedores() { Documento = d.Key, Nome = d.FirstOrDefault().Fornecedor ?? "" }).Take(100).ToListAsync();
			return Ok(extratos);
		}

		[ResponseType(typeof(IEnumerable<Fornecedores>))]
		public async Task<IHttpActionResult> GetFornecedor(string Id)
		{
			var extratos = (await db.Extrato.Where(d => d.DocumentoFornecedor == Id).Take(100).ToListAsync()).Select(d => new VMExtrato()
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
			var quantidade = extratos.Count();
			var soma = extratos.Sum(d => d.Valor ?? 0);
			var firstDate = extratos.Select(d => DateTime.Parse(d.DataAssinatura)).Distinct().Min();
			var orgaos = extratos.GroupBy(d => d.Orgão).Select(d => new FornecedorData.Orgao() { Nome = d.Key, Quantidade = d.Count() });
			return Ok(new FornecedorData()
			{
				Documento = Id,
				Nome = extratos.FirstOrDefault()?.Fornecedor ?? "",
				Quantidade = quantidade,
				Total = soma,
				Desde = firstDate,
				Orgaos = orgaos,
				Extratos = extratos
			});
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

	public class Fornecedores
	{
		public string Documento { get; set; }
		public string Nome { get; set; }
	}

	public class FornecedorData
	{
		public String Documento { get; set; }
		public string Nome { get; set; }
		public int Quantidade { get; set; }
		public decimal Total { get; set; }
		public DateTime Desde { get; set; }
		public  IEnumerable<Orgao> Orgaos { get; set; }
		public IEnumerable<VMExtrato> Extratos { get; set; }

		public class Orgao
		{
			public string Nome { get; set; }
			public int Quantidade { get; set; }
		}
	}
}