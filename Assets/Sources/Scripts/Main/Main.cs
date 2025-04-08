using System;
using System.Collections;
using LD;
using LD.Locator;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
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
    
}