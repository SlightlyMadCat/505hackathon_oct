using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

// Road sample element, consists of: piece of road and cars on it
public class RoadSample : MonoBehaviour
{
    [SerializeField] private Transform roadEnd;
    private float curZPos = 0;
    private float speed = 500f; 
    
    // get end transform of road
    public Transform GetRoadEnd()
    {
        return roadEnd;
    }

    // get start transform of road
    public Transform GetRoadStart()
    {
        return transform;
    }

    private void Awake()
    {
        curZPos = transform.position.z; // remember start position
        GenerateCars(); // generate cars
    }

    // Generate cars on road, call from awake
    private void GenerateCars()
    {
       
    }

    public void FixedUpdate()
    {
        // move with speed from speed-controller
        curZPos -= SpeedController.Instance.GetCurrentSpeed();
        var position = transform.position;
        position = new Vector3(position.x, position.y, curZPos);
        transform.position = position;
    }
}
