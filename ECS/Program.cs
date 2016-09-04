using System;
using ECS;

class MainClass
{
    public static void Main(string[] args)
    {
        IEntitySystem moveSystem = new MoveSystem();
        SystemMatcher.SubscribeSystem(moveSystem);

        IComponent moveComp = new moveComp();
        IComponent healthComp = new healthComp();

        Entity ent = new Entity();
        ent.AddComponent(healthComp);
        ent.AddComponent(moveComp);

        Console.Write(ent.HasAnyMatcher(Matcher.Health, Matcher.Move));
        Console.Write(ent.HasAllMatchers(Matcher.Health, Matcher.Move));
    }
}

public class moveComp : IComponent
{
    #region IComponent implementation
    public Entity entity
    {
        set; get;
    }
    public Matcher matcher
    {
        get
        {
            return Matcher.Move;
        }
    }
    #endregion
}

public class healthComp : IComponent
{
    #region IComponent implementation
    public Entity entity
    {
        set; get;
    }
    public Matcher matcher
    {
        get
        {
            return Matcher.Health;
        }
    }
    #endregion
}

public class MoveSystem : IEntitySystem
{
    #region IEntitySystem implementation
    public void AllMatchers(System.Collections.Generic.List<Entity> entities)
    {
        Console.Write("All" + entities.Count);
    }
    public void AnyMatchers(System.Collections.Generic.List<Entity> entities)
    {
        Console.Write("Any" + entities.Count);
    }
    public Matcher[] systemMatchers
    {
        get
        {
            return new Matcher[]{ Matcher.Move, Matcher.Health };
        }
    }
    #endregion
    
}