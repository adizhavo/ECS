using System;
using System.Collections.Generic;

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

    public static void NotifySystems()
    {
        // will notify all subscribed systems with all/any matcher
    }
}