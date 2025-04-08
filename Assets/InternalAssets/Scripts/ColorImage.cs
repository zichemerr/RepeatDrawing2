using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ColorImage : MonoBehaviour, IPointerDownHandler
{
	private Image _image;

	public event Action<Color> OnSelected;

	private void Start()
	{
		_image = GetComponent<Image>();
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		OnSelected?.Invoke(_image.color);
	}
}