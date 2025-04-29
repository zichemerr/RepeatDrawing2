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
        draws.Add(Data.Prefabs.FirstDrawing_1);
        draws.Add(Data.Prefabs.FirstDrawing_2);
        draws.Add(Data.Prefabs.FirstDrawing_3);
        draws.Add(Data.Prefabs.FirstDrawing_4);
        draws.Add(Data.Prefabs.FirstDrawing_5);
        draws.Add(Data.Prefabs.FirstDrawing_6);
        draws.Add(Data.Prefabs.FirstDrawing_7);
        draws.Add(Data.Prefabs.FirstDrawing_8);
        draws.Add(Data.Prefabs.FirstDrawing_9);
        
        draws.Add(Data.Prefabs.FirstDrawing_10);
        draws.Add(Data.Prefabs.FirstDrawing_11);
        draws.Add(Data.Prefabs.FirstDrawing_12);
        draws.Add(Data.Prefabs.FirstDrawing_13);
        draws.Add(Data.Prefabs.FirstDrawing_14);
        draws.Add(Data.Prefabs.FirstDrawing_15);
        draws.Add(Data.Prefabs.FirstDrawing_16);
        draws.Add(Data.Prefabs.FirstDrawing_17);
        draws.Add(Data.Prefabs.FirstDrawing_18);
        draws.Add(Data.Prefabs.FirstDrawing_19);
        
        Define<TagDraws>().draws = draws;
    }
}

public class TagDraws : EntityComponentDefinition
{
    public List<Draw> draws;
}