using System;
using ECS;

class MainClass
{
    public static void Main(string[] args)
    {
        IReactiveSystem reactiveSystem = new SampleSystem();
        SystemMatcher.Subscribe(reactiveSystem);

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

public class SampleSystem : IReactiveSystem
{
    private Filter match = new Filter().AllOf(typeof(moveComp), typeof(healthComp)).AnyOf(typeof(healthComp));
    #region IReactiveSystem implementation
    public Filter filterMatch { get { return match; } }

    public void Execute(Entity entity)
    {
        Console.WriteLine(string.Format("{0} received {1}, the system has a filter with: {2}", this.GetType(), entity.GetType(), MatcherToString()));
    }
    #endregion

    private string MatcherToString()
    {
        string message = string.Empty;

        message += "Any of these components: ";
        foreach(Type t in filterMatch.AnyType)
        {
            message += t.ToString() + " ";
        }

        message += ", All of these components: ";
        foreach(Type t in filterMatch.AllType)
        {
            message += t.ToString() + " ";
        }

        return message;
    }

}