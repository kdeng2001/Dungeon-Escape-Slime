using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitSlimePool : ObjectPool
{
    public static SplitSlimePool SharedInstance;
    public List<SplitSlime> pooledSplitSlimes;
    public override void Awake()
    {
        SharedInstance = this;
        pooledSplitSlimes = new List<SplitSlime>();
    }

    public override void Start()
    {
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
            pooledSplitSlimes.Add(tmp.GetComponent<SplitSlime>());
        }
    }

    public SplitSlime GetPooledSlimeShot()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledSplitSlimes[i];
            }
        }
        return null;
    }
}
