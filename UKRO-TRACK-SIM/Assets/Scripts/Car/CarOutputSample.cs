using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Basic car outputs
 */

public class CarOutputSample : MonoBehaviour
{
    [SerializeField] private Light[] lights;
    [SerializeField] private AudioSource horn;
    [SerializeField] private AudioSource click;

    public void SetLightsState(bool _val)
    {
        foreach (var VARIABLE in lights)
        {
            VARIABLE.enabled = _val;
        }
        
        click.Play();
    }

    public void SetHornState(bool _val)
    {
        if (_val)
        {
            horn.Play();
        }
        else
        {
            horn.Stop();
        }
    }
}
