using System;
using System.Collections.Generic;
using System.Linq;

namespace PaymillWrapper.Utils
{
    public class ParameterMap<K, V>
         where V : class
         where K : class
    {
        private Dictionary<K, List<V>> map;

        public ParameterMap()
        {
            this.map = new Dictionary<K, List<V>>();
        }

        public void Add(K key, V value)
        {
            List<V> values = null;

            if (this.map.ContainsKey(key) == false)
            {
                values = new List<V>();
            }
            else
            {
                values = this.map[key];
            }

            values.Add(value);

            this.map.Add(key, values);
        }

        public V GetFirst(K key)
        {
            return this.map[key] != null ? this.map[key].First() : null;
        }

        public int Size()
        {
            return this.map.Count();
        }

        public Boolean IsEmpty()
        {
            return this.map.Count() == 0;
        }


        public Boolean ContainsKey(K o)
        {
            return this.map.ContainsKey(o);
        }


        public Boolean ContainsValue(List<V> o)
        {
            return this.map.ContainsValue(o);
        }

        public List<V> Get(K o)
        {
            return this.map[o];
        }


        public void Put(K k, List<V> vs)
        {
            this.map.Add(k, vs);
        }

        public void Remove(K o)
        {
            this.map.Remove(o);
        }

        public void PutAll(Dictionary<K, List<V>> map)
        {

            foreach (var item in map)
            {
                this.map.Add(item.Key, item.Value);
            }
        }

        public void Clear()
        {
            this.map.Clear();
        }

        public List<K> KeyCollection()
        {
            return this.map.Keys.ToList();
        }

        public List<List<V>> Values()
        {
            return this.map.Values.ToList();
        }

        public IDictionary<K, List<V>> EntrySet()
        {
            return this.map;
        }
    }
}
