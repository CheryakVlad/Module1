using System;
using Module1.EventArgs;

namespace Module1
{
	public class FileFind : IFileFind
	{
		public ActionType OnFileFinded(
			string fileInfo,
			Func<string, bool> filter,
			EventHandler<FileFindedEventArgs> fileFinded,
			EventHandler<FileFindedEventArgs> filteredFileFinded,
			Action<EventHandler<FileFindedEventArgs>, FileFindedEventArgs> eventEmitter)
		{
			FileFindedEventArgs args = new FileFindedEventArgs
			{
				ActionType = ActionType.Continue,
				FileInfo = fileInfo
			};

			eventEmitter(fileFinded, args);

			if (args.ActionType != ActionType.Continue || filter == null)
			{
				return args.ActionType;
			}

			if (filter(fileInfo))
			{
				args = new FileFindedEventArgs
				{
					ActionType = ActionType.Continue,
					FileInfo = fileInfo
				};

				eventEmitter(filteredFileFinded, args);

				return args.ActionType;
			}

			return ActionType.Exclude;
		}
	}
}
