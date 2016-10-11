using System;
using System.Collections.Generic;

namespace ECS
{
	// Will perform operations on new component added to an entity
    public interface IAllMatchersSystem
    {
        Type[] allMatchers { get; }
        // will get entities that have all the matched component
        void AllMatchers(List<Entity> entities);
    }

    public interface IAnyMatchersSystem
    {
        Type[] anyMatchers { get; }
        // will get entities with at least one matched component
        void AnyMatchers(List<Entity> entities);
    }
}