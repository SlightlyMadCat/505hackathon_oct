using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightStandController : MonoBehaviour
{
    public Light light;
    private Transform target;
    private float activeDistance = 100f;
    
    private void Start()
    {
        target = Player.Instance.transform;
    }

    private void FixedUpdate()
    {
        if (Vector3.Distance(transform.position, target.position) < activeDistance) light.enabled = true;
        else light.enabled = false;
    }
}
