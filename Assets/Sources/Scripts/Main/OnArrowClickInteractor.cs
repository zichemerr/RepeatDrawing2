using System.Collections;
using UnityEngine;

public class OnArrowClickInteractor : BaseInteraction, IOnArrowClick, IOnEncounterStart
{
    public bool leftPosition = true;
    
    public IEnumerator OnArrowClick()
    {
        if (leftPosition)
        {
            G.ui.rightArrow.Disable();
            G.ui.leftArrow.Enable();
            G.main.buttons.targetPosition = G.run.secondPoint.position;
            leftPosition = false;
        }
        else
        {
            G.ui.rightArrow.Enable();
            G.ui.leftArrow.Disable();
            G.main.buttons.targetPosition = G.run.firstPoint.position;
            leftPosition = true;
        }
        
        yield break;
    }

    public IEnumerator OnEncounterStart()
    {
        G.ui.leftArrow.Disable();
        yield break;
    }
}

public interface IOnArrowClick
{
    public IEnumerator OnArrowClick();
}