using System;
using UnityEngine;

[ExecuteInEditMode]
public class ColorSetAlignInEditorY : MonoBehaviour
{
    public float width = 1f;

    void Update()
    {
        var count = transform.childCount;
        var centerOffset = width * (count * 0.5f - 0.5f);
        
        for (var i = 0; i < count; i++)
        {
            transform.GetChild(i).localPosition = new Vector3(0, i * width - centerOffset, 0);
        }
    }
}