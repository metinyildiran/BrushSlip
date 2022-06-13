using DG.Tweening;
using UnityEngine;

public class Bean : MonoBehaviour
{
    [SerializeField] private GameObject childObject;
    private Renderer _renderer;

    private bool isActive;

    private void Awake()
    {
        _renderer = childObject.GetComponent<Renderer>();

        GameManager.Instance.OnGameStarted += ActivateBean;
    }

    private void OnTriggerEnter(Collider other)
    {
        childObject.transform.DOScale(0.0f, 0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        childObject.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);

        Colorize();
    }

    private void Colorize()
    {
        if (!isActive) return;

        _renderer.material.DOColor(Color.yellow, 0.1f);
    }

    private void ActivateBean()
    {
        isActive = true;
    }
}
