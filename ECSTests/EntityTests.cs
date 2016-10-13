using System;
using NUnit.Framework;
using ECS;

namespace ECSTests
{
	[TestFixture()]
	public class EntityTests
	{
		private Entity testEntity;
		private FirstTestComponent firstComponent;
		private SecondTestComponent secondComponent;

		[SetUp]
		public void SetUp()
		{
			testEntity = new Entity();
			firstComponent = new FirstTestComponent();
			secondComponent = new SecondTestComponent();
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
			testEntity.RemoveAllComponent();
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