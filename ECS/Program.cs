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

        Entity ent2 = new Entity();
        ent2.AddComponent(healthComp);

        Console.Write(ent.HasAnyComponent(typeof(moveComp), typeof(healthComp)));
        Console.Write(ent.HasAllComponents(typeof(moveComp), typeof(healthComp)));
    }
}

public class moveComp : IComponent
{
    #region IComponent implementation

    public Entity entity
    {
        set; get;
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

    #endregion
}

public class MoveSystem : IEntitySystem
{
    #region IEntitySystem implementation
    public void AllMatchers(System.Collections.Generic.List<Entity> entities)
    {
        Console.Write("All " + entities.Count + " ");
    }

    public void AnyMatchers(System.Collections.Generic.List<Entity> entities)
    {
        Console.Write("Any " + entities.Count + " ");
    }

    public Type[] matchers
    {
        get 
        {
            return new Type[] {typeof(moveComp), typeof(healthComp)};
        }
    }
    #endregion
    
}