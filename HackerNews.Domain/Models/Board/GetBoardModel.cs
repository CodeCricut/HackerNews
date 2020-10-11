using CleanEntityArchitecture.Domain;
using System;
using System.Collections.Generic;

namespace HackerNews.Domain.Models.Board
{
	public class GetBoardModel : GetModelDto
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime CreateDate { get; set; }
		public int CreatorId { get; set; }
		public List<int> ModeratorIds { get; set; }
		public List<int> SubscriberIds { get; set; }
		public List<int> ArticleIds { get; set; }
	}
}
