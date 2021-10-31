using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarModel : MonoBehaviour
{
    public MeshRenderer[] meshRenderers;

    public void PaintMesh(Material _mat)
    {
        foreach (var t in meshRenderers)
        {
            t.material = _mat;
        }
    }
}
