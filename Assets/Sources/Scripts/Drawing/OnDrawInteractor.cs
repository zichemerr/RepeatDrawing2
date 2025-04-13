using System.Collections;

public class OnDrawInteractor : BaseInteraction, IOnDraw
{
    public IEnumerator OnDraw(Pixel pixel)
    {
        pixel.SetColor(G.run.currentColor.color);
        yield break;
    }
}