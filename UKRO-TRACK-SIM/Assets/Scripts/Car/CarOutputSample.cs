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
    [SerializeField] private GameObject enabledLightsObj;
    [SerializeField] private GameObject disabledLightsObj;

    public void SetLightsState(bool _val)
    {
        foreach (var VARIABLE in lights)
        {
            VARIABLE.enabled = _val;
        }
        
        if(enabledLightsObj != null) enabledLightsObj.SetActive(_val);
        if(disabledLightsObj != null) disabledLightsObj.SetActive(!_val);
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
