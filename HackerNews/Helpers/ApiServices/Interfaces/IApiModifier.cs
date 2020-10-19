﻿using CleanEntityArchitecture.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HackerNews.Helpers.ApiServices.Interfaces
{
	public interface IApiModifier<TEntity, TPostModel, TGetModel>
		where TEntity : DomainEntity
		where TPostModel : PostModelDto
		where TGetModel : GetModelDto
	{
		Task<TGetModel> PostEndpointAsync(string endpoint, TPostModel postModel);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="endpoint"></param>
		/// <param name="postModels"></param>
		/// <returns>Successful.</returns>
		Task<bool> PostEndpointAsync(string endpoint, IEnumerable<TPostModel> postModels);
		Task<TGetModel> PutEndpointAsync(string endpoint, TPostModel updateModel);
		/// <summary>
		/// 
		/// </summary>
		/// <param name="endpoint"></param>
		/// <param name="id"></param>
		/// <returns>Successful.</returns>
		Task<bool> DeleteEndpointAsync(string endpoint, int id);
	}
}