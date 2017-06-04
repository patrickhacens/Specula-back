
namespace hackinsampa.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;
	public class FornecedoresPorVariancia
	{
		[Key]
		public string DocumentoFornecedor { get; set; }

		public double? variancia { get; set; }
	}
}