using Application.IntegrationTests.Common;
using HackerNews.Application.Users.Queries.GetPublicUser;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Queries.GetPublicUser
{
	public class GetPublicUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetPublicUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var sut = new GetPublicUserHandler(deletedUserValidatorMock.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPublicUserModel sutResult = await sut.Handle(new GetPublicUserQuery(user.Id), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(user.Id, sutResult.Id);
		}
	}
}
