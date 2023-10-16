using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{
    private PlayerBehavior pb;
    private Transform splitSpawn;
    void Start()
    {
        pb = GetComponent<PlayerBehavior>();
        splitSpawn = GameObject.Find("SplitSpawn").transform;
    }
    void Update()
    {
        if(pb.takeDamage.WasPerformedThisFrame())
        {
            SplitSlime temp = SplitSlimePool.SharedInstance.GetPooledSlimeShot();
            if(temp == null) { gameObject.SetActive(false); return; }
            temp.transform.position = transform.position + new Vector3(0, 2, 0);
            
            temp.gameObject.SetActive(true);
            Shrink();
            Debug.Log("Shrink");
        }

        if (pb.recoverDamage.WasPerformedThisFrame()) 
        {
            Grow();
            Debug.Log("Grow");
        }

    }

    private void Shrink() 
    {
        if(pb.crouch.IsPressed()) 
        {
            transform.localScale -= new Vector3(0.333f * 4f, 0.333f * 0.25f, 0.333f);
        }
        else 
        { 
            transform.localScale -= new Vector3(0.333f, 0.333f, 0.333f);
        }
        
    }
    private void Grow() 
    {
        transform.localScale += new Vector3(0.333f, 0.333f, 0.333f);
    }
    private void SplitOff() 
    {
    }
}
