using System;
using NUnit.Framework;
using ECS;

namespace ECSTests
{
	[TestFixture()]
	public class EntityMatcherTests
	{
		private Entity testEntity;
		private Filter testFilter;

		[SetUp]
		public void SetUp()
		{
			testEntity = new Entity();
			testFilter = new Filter();
		}

		[TearDown]
		public void TearDown()
		{
			EntityMatcher.subscribedEntities.Clear();
		}

		[Test()]
		public void ShouldSubscribeEntityWhenCreated ()
		{
			Assert.IsTrue(EntityMatcher.subscribedEntities.Contains(testEntity));
		}

		[ExpectedException()]
		public void ShouldThrowExceptionWhenAddNullEntity()
		{
			EntityMatcher.Subscribe(null);
		}

		[ExpectedException()]
		public void ShouldThrowExceptionWhenRemoveNullEntity()
		{
			EntityMatcher.Unsubscribe(null);
		}

		[Test()]
		public void ShouldNotSubscribeDublicateEntities()
		{
			EntityMatcher.Subscribe(testEntity);
			Assert.AreEqual(1, EntityMatcher.subscribedEntities.Count);
		}

		[Test()]
		public void ShouldUnsubscribeEntity()
		{
			EntityMatcher.Unsubscribe(testEntity);
			Assert.AreEqual(0, EntityMatcher.subscribedEntities.Count);
		}

		[ExpectedException]
		public void ThrowExceptionWhenPassNullFilterOrNullEntity()
		{
			EntityMatcher.MatchWithFilter(null, testEntity);
			EntityMatcher.MatchWithFilter(testFilter, null);
		}

		[Test()]
		public void EntityShouldMatchFilterWithAllType()
		{
			testEntity.AddComponent(new FirstTestComponent());
			testEntity.AddComponent(new SecondTestComponent());

			testFilter.AllOf(typeof(FirstTestComponent), typeof(SecondTestComponent));
			Assert.IsTrue(EntityMatcher.MatchWithFilter(testFilter, testEntity));
		}

		[Test()]
		public void EntityShouldMatchFilterWithAnyType()
		{
			testEntity.AddComponent(new FirstTestComponent());
			testEntity.AddComponent(new SecondTestComponent());
			
			testFilter.AnyOf(typeof(FirstTestComponent));
			Assert.IsTrue(EntityMatcher.MatchWithFilter(testFilter, testEntity));

			testFilter.Reset ();

			testFilter.AnyOf(typeof(SecondTestComponent));
			Assert.IsTrue(EntityMatcher.MatchWithFilter(testFilter, testEntity));
		}

		[Test()]
		public void EntityShouldMatchFilterWithNoneType()
		{
			testEntity.AddComponent(new FirstTestComponent());
			testEntity.AddComponent(new SecondTestComponent());
			
			testFilter.NoneOf(typeof(FirstTestComponent));
			Assert.IsFalse(EntityMatcher.MatchWithFilter(testFilter, testEntity));
			
			testFilter.Reset();
			
			testFilter.NoneOf(typeof(SecondTestComponent));
			Assert.IsFalse(EntityMatcher.MatchWithFilter(testFilter, testEntity));

			testFilter.Reset();
			testEntity.RemoveAllComponent();
			testFilter.NoneOf(typeof(FirstTestComponent), typeof(SecondTestComponent));
			Assert.IsTrue(EntityMatcher.MatchWithFilter(testFilter, testEntity));
		}

		[ExpectedException]
		public void ThrowExceptionWhenPassNullFilter()
		{
			EntityMatcher.FilterEntities(null);
		}

		[Test()]
		public void ShouldMatchEntityReceivedFromFilter()
		{
			testEntity.AddComponent(new FirstTestComponent());
			testFilter.AnyOf(typeof(FirstTestComponent));

			Assert.IsTrue(EntityMatcher.FilterEntities(testFilter).Contains(testEntity));
		}
	}
}

