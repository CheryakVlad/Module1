namespace Module1.EventArgs
{
	public class DirectoryFindedEventArgs : System.EventArgs
	{
		public string DirInfo
		{
			get;
			set;
		}

		public ActionType ActionType
		{
			get;
			set;
		}
	}
}
