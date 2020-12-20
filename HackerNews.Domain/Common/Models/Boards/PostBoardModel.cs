using AutoMapper;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HackerNews.Domain.Common.Models.Boards
{
	public class PostBoardModel : PostModelDto, IMapFrom<Board>
	{
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<PostBoardModel, Board>();
		}
	}
}
