using System;
using System.Collections.Generic;

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

    public void AddComponent(IComponent cmp, bool notifySystems = true)
    {
        if (cmp == null)
        {
            Console.Write("Component that you intented to add is null, method will return void");
            return;
        }

        components.Add(cmp);
        cmp.entity = this;

        if (notifySystems)
        {
            SystemMatcher.NotifySystems();
        }
    }

    public void RemoveComponent<T>() where T : class, IComponent
    {
        for (int i= 0; i < components.Count; i ++)
        {
            IComponent cmp = components[i];

            if (cmp is T) 
            {
                cmp.entity = null;
                components.RemoveAt(i);
            }
        }
    }

    public void RemoveAllComponent()
    {
        for (int i= 0; i < components.Count; i ++)
        {
            components[i].entity = null;
            components.RemoveAt(i);
        }

        components.Clear();
    }

    public bool HasComponent<T>() where T : class, IComponent
    {
        foreach(IComponent cmp in components)
        {
            if (cmp is T) 
            {
                return true;
            }
        }

        return false;
    }

    public bool HasMatchers(params Matcher[] matchers)
    {
        int matchedComponents = 0;

        for (int i = 0; i < matchers.Length; i ++)
        {
            foreach(IComponent cmp in components)
            {
                if (cmp.matcher.Equals(matchers[i]))
                    matchedComponents ++;
            }
        }

        return matchedComponents == matchers.Length && matchedComponents != 0;
    }
}