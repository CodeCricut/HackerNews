using System;
using System.Runtime.Serialization;

namespace HackerNews.TUI.Services
{
	[Serializable]
	internal class ViewNotLoadedException : Exception
	{
		public ViewNotLoadedException()
		{
		}

		public ViewNotLoadedException(string message) : base(message)
		{
		}

		public ViewNotLoadedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		protected ViewNotLoadedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}