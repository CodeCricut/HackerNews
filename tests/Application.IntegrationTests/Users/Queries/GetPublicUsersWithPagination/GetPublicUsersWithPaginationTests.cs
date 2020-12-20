using Application.IntegrationTests.Common;
using HackerNews.Application.Users.Commands.RegisterUser;
using HackerNews.Application.Users.Queries.GetPublicUsersWithPagination;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Queries.GetPublicUsersWithPagination
{
	public class GetPublicUsersWithPaginationTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetUsersWithPagination()
		{
			using var scope = Factory.Services.CreateScope();

			// Add users to query from.
			await AddUsersToQueryFrom();

			deletedUserValidatorMock.Setup(pv => pv.ValidateEntityQuerable(It.IsAny<IQueryable<User>>(), It.IsAny<DeletedEntityPolicy>())).Returns(users);

			var sut = new GetPublicUsersWithPaginationHandler(deletedUserValidatorMock.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var pagingParams = new PagingParams
			{
				PageSize = (int)Math.Ceiling((decimal)users.Count() / 2),
				PageNumber = 1
			};

			// Act
			PaginatedList<GetPublicUserModel> sutResult = await sut.Handle(
				new GetPublicUsersWithPaginationQuery(pagingParams), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(pagingParams.PageSize, sutResult.PageSize);
			Assert.Equal(pagingParams.PageSize, sutResult.Items.Count());
			if (pagingParams.PageSize < users.Count()) Assert.True(sutResult.HasNextPage);
		}

		private async Task AddUsersToQueryFrom()
		{
			var postUserModels = new List<RegisterUserModel>
			{
				new RegisterUserModel
				{
					FirstName = "username0",
					LastName = "username0",
					UserName = "username0",
					Password = "username0"
				},
				new RegisterUserModel
				{
					FirstName = "username1",
					LastName = "username1",
					UserName = "username1",
					Password = "username1"
				},
			};

			foreach (var registermodel in postUserModels)
			{
				await new RegisterUserHandler(userManager, unitOfWork, mediator, mapper, currentUserServiceMock.Object)
				.Handle(new RegisterUserCommand(registermodel), new CancellationToken(false));
			}
		}
	}
}
