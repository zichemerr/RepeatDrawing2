using System.Collections;
using DG.Tweening;
using UnityEngine;

public class OnArrowClickInteractor : BaseInteraction, IOnArrowClick, IOnEncounterStart
{
    public bool leftPosition = true;
    
    public IEnumerator OnArrowClick()
    {
        if (leftPosition)
        {
            G.ui.rightArrow.Disable();
            G.main.buttons.DOMove(G.run.secondPoint.position, 0.6f).SetEase(Ease.OutQuad);
            leftPosition = false;
            
            yield return new WaitForSeconds(0.7f);
            G.ui.leftArrow.Enable();
        }
        else
        {
            G.ui.leftArrow.Disable();
            G.main.buttons.DOMove(G.run.firstPoint.position, 0.6f).SetEase(Ease.OutQuad);
            leftPosition = true;
            
            yield return new WaitForSeconds(0.7f);
            G.ui.rightArrow.Enable();
        }
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