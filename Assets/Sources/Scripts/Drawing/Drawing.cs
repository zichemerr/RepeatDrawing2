using System.Collections;
using UnityEngine;

public class Drawing : BaseInteraction, IOnEncounterReady 
{
    [SerializeField] private Color[] _colors;
    [SerializeField] private Pixel[] _draw;
        
    private Hover _hover;
    
    public Color CurrentColor { get; private set; }
    
    public IEnumerator OnEncounterReady()
    {
        _hover = Instantiate(Data.Prefabs.Hover);
        
        for (int i = 0; i < _colors.Length; i++)
            G.main.SpawnColor(_colors[i]).Clicked += OnClickedColor;

        G.locator.Get<ColorSetAlign>().Init();
        OnClickedColor(G.run.colors[0]);
        
        foreach (Pixel pixel in _draw)
            pixel.Clicked += OnClicked;

        yield break;
    }

    private void OnClickedColor(PixelColor color)
    {
        _hover.SetPosition(color.position);
        CurrentColor = color.color;
    }

    private void OnClicked(Pixel pixel)
    {
        pixel.SetColor(CurrentColor);
    }
}