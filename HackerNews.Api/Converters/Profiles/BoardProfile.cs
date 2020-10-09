using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models.Board;
using System.Linq;

namespace HackerNews.Api.Converters.Profiles
{
	public class BoardProfile : Profile
	{
		public BoardProfile()
		{
			CreateMap<Board, Board>();
			CreateMap<PostBoardModel, Board>();
			CreateMap<Board, GetBoardModel>()
				.ForMember(model => model.ArticleIds, board => board.MapFrom(b => b.Articles.Select(a => a.Id)))
				.ForMember(model => model.CreatorId, board => board.MapFrom(b => b.Creator.Id))
				.ForMember(model => model.ModeratorIds, board => board.MapFrom(b => b.Moderators.Select(m => m.UserId)))
				.ForMember(model => model.SubscriberIds, board => board.MapFrom(b => b.Subscribers.Select(s => s.UserId)));
		}
	}
}
