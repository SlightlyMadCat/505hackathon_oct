using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSample : MonoBehaviour
{
    public RectTransform signalImg;
    public RectTransform parentRect;

    public void Init(Transform _parent)
    {
        transform.SetParent(_parent);
        transform.localScale = Vector3.one;
    }
    
    public void SetParentSizeScale(Vector2 _size)
    {
        parentRect.sizeDelta = _size;
    }
    
    public void SetSignalImgSizeScale(Vector2 _size)
    {
        signalImg.sizeDelta = _size;
    }
}
