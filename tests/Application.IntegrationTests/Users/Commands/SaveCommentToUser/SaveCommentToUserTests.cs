using Application.IntegrationTests.Common;
using HackerNews.Application.Users.Commands.SaveCommentToUser;
using HackerNews.Domain.Common.Models.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Users.Commands.SaveCommentToUser
{
	public class SaveCommentToUserTests : AppIntegrationTest
	{
		[Fact]
		public async Task ShouldSaveCommentToUser()
		{
			// Arrange
			using var scope = Factory.Services.CreateScope();

			var sut = new SaveCommentToUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			GetPrivateUserModel sutResult = await sut.Handle(new SaveCommentToUserCommand(comment.Id), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			var updatedUser = await unitOfWork.Users.GetEntityAsync(user.Id);
			Assert.Contains(comment.Id, updatedUser.SavedComments.Select(sc => sc.CommentId));
		}
	}
}
