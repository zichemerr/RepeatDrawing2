using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactor
{
    private List<BaseInteraction> _interactions = new List<BaseInteraction>();
    
    public void Init()
    {
        BaseInteraction[] interactions = GameObject.FindObjectsOfType<BaseInteraction>();

        foreach (var interactor in interactions)
            _interactions.Add(interactor);
        
        _interactions = _interactions.OrderByDescending(i => i.Priority).ToList();
    }
    
    public List<T> FindAll<T>() where T : class
    {
        return _interactions.OfType<T>().ToList();
    }
}