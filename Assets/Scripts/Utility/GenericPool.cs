using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace Utility
{
    public class GenericPool<T> : Object where T : Component
    {
        protected ObjectPool<T> _pool;
        public List<T> ActiveObjects { get; protected set; }
        private GameObject _prefab;
        
        public void Initialize(GameObject prefab, int initialCapacity, int maxCapacity)
        {
            _prefab = prefab;
            ActiveObjects = new List<T>();
            _pool = new ObjectPool<T>(
                OnObjectCreate, OnObjectGet, OnObjectRelease, OnObjectDestroy,
                false, initialCapacity, maxCapacity);
        }

        public void DespawnObject(T obj)
        {
            _pool.Release(obj);
        }

        private T OnObjectCreate()
        {
            GameObject newObj = Instantiate(_prefab);
            T newComponent = newObj.GetComponent<T>();
            newObj.SetActive(false);
            return newComponent;
        }

        private void OnObjectGet(T obj)
        {
            ActiveObjects.Add(obj);
        }

        private void OnObjectRelease(T obj)
        {
            ActiveObjects.Remove(obj);
            obj.gameObject.SetActive(false);
        }

        private void OnObjectDestroy(T obj)
        {
            Destroy(obj.gameObject);
        }
    }
}
