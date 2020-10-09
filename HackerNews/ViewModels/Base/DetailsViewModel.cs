using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Base
{
	public class DetailsViewModel<TGetModel>
	{
		public TGetModel GetModel { get; set; }
	}
}
