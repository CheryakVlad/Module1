using System;
using System.Collections.Generic;
using System.IO;
using Module1.EventArgs;

namespace Module1
{
    public class FileSystemVisitor
    {
        #region Private Fields

        private readonly DirectoryInfo _root;
		private readonly Func<string, bool> _filter;
		private readonly IFileFind _fileFind;

		#endregion

		#region Constructors

		public FileSystemVisitor(DirectoryInfo root, Func<string, bool> filter, 
			IFileFind fileFind)
		{
			_root = root;
			_filter = filter;
			_fileFind = fileFind;
		}

		#endregion

		#region Events

		public event EventHandler<StartEventArgs> Start;
		public event EventHandler<FinishEventArgs> Finish;
		public event EventHandler<FileFindedEventArgs> FileFinded;
		public event EventHandler<FileFindedEventArgs> FilteredFileFinded;
		public event EventHandler<DirectoryFindedEventArgs> DirectoryFinded;

		#endregion

		#region Public methods

		public IEnumerable<string> GetFiles()
		{
			OnEvent(Start, new StartEventArgs());

			foreach (string fileInfo in WalkDirectoryTree(_root, ActionType.Continue))
			{
				yield return fileInfo;
			}

			OnEvent(Finish, new FinishEventArgs());
		}
		
		#endregion

		#region Private methods

		private IEnumerable<string> WalkDirectoryTree(DirectoryInfo root, ActionType currentAction)
		{
			DirectoryInfo[] subDirs = null;
			FileInfo[] files = root.GetFiles("*.*");

			if (files != null)
			{
				foreach (FileInfo fileInfo in files)
				{
					currentAction = _fileFind.OnFileFinded(fileInfo.FullName, _filter, FileFinded, FilteredFileFinded, OnEvent);
					
					if (currentAction == ActionType.Continue)
					{
						yield return fileInfo.FullName;
					}

					if (currentAction == ActionType.Stop)
					{
						yield break;
					}
				}

				subDirs = root.GetDirectories();

				foreach (DirectoryInfo dirInfo in subDirs)
				{
					DirectoryFindedEventArgs args = new DirectoryFindedEventArgs { DirInfo = dirInfo.FullName };
					DirectoryFinded(this, args);
					currentAction = GetActionType(currentAction, args.ActionType);

					if (currentAction != ActionType.Stop)
					{
						foreach (string fileInfo in WalkDirectoryTree(dirInfo, currentAction))
						{
							yield return fileInfo;
						}
					}
					else
					{
						yield break;
					}
				}
			}
		}

		private ActionType GetActionType(ActionType internalType, ActionType externalType)
		{
			return internalType == ActionType.Stop ? internalType : externalType;			
		}

		private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
		{
			someEvent?.Invoke(this, args);
		}

		#endregion
	}
}
