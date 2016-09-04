using System;
using System.Collections.Generic;

namespace ECS
{
	// Will hold all the systems that wants to be notified if a component is added or replaced
    public static class SystemMatcher
    {
        private static List<IEntitySystem> subscribedSystems = new List<IEntitySystem>();

        public static void SubscribeSystem(IEntitySystem system)
        {
            if (!subscribedSystems.Contains(system))
            {
                subscribedSystems.Add(system);
            }
            else
            {
                Console.Write("Entity is already registered");
            }
        }

        public static void UnsubscribeSystem(IEntitySystem system)
        {
            if (subscribedSystems.Contains(system))
            {
                subscribedSystems.Remove(system);
            }
        }

		// The entity will call this when a new component is added or replaced
        public static void NotifySystems()
        {
            foreach (IEntitySystem systems in subscribedSystems)
            {
                List<Entity> allMatchers = EntityMatcher.GetEntitiesWithAllMatches(systems.systemMatchers);
                List<Entity> anyMatch = EntityMatcher.GetEntitiesWithAnyMatch(systems.systemMatchers);

                systems.AllMatchers(allMatchers);
                systems.AnyMatchers(anyMatch);
            }
        }
    }
}