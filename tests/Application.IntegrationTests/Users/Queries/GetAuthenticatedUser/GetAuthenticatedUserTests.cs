using Application.IntegrationTests.Common;
using HackerNews.Application.Users.Queries.GetAuthenticatedUser;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Queries
{
	public class GetAuthenticatedUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetAuthenticatedUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var sut = new GetAuthenticatedUserHandler(userManager, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPrivateUserModel sutResult = await sut.Handle(new GetAuthenticatedUserQuery(), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(user.Id, sutResult.Id);
		}
	}
}
