using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HackerNews.Application.Images.Queries.GetImageById;
using HackerNews.Domain.Common.Models.Images;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HackerNews.Api.Controllers
{
	[Route("api/[controller]")]
	public class ImagesController : ApiController
	{
		[HttpGet("{key:int}")]
		public async Task<ActionResult<GetImageModel>> GetByIdAsync(int key)
		{
			return await Mediator.Send(new GetImageByIdQuery(key));
		}
	}
}
