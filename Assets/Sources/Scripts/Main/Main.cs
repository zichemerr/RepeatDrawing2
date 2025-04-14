using System;
using System.Collections;
using System.Collections.Generic;
using LD;
using LD.Locator;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Main : MonoBehaviour
{
    [SerializeField] private ColorSetAlign _align;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private Transform _colors;
    [SerializeField] private RunState _run;
    
    private void Awake()
    {
        CMS.Init();
        G.main = this;
            
        Interactor interactor = new Interactor();
        
        interactor.Init();
        G.interactor = interactor;
        G.run = _run;
        G.run.Init();
        G.ui = _gameUI;
    }

    private IEnumerator Start()
    {
        //load:
        foreach (var pixel in G.run.pixels)
            pixel.Clicked += PixelOnClicked;
        
        SpawnHover();
        
        foreach (var encounterStart in G.interactor.FindAll<IOnEncounterStart>())
            yield return encounterStart.OnEncounterStart();
        
        foreach (var encounterReady in G.interactor.FindAll<IOnEncounterReady>())
            yield return encounterReady.OnEncounterReady();
    }

    private void Update()
    {
#if UNITY_EDITOR            
        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();
#endif
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public PixelColor SpawnColor(Color color)
    {
        PixelColor pixelColor = Instantiate(Data.Prefabs.Color, _colors.parent);
        pixelColor.SetColor(color);
        G.run.colors.Add(pixelColor);
        return pixelColor;
    }
    
    public void OnClickLevel(int level)
    {
        G.run.level = level;
        G.ui.DrawLevel();

        var en = CMS.Get<DrawsEntity>();

        if (en.Is<TagDraws>(out var tag))
        {
            int index = G.run.level - 1;
            Color[] colors = tag.draws[index].colors;
                
            foreach (var color in colors)
                SpawnColor(color).Clicked += OnClicked;
        
            _align.Init();
        
            var interactors = G.interactor.FindAll<IOnLevelStart>();

            foreach (var interactor in interactors)
                StartCoroutine(interactor.OnLevelStart(en));
        }
    }

    private void PixelOnClicked(Pixel pixel)
    {
        var inter = G.interactor.FindAll<IOnDraw>();

        foreach (var interactor in inter)
            StartCoroutine(interactor.OnDraw(pixel));
    }

    public void OnBack()
    {
        StartCoroutine(OnBackClick());
    }
    
    public IEnumerator OnBackClick()
    {
        foreach (var color in G.run.colors)
            color.Clicked -= OnClicked;

        RestartGame();
        yield break;
    }

    public void OnNext()
    {
        StartCoroutine(OnNextClick());
    }

    public IEnumerator OnNextClick()
    {
        if (G.run.level == G.run.maxLevels)
            yield break;
        
        foreach (var color in G.run.colors)
            color.Destroy();
        
        G.run.colors.Clear();
        G.run.level++;
        OnClickLevel(G.run.level);
        yield break;
    }

    private void OnClicked(PixelColor color)
    {
        var inter = G.interactor.FindAll<IOnColorSelect>();

        foreach (var interactor in inter)
            StartCoroutine(interactor.OnColorSelect(color));
    }

    public void SpawnHover()
    {
        Hover hover = Instantiate(Data.Prefabs.Hover);
        G.run.hover = hover;
        hover.Disable();
    }
}

public static class G
{
    public static Main main;
    public static Interactor interactor;
    public static IServiceLocator<IService> locator;
    public static RunState run;
    public static GameUI ui;
}

[Serializable]
public class RunState
{
    public PixelColor currentColor;
    public Pixel[] pixels;
    public Pixel[] referencePixels;
    public Color[] color;
    public List<PixelColor> colors;
    [HideInInspector] public int level;
    public int maxLevels;
    public Hover hover;

    public void Init()
    {
        colors = new List<PixelColor>();
    }
}
