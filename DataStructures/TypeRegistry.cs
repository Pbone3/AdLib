using System;
using System.Collections.Generic;

namespace AdLib.DataStructures
{
    public class TypeRegistry<T>
    {
        public Dictionary<Type, T> RegistryObjects;

        public TypeRegistry()
        {
            RegistryObjects = new Dictionary<Type, T>();
        }

        public void Add<TType>() where TType : T, new() => Add(new TType());
        public void Add(T instance) => RegistryObjects.Add(instance.GetType(), instance);
        public T Get<TType>() where TType : T => RegistryObjects[typeof(T)];

        public bool TryGet<TType>(out T instance) where TType : T
        {
            if (RegistryObjects.ContainsKey(typeof(T)))
            {
                instance = default;
                return false;
            }

            instance = Get<TType>();
            return true;
        }
    }
}
