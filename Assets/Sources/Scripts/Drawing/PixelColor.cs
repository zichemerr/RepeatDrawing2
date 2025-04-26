using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PixelColor : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _spriteRenderer;
    
    public event Action<PixelColor> Clicked;
    public Vector2 position => transform.position;
    public Color color => _spriteRenderer.color;

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Clicked?.Invoke(this);
        Debug.Log("def");
    }
}