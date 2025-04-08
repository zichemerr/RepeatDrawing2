using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class Painting : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
	[SerializeField] private ImageComparison _comparison;
	[SerializeField] private ColorSelection _colorSelection;

	private Image _image;

	private void Start()
	{
		_image = GetComponent<Image>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (Input.GetKey(KeyCode.Mouse0))
		{
			_image.color = _colorSelection.SelectedColor;
			_comparison.Compare();
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_image.color = _colorSelection.SelectedColor;
		_comparison.Compare();
	}
}