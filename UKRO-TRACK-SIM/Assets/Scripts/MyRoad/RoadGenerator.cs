using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roadSamplePrefab;


    private void OnTriggerEnter(Collider other)
    {
        Debug.LogError("Need generate new part" + other.gameObject.name);
        // generate new part
        if (other.gameObject.CompareTag("Road"))
        {
            RoadSample _road = other.gameObject.GetComponentInParent(typeof(RoadSample)) as RoadSample;
            GenerateNewRoadPart(_road);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.LogError("exit");
        
        // destroy old road part
        if (other.gameObject.CompareTag("Road"))
        {
            RoadSample _road = other.gameObject.GetComponentInParent(typeof(RoadSample)) as RoadSample;
            if (_road != null)
            {
                Destroy(_road.gameObject);
            }
        }
    }

    private void GenerateNewRoadPart(RoadSample _oldRoad)
    {
        Debug.LogError("generate part");
        GameObject _newRoad = Instantiate(roadSamplePrefab, _oldRoad.GetRoadEnd().position ,Quaternion.identity);
       // _newRoad.transform.position = _oldRoad.GetRoadEnd().position;
    }
}
