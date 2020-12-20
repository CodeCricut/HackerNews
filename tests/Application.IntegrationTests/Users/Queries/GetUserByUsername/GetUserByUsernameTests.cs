using Application.IntegrationTests.Common;
using HackerNews.Application.Users.Queries.GetUserByUsername;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Queries.GetUserByUsername
{
	public class GetUserByUsernameTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldGetValidUser()
		{
			using var scope = Factory.Services.CreateScope();

			deletedUserValidatorMock.Setup(pv => pv.ValidateEntity(It.IsAny<User>(), It.IsAny<DeletedEntityPolicy>())).Returns(user);

			var sut = new GetUserByUsernameHandler(deletedUserValidatorMock.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPublicUserModel sutResult = await sut.Handle(
				new GetUserByUsernameQuery(user.UserName), new System.Threading.CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(user.UserName, sutResult.Username);
			Assert.Equal(user.Id, sutResult.Id);
		}
	}
}
