using HackerNews.Application.Images.Queries.GetImageById;
using HackerNews.Domain.Common.Models.Images;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

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
