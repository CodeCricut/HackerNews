using Application.IntegrationTests.Common;
using AutoMapper;
using HackerNews.Application.Common.DeletedEntityValidators;
using HackerNews.Application.Common.Interfaces;
using HackerNews.Application.Users.Commands.RegisterUser;
using HackerNews.Application.Users.Queries.GetPublicUsersWithPagination;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Users;
using HackerNews.Domain.Entities;
using HackerNews.Domain.Interfaces;
using MediatR;
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

			var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
			var user = (await unitOfWork.Users.GetEntitiesAsync()).First();
			var board = (await unitOfWork.Boards.GetEntitiesAsync()).First();
			var article = (await unitOfWork.Articles.GetEntitiesAsync()).First();
			var comment = (await unitOfWork.Comments.GetEntitiesAsync()).First();

			var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
			var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

			var currentUserServiceMock = new Mock<ICurrentUserService>();
			currentUserServiceMock.Setup(mock => mock.UserId).Returns(user.Id);

			// Add users to query from.
			var postUserModels = new List<RegisterUserModel>
			{
				new RegisterUserModel
				{
					FirstName = "first name 0",
					LastName = "last name 0",
					Username = "username 0",
					Password = "password 0"
				},
				new RegisterUserModel
				{
					FirstName = "first name 1",
					LastName = "last name 1",
					Username = "username 1",
					Password = "password 1"
				},
			};
			foreach (var registermodel in postUserModels)
			{
				await new RegisterUserHandler(unitOfWork, mediator, mapper, currentUserServiceMock.Object)
				.Handle(new RegisterUserCommand(registermodel), new CancellationToken(false));
			}

			var deletedPolicyValidator = new Mock<DeletedUserPolicyValidator>();
			deletedPolicyValidator.Setup(pv => pv.ValidateEntity(It.IsAny<User>(), It.IsAny<DeletedEntityPolicy>()));

			var sut = new GetPublicUsersWithPaginationHandler(deletedPolicyValidator.Object, unitOfWork, mediator, mapper, currentUserServiceMock.Object);

			var allUsers = await unitOfWork.Users.GetEntitiesAsync();
			var pagingParams = new PagingParams
			{
				PageSize = (int)Math.Ceiling((decimal)allUsers.Count() / 2),
				PageNumber = 1
			};

			// Act
			PaginatedList<GetPublicUserModel> sutResult = await sut.Handle(
				new GetPublicUsersWithPaginationQuery(pagingParams), new CancellationToken(false));

			// Assert
			Assert.NotNull(sutResult);

			Assert.Equal(pagingParams.PageSize, sutResult.PageSize);
			Assert.Equal(pagingParams.PageSize, sutResult.Items.Count());
			if (pagingParams.PageSize < allUsers.Count()) Assert.True(sutResult.HasNextPage);
		}
	}
}
