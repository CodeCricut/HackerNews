using Application.IntegrationTests.Common;
using HackerNews.Application.Users.Commands.RegisterUser;
using HackerNews.Application.Users.Queries.GetPublicUsersByIds;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Queries.GetPublicUsersByIds
{
	public class GetPublicUsersByIdsTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetUsersByIds()
		{
			// Setup
			using var scope = Factory.Services.CreateScope();

			// Add users to query from.
			await AddUsersToQueryFrom();

			var oddUsers = users.Where(a => a.Id % 2 == 1);
			deletedUserValidatorMock.Setup(pv => pv.ValidateEntityQuerable(It.IsAny<IQueryable<User>>(), It.IsAny<DeletedEntityPolicy>())).Returns(oddUsers);

			var sut = new GetPublicUsersByIdsHandler(deletedUserValidatorMock.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			PaginatedList<GetPublicUserModel> sutResult = await sut.Handle(
				new GetPublicUsersByIdsQuery(oddUsers.Select(a => a.Id),
				new PagingParams(1, 20)), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			// Ids should all be odd
			Assert.True(sutResult.Items.All(a => a.Id % 2 == 1));
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
