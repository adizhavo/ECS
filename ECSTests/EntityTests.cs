using System;
using NUnit.Framework;
using ECS;
using System.Collections.Generic;

namespace ECSTests
{
	[TestFixture()]
	public class EntityTests
	{
		private Entity testEntity;
		private Filter testFilter;
		private FirstTestComponent firstComponent;
		private SecondTestComponent secondComponent;
        private IComponent[] componentList;

        [SetUp]
		public void SetUp()
		{
			testEntity = new Entity();
			testFilter = new Filter();
			firstComponent = new FirstTestComponent();
			secondComponent = new SecondTestComponent();
		}

		[TearDown()]
		public void TearDown()
		{
			testFilter.Reset();
			testEntity.RemoveAllComponents();
		}

		[Test()]
		public void ShouldAddComponent()
		{
			testEntity.AddComponent(firstComponent);
			Assert.AreEqual(1, testEntity.components.Count);
		}

		[Test()]
		public void ShouldIgnoreWhenAddNullComponent()
		{
			testEntity.AddComponent(null);
			Assert.AreEqual(0, testEntity.components.Count);
		}

		[Test()]
		public void ShouldHaveOneAddedComponent()
		{
			testEntity.AddComponent(firstComponent);
			Assert.IsTrue(testEntity.HasComponent<FirstTestComponent>());
		}

		[Test()]
		public void ShouldHaveTwoAddedComponent()
		{
			testEntity.AddComponent(firstComponent);
			testEntity.AddComponent(secondComponent);
			Assert.IsTrue(testEntity.HasComponent<FirstTestComponent>());
			Assert.IsTrue(testEntity.HasComponent<SecondTestComponent>());
		}

		[Test()]
		public void ShouldRemoveOneComponent()
		{
			testEntity.AddComponent(firstComponent);
			testEntity.RemoveComponent<FirstTestComponent>();
			Assert.IsFalse(testEntity.HasComponent<FirstTestComponent>());
		}

		[Test()]
		public void ShouldRemoveMultipleComponentsOfSameType()
		{
			testEntity.AddComponent(firstComponent);
			testEntity.AddComponent(firstComponent);
			testEntity.RemoveComponent<FirstTestComponent>();
			Assert.IsFalse(testEntity.HasComponent<FirstTestComponent>());
		}

		[Test()]
		public void ShouldRemoveMultipleComponentsOfDifferentType()
		{
			testEntity.AddComponent(firstComponent);
			testEntity.AddComponent(firstComponent);
			testEntity.AddComponent(secondComponent);
			testEntity.RemoveComponent<FirstTestComponent>();
			testEntity.RemoveComponent<SecondTestComponent>();
			Assert.IsFalse(testEntity.HasComponent<FirstTestComponent>());
			Assert.IsFalse(testEntity.HasComponent<SecondTestComponent>());
		}

		[Test()]
		public void ShouldRemoveAllComponents()
		{
			testEntity.AddComponent(firstComponent);
			testEntity.AddComponent(firstComponent);
			testEntity.AddComponent(secondComponent);
			testEntity.RemoveAllComponents();
			Assert.IsFalse(testEntity.HasComponent<FirstTestComponent>());
			Assert.IsFalse(testEntity.HasComponent<SecondTestComponent>());
		}

		[Test()]
		public void ShouldReturnOneRequestedComponent()
		{
			testEntity.AddComponent(firstComponent);
			Assert.IsTrue(testEntity.GetComponents<FirstTestComponent>().Count == 1);
		}

		[Test()]
		public void ShouldReturnMultipleComponentsOfSameType()
		{
			testEntity.AddComponent(firstComponent);
			testEntity.AddComponent(firstComponent);
			Assert.IsTrue(testEntity.GetComponents<FirstTestComponent>().Count == 2);
		}

		[Test()]
		public void ShouldReturnMultipleComponentsOfDifferentType()
		{
			testEntity.AddComponent(firstComponent);
			testEntity.AddComponent(secondComponent);
			Assert.IsTrue(testEntity.GetComponents<FirstTestComponent>().Count == 1);
			Assert.IsTrue(testEntity.GetComponents<SecondTestComponent>().Count == 1);
		}

		[Test()] 
		public void ShouldMatchAllOfComponents()
		{
			testEntity.AddComponent(firstComponent);
			testEntity.AddComponent(secondComponent);

			Type[] allTypes = new Type[] { typeof(FirstTestComponent), typeof(SecondTestComponent) };
			Assert.IsTrue(testEntity.HasAllComponents(allTypes));
		}

		[Test()] 
		public void ShouldNotMatchAllOfComponents()
		{
			testEntity.AddComponent(firstComponent);
			
			Type[] allTypes = new Type[] { typeof(FirstTestComponent), typeof(SecondTestComponent) };
			Assert.IsFalse(testEntity.HasAllComponents(allTypes));
		}

		[Test()]
		public void ShouldMatchAnyComponent()
		{
			testEntity.AddComponent(firstComponent);
			testEntity.AddComponent(secondComponent);
			
			Type[] anyTypes = new Type[] { typeof(FirstTestComponent), typeof(SecondTestComponent) };
			Assert.IsTrue(testEntity.HasAnyComponent(anyTypes));

			testEntity.RemoveComponent<SecondTestComponent> ();
			Assert.IsTrue(testEntity.HasAnyComponent(anyTypes));
		}

		[Test()]
		public void ShouldNotMatchNoneComponent()
		{
			testEntity.AddComponent(firstComponent);
			
			Type[] noneType = new Type[] {typeof(FirstTestComponent)};
			Assert.IsFalse(testEntity.HasNoneComponent(noneType));

			testEntity.RemoveComponent<FirstTestComponent>();
			Assert.IsTrue(testEntity.HasNoneComponent(noneType));
		}

		[ExpectedException]
		public void ThrowExceptionWhenPassNullFilter()
		{
			testEntity.DoesMatchFilter(null);
		}
		
		[Test()]
		public void EntityShouldMatchFilterWithAllType()
		{
			testEntity.AddComponent(new FirstTestComponent());
			testEntity.AddComponent(new SecondTestComponent());
			
			testFilter.AllOf(typeof(FirstTestComponent), typeof(SecondTestComponent));
			Assert.IsTrue(testEntity.DoesMatchFilter(testFilter));
		}
		
		[Test()]
		public void EntityShouldMatchFilterWithAnyType()
		{
			testEntity.AddComponent(new FirstTestComponent());
			testEntity.AddComponent(new SecondTestComponent());
			
			testFilter.AnyOf(typeof(FirstTestComponent));
			Assert.IsTrue(testEntity.DoesMatchFilter(testFilter));
			
			testFilter.Reset ();
			
			testFilter.AnyOf(typeof(SecondTestComponent));
			Assert.IsTrue(testEntity.DoesMatchFilter(testFilter));
		}
		
		[Test()]
		public void EntityShouldMatchFilterWithNoneType()
		{
			testEntity.AddComponent(new FirstTestComponent());
			testEntity.AddComponent(new SecondTestComponent());
			
			testFilter.NoneOf(typeof(FirstTestComponent));
			Assert.IsFalse(testEntity.DoesMatchFilter(testFilter));
			
			testFilter.Reset();
			
			testFilter.NoneOf(typeof(SecondTestComponent));
			Assert.IsFalse(testEntity.DoesMatchFilter(testFilter));
			
			testFilter.Reset();
			testEntity.RemoveAllComponents();
			testFilter.NoneOf(typeof(FirstTestComponent), typeof(SecondTestComponent));
			Assert.IsTrue(testEntity.DoesMatchFilter(testFilter));
		}

        [Test()]
        public void AddMultipleComponentsToEntity()
        {
            componentList = new IComponent[2] { firstComponent, secondComponent };

            testEntity.AddComponents(true, componentList);
            Assert.AreEqual(2, testEntity.components.Count);
        }
	}

	public class FirstTestComponent : IComponent
	{
		#region IComponent implementation
		
		public Entity entity
		{
			set; get;
		}
		
		#endregion
	}

	public class SecondTestComponent : IComponent
	{
		#region IComponent implementation
		
		public Entity entity
		{
			set; get;
		}
		
		#endregion
	}
}