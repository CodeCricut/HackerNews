using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using HackerNews.Infrastructure.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Infrastructure.Repository.Images
{
	class ImageRepository : EntityRepository<Image>, IImageRepository
	{
		public ImageRepository(DbContext context) : base(context)
		{
		}

		public override Task<IQueryable<Image>> GetEntitiesAsync()
		{
			return Task.FromResult(
				_context.Set<Image>()
					.AsQueryable());
		}

		public override Task<Image> GetEntityAsync(int id)
		{
			return Task.FromResult(
				_context.Set<Image>()
					.FirstOrDefault(img => img.Id == id));
		}
	}
}
