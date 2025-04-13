using System;
using System.Collections.Generic;
using System.Linq;
using LD;
using UnityEngine;

public class Interactor
{
    private List<BaseInteraction> _interactions;
        
    public void Init()
    {
        var subs = ReflectionUtil.FindAllSubslasses<BaseInteraction>();
        _interactions = new List<BaseInteraction>();

        foreach (var type in subs)
        {
            var interaction = (BaseInteraction)Activator.CreateInstance(type);
            _interactions.Add(interaction);
        }
            
        _interactions = _interactions.OrderByDescending(i => i.Priority).ToList();
    }

    public List<T> FindAll<T>() where T : class
    {
        return _interactions.OfType<T>().ToList();
    }
}