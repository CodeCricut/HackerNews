using HackerNews.CLI.Verbs.GetEntity;

namespace HackerNews.CLI.Options.Verbs
{
	public interface IGetEntityByIdOptions :
		IVerbosityOptions,
		IPrintOptions,
		IFileOptions,
		IIdOptions
	{
	}
}
