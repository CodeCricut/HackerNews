using AutoMapper;

namespace HackerNews.Application.Common.Mappings
{
	public interface IMapFrom<T>
	{
		void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
	}
}
