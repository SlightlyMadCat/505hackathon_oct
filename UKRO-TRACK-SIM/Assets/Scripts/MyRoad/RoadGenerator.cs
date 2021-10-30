using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    [SerializeField] private GameObject roadSamplePrefab;
    
    private void OnTriggerEnter(Collider other)
    {
        // generate new road
        if (other.gameObject.CompareTag("Road"))
        {
            var _road = other.gameObject.GetComponentInParent(typeof(RoadSample)) as RoadSample;
            GenerateNewRoadPart(_road);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // destroy old road part
        if (other.gameObject.CompareTag("Road"))
        {
            var _road = other.gameObject.GetComponentInParent(typeof(RoadSample)) as RoadSample;
            if (_road != null)
            {
                Destroy(_road.gameObject);
            }
        }
    }

    // add new road on scene and generate cars 
    private void GenerateNewRoadPart(RoadSample _oldRoad)
    {
        Debug.LogError("generate part");
        var _newRoad = Instantiate(roadSamplePrefab, _oldRoad.GetRoadEnd().position ,Quaternion.identity);
    }
}
