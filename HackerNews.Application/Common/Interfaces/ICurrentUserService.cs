using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Application.Common.Interfaces
{
	public interface ICurrentUserService
	{
		int UserId { get; }
	}
}
