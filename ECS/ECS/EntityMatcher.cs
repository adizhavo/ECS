using System;
using System.Collections.Generic;

public static class EntityMatcher
{
    private static List<Entity> registeredEntities = new List<Entity>();

    // This will be called autatically by the Entity constructor
    public static void SubscribeEntity(Entity entity)
    {
        if (!registeredEntities.Contains(entity))
            registeredEntities.Add(entity);
        else
        {
            Console.Write("Entity is already registered");
        }
    }

    // This will be called autatically by the Entity destructor
    public static void UnsubscribeEntity(Entity entity)
    {
        if (registeredEntities.Contains(entity))
        {
            registeredEntities.Remove(entity);
        }
    }

    public static List<Entity> GetEntitiesWithComponent<T>() where T : class, IComponent
    {
        List<Entity> matchedEntities = new List<Entity>();

        foreach(Entity ent in registeredEntities)
        {
            if (ent.HasComponent<T>())
            {
                matchedEntities.Add(ent);
            }
        }

        return matchedEntities;
    }

    public static List<Entity> GetEntitiesWithMatch(Matcher matcher)
    {
        List<Entity> matchedEntities = new List<Entity>();

        foreach(Entity ent in registeredEntities)
        {
            if (ent.HasMatcher(matcher))
            {
                matchedEntities.Add(ent);
            }
        }

        return matchedEntities;
    }
}