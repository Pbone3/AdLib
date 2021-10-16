using AdLib.EntityFramework;
using Microsoft.Xna.Framework;
using System;
using System.Collections;
using System.Collections.Generic;

namespace AdLib.Collections
{
    public class BehaviourCollection : IEnumerable<Behaviour>, IEnumerable
    {
        public int Length => Inner.Count;

        private List<Behaviour> Inner = new List<Behaviour>();

        public BehaviourCollection()
        {
        }

        public T Get<T>() where T : Behaviour
        {
            foreach (Behaviour b in Inner)
            {
                if (b is T t)
                    return t;
            }

            return null;
        }

        public void Add<T>() where T : Behaviour, new() => Add(new T());
        public void Add<T>(T instance) where T : Behaviour
        {
            if (Contains<T>())
                throw new InvalidOperationException("A BehaviourCollection can not contain multiple of the same type of behaviour.");

            Inner.Add(instance);
        }

        public bool Remove<T>() where T : Behaviour
        {
            for (int i = 0; i < Length; i++)
            {
                if (Inner[i] is T)
                {
                    Inner.RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public bool Contains<T>()
        {
            foreach (Behaviour b in Inner)
            {
                if (b is T)
                    return true;
            }

            return false;
        }

        public bool TryGet<T>(out T value) where T : Behaviour
        {
            if (!Contains<T>())
            {
                value = null;
                return false;
            }

            value = Get<T>();
            return true;
        }

        public void UpdateOne<T>(Entity entity, GameTime gameTime) where T : Behaviour
        {
            if (TryGet(out T b))
                b.Update(entity, gameTime);
        }

        public void UpdateAll(Entity entity, GameTime gameTime)
        {
            foreach (Behaviour b in Inner)
                b.Update(entity, gameTime);
        }

        public IEnumerator<Behaviour> GetEnumerator() => Inner.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Inner.GetEnumerator();

        public override int GetHashCode() => Inner.GetHashCode();
    }
}
