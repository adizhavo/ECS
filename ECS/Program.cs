using System;
using ECS;

class MainClass
{
    public static void Main(string[] args)
    {
        IEntitySystem moveSystem = new SampleSystem();
        SystemMatcher.SubscribeSystem(moveSystem);

        IComponent moveComp = new moveComp();
        IComponent healthComp = new healthComp();

        Entity ent = new Entity();
        ent.AddComponent(healthComp);
        ent.AddComponent(moveComp);

        Entity ent2 = new Entity();
        ent2.AddComponent(healthComp);
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

public class SampleSystem : IEntitySystem
{
    #region IEntitySystem implementation
    public void AllMatchers(System.Collections.Generic.List<Entity> entities)
    {
        Console.WriteLine(string.Format("{0} received {1} entities with all of these components : {2}", this.GetType(), entities.Count, MatcherToString()));
    }

    public void AnyMatchers(System.Collections.Generic.List<Entity> entities)
    {
        Console.WriteLine(string.Format("{0} System received {1} entities with any of these components : {2}", this.GetType(), entities.Count, MatcherToString()));
    }

    public Type[] matchers
    {
        get 
        {
            return new Type[] {typeof(moveComp), typeof(healthComp)};
        }
    }
    #endregion

    private string MatcherToString()
    {
        string message = string.Empty;

        foreach(Type t in matchers)
        {
            message += t.ToString() + " ";
        }

        return message;
    }
}