using HackerNews.CLI.InclusionConfiguration;
using HackerNews.CLI.Options;
using HackerNews.CLI.Requests.Configuration;
using HackerNews.CLI.Verbs.GetBoards;
using HackerNews.CLI.Verbs.GetEntity;
using HackerNews.CLI.Verbs.GetPublicUsers;
using HackerNews.Domain.Common.Models;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.Domain.Common.Models.Users;

namespace HackerNews.CLI.Requests.EntityRequest
{
	public static class OptionExtensions
	{
		public static TOptions SetVerbosity<TOptions>(this TOptions options, bool verbose)
			where TOptions : IVerbosityOptions
		{
			options.Verbose = verbose;
			return options;
		}

		public static TOptions SetPrint<TOptions>(this TOptions options, bool print)
			where TOptions : IPrintOptions
		{
			options.Print = print;
			return options;
		}

		public static TOptions SetPostBoardOptions<TOptions>(this TOptions options, PostBoardModel postBoardModel)
			where TOptions : IPostBoardOptions
		{
			// TODO: should not be responsible for mapping types???
			options.Title = postBoardModel.Title;
			options.Description = postBoardModel.Description;
			return options;
		}

		public static TOptions SetPageOptions<TOptions>(this TOptions options, PagingParams pagingParams)
			where TOptions : IPageOptions
		{
			options.PageNumber = pagingParams.PageNumber;
			options.PageSize = pagingParams.PageSize;
			return options;
		}

		public static TOptions SetLoginOptions<TOptions>(this TOptions options, LoginModel loginModel)
			where TOptions : ILoginOptions
		{
			options.Username = loginModel.UserName;
			options.Password = loginModel.Password;
			return options;
		}

		public static TOptions SetIncludeAll<TOptions>(this TOptions options, bool includeAll)
			where TOptions : IAllInclusionOptions
		{
			options.IncludeAllFields = includeAll;
			return options;
		}

		public static TOptions SetId<TOptions>(this TOptions options, int id)
			where TOptions : IIdOptions
		{
			options.Id = id;
			return options;
		}

		public static TOptions SetFileLocation<TOptions>(this TOptions options, string fileLocation)
			where TOptions : IFileOptions
		{
			options.FileLocation = fileLocation;
			return options;
		}

		public static TOptions SetCommentInclusion<TOptions>(this TOptions options, CommentInclusionConfiguration inclusionConfig)
			where TOptions : ICommentInclusionOptions
		{
			// TODO: urgent, provide a mapper for ICommentInclusionOptions to CommentInclusionConfiguration
			return options;
		}

		public static TOptions SetBoardInclusion<TOptions>(this TOptions options, BoardInclusionConfiguration inclusionConfig)
			where TOptions : IBoardInclusionOptions
		{
			// TODO: urgent, provide a mapper for IBoardInclusionOptions to BoardInclusionConfiguration
			return options;
		}

		public static TOptions SetArticleInclusion<TOptions>(this TOptions options, ArticleInclusionConfiguration inclusionConfig)
			where TOptions : IArticleInclusionOptions
		{
			// TODO: urgent, provide a mapper for IArticleInclusionOptions to ArticleInclusionConfiguration
			return options;
		}

		public static TOptions SetPublicUserInclusion<TOptions>(this TOptions options, PublicUserInclusionConfiguration inclusionConfig)
			where TOptions : IPublicUserInclusionOptions
		{
			// TODO: urgent, provide a mapper for IPublicUserInclusionOptions to PublicUserInclusionConfiguration
			return options;
		}
	}
}
