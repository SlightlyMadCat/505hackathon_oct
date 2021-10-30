using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CarModelRandom : MonoBehaviour
{
    public Material[] materials;
    public CarModel[] carModels;

    public void Awake()
    {
        var _material = UnityEngine.Random.Range(0, materials.Length);
        var _model = UnityEngine.Random.Range(0, carModels.Length);
        carModels[_model].gameObject.SetActive(true);
        carModels[_model].PaintMesh(materials[_material]);
    }
}
