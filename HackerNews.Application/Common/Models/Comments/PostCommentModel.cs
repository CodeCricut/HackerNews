using AutoMapper;
using HackerNews.Application.Common.Mappings;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HackerNews.Application.Common.Models.Comments
{
	public class PostCommentModel : PostModelDto, IMapFrom<Comment>
	{
		[Required]
		public string Text { get; set; }
		[Range(0, int.MaxValue)]
		public int ParentCommentId { get; set; }
		[Range(0, int.MaxValue)]
		public int ParentArticleId { get; set; }

		public PostCommentModel()
		{

		}

		public void Mapping(Profile profile)
		{
			profile.CreateMap<PostCommentModel, Comment>()
				.ForMember(c => c.ParentArticleId, opt => opt
					.MapFrom(model => model.ParentArticleId != 0 ? model.ParentArticleId : (int?)null))
				.ForMember(c => c.ParentCommentId, opt => opt
					.MapFrom(model => model.ParentCommentId != 0 ? model.ParentCommentId : (int?)null));
		}
	}
}
