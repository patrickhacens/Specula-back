using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hackinsampa.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			ViewBag.Title = "Home Page";
			//BuildMatchUp();
			return View();
		}

		public void BuildMatchUp()
		{
			IList<Models.Extrato> extratos;
			using (Models.Db db2 = new Models.Db())
			{

				extratos = db2.Extrato.Include("FraseMatchUp").Where(d => d.Objeto != null && !d.FraseMatchUp.Any()).ToList();
			}
			int count = 0;
			var db = new Models.Db();

			foreach (var extrato in extratos)
			{
				count++;
				var frases = extrato.Objeto.Split(' ').Select(d => d.ToLowerInvariant()).Distinct().Where(d => d != null && d.Length > 0).ToList();

				foreach (var frase in frases)
				{
					db.FraseMatchUp.Local.Add(new Models.FraseMatchUp()
					{
						ExtratoId = extrato.Id,
						Frase = frase
					});
				}
				if (count % 100 == 0)
				{
					try
					{
						db.SaveChanges();
					}
					catch (System.Data.Entity.Validation.DbEntityValidationException ex)
					{
						foreach (var item in ex.EntityValidationErrors)
						{
							item.Entry.State = System.Data.Entity.EntityState.Detached;
						}
						db.SaveChanges();
					}
					catch (Exception ex)
					{

					}
					finally
					{
						db.Dispose();
						db = new Models.Db();
					}
				}
			}
		}

	}
}