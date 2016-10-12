using System;
using System.Collections.Generic;

namespace ECS
{
    public class Filter
    {
        public readonly HashSet<Type> AnyType = new HashSet<Type>();
        public readonly HashSet<Type> AllType = new HashSet<Type>();
        public readonly HashSet<Type> NoneType = new HashSet<Type>();

        public Filter AnyOf(params Type[] includeAnyType)
        {
            foreach(Type t in includeAnyType)
                if (!AnyType.Contains(t))
                    AnyType.Add(t);

            return this;
        }

        public Filter AllOf(params Type[] includeAllType)
        {
            foreach(Type t in includeAllType)
                if (!AllType.Contains(t))
                    AllType.Add(t);

            return this;
        }

        public Filter NoneOf(params Type[] excludeType)
        {
            foreach(Type t in excludeType)
                if (!NoneType.Contains(t))
                    NoneType.Add(t);

            return this;
        }

        public void Reset()
        {
            AnyType.Clear();
            AllType.Clear();
            NoneType.Clear();
        }
    }
}