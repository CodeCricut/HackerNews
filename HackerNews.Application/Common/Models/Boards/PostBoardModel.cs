using AutoMapper;
using HackerNews.Application.Common.Mappings;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;

namespace HackerNews.Application.Common.Models.Boards
{
	public class PostBoardModel : PostModelDto, IMapFrom<Board>
	{
		public string Title { get; set; }
		public string Description { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<PostBoardModel, Board>();
		}
	}
}
