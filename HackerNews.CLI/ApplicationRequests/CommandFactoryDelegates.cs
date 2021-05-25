using HackerNews.CLI.MediatR.Commands.LogEntities;
using HackerNews.CLI.MediatR.Commands.PostEntity;
using HackerNews.CLI.MediatR.Commands.PrintEntity;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Commands.SignIn;
using HackerNews.CLI.MediatR.Commands.WriteBoardPage;
using HackerNews.CLI.MediatR.Commands.WriteEntity;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Models;

namespace HackerNews.CLI.ApplicationRequests
{
	public delegate SetVerbosityCommand CreateVerbosityCommand();

	public delegate SignInCommand CreateSignInCommand();

	public delegate PostEntityCommand<TPostModel, TGetModel> CreatePostCommand<TPostModel, TGetModel>()
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto;


	public delegate LogEntityCommand<TGetModel> CreateLogEntityCommand<TGetModel>(TGetModel model)
		where TGetModel : GetModelDto;

	public delegate LogEntityPageCommand<TGetModel> CreateLogEntityPageCommand<TGetModel>(PaginatedList<TGetModel> models)
		where TGetModel : GetModelDto;

	public delegate WriteEntityCommand<TGetModel> CreateWriteEntityCommand<TGetModel>(TGetModel model)
		where TGetModel : GetModelDto;

	public delegate WriteEntityPageCommand<TGetModel> CreateWriteEntityPageCommand<TGetModel>(PaginatedList<TGetModel> models)
		where TGetModel : GetModelDto;
}
