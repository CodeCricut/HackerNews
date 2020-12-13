using AutoMapper;
using HackerNews.Domain.Common;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerNews.Domain.Common.Models.Boards
{
	public class GetBoardModel : GetModelDto, IMapFrom<Board>
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreateDate { get; set; }
		public int CreatorId { get; set; }
		public List<int> ModeratorIds { get; set; } = new List<int>(); 
		public List<int> SubscriberIds { get; set; } = new List<int>();
		public List<int> ArticleIds { get; set; } = new List<int>();
		public bool Deleted { get; set; }

		public int BoardImageId { get; set; }


		public void Mapping(Profile profile)
		{
			profile.CreateMap<Board, GetBoardModel>()
				.ForMember(model => model.ArticleIds, board => board.MapFrom(b => b.Articles.Select(a => a.Id)))
				.ForMember(model => model.CreatorId, board => board.MapFrom(b => b.Creator.Id))
				.ForMember(model => model.ModeratorIds, board => board.MapFrom(b => b.Moderators.Select(m => m.UserId)))
				.ForMember(model => model.SubscriberIds, board => board.MapFrom(b => b.Subscribers.Select(s => s.UserId)))
				//.ForMember(model => model.BoardImageId, board => board.MapFrom(a => a.BoardImage.Id));
			;
		}
	}
}
