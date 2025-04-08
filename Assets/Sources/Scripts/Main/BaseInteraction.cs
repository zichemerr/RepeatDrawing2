using System.Collections;
using LD.Locator;
using UnityEngine;

public abstract class BaseInteraction : MonoBehaviour, IService
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