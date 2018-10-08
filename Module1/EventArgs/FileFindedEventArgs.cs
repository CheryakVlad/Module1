namespace Module1.EventArgs
{
	public class FileFindedEventArgs : System.EventArgs
	{
		public string FileInfo
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
