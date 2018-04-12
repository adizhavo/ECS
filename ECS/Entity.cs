using System;
using System.Linq;
using System.Collections.Generic;

namespace ECS
{
	// is the facade for different components, a bag for data and simple operations
    public class Entity : IEquatable<Entity>
	{
        public readonly List<IComponent> components;

        public Entity()
        {
            EntityMatcher.Subscribe(this);
            components = new List<IComponent>();
        }

		public Entity(string _id)
		{
			Id = _id;
			EntityMatcher.Subscribe(this);
			components = new List<IComponent>();
		}

        ~Entity()
        {
            EntityMatcher.Unsubscribe(this);
			RemoveAllComponents();
        }

		public string Id = string.Empty;

		// Add new component to the entity, can support doubles of components
        public Entity AddComponent(IComponent newComponent, bool notifySystems = true)
        {
            if (newComponent == null)
            {
                Console.WriteLine("Component that you intented to add is null, method will return void");
                return this;
            }

            components.Add(newComponent);
            newComponent.entity = this;

            // Notifies systems so they can perfom operations, like manipulating componet data
            if (notifySystems) SystemObserver.NotifySystems(this);
			return this;
        }

		public Entity AddComponents(bool notifySystems = true, params IComponent[] components)
		{
			foreach (IComponent i in components)
			{
				AddComponent(i);
			}

			if(components.Count() == 0)
			{
				Console.WriteLine("Component that you intented to add is null, method will return void");
				return this;
			}

			return this;
		}

		// Will replace component if there is a match, if not it will add it as a new component
        public Entity ReplaceComponent(IComponent replaceComponent, bool notifySystems = true)
        {
            if (replaceComponent == null)
            {
                Console.WriteLine("Component that you intented to replace is null, method will return void");
                return this;
            }

            for (int i = 0; i < components.Count; i++)
                if (components[i].GetType().Equals(replaceComponent.GetType()))
                {
                    components[i].entity = null;
                    components[i] = null;
                    components[i] = replaceComponent;

                    // Notifies systems so they can perfom operations, like manipulating componet data
                    if (notifySystems) SystemObserver.NotifySystems(this);
                    return this;
                }

            Console.WriteLine("No match for the component, will be added as a new component to the entity");
            return AddComponent(replaceComponent, notifySystems);
        }

		public void RemoveComponents<T>() where T : class, IComponent
		{
			for(int i = 0; i < components.Count; i ++)
				if (components[i] is T)
			{
				components[i].entity = null;
				components.RemoveAt(i);
				i --;
			}
		}
		
		public void RemoveAllComponents()
		{
			for (int i = 0; i < components.Count; i++)
			{
				components[i].entity = null;
				components.RemoveAt(i);
			}
			
			components.Clear();
		}

		public T GetComponent<T>() where T : class, IComponent
		{
			foreach (IComponent cmp in components)
				if (cmp is T)
					return (T)cmp;

			return null;
		}
		
		public List<T> GetComponents<T>() where T : class, IComponent
		{
			List<T> requestedComponents = new List<T>();
			
			foreach (IComponent cmp in components)
                if (cmp is T)
                    requestedComponents.Add((T)cmp);
			
			return requestedComponents;
		}

		public bool HasComponent<T>() where T : class, IComponent
		{
			foreach (IComponent cmp in components)
				if (cmp is T) return true;
			
			return false;
		}
		
		public bool HasAnyComponent(params Type[] matchers)
		{
			for (int i = 0; i < matchers.Length; i++)
				foreach (IComponent cmp in components)
					if (cmp.GetType().Equals(matchers[i]))
						return true;
			
			return matchers.Length == 0;
		}
		
		public bool HasAllComponents(params Type[] matchers)
		{
            int matchedComponents = 0;

            for (int i = 0; i < matchers.Length; i++)
                foreach (IComponent cmp in components)
                    if (cmp.GetType().Equals(matchers[i]))
                    {
                        matchedComponents++;
                        break;
                    }

            return matchers.Length == 0 || (matchedComponents == matchers.Length && matchedComponents != 0);
        }

		public bool HasNoneComponent(params Type[] matchers)
		{
			for (int i = 0; i < matchers.Length; i++)
				foreach (IComponent cmp in components)
					if (cmp.GetType().Equals(matchers[i]))
						return false;
			
			return true;
		}
		
		public bool DoesMatchFilter(Filter request)
		{
			if (request == null) throw new ArgumentNullException ();
			
			return HasAllComponents(request.AllType.ToArray()) 
				   && HasAnyComponent(request.AnyType.ToArray()) 
				   && HasNoneComponent(request.NoneType.ToArray());
		}

		public bool Equals(Entity other)
		{
			return this.Id == other.Id;
		}
	}
}