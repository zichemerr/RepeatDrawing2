using System;
using UnityEngine;

public class PixelColor : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    
    public event Action<PixelColor> Clicked;
    public Vector2 position => transform.position;
    public Color color => _spriteRenderer.color;

    private void Awake()
    {
        //Ты здесь потому-что тут ошибка, а я делаю русский, так-что сам чини лох :)
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

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