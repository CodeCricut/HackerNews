using System.Collections.Generic;

namespace HackerNews.ViewModels.Base
{
	public class ListViewModel<TGetModel>
	{
		public IEnumerable<TGetModel> GetModels { get; set; }
	}
}
