using System;
using System.Collections.Generic;

public static class EntityMatcher
{
    private static List<Entity> subscribedEntities = new List<Entity>();

    public delegate void EntityAdded(Entity ent);
    public static event EntityAdded OnEntityRegistered;

    // This will be called autatically by the Entity constructor
    public static void SubscribeEntity(Entity entity)
    {
        if (!subscribedEntities.Contains(entity))
        {
            subscribedEntities.Add(entity);
            if (OnEntityRegistered != null) OnEntityRegistered(entity);
        }
        else
        {
            Console.Write("Entity is already registered");
        }
    }

    // This will be called autatically by the Entity destructor
    public static void UnsubscribeEntity(Entity entity)
    {
        if (subscribedEntities.Contains(entity))
        {
            subscribedEntities.Remove(entity);
        }
    }

    public static List<Entity> GetEntitiesWithComponent<T>() where T : class, IComponent
    {
        List<Entity> matchedEntities = new List<Entity>();

        foreach(Entity ent in subscribedEntities)
        {
            if (ent.HasComponent<T>())
            {
                matchedEntities.Add(ent);
            }
        }

        return matchedEntities;
    }

    public static List<Entity> GetEntitiesWithAllMatches(params Matcher[] matchers)
    {
        List<Entity> matchedEntities = new List<Entity>();

        foreach(Entity ent in subscribedEntities)
        {
            if (ent.HasAllMatchers(matchers))
            {
                matchedEntities.Add(ent);
            }
        }

        return matchedEntities;
    }

    public static List<Entity> GetEntitiesWithAnyMatch(params Matcher[] matchers)
    {
        List<Entity> matchedEntities = new List<Entity>();

        foreach(Entity ent in subscribedEntities)
        {
            if (ent.HasAnyMatcher(matchers))
            {
                matchedEntities.Add(ent);
            }
        }

        return matchedEntities;
    }
}