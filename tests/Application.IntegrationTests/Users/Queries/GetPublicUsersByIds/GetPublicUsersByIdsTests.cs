using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Common.DeletedEntityValidators;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Users.Commands.RegisterUser;
using HackerNews.Application.Users.Queries.GetPublicUsersByIds;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
			using var scope = Factory.Services.CreateScope();

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			// Add users to query from.
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

			var allUsers = await unitOfWork.Users.GetEntitiesAsync();
			var oddUsers = allUsers.Where(a => a.Id % 2 == 1);


			var deletedPolicyValidator = new Mock<IDeletedEntityPolicyValidator<User>>();
			deletedPolicyValidator.Setup(pv => pv.ValidateEntityQuerable(It.IsAny<IQueryable<User>>(), It.IsAny<DeletedEntityPolicy>())).Returns(oddUsers);

			var sut = new GetPublicUsersByIdsHandler(deletedPolicyValidator.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			// Act
			PaginatedList<GetPublicUserModel> sutResult = await sut.Handle(
				new GetPublicUsersByIdsQuery(oddUsers.Select(a => a.Id),
				new PagingParams(1, 20)), new CancellationToken(false));


			// Assert
			Assert.NotNull(sutResult);

			// Ids should all be odd
			Assert.True(sutResult.Items.All(a => a.Id % 2 == 1));
		}
	}
}
