using System.Collections;
using UI;
using UnityEngine;
using DG.Tweening;

public class CrossFade : SceneTransition
{
    public CanvasGroup crossFade;

    public override IEnumerator animateTransitionIn()
    {
        var tween = crossFade.DOFade(1, 1);
        yield return tween.WaitForCompletion();
    }

    public override IEnumerator animateTransitionOut()
    {
        var tween = crossFade.DOFade(0, 1);
        yield return tween.WaitForCompletion();
    }
}