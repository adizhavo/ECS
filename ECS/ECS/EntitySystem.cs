using System.Collections.Generic;

namespace ECS
{
    public interface IEntitySystem
    {
        Matcher[] systemMatchers { get; }

        // will get entities that have all the matched component
        void AllMatchers(List<Entity> entities);

        // will get entities with at least one matched component
        void AnyMatchers(List<Entity> entities);
    }
}