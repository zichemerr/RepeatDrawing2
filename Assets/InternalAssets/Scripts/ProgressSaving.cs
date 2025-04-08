using YG;

public class ProgressSaving
{
	public void Save(int level)
	{
		if (level > YandexGame.savesData.level)
		{
			YandexGame.savesData.level = level;
			YandexGame.SaveProgress();
		}
	}
}