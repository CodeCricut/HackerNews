using HackerNews.Application.Common.Models.Users;
using HackerNews.Application.Common.Requests;
using HackerNews.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Threading;
using System.Threading.Tasks;

namespace HackerNews.Application.Users.Queries.GetPublicUserQuery
{
	public class GetPublicUserQuery : IRequest<GetPublicUserModel>
	{
		public GetPublicUserQuery(int id)
		{
			Id = id;
		}

		public int Id { get; }
	}

	public class GetPublicUserHandler : DatabaseRequestHandler<GetPublicUserQuery, GetPublicUserModel>
	{
		public GetPublicUserHandler(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
		{
		}

		public override async Task<GetPublicUserModel> Handle(GetPublicUserQuery request, CancellationToken cancellationToken)
		{
			using (UnitOfWork)
			{
				var user = await UnitOfWork.Users.GetEntityAsync(request.Id);
				if (user == null) throw new NotFoundException();

				return Mapper.Map<GetPublicUserModel>(user);
			}
		}
	}
}
