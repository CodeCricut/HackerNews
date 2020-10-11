using AutoMapper;
using CleanEntityArchitecture.EntityModelServices;
using CleanEntityArchitecture.Repository;
using HackerNews.Domain;
using HackerNews.Domain.Models.Users;

namespace HackerNews.Api.Helpers.EntityServices.Base.UserServices
{
	public class ReadPublicUserService : ReadEntityService<User, GetPublicUserModel>
	{
		public ReadPublicUserService(IMapper mapper, IReadEntityRepository<User> readRepository) : base(mapper, readRepository)
		{
		}
	}
}
