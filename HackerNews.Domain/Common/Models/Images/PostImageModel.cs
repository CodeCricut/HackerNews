using AutoMapper;
using HackerNews.Domain.Common.Mappings;
using HackerNews.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Common.Models.Images
{
	public class PostImageModel : PostModelDto, IMapFrom<Image>
	{
		public string ImageTitle { get; set; }
		public string ImageDescription { get; set; }
		public byte[] ImageData { get; set; }

		public void Mapping(Profile profile)
		{
			profile.CreateMap<PostImageModel, Image>();
		}
	}
}
