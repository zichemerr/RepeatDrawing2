using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LD;
using LD.Locator;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using YG;

public class Main : MonoBehaviour
{
    [SerializeField] private ColorSetAlign _setAlign;
    [SerializeField] private GameUI _gameUI;
    [SerializeField] private Transform _colors;
    [SerializeField] private RunState _run;

    public MovableSmoothDamp buttons;
    public AudioClip draw;
    public AudioClip select;
    
    private void Awake()
    {
        CMS.Init();
        G.main = this;
            
        Interactor interactor = new Interactor();
        AudioSystem audioSystem = new AudioSystem();
        
        interactor.Init();
        G.interactor = interactor;
        G.run = _run;
        G.run.Init();
        G.audio = audioSystem;
        _gameUI.Init();
        G.ui = _gameUI;
        buttons.targetPosition = G.run.firstPoint.position;
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
        {
            YandexGame.ResetSaveProgress();
            YandexGame.SaveProgress();
            RestartGame();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            for (int i = 0; i < G.run.referencePixels.Length; i++)
            {
                G.run.pixels[i].SetColor(G.run.referencePixels[i].color);
            }
        }
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
        YandexGame.FullscreenShow();
        G.run.level = level;
        G.ui.DrawLevel();

        var en = CMS.Get<DrawsEntity>();

        if (en.Is<TagDraws>(out var tag))
        {
            int index = G.run.level - 1;
            Color[] colors = tag.draws[index].colors;
                
            foreach (var color in colors)
                SpawnColor(color).Clicked += OnClicked;
            
            _setAlign.Init();
            
            var inter = G.interactor.FindAll<IOnColorSelect>();

            foreach (var interactor in inter)
                StartCoroutine(interactor.OnColorSelect(G.run.colors[0]));
        
            var interactors = G.interactor.FindAll<IOnLevelStart>();

            foreach (var interactor in interactors)
                StartCoroutine(interactor.OnLevelStart(en));
        }
    }

    private float lastTime;
    private float cooldownTime = 0.05f;
    
    private void PixelOnClicked(Pixel pixel)
    {
        if (pixel.color != G.run.currentColor.color && Time.time - lastTime >= cooldownTime)
        {
            G.audio.Play(draw, 0.5f);
            lastTime = Time.time;
        }
        
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
        G.audio.Play(Data.Sound.Click);
        YandexGame.FullscreenShow();
        
        foreach (var color in G.run.colors)
            color.Clicked -= OnClicked;

        G.ui.DrawMenu();
        
        foreach (var color in G.run.colors)
            color.Destroy();
        
        G.run.colors.Clear();
        G.run.hover.Disable();
        
        yield break;
    }

    public void OnNext()
    {
        StartCoroutine(OnNextClick());
    }

    public IEnumerator OnNextClick()
    {
        G.audio.Play(Data.Sound.Click);
        YandexGame.FullscreenShow();
        
        if (G.run.level == G.run.maxLevels)
        {
            Debug.Log("Все уровни пройденны");
            RestartGame();
            yield break;
        }
        
        foreach (var color in G.run.colors)
            color.Destroy();
        
        G.run.colors.Clear();
        G.run.level++;
        OnClickLevel(G.run.level);
    }

    private void OnClicked(PixelColor color)
    {
        G.audio.Play(select);
        var inter = G.interactor.FindAll<IOnColorSelect>();

        foreach (var interactor in inter)
            StartCoroutine(interactor.OnColorSelect(color));
    }
    
    public void OnArrow()
    {
        StartCoroutine(OnClickArrow());
    }
    
    private IEnumerator OnClickArrow()
    {
        var inters = G.interactor.FindAll<IOnArrowClick>();
        foreach (var interactor in inters)
            yield return interactor.OnArrowClick();
    }

    public void SpawnHover()
    {
        Hover hover = Instantiate(Data.Prefabs.Hover, _colors.parent);
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
    public static AudioSystem audio;
}

[Serializable]
public class RunState
{
    [SerializeField] private Star[] _stars;
    public PixelColor currentColor;
    public Pixel[] pixels;
    public Pixel[] referencePixels;
    public List<PixelColor> colors;
    [HideInInspector] public int level;
    public int maxLevels;
    public Hover hover;
    public Dictionary<int, Star> stars;
    public Transform firstPoint;
    public Transform secondPoint;

    public void Init()
    {
        colors = new List<PixelColor>();
        stars = _stars.ToDictionary(KeySelector);
    }

    private int index = 0;
    private int KeySelector(Star stat)
    {
        index++;
        return index;
    }

    public void EnableStar(int key)
    {
        stars[key].Enable();
        YandexGame.savesData.indexes[key] = key;
        YandexGame.SaveProgress();
    }
}

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
