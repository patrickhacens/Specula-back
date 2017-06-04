namespace hackinsampa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class FornecedoresComValor20porcentoAcimaDaMediaObjeto
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Orgão { get; set; }

        [StringLength(255)]
        public string Retranca { get; set; }

        [StringLength(255)]
        public string Modalidade { get; set; }

        [StringLength(255)]
        public string Licitacao { get; set; }

        [StringLength(255)]
        public string Processo { get; set; }

        [StringLength(255)]
        public string Evento { get; set; }

        public string Objeto { get; set; }

        [StringLength(255)]
        public string DataPublicacao { get; set; }

        [StringLength(255)]
        public string Fornecedor { get; set; }

        [StringLength(255)]
        public string TipoFornecedor { get; set; }

        [StringLength(255)]
        public string DocumentoFornecedor { get; set; }

        [StringLength(255)]
        public string DataAssinatura { get; set; }

        public double? Validade { get; set; }

        [StringLength(255)]
        public string TipoValidade { get; set; }

        [Column(TypeName = "money")]
        public decimal? Valor { get; set; }

        [StringLength(255)]
        public string NumeroContrato { get; set; }

        public double? ValorDiario { get; set; }

        public double? MédiaDeObjeto { get; set; }
    }
}
