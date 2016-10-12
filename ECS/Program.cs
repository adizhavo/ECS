using ECS;
using System;

class MainClass
{
    public static void Main(string[] args)
    {
        IReactiveSystem sampleReactiveSystem = new SampleSystem();
        SystemObserver.Subscribe(sampleReactiveSystem);

        IComponent moveComp = new MoveComp();
        IComponent healthComp = new HealthComp();

        Entity ent = new Entity();
        ent.AddComponent(moveComp);
        ent.AddComponent(healthComp);

        Entity ent2 = new Entity();
        ent2.AddComponent(healthComp);
    }
}

namespace ECS
{
    public class MoveComp : IComponent
    {
        #region IComponent implementation

        public Entity entity
        {
            set; get;
        }

        #endregion
    }

    public class HealthComp : IComponent
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
        private Filter match = new Filter().AnyOf(typeof(HealthComp)).NoneOf(typeof(MoveComp));

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

            message += ", None of these components: ";
            foreach(Type t in filterMatch.NoneType)
            {
                message += t.ToString() + " ";
            }

            return message;
        }
    }
}