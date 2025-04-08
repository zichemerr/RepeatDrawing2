using UnityEngine;
using UnityEngine.UI;

public class ImageComparison : MonoBehaviour
{
	private LevelFinish _level = new LevelFinish();

	[SerializeField] private GameObject _button;
	[SerializeField] private Image[] _images;
	[SerializeField] private Image[] _imagesTail;

	public void Compare()
	{
		if (Check())
		{
			_level.Finish();
			_button.SetActive(true);
		}
	}

	private bool Check()
	{
        for (int i = 0; i < _images.Length; i++)
			if (_images[i].color != _imagesTail[i].color)
				return false;

		return true;
    }
}