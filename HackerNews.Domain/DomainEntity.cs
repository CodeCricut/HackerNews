using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace HackerNews.Domain
{
	public abstract class DomainEntity
	{
		[Key]
		public int Id { get; set; }
		public bool Deleted { get; set; }
	}
}
