using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

public class Level : MonoBehaviour
{
	private readonly int _indexMainScene = 0;
	private readonly int _indexFirstScene = 1;

	private int _indexActiveScene;

	public void StartLevel(int scene)
	{
		int level = YandexGame.savesData.level + 1;

		if (level > 0)
		{
			if (scene <= level)
			{
				SceneManager.LoadScene(scene);
			}
		}
		else
		{
			if (scene == _indexFirstScene)
			{
				SceneManager.LoadScene(scene);
			}
		}
	}

	public void NextLevel()
	{
		_indexActiveScene = SceneManager.GetActiveScene().buildIndex + 1;
		SceneManager.LoadScene(_indexActiveScene);
	}

	public void Back()
	{
		SceneManager.LoadScene(_indexMainScene);
	}
}