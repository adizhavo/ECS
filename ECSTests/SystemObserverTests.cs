using System;
using NUnit.Framework;
using ECS;

namespace ECSTests
{
	[TestFixture()]
	public class SystemObserverTests
	{
		private Entity testEntity;
		private TestReactiveSystem testSystem;

		[SetUp()]
		public void SetUp()
		{
			testEntity = new Entity();
			testSystem = new TestReactiveSystem();
		}

		[TearDown()]
		public void TearDown()
		{
			testSystem.Reset();
			SystemObserver.Unsubscribe(testSystem);
		}

		[Test()]
		public void ShouldAddSystemWhenSubscribed()
		{
			SystemObserver.Subscribe(testSystem);
			Assert.AreEqual(1, SystemObserver.reactiveSystems.Count);
		}

		[ExpectedException()]
		public void ThrowExceptionWhenSubscribeNullSystem()
		{
			SystemObserver.Subscribe(null);
		}

		[Test()]
		public void ShouldIgnoreDuplicateSystems()
		{
			SystemObserver.Subscribe(testSystem);
			SystemObserver.Subscribe(testSystem);
			Assert.AreEqual(1, SystemObserver.reactiveSystems.Count);
		}

		[Test()]
		public void ShouldRemoveSystemWhenUnSubscribed()
		{
			SystemObserver.Subscribe(testSystem);
			SystemObserver.Unsubscribe(testSystem);
			Assert.AreEqual(0, SystemObserver.reactiveSystems.Count);
		}
		
		[ExpectedException()]
		public void ThrowExceptionWhenUnsubscribeNullSystem()
		{
			SystemObserver.Unsubscribe(null);
		}

		[Test()]
		public void ShouldTriggerSystemIfComponentIsAddedAndMatches()
		{
			Filter testFilter = new Filter().AllOf(typeof(FirstTestComponent), typeof(SecondTestComponent));
			testSystem.filter = testFilter;
			SystemObserver.Subscribe(testSystem);

			testEntity.AddComponent(new FirstTestComponent());
			Assert.IsFalse(testSystem.isTriggered);

			testEntity.AddComponent(new SecondTestComponent());
			Assert.IsTrue(testSystem.isTriggered);
		}

		[Test()]
		public void ShouldReceiveModifiedEntity()
		{
			Filter testFilter = new Filter().AnyOf(typeof(FirstTestComponent));
			testSystem.filter = testFilter;
			SystemObserver.Subscribe(testSystem);

			testEntity.AddComponent(new FirstTestComponent());
			Assert.AreSame(testEntity, testSystem.receivedEntity);
		}
	}

	public class TestReactiveSystem : IReactiveSystem
	{
		public bool isTriggered { get { return receivedEntity != null; } }
		public Entity receivedEntity;
		public Filter filter;

		public void Reset()
		{
			filter = null;
			receivedEntity = null;
		}

		#region IReactiveSystem implementation
		public Filter filterMatch 
		{
			get { return filter; }
		}
		
		public void Execute(Entity entity) 
		{ 
			receivedEntity = entity;
		}
		#endregion
	}
}