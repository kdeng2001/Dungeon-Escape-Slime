using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public GameObject objectToPool;
    public int amountToPool;

    public abstract void Awake(); // to create SharedInstance
    public virtual void Start()
    {
        objectToPool = new GameObject();
        GameObject tmp;
        pooledObjects = new List<GameObject>();

        for(int i = 0; i < amountToPool; i++) 
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public virtual GameObject GetPooledObject() 
    {
        for(int i = 0; i < amountToPool; i++) 
        {
            if (!pooledObjects[i].activeInHierarchy) 
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
}
