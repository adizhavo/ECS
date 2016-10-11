using System;
using System.Collections.Generic;

namespace ECS
{
	// This class will change into two different interfaces because not every system would like
	// to perform AllMatcher or AnyMatcher if one of them is not needed

	// Should be also notified for removed components

	// Will perform operations of new component wich are added to an entity
    public interface IEntitySystem
    {
        Type[] matchers { get; }
        // will get entities that have all the matched component
        void AllMatchers(List<Entity> entities);

        // will get entities with at least one matched component
        void AnyMatchers(List<Entity> entities);
    }
}