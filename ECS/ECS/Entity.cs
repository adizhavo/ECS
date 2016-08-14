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
        components = new List<IComponent>();
    }

    ~Entity()
    {
        RemoveAllComponent();
    }

    public void AddComponent(IComponent newCompoent)
    {
        if (newCompoent == null)
        {
            Console.Write("Component that you intented to add is null, method will return void");
            return;
        }

        components.Add(newCompoent);
        newCompoent.entity = this;
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
}