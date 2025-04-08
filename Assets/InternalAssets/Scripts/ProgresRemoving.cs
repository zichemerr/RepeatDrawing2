using UnityEngine;
using YG;

public class ProgresRemoving : MonoBehaviour
{
	[ContextMenu(nameof(Remove))]
	private void Remove()
	{
		YandexGame.savesData.level = 0;
		YandexGame.SaveProgress();
	}
	//Мяяу
}