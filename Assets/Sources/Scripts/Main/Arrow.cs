using Michsky.MUIP;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour
{
    [SerializeField] private Image _spriteRenderer;
    [SerializeField] private Button _buttonManager;
    
    public void Enable()
    {
        _spriteRenderer.enabled = true;
        _buttonManager.enabled = true;
    }

    public void Disable()
    {
        _spriteRenderer.enabled = false;
        _buttonManager.enabled = false;
    }
}