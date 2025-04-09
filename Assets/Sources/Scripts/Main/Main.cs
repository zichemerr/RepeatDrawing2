using System;
using System.Collections;
using System.Collections.Generic;
using LD;
using LD.Locator;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] private Transform _colors;
    [SerializeField] private RunState _run;
    
    private void Awake()
    {
        CMS.Init();
        G.main = this;
            
        Interactor interactor = new Interactor();
        ServiceLocator<IService> serviceLocator = new ServiceLocator<IService>();
        
        interactor.Init();
        G.interactor = interactor;
        G.locator = serviceLocator;
        G.run = _run;

        foreach (var interaction in G.interactor.FindAll<BaseInteraction>())
            serviceLocator.Register(interaction);
        
        G.ui = G.locator.Get<GameUI>();
    }

    private IEnumerator Start()
    {
        foreach (var encounterStart in G.interactor.FindAll<IOnEncounterStart>())
            yield return encounterStart.OnEncounterStart();
            
        //load:
            
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
    [HideInInspector] public int level;
    public List<PixelColor> colors = new List<PixelColor>();
    public int maxLevels;
}