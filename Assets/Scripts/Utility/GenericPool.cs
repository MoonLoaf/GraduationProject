using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Utility
{
    public class GenericPool<T> : Object where T : Component
    {
        public event Action OnActivePoolEmpty;
        protected ObjectPool<T> _pool;
        private List<T> _activeObjects { get; set; }
        private GameObject _prefab;
        
        public void Initialize(GameObject prefab, int initialCapacity, int maxCapacity)
        {
            _prefab = prefab;
            _activeObjects = new List<T>();
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
            _activeObjects.Add(obj);
        }

        private void OnObjectRelease(T obj)
        {
            _activeObjects.Remove(obj);
            obj.gameObject.SetActive(false);
            
            if(_activeObjects.Count != 0){return;}
            OnActivePoolEmpty?.Invoke();
        }

        private void OnObjectDestroy(T obj)
        {
            Destroy(obj.gameObject);
        }
    }
}
