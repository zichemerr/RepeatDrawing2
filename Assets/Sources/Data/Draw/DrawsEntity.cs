using System.Collections;
using System.Collections.Generic;
using LD;
using UnityEngine;

public class DrawsEntity : CMSEntity
{
    public DrawsEntity()
    {
        List<Draw> draws = new List<Draw>();
        
        draws.Add(Data.Prefabs.FirstDrawing);
        draws.Add(Data.Prefabs.FirstDrawing2);
        
        Define<TagDraws>().draws = draws;
    }
}

public class TagDraws : EntityComponentDefinition
{
    public List<Draw> draws;
}