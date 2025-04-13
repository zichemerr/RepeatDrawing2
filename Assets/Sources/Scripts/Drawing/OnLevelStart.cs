using System.Collections;
using System.Collections.Generic;

public class OnLevelStartInteractor : BaseInteraction, IOnLevelStart
{
    public IEnumerator OnLevelStart(DrawsEntity drawsEntity)
    {
        if (drawsEntity.Is<TagDraws>(out var tag))
        {
            int drawIndex = G.run.level - 1;
            List<Pixel> pixels = tag.draws[drawIndex].pixels;
            
            for (int i = 0; i < G.run.referencePixels.Length; i++)
                G.run.referencePixels[i].SetColor(pixels[i].color);
        }
        
        yield break;
    }
}

public interface IOnLevelStart
{
    public IEnumerator OnLevelStart(DrawsEntity drawsEntity);
}