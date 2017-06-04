namespace hackinsampa.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FraseMatchUp")]
    public partial class FraseMatchUp
    {
        public int Id { get; set; }

        public int ExtratoId { get; set; }

        [Required]
        [StringLength(100)]
        public string Frase { get; set; }

        public virtual Extrato Extrato { get; set; }
    }
}
