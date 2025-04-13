using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSetAlign : MonoBehaviour
{
    public float width = 1f;

    [ContextMenu("Align")]
    public void Init()
    {
        var centerOffset = width * (G.run.colors.Count * 0.5f - 0.5f);
        
        for (var i = 0; i < G.run.colors.Count; i++)
            G.run.colors[i].transform.localPosition = new Vector3(i * width - centerOffset, 0, 0);
    }
}