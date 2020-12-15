using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Common
{
	public enum DeletedEntityPolicy
	{
		INACCESSIBLE,
		OWNER,
		PUBLIC,
		RESTRICTED
	}
}
