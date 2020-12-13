﻿using HackerNews.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace HackerNews.Domain.Entities
{
	public class Image : DomainEntity
	{
		public string ImageTitle { get; set; }
		public string ImageDescription { get; set; }
		public byte[] ImageData { get; set; }
	}
}