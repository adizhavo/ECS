using System;
using System.Collections.Generic;

namespace ECS
{
	// Will hold all the entities and return a specific pool of them, like a search engine of entities and components
	// this can be extended in much more methods
    public static partial class EntityMatcher
    {
		// All entities created
        private static List<Entity> subscribedEntities = new List<Entity>();

        public delegate void EntityAdded(Entity ent);

        public static event EntityAdded OnEntityRegistered;

        // Entity constructor will subriscribe with this method
        public static void SubscribeEntity(Entity entity)
        {
            if (!subscribedEntities.Contains(entity))
            {
                subscribedEntities.Add(entity);
                if (OnEntityRegistered != null)
                    OnEntityRegistered(entity);
            }
            else
            {
                Console.Write("Entity is already subscribed");
            }
        }

		// Entity constructor will unsubriscribe with this method
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

            foreach (Entity ent in subscribedEntities)
            {
                if (ent.HasComponent<T>())
                {
                    matchedEntities.Add(ent);
                }
            }

            return matchedEntities;
        }

		// Entities must have all specified matchers
        public static List<Entity> GetEntitiesWithAllMatches(params Type[] matchers)
        {
            List<Entity> matchedEntities = new List<Entity>();

            foreach (Entity ent in subscribedEntities)
            {
                if (ent.HasAllComponents(matchers))
                {
                    matchedEntities.Add(ent);
                }
            }

            return matchedEntities;
        }

		// Entities can have at least one of specified matchers
        public static List<Entity> GetEntitiesWithAnyMatch(params Type[] matchers)
        {
            List<Entity> matchedEntities = new List<Entity>();

            foreach (Entity ent in subscribedEntities)
            {
                if (ent.HasAnyComponent(matchers))
                {
                    matchedEntities.Add(ent);
                }
            }

            return matchedEntities;
        }
    }
}