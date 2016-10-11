using System;
using System.Collections.Generic;

namespace ECS
{
	// Will hold all the systems that wants to be notified if a component is added or replaced
    public static class SystemMatcher
    {
        private static List<IAllMatchersSystem> subscribedAllMatcherSystems = new List<IAllMatchersSystem>();
        private static List<IAnyMatchersSystem> subscribedAnyMatcherSystems = new List<IAnyMatchersSystem>();

        public static void SubscribeAllMatcherSystem(IAllMatchersSystem system)
        {
            if (!subscribedAllMatcherSystems.Contains(system))
            {
                subscribedAllMatcherSystems.Add(system);
            }
            else
            {
                Console.Write("Entity is already subscribed");
            }
        }

        public static void SubscribeAnyMatcherSystem(IAnyMatchersSystem system)
        {
            if (!subscribedAnyMatcherSystems.Contains(system))
            {
                subscribedAnyMatcherSystems.Add(system);
            }
            else
            {
                Console.Write("Entity is already subscribed");
            }
        }

        public static void UnsubscribeAllMatcherSystem(IAllMatchersSystem system)
        {
            if (subscribedAllMatcherSystems.Contains(system))
            {
                subscribedAllMatcherSystems.Remove(system);
            }
        }

        public static void UnsubscribeAnyMatcherSystem(IAnyMatchersSystem system)
        {
            if (subscribedAnyMatcherSystems.Contains(system))
            {
                subscribedAnyMatcherSystems.Remove(system);
            }
        }

		// The entity will call this when a new component is added or replaced
        public static void NotifySystems()
        {
            NotifyAllMatcherSystem();
            NotifyAnyMatcherSystem();
        }

        private static void NotifyAllMatcherSystem()
        {
            foreach (IAllMatchersSystem s in subscribedAllMatcherSystems)
            {
                List<Entity> anyMatch = EntityMatcher.GetEntitiesWithAnyMatch(s.allMatchers);
                s.AllMatchers(anyMatch);
            }
        }

        private static void NotifyAnyMatcherSystem()
        {
            foreach (IAnyMatchersSystem s in subscribedAnyMatcherSystems)
            {
                List<Entity> anyMatch = EntityMatcher.GetEntitiesWithAnyMatch(s.anyMatchers);
                s.AnyMatchers(anyMatch);
            }
        }
    }
}