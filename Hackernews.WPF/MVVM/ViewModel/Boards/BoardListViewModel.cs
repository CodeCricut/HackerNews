using Hackernews.WPF.MVVM.ViewModel.Common;
using HackerNews.Domain.Common.Models.Boards;
using HackerNews.WPF.Core.Commands;

namespace Hackernews.WPF.MVVM.ViewModel
{
	public class BoardListViewModel : EntityListViewModel<BoardViewModel, GetBoardModel>
	{
		public BoardListViewModel(CreateBaseCommand<EntityListViewModel<BoardViewModel, GetBoardModel>> createLoadCommand) : base(createLoadCommand)
		{
		}
	}
}
