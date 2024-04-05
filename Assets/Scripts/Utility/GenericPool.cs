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
        
        public void InitializePool(GameObject prefab, int initialCapacity, int maxCapacity)
        {
            _prefab = prefab;
            ActiveObjects = new List<T>();
            _pool = new ObjectPool<T>(
                OnObjectCreate, OnObjectGet, OnObjectRelease, OnObjectDestroy,
                false, initialCapacity, maxCapacity);
        }
        
        public virtual T SpawnObject(Vector3 spawnPos)
        {
            T spawnedObject = _pool.Get();
            spawnedObject.transform.position = spawnPos;
            spawnedObject.gameObject.SetActive(true);
            ActiveObjects.Add(spawnedObject);
            return spawnedObject;
        }

        public virtual void DespawnObject(T obj)
        {
            _pool.Release(obj);
            ActiveObjects.Remove(obj);
        }

        protected virtual T OnObjectCreate()
        {
            GameObject newObj = Instantiate(_prefab);
            T newComponent = newObj.GetComponent<T>();
            newObj.SetActive(false);
            return newComponent;
        }

        protected virtual void OnObjectGet(T obj)
        {
            // Override if needed
        }

        protected virtual void OnObjectRelease(T obj)
        {
            obj.gameObject.SetActive(false);
        }

        protected virtual void OnObjectDestroy(T obj)
        {
            Destroy(obj.gameObject);
        }
    }
}
