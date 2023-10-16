using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Launch : MonoBehaviour
{
    private PlayerBehavior pb;
    private Transform spawn;
    private Flatten flatten;
    public float launchTime;
    public float launchCooldownTime;
    void Start()
    {
        pb = GetComponent<PlayerBehavior>();
        spawn = transform.Find("ShotSpawnPos");

        launchTime = Time.time;
        launchCooldownTime = .5f;

        flatten = GetComponent<Flatten>();
    }

    // Update is called once per frame
    void Update()
    {
        if(flatten.crouching) { return; }
        if(pb.launch.WasPerformedThisFrame() && Time.time - launchTime > launchCooldownTime) 
        {
            SlimeShot slimeShot = SlimeShotPool.SharedInstance.GetPooledSlimeShot();
            slimeShot.transform.position = spawn.position;
            slimeShot.xDirection = pb.currentDirection;
            slimeShot.yDirection = pb.movement.ReadValue<Vector2>().y;
            if(pb.rb.velocity.x != 0) { slimeShot.moving = true; }
            else { slimeShot.moving = false; }
            slimeShot.gameObject.SetActive(true);
            launchTime = Time.time;
            Debug.Log("launch");
        }
    }
}
