namespace hackinsampa.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class Db : DbContext
	{
		public Db()
			: base("name=Db")
		{
		}

		public virtual DbSet<Extrato> Extrato { get; set; }
		public virtual DbSet<FraseMatchUp> FraseMatchUp { get; set; }
		public virtual DbSet<IG> IG { get; set; }
		public virtual DbSet<FornecedoresComValor20porcentoAcimaDaMediaObjeto> FornecedoresComValor20porcentoAcimaDaMediaObjeto { get; set; }
		public virtual DbSet<FornecedoresPorVariancia> FornecedoresPorVariancia { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Extrato>()
				.Property(e => e.Valor)
				.HasPrecision(19, 4);

			modelBuilder.Entity<Extrato>()
				.HasMany(e => e.FraseMatchUp)
				.WithRequired(e => e.Extrato)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<FraseMatchUp>()
				.Property(e => e.Frase)
				.IsUnicode(false);

			modelBuilder.Entity<FornecedoresComValor20porcentoAcimaDaMediaObjeto>()
				.Property(e => e.Valor)
				.HasPrecision(19, 4);

			modelBuilder.Entity<FornecedoresPorVariancia>()
				.ToTable("FornecedoresPorVariancia");
		}
	}
}
