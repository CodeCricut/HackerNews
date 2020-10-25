using HackerNews.Domain.Entities;
using System.Threading.Tasks;

namespace HackerNews.Domain.Interfaces
{
	public interface IArticleRepository : IEntityRepository<Article>
	{
	}
}
