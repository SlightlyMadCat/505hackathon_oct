using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RoadSample : MonoBehaviour
{
    [SerializeField] private Transform roadEnd;
    private float curz = 0;
    private float speed = 500f; 
    public Transform GetRoadEnd()
    {
        return roadEnd;
    }

    public Transform GetRoadStart()
    {
        return transform;
    }

    private void Awake()
    {
        curz = transform.position.z;
    }

    public void FixedUpdate()
    {
        curz -= math.abs( Time.deltaTime * speed) ;
        transform.position = new Vector3(transform.position.x, transform.position.y,
            curz);
    }
}
