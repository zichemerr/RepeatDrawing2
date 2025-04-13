using System.Collections;

public class OnColorSelected : BaseInteraction, IOnColorSelect
{
    public IEnumerator OnColorSelect(PixelColor color)
    {
        G.run.currentColor = color;
        G.run.hover.Enable();
        G.run.hover.SetPosition(color.position);
        
        yield break;
    }
}