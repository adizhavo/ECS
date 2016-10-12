using System;

namespace ECS
{
    public interface IReactiveSystem
    {
        Filter filterMatch { get; }
        // will get entities that respects the defined filter
        void Execute(Entity modifiedEntity);
    }
}