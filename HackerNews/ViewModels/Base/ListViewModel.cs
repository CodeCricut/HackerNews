using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.ViewModels.Base
{
	public class ListViewModel<TGetModel>
	{
		public IEnumerable<TGetModel> GetModels { get; set; }
	}
}
