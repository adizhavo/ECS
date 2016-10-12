using System;
using System.Collections.Generic;

namespace ECS
{
    public class Filter
    {
        public HashSet<Type> AnyType = new HashSet<Type>();
        public HashSet<Type> AllType = new HashSet<Type>();
        public HashSet<Type> NoneType = new HashSet<Type>();

        public Filter AnyOf(params Type[] anyType)
        {
            foreach(Type t in anyType)
                if (!AnyType.Contains(t))
                    AnyType.Add(t);

            return this;
        }

        public Filter AllOf(params Type[] allType)
        {
            foreach(Type t in allType)
                if (!AllType.Contains(t))
                    AllType.Add(t);

            return this;
        }

        public Filter NoneOf(params Type[] noneType)
        {
            foreach(Type t in noneType)
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