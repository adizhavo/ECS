using System;
using NUnit.Framework;
using ECS;

namespace ECSTests
{
	[TestFixture()]
	public class FilterTests
	{
		private Filter testFilter;

		[SetUp]
		public void SetUp()
		{
			testFilter = new Filter();
		}

		[Test()]
		public void ShouldIgnoreNullInAnyType()
		{
			testFilter.AnyOf(null);
			Assert.AreEqual(0, testFilter.AnyType.Count);
		}

		[Test()]
		public void ShouldAddSingleTypeInAnyType()
		{
			testFilter.AnyOf(typeof(FirstTestComponent));
			Assert.AreEqual(1, testFilter.AnyType.Count);
		}

		[Test()]
		public void ShouldAddMultipleTypeInAnyType()
		{
			testFilter.AnyOf(typeof(FirstTestComponent), typeof(SecondTestComponent));
			Assert.AreEqual(2, testFilter.AnyType.Count);
		}

		[Test()]
		public void ShouldIgnoreNullInAllType()
		{
			testFilter.AllOf(null);
			Assert.AreEqual(0, testFilter.AllType.Count);
		}

		[Test()]
		public void ShouldAddSingleTypeInAllType()
		{
			testFilter.AllOf(typeof(FirstTestComponent));
			Assert.AreEqual(1, testFilter.AllType.Count);
		}
		
		[Test()]
		public void ShouldAddMultipleTypeInAllType()
		{
			testFilter.AllOf(typeof(FirstTestComponent), typeof(SecondTestComponent));
			Assert.AreEqual(2, testFilter.AllType.Count);
		}

		[Test()]
		public void ShouldIgnoreNullInNoneType()
		{
			testFilter.NoneOf(null);
			Assert.AreEqual(0, testFilter.NoneType.Count);
		}

		[Test()]
		public void ShouldAddSingleTypeInNoneType()
		{
			testFilter.NoneOf(typeof(FirstTestComponent));
			Assert.AreEqual(1, testFilter.NoneType.Count);
		}
		
		[Test()]
		public void ShouldAddMultipleTypeInNoneType()
		{
			testFilter.NoneOf(typeof(FirstTestComponent), typeof(SecondTestComponent));
			Assert.AreEqual(2, testFilter.NoneType.Count);
		}

		[Test()]
		public void ShouldChainDifferentTypes()
		{
			testFilter.AnyOf(typeof(FirstTestComponent)).AllOf(typeof(FirstTestComponent)).NoneOf(typeof(FirstTestComponent));
			Assert.AreEqual(1, testFilter.AnyType.Count);
			Assert.AreEqual(1, testFilter.AllType.Count);
			Assert.AreEqual(1, testFilter.NoneType.Count);
		}

		[Test()]
		public void ShouldReset()
		{
			testFilter.AnyOf(typeof(FirstTestComponent)).AllOf(typeof(FirstTestComponent)).NoneOf(typeof(FirstTestComponent));
			testFilter.Reset();
			Assert.AreEqual(0, testFilter.AnyType.Count);
			Assert.AreEqual(0, testFilter.AllType.Count);
			Assert.AreEqual(0, testFilter.NoneType.Count);
		}
	}
}