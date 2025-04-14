using System.Collections;
using UnityEngine;

public class OnCompareInteractor : BaseInteraction, IOnDraw
{
    public override int Priority => 9;
    
    public IEnumerator OnDraw(Pixel pixel)
    {
        if (CheckCompare(G.run.pixels, G.run.referencePixels))
        {
            G.ui.nextButton.gameObject.SetActive(true);
        }
        else
        {
            Debug.Log("No");
        }
        
        yield break;
    }
    
    public bool CheckCompare(Pixel[] firstDrawing, Pixel[] secondDrawing)
    {
        for (int i = 0; i < firstDrawing.Length; i++)
            if (firstDrawing[i].color != secondDrawing[i].color)
                return false;

        return true;
    }
}