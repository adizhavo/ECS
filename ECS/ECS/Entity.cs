using System;
using System.Collections.Generic;

namespace ECS
{
    public class Entity
    {
        public List<IComponent> components
        {
            private set;
            get;
        }

        public Entity()
        {
            EntityMatcher.SubscribeEntity(this);
            components = new List<IComponent>();
        }

        ~Entity()
        {
            EntityMatcher.UnsubscribeEntity(this);
            RemoveAllComponent();
            components = null;
        }

        public void AddComponent(IComponent newComponent, bool notifySystems = true)
        {
            if (newComponent == null)
            {
                Console.Write("Component that you intented to add is null, method will return void");
                return;
            }

            components.Add(newComponent);
            newComponent.entity = this;

            if (notifySystems)
            {
                SystemMatcher.NotifySystems();
            }
        }

        public void ReplaceComponent(IComponent replaceComponent, bool notifySystem = true)
        {
            if (replaceComponent == null)
            {
                Console.Write("Component that you intented to replace is null, method will return void");
                return;
            }

            for (int i = 0; i < components.Count; i++)
            {
                if (components[i].matcher.Equals(replaceComponent.matcher))
                {
                    components[i].entity = null;
                    components[i] = null;
                    components[i] = replaceComponent;
                    return;
                }
            }

            Console.Write("No match for the component, will be added as a new component to the entity");
            AddComponent(replaceComponent, notifySystem);
        }

        public void RemoveComponent<T>() where T : class, IComponent
        {
            foreach (IComponent cmp in components)
            {
                if (cmp is T)
                {
                    cmp.entity = null;
                    components.Remove(cmp);
                }
            }
        }

        public void RemoveMatchedComponent(Matcher deleteMatcher)
        {
            foreach (IComponent cmp in components)
            {
                if (cmp.matcher.Equals(deleteMatcher))
                {
                    components.Remove(cmp);
                }
            }
        }

        public void RemoveAllComponent()
        {
            for (int i = 0; i < components.Count; i++)
            {
                components[i].entity = null;
                components.RemoveAt(i);
            }

            components.Clear();
        }

        public List<IComponent> GetComponentsWithMatcher(Matcher requestMatcher)
        {
            List<IComponent> requestedComponents = new List<IComponent>();

            foreach (IComponent cmp in components)
            {
                if (cmp.matcher.Equals(requestMatcher))
                {
                    requestedComponents.Add(cmp);
                }
            }

            return requestedComponents;
        }

        public List<T> GetComponents<T>() where T : class, IComponent
        {
            List<T> requestedComponents = new List<T>();

            foreach (IComponent cmp in components)
            {
                if (cmp is T)
                {
                    requestedComponents.Add((T)cmp);
                }
            }

            return requestedComponents;
        }

        public bool HasComponent<T>() where T : class, IComponent
        {
            foreach (IComponent cmp in components)
            {
                if (cmp is T)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasAllMatchers(params Matcher[] matchers)
        {
            int matchedComponents = 0;

            for (int i = 0; i < matchers.Length; i++)
            {
                foreach (IComponent cmp in components)
                {
                    if (cmp.matcher.Equals(matchers[i]))
                    {
                        matchedComponents++;
                        break;
                    }
                }
            }

            return matchedComponents == matchers.Length && matchedComponents != 0;
        }

        public bool HasAnyMatcher(params Matcher[] matchers)
        {
            for (int i = 0; i < matchers.Length; i++)
            {
                foreach (IComponent cmp in components)
                {
                    if (cmp.matcher.Equals(matchers[i]))
                        return true;
                }
            }

            return false;
        }
    }
}