using DG.Tweening;
using UnityEngine;

public class Bean : MonoBehaviour
{
    [SerializeField] private GameObject childObject;
    private Renderer _renderer;

    private void Awake()
    {
        _renderer = childObject.GetComponent<Renderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        childObject.transform.DOScale(0.0f, 0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        childObject.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
        _renderer.material.DOColor(Color.red, 0.1f);
    }
}
