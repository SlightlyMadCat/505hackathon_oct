using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

/**
 * Script generate cars on road part
 */
public class CarGenerator : MonoBehaviour
{
    [SerializeField] private List<Transform> carSpawnPlaces; // available spawn place
    [SerializeField] private GameObject[] carSamples; // available cars for spawn
    [SerializeField] private Vector2 carCount = new Vector2(3, 5);

    [Header("Stop car coef = 1/stopCarCoef")]
    [SerializeField] private int stopCarCoef;
    private void Awake()
    {
        GenerateCars();
    }

    // generate cars on road
    private void GenerateCars()
    {
        if (carCount.y > carSpawnPlaces.Count) // fix range
            carCount.y = carSpawnPlaces.Count;
        var carCountOnRoad = Random.Range(carCount.x, carCount.y);
        var carSpawnPlaceTemp = carSpawnPlaces;
        for (int i = 0; i < carCountOnRoad; i++)
        {
            SpawnOneCar(carSpawnPlaceTemp);
        }
    }

    private void SpawnOneCar(List<Transform> _spawnPlaces)
    {
        var _spawnPlaceNumber = Random.Range(0, _spawnPlaces.Count);
        var _carNumber = Random.Range(0, carSamples.Length);
        GameObject _car = Instantiate(carSamples[_carNumber], gameObject.transform);
        _car.transform.position = _spawnPlaces[_spawnPlaceNumber].position;
        _spawnPlaces.Remove(_spawnPlaces[_spawnPlaceNumber]);
        
        var _carStop = Random.Range(0, stopCarCoef);
        if (_carStop == 0)
            _car.gameObject.AddComponent<CarStop>();
    }
}
