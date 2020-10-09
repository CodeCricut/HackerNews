using AutoMapper;
using HackerNews.Domain.Models.Users;
using HackerNews.EF.Repositories;

namespace HackerNews.Api.Helpers.EntityServices.Base.UserServices
{
	public class ReadPublicUserService : ReadEntityService<Domain.User, GetPublicUserModel>
	{
		public ReadPublicUserService(IMapper mapper, IEntityRepository<Domain.User> entityRepository) : base(mapper, entityRepository)
		{
		}
	}
}
