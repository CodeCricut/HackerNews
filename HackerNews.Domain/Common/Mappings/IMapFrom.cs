using AutoMapper;

namespace HackerNews.Domain.Common.Mappings
{
	/// <summary>
	/// Provides a common interface for creating profiles between types. To create a mapping (to OR from) another class,
	/// you can use the default Mapping implementation or override it.
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface IMapFrom<T>
	{
		void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
	}
}
