using System;
using UnityEngine;

public class Pixel : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public event Action<Pixel> Clicked;
    public Color color => _spriteRenderer.color;

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