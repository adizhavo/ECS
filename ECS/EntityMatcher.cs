using System;
using System.Linq;
using System.Collections.Generic;

namespace ECS
{
	// Will hold all the entities and return a specific pool of them, like a search engine of entities and components
	// this can be extended in much more methods
    public static partial class EntityMatcher
    {
		// All entities created
        public readonly static HashSet<Entity> subscribedEntities = new HashSet<Entity>();

        public delegate void EntityAdded(Entity ent);
        public static event EntityAdded OnEntityRegistered;

        // Entity constructor will subriscribe with this method
        public static void Subscribe(Entity entity)
        {
			if (entity == null) throw new ArgumentNullException();

			if (string.IsNullOrEmpty(entity.Id))
				throw new Exception("The Entity Id you entered was blank or null.");

			if (subscribedEntities.Any(ent => ent.Id == entity.Id))
				throw new Exception("This entity already exists.");

            if (!subscribedEntities.Contains(entity))
            {
                subscribedEntities.Add(entity);
                if (OnEntityRegistered != null)
                    OnEntityRegistered(entity);
            }
            else Console.WriteLine("Entity is already subscribed");
        }

		public static Entity GetEntity(string whichEntity)
		{
			return subscribedEntities.First(_ => _.Id == whichEntity);
		}

		public static bool DoesEntityExist(string whichEntity)
		{
			return subscribedEntities.Any(_ => _.Id == whichEntity);
		}

		public static void Remove(string whichEntity)
		{
			subscribedEntities.Remove(GetEntity(whichEntity));
		}

		public static bool Remove(Entity whichEntity)
		{
			return subscribedEntities.Remove(whichEntity);
		}

		public static void RemoveAll()
		{
			subscribedEntities.Clear();
		}

		// Entity deconstructor will unsubriscribe with this method
        public static void Unsubscribe(Entity entity)
        {
			if (entity == null) throw new ArgumentNullException();
            if (subscribedEntities.Contains(entity)) subscribedEntities.Remove(entity);
        }

        public static HashSet<Entity> GetMatchedEntities(Filter request)
        {
			if (request == null) throw new ArgumentNullException ();

            HashSet<Entity> result = new HashSet<Entity>();
            foreach(Entity ent in subscribedEntities)
                if (ent.HasAnyComponent(request.AnyType.ToArray()) && ent.HasAllComponents(request.AllType.ToArray()) && ent.HasNoneComponent(request.NoneType.ToArray()))
                    result.Add(ent);

            return result;
        }
    }
}