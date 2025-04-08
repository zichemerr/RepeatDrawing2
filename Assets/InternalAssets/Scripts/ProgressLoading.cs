using System.Collections.Generic;
using UnityEngine;
using YG;

public class ProgressLoading : MonoBehaviour
{
	[SerializeField] private List<Star> _stars;

	private void Awake()
	{
		int starsCount = YandexGame.savesData.level;

		if (starsCount >= 0)
		{
			for (int i = 0; i < starsCount; i++)
				_stars[i].Enable();
		}
	}
}