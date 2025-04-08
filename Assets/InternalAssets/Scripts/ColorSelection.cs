using UnityEngine;

public class ColorSelection : MonoBehaviour
{
	[SerializeField] private ColorImage[] _images;

	public Color SelectedColor { get; private set; }

	private void Start()
	{
		SelectedColor = Color.white;
	}

	private void OnEnable()
	{
        foreach (var image in _images)
			image.OnSelected += OnSelected;
    }

	private void OnDisable()
	{
		foreach (var image in _images)
			image.OnSelected -= OnSelected;
	}

	private void OnSelected(Color color)
	{
		SelectedColor = color;
	}
}
