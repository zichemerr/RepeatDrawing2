using System;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    public event Action<Pixel> Clicked;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnMouseDown()
    {
        Clicked?.Invoke(this);
    }

    private void OnMouseEnter()
    {
        if (Input.GetMouseButton(0))
            Clicked?.Invoke(this);
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}