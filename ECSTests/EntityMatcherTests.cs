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
		public void ThrowExceptionWhenPassNullFilter()
		{
			EntityMatcher.GetMatchedEntities(null);
		}

		[Test()]
		public void ShouldMatchEntityReceivedFromFilter()
		{
			testEntity.AddComponent(new FirstTestComponent());
			testFilter.AnyOf(typeof(FirstTestComponent));

			Assert.IsTrue(EntityMatcher.GetMatchedEntities(testFilter).Contains(testEntity));
		}
	}
}

