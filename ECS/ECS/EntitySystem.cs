using System.Collections.Generic;

public interface IEntitySystem
{
    Matcher[] systemMatchers {get;}

    void AllMatchers(List<Entity> entities); // will get entities that have all the matched component
    void AnyMatchers(List<Entity> entities); // will get entities with at least one matched component
}