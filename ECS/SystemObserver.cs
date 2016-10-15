using System;
using System.Collections.Generic;

namespace ECS
{
	// Will hold all the systems that wants to be notified if a component is added or replaced
    public static class SystemObserver
    {
        public readonly static HashSet<IReactiveSystem> reactiveSystems = new HashSet<IReactiveSystem>();

        public static void Subscribe(IReactiveSystem system)
        {
			if (system == null) throw new ArgumentNullException();

            if (!reactiveSystems.Contains(system)) reactiveSystems.Add(system);
            else Console.Write("System is already subscribed");
        }

        public static void Unsubscribe(IReactiveSystem system)
        {
			if (system == null) throw new ArgumentNullException();
            if (reactiveSystems.Contains(system)) reactiveSystems.Remove(system);
        }

		// The entity will call this when a new component is added or replaced
        public static void NotifySystems(Entity modifiedEntity)
        {
            foreach(IReactiveSystem rs in reactiveSystems)
                if (modifiedEntity.DoesMatchFilter(rs.filterMatch)) 
					rs.Execute(modifiedEntity);
        }
    }
}