using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace HackerNews.Api.IntegrationTests
{
	public class DeveloperTests : IClassFixture<IntegrationTest>
	{
		private readonly IntegrationTest _testFixture;

		public DeveloperTests(IntegrationTest testFixture)
		{
			_testFixture = testFixture;
		}

		[Fact]
		public async Task ThrowError_ReturnsApiException()
		{
			var response = await _testFixture.Client.GetAsync("/api/developer");

			// tests
			Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
		}
	}
}
