using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models.Articles;
using System.Linq;

namespace HackerNews.Api.Converters.Profiles
{
	public class ArticleProfile : Profile
	{
		public ArticleProfile()
		{
			CreateMap<Article, Article>();

			CreateMap<PostArticleModel, Article>();

			CreateMap<Article, GetArticleModel>()
				.ForMember(model => model.CommentIds, article => article.MapFrom(a => a.Comments.Select(a => a.Id)))
				
				.ForMember(model => model.UsersLiked, article => article.MapFrom(a => a.UsersLiked.Select(ul => ul.UserId)))
				.ForMember(model => model.UsersDisliked, article => article.MapFrom(a => a.UsersDisliked.Select(ud => ud.UserId)));
		}
	}
}
