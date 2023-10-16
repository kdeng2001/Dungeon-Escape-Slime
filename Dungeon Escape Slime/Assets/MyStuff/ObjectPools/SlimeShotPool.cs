using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeShotPool : ObjectPool
{
    public static SlimeShotPool SharedInstance;    
    public List<SlimeShot> pooledSlimeShots;
    public override void Awake()
    {
        SharedInstance = this;
        pooledSlimeShots = new List<SlimeShot>();
    }



    public override void Start()
    {
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
            pooledSlimeShots.Add(tmp.GetComponent<SlimeShot>());
        }
    }

    public SlimeShot GetPooledSlimeShot()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledSlimeShots[i];
            }
        }
        return null;
    }
}
