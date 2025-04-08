using UnityEngine.SceneManagement;

public class LevelFinish
{
	private readonly ProgressSaving _progres = new ProgressSaving();

	private int _indexActiveScene;

	public void Finish()
	{
		_indexActiveScene = SceneManager.GetActiveScene().buildIndex;
		_progres.Save(_indexActiveScene);
	}
}