using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draw : MonoBehaviour
{
    public List<Pixel> pixels;
    public Color[] colors;

    [ContextMenu("SetPixels")]
    private void SetPixels()
    {
        pixels = GetPixels();
    }

    [ContextMenu("SetColors")]
    private void SetColors()
    {
        colors = GetColors();
    }

    private List<Pixel> GetPixels()
    {
        List<Pixel> list = new List<Pixel>();
        List<GameObject> lines = new List<GameObject>();
        
        for (int i = 0; i < transform.childCount; i++)
            lines.Add(transform.GetChild(i).gameObject);

        foreach (var line in lines)
            for (int i = 0; i < line.transform.childCount; i++)
                list.Add(line.transform.GetChild(i).GetComponent<Pixel>());
        
        return list;
    }
    
    public Color[] GetColors()
    {
        List<Color> uniqueColors = new List<Color>();

        foreach (var pixel in pixels)
        {
            // Если цвет ещё не добавлен в список
            if (!uniqueColors.Contains(pixel.color))
            {
                uniqueColors.Add(pixel.color);
            }
        }
    
        return uniqueColors.ToArray();
    }

    [ContextMenu("SetFullFadeColors")]
    private void SetFullFadeColors()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = new Color(colors[i].r, colors[i].g, colors[i].b, 1);
        }
    }
}