using System.Collections;
using YG;

public class StarsLoaderInteractor : BaseInteraction, IOnEncounterStart
{
    public IEnumerator OnEncounterStart()
    {
        for (int i = 0; i < G.run.stars.Count + 1; i++)
        {
            int index = YandexGame.savesData.indexes[i];
            
            if (index == 0)
                continue;
            
            G.run.stars[index].Enable();
        }
        
        yield break;
    }
}