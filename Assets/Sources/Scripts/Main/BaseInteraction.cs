using System.Collections;
using LD.Locator;
using UnityEngine;

public abstract class BaseInteraction
{
    public virtual int Priority => 10; 
}

public interface IOnEncounterStart
{
    public IEnumerator OnEncounterStart();
}
    
public interface IOnEncounterReady
{
    public IEnumerator OnEncounterReady();
}

public interface IOnBackClick
{
    public IEnumerator OnBackClick();
}

public interface IOnColorSelect
{
    public IEnumerator OnColorSelect(PixelColor pixelColor);
}

public interface IOnDraw
{
    public IEnumerator OnDraw(Pixel pixel);
}