using System;
using Module1.EventArgs;

namespace Module1
{
	public interface IFileFind
	{
		ActionType OnFileFinded(
			string fileInfo,
			Func<string, bool> filter,
			EventHandler<FileFindedEventArgs> fileFinded,
			EventHandler<FileFindedEventArgs> filteredFileFinded,
			Action<EventHandler<FileFindedEventArgs>, FileFindedEventArgs> eventEmitter);
	}
}