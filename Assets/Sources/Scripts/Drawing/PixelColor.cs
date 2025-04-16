using System;
using UnityEngine;

public class PixelColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    
    public event Action<PixelColor> Clicked;
    public Vector2 position => transform.position;
    public Color color => _spriteRenderer.color;
    
    private void OnMouseDown()
    {
        Clicked?.Invoke(this);
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}