using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.MediatR.Commands.LogEntity;
using HackerNews.CLI.MediatR.Commands.PostEntity;
using HackerNews.CLI.MediatR.Commands.SetVerbosity;
using HackerNews.CLI.MediatR.Commands.SignIn;
using HackerNews.CLI.MediatR.Commands.WriteEntity;
using HackerNews.CLI.Options;
using HackerNews.CLI.Util;
using HackerNews.Domain.Common.Models.Boards;

namespace HackerNews.CLI.ApplicationRequests
{
	public class PostBoardRequest : IPostEntityRequest<PostBoardModel, GetBoardModel>
	{
		public CreateVerbosityCommand CreateVerbosityCommand { get; }
		public CreateSignInCommand CreateSignInCommand { get; }
		public CreatePostCommand<PostBoardModel, GetBoardModel> CreatePostCommand { get; }
		public CreateLogEntityCommand<GetBoardModel> CreateLogCommand { get; }
		
		public CreateWriteEntityCommand<GetBoardModel> CreateWriteCommand { get; }

		public PostBoardRequest(PostBoardOptions opts)
		{
			CreateVerbosityCommand = () => new SetVerbosityCommand(opts);
			CreateSignInCommand = () => new SignInCommand(opts);
			CreatePostCommand = () => new PostBoardCommand(opts.ToPostModel());
			CreateLogCommand = postedBoard => new LogBoardCommand(postedBoard, opts);
			CreateWriteCommand = postedBoard => new WriteBoardCommand(postedBoard, opts);
		}
	}
}
