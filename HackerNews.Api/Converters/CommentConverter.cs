using AutoMapper;
using HackerNews.Domain;
using HackerNews.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HackerNews.Api.Converters
{
	public class CommentConverter : EntityConverter<Comment, PostCommentModel, GetCommentModel>
	{
		public CommentConverter(IMapper mapper) : base(mapper)
		{
		}

		public override async Task<DestinationT> ConvertEntityAsync<DestinationT>(Comment entity)
		{
			return await Task.Factory.StartNew(() => _mapper.Map<DestinationT>(entity));
		}

		public override async Task<Comment> ConvertEntityModelAsync(PostCommentModel entityModel)
		{
			return await Task.Factory.StartNew(() => _mapper.Map<Comment>(entityModel));
		}
	}
}
