using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models.Users;
using System.Linq;

namespace HackerNews.Api.Converters.Profiles
{
	public class UserProfile : Profile
	{
		public UserProfile()
		{
			CreateMap<User, User>();

			CreateMap<RegisterUserModel, User>();
			CreateMap<User, GetPublicUserModel>()
				.ForMember(model => model.ArticleIds, user => user.MapFrom(u => u.Articles.Select(a => a.Id)))
				.ForMember(model => model.CommentIds, user => user.MapFrom(u => u.Comments.Select(c => c.Id)));

			CreateMap<User, GetPrivateUserModel>()
				.ForMember(model => model.ArticleIds, user => user.MapFrom(u => u.Articles.Select(a => a.Id)))
				.ForMember(model => model.CommentIds, user => user.MapFrom(u => u.Comments.Select(c => c.Id)))

				.ForMember(model => model.SavedArticles, user => user.MapFrom(u => u.SavedArticles.Select(sa => sa.ArticleId)))
				.ForMember(model => model.SavedComments, user => user.MapFrom(u => u.SavedComments.Select(sc => sc.CommentId)))

				.ForMember(model => model.LikedArticles, user => user.MapFrom(u => u.LikedArticles.Select(la => la.ArticleId)))
				.ForMember(model => model.LikedComments, user => user.MapFrom(u => u.LikedComments.Select(lc => lc.CommentId)))

				.ForMember(model => model.BoardsModerating, user => user.MapFrom(u => u.BoardsModerating.Select(bm => bm.BoardId)))
				.ForMember(model => model.BoardsSubscribed, user => user.MapFrom(u => u.BoardsSubscribed.Select(bs => bs.BoardId)));
		}
	}
}
