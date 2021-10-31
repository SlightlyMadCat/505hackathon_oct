using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileSample : MonoBehaviour
{
    public RectTransform signalImg;
    public RectTransform parentRect;

    [SerializeField] private Color32[] colors;
    [SerializeField] private GameObject[] icons;
    
    public void Init(Transform _parent, int _tileType)
    {
        transform.SetParent(_parent);
        transform.localScale = Vector3.one;

        signalImg.GetComponent<Image>().color = colors[_tileType];
        
        icons[_tileType].SetActive(true);
        icons[_tileType].transform.localScale = Vector3.one*.5f;
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
