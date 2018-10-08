using System;
using NUnit.Framework;

namespace Module1.Tests
{
	[TestFixture]
	public class FileFindTests
	{
		#region Test context

		private IFileFind _fileFind;
		private string _fileInfo = @"C:\.NET Mentoring\doc1.doc";

		[SetUp]
		public void TestInit()
		{
			_fileFind = new FileFind();
		}

		#endregion

		private void OnEvent<TArgs>(EventHandler<TArgs> someEvent, TArgs args)
		{
			someEvent?.Invoke(this, args);
		}

		[Test]
		public void Test_It_FileFindedCall()
		{
			#region ARRANGE

			int delegatesCallCount = 0;

			#endregion

			#region ACT

			_fileFind.OnFileFinded(_fileInfo, null, (s, e) => delegatesCallCount++, null, OnEvent);

			#endregion

			#region ASSERT

			Assert.AreEqual(1, delegatesCallCount);

			#endregion
		}


		[Test]
		public void Test_It_FilteredFileFindedCall()
		{
			#region ARRANGE

			int delegatesCallCount = 0;

			#endregion

			#region ACT

			_fileFind.OnFileFinded(
				_fileInfo, info => true, (s, e) => delegatesCallCount++, (s, e) => delegatesCallCount++, OnEvent);

			#endregion

			#region ASSERT

			Assert.AreEqual(2, delegatesCallCount);
			
			#endregion
		}

		[Test]
		public void Test_It_FileNotPassFilter()
		{
			#region ARRANGE

			int delegatesCallCount = 0;

			#endregion

			#region ACT

			_fileFind.OnFileFinded(
				_fileInfo, info => false, (s, e) => delegatesCallCount++, (s, e) => delegatesCallCount++, OnEvent);

			#endregion

			#region ASSERT

			Assert.AreEqual(1, delegatesCallCount);

			#endregion
		}

		[Test]
		public void Test_It_FileFinded_ContinueAction()
		{
			#region ARRANGE


			#endregion

			#region ACT

			var action = _fileFind.OnFileFinded(_fileInfo, null, (s, e) => { }, null, OnEvent);

			#endregion

			#region ASSERT

			Assert.AreEqual(ActionType.Continue, action);

			#endregion
		}

		[Test]
		public void Test_It_FilteredFileFinded_ContinueAction()
		{
			#region ARRANGE


			#endregion

			#region ACT

			var action = _fileFind.OnFileFinded(
				_fileInfo, info => true, (s, e) => { }, (s, e) => { }, OnEvent);

			#endregion

			#region ASSERT

			Assert.AreEqual(ActionType.Continue, action);

			#endregion
		}

		[Test]
		public void Test_It_FindedFileExcluded_ExcludeAction()
		{
			#region ARRANGE

			int delegatesCallCount = 0;

			#endregion

			#region ACT

			var action = _fileFind.OnFileFinded(
				_fileInfo, info => true, (s, e) =>
				{
					delegatesCallCount++;
					e.ActionType = ActionType.Exclude;
				}, (s, e) => delegatesCallCount++, OnEvent);

			#endregion

			#region ASSERT

			Assert.AreEqual(ActionType.Exclude, action);
			Assert.AreEqual(1, delegatesCallCount);

			#endregion
		}

		[Test]
		public void Test_It_FilteredFindedFileExcluded_ExcludeAction()
		{
			#region ARRANGE

			int delegatesCallCount = 0;

			#endregion

			#region ACT

			var action = _fileFind.OnFileFinded(
				_fileInfo, info => true,
				(s, e) => delegatesCallCount++,
				(s, e) =>
				{
					delegatesCallCount++;
					e.ActionType = ActionType.Exclude;
				}, OnEvent);

			#endregion

			#region ASSERT

			Assert.AreEqual(ActionType.Exclude, action);
			Assert.AreEqual(2, delegatesCallCount);

			#endregion
		}

		[Test]
		public void Test_It_FindedFileStopped_StopAction()
		{
			#region ARRANGE

			int delegatesCallCount = 0;

			#endregion

			#region ACT

			var action = _fileFind.OnFileFinded(
				_fileInfo, info => true, (s, e) =>
				{
					delegatesCallCount++;
					e.ActionType = ActionType.Stop;
				}, (s, e) => delegatesCallCount++, OnEvent);

			#endregion

			#region ASSERT

			Assert.AreEqual(ActionType.Stop, action);
			Assert.AreEqual(1, delegatesCallCount);

			#endregion
		}

		[Test]
		public void Test_It_FilteredFindedFileStopped_StopAction()
		{
			#region ARRANGE

			int delegatesCallCount = 0;

			#endregion

			#region ACT

			var action = _fileFind.OnFileFinded(
				_fileInfo, info => true,
				(s, e) => delegatesCallCount++,
				(s, e) =>
				{
					delegatesCallCount++;
					e.ActionType = ActionType.Stop;
				}, OnEvent);

			#endregion

			#region ASSERT

			Assert.AreEqual(ActionType.Stop, action);
			Assert.AreEqual(2, delegatesCallCount);

			#endregion
		}
	}
}
