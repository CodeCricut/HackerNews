using AutoMapper;
using HackerNews.Application.Common.Mappings;
using HackerNews.Domain.Common;
using HackerNews.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace HackerNews.Application.Common.Models.Articles
{
	public class PostArticleModel : PostModelDto, IMapFrom<Article>
	{
		[Required]
		public string Type { get; set; }
		[Required]
		public string Text { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		[Range(1, int.MaxValue)]
		public int BoardId { get; set; }

		// needed for model binding
		public PostArticleModel()
		{

		}

		public void Mapping(Profile profile)
		{
			profile.CreateMap<PostArticleModel, Article>();
		}
	}
}
