using System;
using System.IO;

namespace Module1
{
	class Program
	{
		static void Main(string[] args)
		{
			DirectoryInfo rootDir = new DirectoryInfo(@"C:\.NET Mentoring");

			FileSystemVisitor visitor = new FileSystemVisitor(rootDir, new FileFilter("doc").Filter,
				new FileFind());

			int counter = 0;

			visitor.Start += (s, e) =>
			{
				Console.WriteLine("Start");
			};

			visitor.Finish += (s, e) =>
			{
				Console.WriteLine("Finish");
			};

			visitor.FileFinded += (s, e) =>
			{	 
				if (counter > 10)
				{
					e.ActionType = ActionType.Stop;
				}
				else
				{
					Console.WriteLine($"File Finded: {e.FileInfo}");
				}
				counter++;
			};

			visitor.FilteredFileFinded += (s, e) =>
			{	
				if (counter > 3)
				{
					e.ActionType = ActionType.Stop;
				}
				else
				{
					Console.WriteLine($"Filtered File Finded: {e.FileInfo}");
				}
				counter++;
			};

			visitor.DirectoryFinded += (s, e) =>
			{
				Console.WriteLine($"Directory finded: {e.DirInfo}");
			};

			foreach (string file in visitor.GetFiles())
			{
				
			}

			Console.WriteLine("Press any key");
			Console.ReadKey();
		}
	}
}
