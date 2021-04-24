using System;
using System.Collections.Generic;
using System.Text;

namespace Hackernews.WPF.Factories
{
	public interface ILoadEntityCommandFactoryPrincipal
	{
		ILoadBoardCommandFactory BoardCF { get; }
		ILoadArticleCommandFactory ArticleCF { get; }
		ILoadCommentCommandFactory CommentCF { get; }
		ILoadUserCommandFactory UserCF { get; }
	}

	public class LoadEntityCommandFactoryPrincipal : ILoadEntityCommandFactoryPrincipal
	{
		public ILoadBoardCommandFactory BoardCF { get; }

		public ILoadArticleCommandFactory ArticleCF { get; }

		public ILoadCommentCommandFactory CommentCF { get; }

		public ILoadUserCommandFactory UserCF { get; }

		public LoadEntityCommandFactoryPrincipal(
			ILoadBoardCommandFactory boardCF,
			ILoadArticleCommandFactory articleCF,
			ILoadCommentCommandFactory commentCF,
			ILoadUserCommandFactory userCF
			)
		{
			BoardCF = boardCF;
			ArticleCF = articleCF;
			CommentCF = commentCF;
			UserCF = userCF;
		}
	}
}
