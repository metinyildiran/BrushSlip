using DG.Tweening;
using UnityEngine;

public class Bean : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOShakeScale(0.2f, 0.4f, 1)).onComplete += () =>
        {
            transform.DOScale(0.8f, 0.0f);
        };


    }
}
