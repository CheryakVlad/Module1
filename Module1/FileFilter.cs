using System;

namespace Module1
{
	public class FileFilter
	{
		private readonly string _fragment;

		public FileFilter(string fragment)
		{
			_fragment = fragment;
		}

		public bool Filter(string filename)
		{
			return filename.Contains(_fragment);
		}
	}
}
